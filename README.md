<div align="center"><h1 align="center">AKStreamUI</a></h1></div>
<div align="center"><h3 align="center">AKStream前后端分离架构，开箱即用</h3></div>

### 概述

* 基于.NET 5实现的AKStream平台。整合最新技术，模块插件式开发，前后端分离，开箱即用。
* 后台基于Furion框架，vue2前端基于小诺框架。
* 流媒体模块包括：设备列表、流媒体服务、Sip服务（通道查看，直播预览）。
* 核心模块包括：用户、角色、职位、组织机构、菜单、字典、日志、多应用管理、文件管理、定时任务等功能。
* 代码简洁、易扩展，让开发更简单、更通用、更流行！

```
如果对您有帮助，点击右上角⭐Star⭐关注 ，感谢支持开源！
```

### 快速启动

需要安装（如果不调试源码可以不需要，直接编译项目）：VS2019（最新版）、npm或yarn（最新版）
注：请按照以下步骤进行
* 启动后台：打开backend/Admin.NET.sln解决方案，直接运行（F5）即可启动（数据库默认mysql）（不调试代码忽略此步骤）
* 编译项目：backend 目录下直接 dotnet build
* 数据库：默认 Mysql
（1）新建一个数据库，还原 database 目录下的 .sql 文件
（2）清空 VideoChannel 表的数据。
（3）配置 backend/Admin.NET.Database.Migrations 项目下的 dbsettings.json 数据库链接文件，使用默认 DefaultConnection 链接。
（4）修改 backend/Furion.Extras.Admin.NET 项目下的 applicationconfig.json 文件，最后 AKStream节点配置AKStream的接口URL和AccessKey
（5）修改 AKStream 数据库链接与本项目使用一致。
* 启动前端：打开frontend文件夹，进行依赖下载，运行 yarn install 命令，再运行 yarn run serve
* 浏览器访问：`http://localhost:81` （默认前端端口为：81，后台端口为：5566）

### 效果图

<table>
    <tr>
        <td><img src="https://i.loli.net/2021/07/14/3jCED5l9vGXKmW7.png"/></td>
        <td><img src="https://i.loli.net/2021/07/14/hLpbMCWJrHEmlBi.png"/></td>
    </tr>
    <tr>
        <td><img src="https://i.loli.net/2021/07/14/1nRxJIGWN3l4TmZ.png"/></td>
        <td><img src="https://i.loli.net/2021/07/14/xhvHB8Daf5ztdLq.png"/></td>
    </tr>
</table>

### 详细功能

1. 设备列表：管理GB28181通过Sip网关添加到数据库中的设备。
2. 流媒体服务：AKStreamKeeper所管理的ZLM服务。
3. Sip服务：通道查看、直播预览。

### 补充说明

* 有问题讨论的小伙伴可加群一起学习讨论。 QQ群【870526956】

### 特别鸣谢
- 👉 ZLMediaKit：[https://github.com/ZLMediaKit/ZLMediaKit](https://github.com/ZLMediaKit/ZLMediaKit)
- 👉 AKStream：[https://github.com/chatop2020/AKStream](https://github.com/chatop2020/AKStream)

如果对您有帮助，您可以点右上角 💘Star💘支持一下，这样我们才有持续下去的动力，谢谢！！！
