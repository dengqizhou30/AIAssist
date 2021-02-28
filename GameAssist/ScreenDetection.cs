using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;

namespace GameAssist
{
    //检测结果结构体
    public struct DetectionResult
    {
        public DetectionResult4Rect detectionResult4Rect;//检测结果结构体(位置信息)

        public Mat frameMat; //检查的帧图像
        public List<ObjectPosRect> objectPosRects; //检测到的对象列表
        public long toltalMillis; //检测执行的时间

        public void setValues(DetectionRect rawDetectionRect1, DetectionRect detectionRect1, Mat frameMat1, List<ObjectPosRect> objectPosRects1, ObjectPosRect maxConfidencePos1, int maxPersonW1, long toltalMillis1)
        {
            detectionResult4Rect = new DetectionResult4Rect();
            detectionResult4Rect.rawDetectionRect = rawDetectionRect1;
            detectionResult4Rect.detectionRect = detectionRect1;
            detectionResult4Rect.maxConfidencePos = maxConfidencePos1;
            detectionResult4Rect.maxPersonW = maxPersonW1;

            frameMat = frameMat1;
            objectPosRects = objectPosRects1;           
            toltalMillis = toltalMillis1;
        }
    };

    //检测结果结构体(位置信息)
    public struct DetectionResult4Rect
    {
        public DetectionRect rawDetectionRect;//原始的屏幕检查的区域
        public DetectionRect detectionRect; //裁剪后的实际检查区域
        public ObjectPosRect maxConfidencePos; //最大相识度对象位置
        //检测出来屏幕下方正中的最大人员模型，也就是游戏操作人；
        public int maxPersonW;
    };

    //要检测的屏幕区域结构体
    public struct DetectionRect
    {
        public int x;
        public int y;
        public int w;
        public int h;

        public void setValues(int x1, int y1, int w1, int h1)
        {
            x = x1;
            y = y1;
            w = w1;
            h = h1;
        }
    };

    //人员位置检测结果结构体
    public struct ObjectPosRect
    {
        //类型ID
        public int classid;
        //置信度
        public float confidence;
        //位置
        public int x1;
        public int y1;
        public int x2;
        public int y2;

        public void setValues(ObjectPosRect objectPosRect)
        {
            classid = objectPosRect.classid;
            confidence = objectPosRect.confidence;
            x1 = objectPosRect.x1;
            y1 = objectPosRect.y1;
            x2 = objectPosRect.x2;
            y2 = objectPosRect.y2;
        }

        public void setValues(int tmpClassid, float tmpConfidence, int tmpX1, int tmpY1, int tmpX2, int tmpY2)
        {
            classid = tmpClassid;
            confidence = tmpConfidence;
            x1 = tmpX1;
            y1 = tmpY1;
            x2 = tmpX2;
            y2 = tmpY2;
        }
    };

    //屏幕检测类
    class ScreenDetection
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        //AI模型文件路径
        private const string configFile = @"data\ssd_mobilenet_v3.pbtxt";
        private const string modelFile = @"data\ssd_mobilenet_v3.pb";
        //private const string configFile = @"data\pipeline.config";
        //private const string modelFile = @"data\frozen_inference_graph.pb";
        private const string labelFile = @"data\coco.names";

        //DNN AI检测网络对象
        private Net detectionNet = null;
        //训练数据集的标签数组
        private string[] labelNames = null;

        //原始屏幕区域
        private DetectionRect rawDetectionRect;
        //裁剪后的实际检查区域
        private DetectionRect detectionRect;
        //检测出来屏幕下方正中的最大人员模型，也就是游戏操作人；
        //为避免处理逻辑太复杂，最大人员模型宽度设置为一个常量；
        //private ObjectPosRect maxPersonPos;
        public int maxPersonW = 150;
        //游戏者的位置的x轴中心点
        //对于绝地求生，在屏幕下方靠左一点，大概 860/1920处
        public int playerCentX;

        //private static System.Drawing.Bitmap bitmap = new Bitmap(w, h);
        //private static System.Drawing.Graphics graphics = Graphics.FromImage(bitmap);
        private System.Drawing.Bitmap detectBitmap = null;
        private System.Drawing.Graphics detectGraphics = null;

        //主窗体对象、消息显示文本对象，及是否显示检测结果图像
        private Form mainForm = null;
        private PictureBox outPictureBox = null;
        private bool showPicture = false;

