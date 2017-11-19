# ProtoToCS
小工具.生成protobuf的cs文件.也可用于ILRuntime

基本完成.选中目录一键导出cs文件.

导出的cs文件需要用到Google.Protobuf.dll库.或者使用LitEngine库.也可以下载对应文件编译

其实只用到了Google.Protobuf的IO部分.

理论上,2.0和3.0都可以绝大部分支持.请猥琐发育.

Oneof Fields 暂不支持.

导出的类包含读写两个方法.请自行了解.