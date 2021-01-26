GameAssist是一个AI游戏助手，结合OpenCv、OpenCvSharp4、ssd_mobilenet_v3等技术，对游戏对象进行识别，支持自动瞄准/自动开枪等功能，提升玩家的游戏体验；</br>

重要的事情说三遍：这个工具和普通游戏外挂不同！不同！不同！</br>
普通游戏外挂：通过需改游戏执行代码/修改游戏内存数据/拦截游戏网络通讯等手段，破坏游戏进程，达到提升目的，这些手段是违法的。</br>
GameAssist：使用AI技术进行屏幕检测，原理是用人工智能技术来玩游戏，不使用任何破坏游戏进程的手段。</br>
</br>
</br>
一、技术栈说明：</br>
</br>
1、图像处理框架：</br>
使用OpenCv进行图像封装及AI对象识别；</br>
使用OpenCvSharp4封装库，将OpenCv引入C#开发运行环境；</br>
使用windwos gdi32的Bitmap的Graphics类的CopyFromScreen，获取屏幕像素作为输入图像；</br>
</br>
2、AI模型选择：</br>
使用OpenCv的DNN网络模块，加载AI模型进行图像检测；</br>
使用tensoflow的对象检测模型ssd_mobilenet_v3，作为人像检测模型；</br>
</br>
注意：OpenCV DNN 模块调用 TensorFlow 训练的目标检测模型时，需要一个额外的配置文件，其主要是基于与 protocol buffers(protobuf) 格式序列化图(graph) 相同的文本格式版本.</br>
OpenCV提供对应的python脚本，根据模型文件来生成模型配置文件：</br>
生成OpenCV生成模型配置文件命令参考：</br>
docker run -it -v /root/123:/root/123 tensorflow/tensorflow:latest-py3 /bin/bash</br>
python tf_text_graph_ssd.py --input frozen_inference_graph.pb --config pipeline.config --output ssd_mobilenet_v2.pbtxt</br>
python tf_text_graph_ssd.py --input frozen_inference_graph.pb --config pipeline.config --output ssd_mobilenet_v3.pbtxt</br>
</br>
3、键盘鼠标操作设置：</br>
很多游戏禁止鼠标键盘hook，外部进程不能向游戏进程发送鼠标键盘事件。为实现键盘操作，需要使用可编程的鼠标键盘设备，程序调用设备SDK，通过硬件设备来发送鼠标键盘事件。</br>
这种鼠标键盘设备，淘宝上有，USB接口的，这里不方便列出来，各位兄弟自己去找。</br>
</br>
</br>

二、测试效果：</br>
</br>
1、运行环境：</br>
windows10，16核CPU，16G内存，8G rtx3070显卡</br>
</br>
2、测试游戏：</br>
目前只测试了PUBG的绝地求生、腾讯的逆战两款游戏。</br>
逆战图像里面的图像识别准确率比绝地求生高，效果也更好，借助工具辅助，个人由一个菜鸟升级为中高级玩家。</br>
</br>
游戏效果截图：</br>
![img](https://i.postimg.cc/K12mfTYx/juediqiusheng1.jpg)
</br>
![img](https://i.postimg.cc/yJ6s4z9G/nizhan1.jpg)
</br>
</br>
三、后续优化方向：</br>
</br>
模型调优1：直接使用了预训练模型，需要对具体的游戏，整理训练数据进行调优训练，提升识别准确率；</br>
模型调优2：目前直接使用对象识别模型，后续尝试引入对象追踪算法模型，提升模型识别效率；</br>
鼠标追踪算法调优：3D游戏鼠标移动瞄准算法，需要不断尝试调优；</br>
技术栈升级：opencv、AI模型等，全栈跟随技术发展，最快升级到最新版；</br>
</br>
</br>
四、引用的框架及项目：</br>
</br>
1、intel贡献的大神级图像处理框架OpenCv：</br>
https://opencv.org/</br>
</br>
OpenCV DNN 模块目前支持多种AI对象检测模型推理运行：</br>
https://github.com/opencv/opencv/tree/master/samples/dnn</br>
https://github.com/opencv/opencv/wiki/TensorFlow-Object-Detection-API</br>
https://github.com/openvinotoolkit/open_model_zoo</br>
</br>
2、OpenCvSharp4封装库，将OpenCv引入C#开发运行环境；</br>
https://github.com/shimat/opencvsharp</br>
在Visual Studio中，可以通过NuGet安装OpenCvSharp程序包；</br>
</br>
3、谷歌tensoflow的对象检测模型ssd_mobilenet_v3；</br>
https://github.com/tensorflow</br>
https://github.com/tensorflow/models</br>
https://github.com/tensorflow/models/blob/master/research/slim/nets/mobilenet/README.md</br>
https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf2_detection_zoo.md</br>
https://github.com/tensorflow/models/blob/master/research/object_detection/g3doc/tf1_detection_zoo.md</br>