        public ScreenDetection(Form mainForm, PictureBox outPictureBox)
        {
            this.mainForm = mainForm;
            this.outPictureBox = outPictureBox;

            this.detectionNet = CvDnn.ReadNetFromTensorflow(modelFile, configFile);
            this.detectionNet.SetPreferableBackend(Net.Backend.DEFAULT);
            this.detectionNet.SetPreferableTarget(Net.Target.CPU);
            //this.detectionNet.SetPreferableTarget(Net.Target.OPENCL);

            this.labelNames = File.ReadAllLines(labelFile)
            .Select(line => line.Split('\n').Last())
            .ToArray();

            //设置初始检测区域
            rawDetectionRect = new DetectionRect();
            //缺省使用使用全屏作为工作屏幕
            rawDetectionRect.setValues(0,0, 900, 760);
            SetSrcScreenRect(rawDetectionRect);

        }

        //设置需要检测的屏幕区域
        public void SetSrcScreenRect(DetectionRect srcRect)
        {
            this.rawDetectionRect = srcRect;
            if (srcRect.w > 0 && srcRect.h > 0)
            {
                //游戏者的位置的x轴中心点
                //对于绝地求生，在屏幕下方靠左一点，大概 860/1920处
                playerCentX = srcRect.w * 860 / 1920 - srcRect.x;
            }

            //计算需要实际检测的屏幕区域
            CalDetectionRect(320,220);
        }

        //计算需要实际检测的屏幕区域
        //只检测瞄准位置的一小块窗口，提升检测效率
        public void CalDetectionRect(int w, int h)
        {
            if (w>0 && h>0 && this.rawDetectionRect.w > 0 && this.rawDetectionRect.h > 0 && w <= this.rawDetectionRect.w &&  h <= this.rawDetectionRect.h)
            {
                //获取到窗口坐标
                this.detectionRect = new DetectionRect();

                //裁剪窗口，新算法，先算出屏幕中心点，只检查屏幕正中的一小块区域，提升检测速度
                int centX = this.rawDetectionRect.x + this.rawDetectionRect.w / 2;
                int centy = this.rawDetectionRect.y + this.rawDetectionRect.h / 2;
                this.detectionRect.x = centX - w/2;
                this.detectionRect.w = w;
                //检测区域在中心点稍微向上偏移
                this.detectionRect.y = centy - (h/2 + h/10);
                this.detectionRect.h = h;
            }

            if (this.detectGraphics != null)
                this.detectGraphics.Dispose();
            if (this.detectBitmap != null)
                this.detectBitmap.Dispose();

            //初始化全局检测图像处理对象
            this.detectBitmap = new Bitmap(this.detectionRect.w, this.detectionRect.h);
            this.detectGraphics = Graphics.FromImage(detectBitmap);
        }

