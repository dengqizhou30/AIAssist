GameAssist是一个AI游戏助手，结合OpenCv、OpenCvSharp4、ssd_mobilenet_v3等技术，对游戏对象进行识别，支持自动瞄准/自动开枪等功能，提升玩家的游戏体验；</br>
</br>
**
OpenCvSharp作者不再支持CUDA加速，编译CUDA一堆问题，正在考虑是否继续使用OpenCvSharp，或者切换到c++项目。c++的UI前端选型比较麻烦，mfc等技术太老，winui3又太新，试用中发现各种不便，纠结中......</br>
找个节假日，切换到c++项目算了，有兴趣的兄弟多交流</br>
**
</br>
工具和普通游戏外挂不同：</br>
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
很多游戏禁止鼠标键盘hook，外部进程无法通过传统事件机制向游戏进程发送鼠标键盘事件。</br>
要实现游戏不会拦截的键盘鼠标操作，需要使用可编程的鼠标键盘硬件设备，这些硬件设备提供程序可以调用的SDK，控制硬件设备来发送鼠标键盘事件。</br>
这种鼠标键盘设备，淘宝上有，USB接口的，具体信息这里不方便列出来，各位兄弟自己去找。</br>
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
![img](https://github.com/dengqizhou30/AIAssist/blob/master/GameAssist/test/AIAssist.png)</br>
![img](https://github.com/dengqizhou30/AIAssist/blob/master/GameAssist/test/juediqiusheng1.png)</br>
![img](https://github.com/dengqizhou30/AIAssist/blob/master/GameAssist/test/nizhan1.png)</br>
![img](https://i.postimg.cc/yJ6s4z9G/nizhan1.jpg)</br>
</br>
</br>
三、后续优化方向：</br>
</br>
1、模型调优1：目前直接使用了预训练模型，图像识别效果一般。后续需要针对具体的游戏，整理训练样本，进行调优训练，提升识别准确率；（这个需要手工截图、标注，太繁琐，目前实在懒得做）</br>
2、模型调优2：目前只使用对象识别模型，后续尝试对象识别模型和对象追踪算法进行组合，提升模型识别效率；</br>
3、鼠标追踪算法调优：3D游戏鼠标移动瞄准算法，需要不断尝试调优；</br>
4、技术栈升级：opencv、AI模型等，全栈技术紧追业界发展，最快升级到新版本；</br>
5、图像检测使用gpu/CUDA加速，降低cpu消耗同时提升图像检测速度；</br>
</br>
</br>
四、使用gpu加速：</br>
</br>
OpenCvSharp4缺省不支持CUDA，需要使用者自己定制编译。
1、定制编译支持CUDA的OpenCv：</br>
https://github.com/shimat/opencv_files</br>
</br>
官方文档中没有说明如何cuda，在运行build_windows.ps1前，需要修改这个文件，添加CUDA配置：</br>
          -D WITH_CUDA=ON `</br>
          -D CUDA_ARCH_BIN=8.6 `</br>
          -D CUDA_ARCH_PTX=8.6 `</br>
文件内容参考 .\GameAssist\tool\cuda\build_windows.ps1</br>
注意CUDA_ARCH_*的值，和你显卡实际的计算能力对应，具体值参考显卡官网：</br>
https://developer.nvidia.com/zh-cn/cuda-gpus
</br>
2、定制编译支持CUDA的OpenCvSharp4；</br>
https://github.com/shimat/opencvsharp</br>
在OpenCvSharp.csproj文件中，增加ENABLED_CUDA。</br>
在OpenCvSharpExtern项目上右键，属性，找到c/c++，预编译，增加ENABLED_CUDA。</br>
详细步骤可以参考下面的链接：
https://github.com/shimat/opencvsharp/issues/1299</br>
https://blog.csdn.net/bashendixie5/article/details/106162481</br>
</br>
OpenCvSharp作者不再支持CUDA加速，这块编译一堆问题，正在考虑是否继续使用OpenCvSharp，或者切换到c++项目......</br>
</br>
</br>
3、代码中指定使用CUDA进行后台加速：</br>
this.detectionNet.SetPreferableBackend(Backend.CUDA);</br>
this.detectionNet.SetPreferableTarget(Target.CUDA);</br>
</br>
</br>
五、引用的框架及项目：</br>
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
</br>
</br>
六、联系方式：</br>
</br>
日常工作繁忙，不能及时回复，各位可以在 issues 区留言交流。

