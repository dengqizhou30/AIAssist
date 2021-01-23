AI游戏助手，使用AI技术进行游戏屏幕人员识别，自动瞄准等，辅助玩家提升游戏体验


一、技术栈说明：

1、图像处理框架：
使用OpenCv进行图像封装及AI对象识别；
使用OpenCvSharp4封装库，将OpenCv引入C#开发运行环境；
使用windwos gdi32的Bitmap的Graphics类的CopyFromScreen，获取屏幕像素作为输入图像；

2、AI模型选择：
使用OpenCv的DNN网络模块，加载AI模型进行图像检测；
使用tensoflow的对象检测模型ssd_mobilenet_v3，作为人像检测模型；

注意：OpenCV DNN 模块调用 TensorFlow 训练的目标检测模型时，需要一个额外的配置文件，其主要是基于与 protocol buffers(protobuf) 格式序列化图(graph) 相同的文本格式版本.
OpenCV提供对应的python脚本，根据模型文件来生成模型配置文件：
生成OpenCV生成模型配置文件命令参考：
docker run -it -v /root/123:/root/123 tensorflow/tensorflow:latest-py3 /bin/bash
python tf_text_graph_ssd.py --input frozen_inference_graph.pb --config pipeline.config --output ssd_mobilenet_v2.pbtxt
python tf_text_graph_ssd.py --input frozen_inference_graph.pb --config pipeline.config --output ssd_mobilenet_v3.pbtxt

3、键盘鼠标操作设置：
很多游戏禁止鼠标键盘hook，外部进程不能向游戏进程发送鼠标键盘事件。为实现键盘操作，需要使用可编程的鼠标键盘设备，程序调用设备SDK，通过硬件设备来发送鼠标键盘事件。
这种鼠标键盘设备，淘宝上有，USB接口的，这里不方便列出来，各位兄弟自己去找。



二、测试效果：

1、运行环境：
windows10，16核CPU，16G内存，8G rtx3070显卡

2、测试游戏：
目前只测试了PUBG的绝地求生、腾讯的逆战两款游戏。
逆战图像里面的图像识别准确率比绝地求生高，效果也更好，借助工具辅助，个人由一个菜鸟升级为中高级玩家。
游戏效果截图：




三、后续优化方向：

模型调优1：直接使用了预训练模型，需要对具体的游戏，整理训练数据进行调优训练，提升识别准确率；
模型调优2：目前直接使用对象识别模型，后续尝试引入对象追踪算法模型，提升模型识别效率；
鼠标追踪算法调优：3D游戏鼠标移动瞄准算法，需要不断尝试调优；
技术栈升级：opencv、AI模型等，全栈跟随技术发展，最快升级到最新版；



四、感谢以下框架及项目：

1、intel贡献的大神级图像处理框架OpenCv：
https://opencv.org/

OpenCV DNN 模块目前支持多种AI对象检测模型推理运行：
https://github.com/opencv/opencv/tree/master/samples/dnn
https://github.com/opencv/opencv/wiki/TensorFlow-Object-Detection-API
https://github.com/openvinotoolkit/open_model_zoo

2、OpenCvSharp4封装库，将OpenCv引入C#开发运行环境；
https://github.com/shimat/opencvsharp
在Visual Studio中，可以通过NuGet安装OpenCvSharp程序包；

3、谷歌tensoflow的对象检测模型ssd_mobilenet_v3；
https://github.com/tensorflow
https://github.com/tensorflow/models
https://github.com/tensorflow/models/blob/master/research/slim/nets/mobilenet/README.md
https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf2_detection_zoo.md
https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf1_detection_zoo.md