        //使用AI模型检测屏幕，识别人员及位置
        public DetectionResult DetectionScreen()
        {
            long toltalMillis = 0;
            long startTicks = DateTime.Now.Ticks;

            //保存所有检测对象及最大置信度对象的位置
            DetectionResult detectionResult = new DetectionResult();
            List<ObjectPosRect> objectPosRects = new List<ObjectPosRect>();
            ObjectPosRect maxConfidencePos = new ObjectPosRect();
            maxConfidencePos.setValues(0,0, 0, 0, 0, 0);

            //拷贝需要检测屏幕区域的图像
            //c#中处理图形，需要注意手动释放资源，及线程不安全的问题。
            //System.Drawing.Bitmap bitmap = new Bitmap(this.detectionRect.w, this.detectionRect.h);
            //System.Drawing.Graphics graphics = Graphics.FromImage(bitmap);
            this.detectGraphics.CopyFromScreen(this.detectionRect.x, this.detectionRect.y, 0, 0, new System.Drawing.Size(this.detectionRect.w, this.detectionRect.h), CopyPixelOperation.SourceCopy);
        
            try
            {
                //注意这里不用using语句，因为会隐式释放图像对象，导致外面其他函数使用对象时无法访问。
                Mat frameMat = BitmapConverter.ToMat(this.detectBitmap);
                if (frameMat != null && !frameMat.Empty())
                {
                    //屏幕截图和视频截图格式不一样，需要进行图像格式转换
                    Cv2.CvtColor(frameMat, frameMat, ColorConversionCodes.RGBA2RGB);

                    // Convert Mat to batch of images
                    var frameWidth = frameMat.Cols;
                    var frameHeight = frameMat.Rows;

                    //注意转换位输入数据时，最好保持原图大小，或者按比例缩放，相对于不按比例缩放，可以让识别准确率大幅提升。
                    //注意scaleFactor和scalemean参数设置，涉及模型输入数据归一化，严重影响模型准确率。
                    //using (var inputBlob = CvDnn.BlobFromImage(frameMat, 0.008, new OpenCvSharp.Size(320, 320), new Scalar(104, 117, 123), true, false))
                    //using (var inputBlob = CvDnn.BlobFromImage(frameMat, 0.008, new OpenCvSharp.Size(320, 320), new Scalar(103.939, 116.779, 123.68), true, false))
                    //using (var inputBlob = CvDnn.BlobFromImage(frameMat, 0.008, new OpenCvSharp.Size(frameWidth, frameHeight), new Scalar(103.939, 116.779, 123.68), true, false))
                    using (var inputBlob = CvDnn.BlobFromImage(frameMat, 1.0 / 127.5, new OpenCvSharp.Size(frameWidth, frameHeight), new Scalar(127.5, 127.5, 127.5), true, false))  
                    {
                        //使用AI模型推理检测图像
                        detectionNet.SetInput(inputBlob);
                        var output = detectionNet.Forward();

                        //分析检测结果
                        var detectionMat = new Mat(output.Size(2), output.Size(3), MatType.CV_32F, output.Ptr(0));                        
                        for (int i = 0; i < detectionMat.Rows; i++)
                        {
                            float confidence = detectionMat.At<float>(i, 2);

                            if (confidence > 0.67)
                            {
                                int classid = (int)detectionMat.At<float>(i, 1);
                                //判断识别的类型，这个应用场景只显示识别的人，提升处理效率
                                //if (classid < this.labelNames.Length &&  (classid == 1 || classid != 1))
                                if (classid < this.labelNames.Length && (classid == 1))
                                {
                                    int x1 = (int)(detectionMat.At<float>(i, 3) * frameWidth);
                                    int y1 = (int)(detectionMat.At<float>(i, 4) * frameHeight);
                                    int x2 = (int)(detectionMat.At<float>(i, 5) * frameWidth);
                                    int y2 = (int)(detectionMat.At<float>(i, 6) * frameHeight);

                                    ObjectPosRect objectPosRect = new ObjectPosRect();
                                    objectPosRect.setValues(classid, confidence, x1, y1, x2, y2);
                                    objectPosRects.Add(objectPosRect);

                                    //判断是否是最大人员模型；
                                    //为避免处理逻辑太复杂，最大模型宽度设置为一个常量；
                                    //if ((objectPosRect.x2 - objectPosRect.x1) < this.detectionRect.w / 3 && 
                                    //    (this.maxPersonPos.x2 - this.maxPersonPos.x1 < objectPosRect.x2 - objectPosRect.x1))
                                    //    this.maxPersonPos.setValues(objectPosRect);

                                    //为保障项目，排除太大或者太小的模型
                                    if((objectPosRect.x2 - objectPosRect.x1)<=200 && (objectPosRect.x2 - objectPosRect.x1) >= 10 &&
                                       (objectPosRect.y2 - objectPosRect.y1) <= 280 && (objectPosRect.y2 - objectPosRect.y1) >= 10)
                                    { 
                                        //判断是否是游戏操作人,模型位置为屏幕游戏者位置
                                        //游戏者的位置在屏幕下方靠左一点，大概 860/1920处
                                        //另外游戏中左右摇摆幅度较大，所以x轴的兼容值要设置大一些。
                                        if (Math.Abs(objectPosRect.x1 + (objectPosRect.x2 - objectPosRect.x1) / 2 - this.playerCentX) <= 100 &&
                                            objectPosRect.y1 > this.detectionRect.h *1/2 &&
                                            Math.Abs(this.detectionRect.h - objectPosRect.y2) <= 10)
                                        {
                                            //排除游戏者自己
                                            //var testi = 0;
                                        }
                                        else
                                        {
                                            //保存置信度最大的人员的位置,同时排除屏幕下方正中的游戏者自己
                                            if (objectPosRect.confidence > maxConfidencePos.confidence)
                                                maxConfidencePos.setValues(objectPosRect);
                                        }
                                    }

                                    long endTicks = DateTime.Now.Ticks;
                                    toltalMillis = (endTicks - startTicks) / 10000;

                                }
                            }
                        }
                    }
                    detectionResult.setValues(this.rawDetectionRect, this.detectionRect, frameMat, objectPosRects, maxConfidencePos, this.maxPersonW, toltalMillis);
                    //图像显示放到其他线程中处理
                    //this.ShowPicture(frameMat, objectPosRects, maxConfidencePos, toltalMillis);
                }
             
                //Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }     

            //使用类属性，只创建了一个对象，不能手工释放
            //graphics.Dispose();
            //bitmap.Dispose();

            return detectionResult;
        }

        //拷屏测试函数
        public void CopyScreenTest()
        {
            //c#中处理图形，需要注意手动释放资源，及线程不安全的问题。
            System.Drawing.Bitmap bitmap = new Bitmap(this.detectionRect.w, this.detectionRect.h);
            System.Drawing.Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(this.detectionRect.x, this.detectionRect.y, 0, 0, new System.Drawing.Size(this.detectionRect.w, this.detectionRect.h), CopyPixelOperation.SourceCopy);

            var hBitmap = bitmap.GetHbitmap();
            Image img = Image.FromHbitmap(hBitmap);

            //hBitmap会导致内存泄漏，必须人工释放
            DeleteObject(hBitmap);
            graphics.Dispose();
            bitmap.Dispose();

            this.ShowImage(img);
        }

        //显示图像测试函数
        private void ShowImage(Image img)
        {
            if (showPicture && mainForm != null && outPictureBox != null && mainForm.Visible)
            {
                if (img != null)
                {
                    outPictureBox.Image?.Dispose();
                    outPictureBox.Image = img;
                }
            }
        }

        public void IfShowPicture(bool showPicture)
        {
            this.showPicture = showPicture;
        }

        //显示检测后的图像
        public void ShowPicture(Mat frameMat, List<ObjectPosRect> objectPosRects, ObjectPosRect objectPosRect, long toltalMillis)
        {
            if (showPicture && mainForm != null && outPictureBox != null && mainForm.Visible)
            {
                if(frameMat != null)
                {
                    foreach(ObjectPosRect rect in objectPosRects)
                    {
                        Cv2.Rectangle(frameMat, new OpenCvSharp.Point(rect.x1, rect.y1), new OpenCvSharp.Point(rect.x2, rect.y2), new Scalar(0, 255, 0), 2, LineTypes.Link4);
                    
                        string msg = (string)this.labelNames.GetValue(rect.classid - 1) + ", " + rect.confidence.ToString("F2") + ", " + toltalMillis;
                        Cv2.PutText(frameMat, msg, new OpenCvSharp.Point(rect.x1, rect.y1 + 20), HersheyFonts.HersheySimplex, 0.95, new Scalar(0, 0, 255));  
                    }

                    //计算对象的中心点，靠上方一些
                    int objCenterX = objectPosRect.x1 + (objectPosRect.x2 - objectPosRect.x1) / 2;
                    int objCenterY = objectPosRect.y1 + (objectPosRect.y2 - objectPosRect.y1) / 3;
                    objCenterX = detectionRect.x + objCenterX;
                    objCenterY = detectionRect.y + objCenterY;

                    //计算屏幕中心点
                    int screenCenterX = rawDetectionRect.x + rawDetectionRect.w / 2;
                    int screenCenterY = rawDetectionRect.y + rawDetectionRect.h / 2;

                    Cv2.Rectangle(frameMat, new OpenCvSharp.Point(objCenterX - 10 - detectionRect.x, objCenterY - 10 - detectionRect.y), 
                        new OpenCvSharp.Point(objCenterX + 10 - detectionRect.x, objCenterY + 10 - detectionRect.y), new Scalar(0, 255, 0), 2, LineTypes.Link4);
                    Cv2.Rectangle(frameMat, new OpenCvSharp.Point(screenCenterX - 10 - detectionRect.x, screenCenterY - 10 - detectionRect.y), 
                        new OpenCvSharp.Point(screenCenterX + 10 - detectionRect.x, screenCenterY + 10 - detectionRect.y), new Scalar(0, 0, 255), 2, LineTypes.Link4);

                    var frameBitmap = BitmapConverter.ToBitmap(frameMat);

                    outPictureBox.Image?.Dispose();
                    outPictureBox.Image = frameBitmap;
                }
            }
        }
    }
}
