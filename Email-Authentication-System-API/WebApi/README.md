# 邮箱验证登录系统

#### 介绍
前端vue、elementPlus
后端c#


#### 安装教程
1.打开Service=>utils=>EmailUtil.cs

参照我的博客
http://t.csdnimg.cn/qDKTf 获取自己的邮箱授权码，替换在如下图内容中

![img.png](img%2Fimg.png)

2. 需要运行Redis数据库，如果不是在本地运行，请调整appsettings.json中的内容："Redis": "127.0.0.1:6379"
3. 此时，已经可以启动项目了，本项目使用的是Sqlite数据库，数据库文件是data.db