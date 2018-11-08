### 本项目使用了V(View)B(Bridge)B(Business)架构。

#### View：

- Resources(资源)：
  - 图片资源(Image)
  - 语言资源(Language) 
- StyleResourcces ：样式资源资源
- UI：界面
- UIBusiness：纯UI业务

------

#### Bridge：

绑定View和Business，View会传入this到Bridge，由Bridge获取View内的参数来调用Business相关代码（如FileBusiness - 文件业务）处理逻辑。

------

#### Business：

- CurrencyBusiness（通用业务）
- Model(业务模型)
- ViewBusiness(与UI一一对应，处理自己UI页独有的业务)