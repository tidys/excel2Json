# excel2Json 
excel 转换 Json 工具  
- [v1.1 点击下载](https://github-production-repository-file-5c1aeb.s3.amazonaws.com/67108297/1976222?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20180505%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20180505T014717Z&X-Amz-Expires=300&X-Amz-Signature=df7182374cf1967eb5c6c88bf8a7d3be023d1237a6a62194fcc8f1c124c5d85e&X-Amz-SignedHeaders=host&actor_id=7894208&response-content-disposition=attachment%3Bfilename%3Dtools.zip&response-content-type=application%2Fzip)

## 工具介绍
采用c#编写开发, 将excel表格的数据转换成json格式,方便程序使用.		

## 工具特点
1.该工具支持一个excel多个工作薄,方便相同模块的表格存放到一个excel		
2.工具可以停靠在窗口顶部隐藏,类似qq的功能		
3.工具可以导出txt格式,方便导入到数据库		
4.支持bat脚本,可以简单扩展你的想要的功能		

## 使用说明
### 注意事项:
1.程序在启动的时候,会读取exe所在目录下的文件夹,这一步很重要		
2.exe同级文件夹下必须存在 json/txt/xlsx 这三个目录,如果没有程序会自动生成		
3.excel文件必须存放在xlsx目录下,其他目录不予读取			
4.如果xlsx文件夹出现多层目录结构,该工具会自动检索出该目录下所有的excel文件		
5.如果使用过程中出现问题,请多留意下边的log提示信息			
###表格配置
>多工作薄配置表格(必须存在)

|sheet|	name	|index	|desc|
|-------| --------| ------- | ------ |
|lv	|lv	|1|关卡表|

说明: 			
sheet: 具体的数据表格名字,excel必须有工作薄			
name: 导出json时的文件名			
index:该工作薄在excel的位置			
desc:对该工作薄的一个简单描述说明,程序不会使用,可作为注释			

> 具体的数据表格:

|id|name|PowerCost|LvLimite|
|-------| --------| ------- | ------ |
|关卡编号|关卡名称|消耗体力|开启等级|
|10010001|英雄出世1|    5|	1|
|10010002|英雄出世2|    6|	2|
|10010003|英雄出世3|    7|	3|
|10010004|英雄出世4|    8|	4|
第一行为最终json对象的key			
第二行为中文注释说明			
第三行之后为具体的数据		
####最终的excel请参考 excel2Json/DataConvert/bin/Debug/策划/xlsx/数值/test.xlsx, 最终转换的json
```json
[
    {
        "id": "10010001",
        "name": "英雄出世1",
        "PowerCost": "5",
        "LvLimite": "1"
    },
    {
        "id": "10010002",
        "name": "英雄出世2",
        "PowerCost": "6",
        "LvLimite": "2"
    },
    {
        "id": "10010003",
        "name": "英雄出世3",
        "PowerCost": "7",
        "LvLimite": "3"
    },
    {
        "id": "10010004",
        "name": "英雄出世4",
        "PowerCost": "8",
        "LvLimite": "4"
    }
]
```

		
### 工具使用
1.bat脚本:可指定bat脚本文件路径,按下F12即可执行该bat脚本,示例脚本:		

    :: 拷贝文件到目标文件
    :: copy /y 
	@echo off
	cd /d %~dp0
	set desDir="D:\project\mhgjClient\assets\resources\json"
	:: 遍历所在文件夹的json文件
	for /R %%s in (*.json) do ( 
		echo %%s 
		copy /y %%s  %desDir%	
	) 
	pause

2.载入excel之后,右击listView的item,弹出菜单,可以打开相应的 文件/文件夹, 如果文件夹已经打开,则该文件夹窗口置顶.		
3.选中listView的item之后,按下 空格键 可以迅速打开excel文件		
## 关于
该工具是自己游戏开发生涯中的一个小积累			
如果你喜欢,请告诉你的小伙伴,		
如果不喜欢,请告诉我哪里不好(企鹅 774177933),或者直接在Issues里面提问,帮助我完善它		
### QQ群 
CocosCreator插件开发交流: 224756137	

![群图片](http://7xq9nm.com1.z0.glb.clouddn.com/%E6%8F%92%E4%BB%B6%E4%BA%A4%E6%B5%81%E7%BE%A4.png)	

点击链接加入群【CocosCreator插件开发交流】：https://jq.qq.com/?_wv=1027&k=5ANhoTC	

### QQ打赏:		 			
![enter image description here](http://7xq9nm.com1.z0.glb.clouddn.com/qqPay.png)












