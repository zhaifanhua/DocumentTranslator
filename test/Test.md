## 🍟 概述

- 本项目适合有一定 NetCore 和 vue 基础的开发人员
- 基于.NET5/.NET7 实现的通用权限管理平台（RBAC 模式）。整合最新技术高效快速开发，前后端分离模式，开箱即用。
- 代码量少、学习简单、通俗易懂、功能强大、易扩展、轻量级，让 web 开发更快速、简单高效（从此告别 996），解决 70%的重复工作，专注您的业务，轻松开发从现在开始！
- 提供了技术栈(Ant Design Vue)版[Ant Design Vue](https://gitee.com/billzh/mc-dull.git)
- 七牛云通用云产品优惠券：[点我进入](https://s.qiniu.com/FzEfay)。
- 腾讯云秒杀场：[☛☛ 点我进入 ☚☚](https://curl.qcloud.com/4yEoRquq)。
- 腾讯云优惠券：[☛☛ 点我领取 ☚☚](https://curl.qcloud.com/5J4nag8D)。
- 阿里云特惠专区：[☛☛ 点我进入 ☚☚](https://www.aliyun.com/minisite/goods?userCode=uotn5vt1&share_source=copy_link)

## 🍀 Back-end technology

- Core Framework: . Net7.0 + Web API + sqlsugar + swagger + signalR + IpRateLimit + Quartz.net + Redis
- Scheduled tasks: Quartz.Net component that supports the execution of assemblies or HTTP network requests
- Security support: filters (data permission filtering), SQL injection, request forgery
- Log management: NLog, login log, operation log, scheduled task log
- Tools: Captcha, rich public functions
- Interface throttling: Supports interface throttling to avoid excessive pressure on the service layer caused by malicious requests
- Code generation: efficient development, the code generator can generate all front-end and back-end code with one click
- Data dictionary: Support data dictionary, which can facilitate the management of some states
- Sharding and sharding: Using ORM SQLSUGAR, you can easily achieve superior sharding and sharding performance
- Multi-tenant: Support multi-tenancy function
- Cache data: Built-in memory cache and Redis

```
如果对您有帮助，您可以点右上角 “Star” 收藏一下 ，谢谢！~
```

## 📈 快速开始

- 快速开始：[https://www.izhaorui.cn/doc/quickstart.html](https://www.izhaorui.cn/doc/quickstart.html)

## 🍿 在线体验

- 官方文档：http://www.izhaorui.cn/doc
- 加入群聊：[立即加入](http://www.izhaorui.cn/doc/contact.html)
- Vue3.x 版本体验：[http://demo.izhaorui.cn/vue3](http://demo.izhaorui.cn/vue3)
- Vue2.x 版本体验：[http://demo.izhaorui.cn/admin](http://demo.izhaorui.cn/admin)
- Uniapp 版本体验(vue2)：[http://demo.izhaorui.cn/h5](http://demo.izhaorui.cn/h5)
- Uniapp 版本体验(vue3)：[http://demo.izhaorui.cn/uplus](http://demo.izhaorui.cn/uplus)
- 账号密码：admin/123456，普通用户 user/123456

## 🍖 Built-in features

1. User management: The user is the system operator, and this function mainly completes the system user configuration.
2. Department management: configure the system organization (company, department, group), tree structure display.
3. Job management: configure the position of the system user.
4. Menu management: configure system menus, operation permissions, button permission identification, etc.
5. Role Management: Role menu permission assignment.
6. Dictionary management: maintain some relatively fixed data that is often used in the system.
7. Operation log: system normal operation log records and queries; System exception information logging and querying.
8. Logon logon: The system logon log record query contains logon exceptions.
9. System Interface: Use Swagger to generate relevant API interface documentation.
10. Service monitoring: Monitor the current system CPU, memory, disk, stack, and other related information.
11. Online Builder: Drag form elements to generate the corresponding VUE code (only VUE2 supported).
12. Task system: Based on the Quartz.NET, you can schedule tasks online (add, modify, delete, manually execute) including execution result logs.
13. Article management: You can write article records.
14. Code generation: You can generate front-end and back-end code (.cs, .vue, .js, .sql, etc.) with one click, support download, customize the configuration of front-end display controls, and make development faster and more efficient (the strongest in history).
15. Parameter management: dynamically configure common parameters for the system.
16. Send Mail: You can send mail to multiple users.
17. File management: You can manage uploaded files, which currently supports uploading to on-premises and Alibaba Cloud.
18. Notification management: The system notifies and announces information release and maintenance, and uses SignalR to realize real-time notification to users.
