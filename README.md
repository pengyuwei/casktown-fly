casktown-fly
============

note:
------
README.me: UTF-8
other files: GBK

介绍：
------
木桶镇的资料片，一段木桶镇太空站的实习生的故事。(纵版设计游戏)

开发运行环境：
------
Visual Studio 2003(C#)
Windows XP/2000

官方网站：
------
http://toyshop.hoker.cc

截图：
![github](https://github.com/hokerffb/casktown-fly/screenshot/wmwins.jpg "github")  


版本历史：
------
        V0.08
        更新时间：2004-4-14 周三
        执行前检查相关文件是否存在、修改敌人运动方式、背景星空、敌人发射子弹、玩家发射氢弹
        两种爆炸方式、两种敌人、开头小动画，RPG对话功能

        V0.07
        更新时间：pyw 2004-4-9周五
        修正飞机在最左上角的时候会提示接触的BUG,同时限制了飞机的移动范围

        V0.06
        更新时间：pyw 2004-4-9周五
        增加空格开枪的功能

        V0.0.5
        最后更新：pyw 2004-4-7
        碰撞检测、边缘裁减（出屏幕的时候），完全应用了面向对象的方式实现

        V0.0.3
        全屏模式运行、修改类结构，抽象物体类（DX层面是物体属性之一）、键盘控制移动
        最后更新：pyw 2004-4-5

        V0.0.2
        在屏幕上显示一个叉子、过程式类结构（所有层面存于集合中）
        最后更新：pyw 2004-2-11
	
	
### aplha混合原理：
        首先，要能取得上层与下层颜色的 RGB三基色，然后用r,g,b 为最后取得的颜色值；r1,g1,b1是上层的颜色值；r2,g2,b2是下层颜色值
        r = r1/2 + r2/2;
        g = g1/2 + g2/2;
        b = b1/2 + b2/2;

        以上为50%透明。若要使用不同的透明度用以下算法（ALPHA=透明度）：

        （50%以下）
        r = r1 - r1/ALPHA + r2/ALPHA;
        g = g1 - g1/ALPHA + g2/ALPHA;
        b = b1 - b1/ALPHA + b2/ALPHA;

        （50%以上）
        r = r1/ALPHA + r2 - r2/ALPHA;
        g = g1/ALPHA + g2 - g2/ALPHA;
        b = b1/ALPHA + b2 - b2/ALPHA;


### 色彩饱和特效
        与Alpha混合相比，色彩饱和更适合于特效的制作，无论从性能上讲，还是从效果上说，色彩饱和比Alpha混合更胜一筹（如图4）。可能因为色彩饱和的算法过于简单，很少有进行介绍的，我们先就对色彩饱和的方法来进行介绍一下，混合公式：

        R1、G1、B1 ： 图象像素点的源色值；
        R2、G2、B2 ： 底图像素点的源色值；
        R = R1 + R2；（ IF R > 255 THEN R = 255 ）
        G = G1 + G2；（ IF G > 255 THEN G = 255 ）
        B = B1 + B2；（ IF B > 255 THEN B = 255 ）


剧本
------
新RPG剧本发布（一）
ABO:2002-7-13 08:54

TOYSHOP新RPG游戏剧本 

延续 

主角： 
        EVAN，一个生物学家的儿子，也是生物学家。 
        AKASA，EVAN喜欢的女孩。 
        其他人物： 
        YEBER，EVAN的爸爸。 
        友情客串：SOME、LONG、SANDY、FFB、ABO、DREAM、AT。 

FFB的要求：要科幻，要微生物的，而且不能玩恐怖，而且要包含哲理。 
难啊。 

ABO说明：昨天通宵郁闷的构思，听《宿命传说》、《EVA》还有《忘忧草》，很喜欢一个人在房间里静静的听这些歌的感觉。准备把风格倾向于JPRPG，笑。 
咖啡喝光了，我就泡茶，轻轻的喝，慢慢的想，EVAN该干什么呢？ 
由于是剧本，所以要考虑很多东西，有错误的地方，请指出来，谢谢。 
为了写出JPRPG风格，ABO在复习《天地创造》和《樱大战》。 

贴一小部分，如果大家觉得可以，我就继续写。 


序章 记忆 
（ffb:游戏开始就是远征舰队登陆这个星球，玩家控制飞机，干掉一些小怪物飞船后，干掉一个敌人的母舰，飞机登陆星球，转到：）
***片头动画*** 

（图片从上往下移动）背景是一个很多星星的夜晚，在野外。
（RPG场景）主角EVAN站在他老爸的左面，用很崇拜的眼神望着他的父亲，傻傻的笑，让人想起农夫山泉的广告或者西藏的天空。YEBER在望着天空，目光坚定。他对EVAN说
（RPG对话）孩子，你要记得这个星空，只要想起他，你就不会失去活下去的勇气，你也要知道你在星空下的渺小，你所做的只是很少的一点。 

（Logo画面显示汉字：）
第一章 两个世界 

***这段是讲述，相当于动画，如果技术不够，就用图片+文本来说明。*** 

（ARPG场景）
很多奇形怪状的生物在追着EVAN，背景颜色可以不停变幻，EVAN跑了很远，
（图片表达：）看见一个黑色的背影，很像YEBER，EVAN想去抓住他，却怎么也摸不到。主画面慢慢模糊。 
（RPG场景，人物自动移动）
EVAN满头大汗的从床上坐起来。面部特写。 
穿衣刷牙洗脸吃饭，出门。 
（转到图片表达：旁白字幕）
EVAN提着他的大箱子站在古往今来最伟大的城市北京里面是在一个阳光灿烂的秋天上午。当时他很年轻，也很英俊，对任何小说而言都是个理想的偶像派主角。在他看来，面前这座城市充满了足够的可能性。功名，爱情，理想等等一切会像宴会上的菜一道道鱼贯而入只要他缓缓穿过这家生物公司大门的阴影进入其中。 

这家公司的图标可以用HOKER，不要忘记打广告。 
严格的保密制度，EVAN只知道自己是负责检验细菌的耐受性。每天做着同样的事情，周而复始，时间就在EVAN不停的用各种各样的药品虐待细菌中度过。 
晚上很晚的时候，EVAN就会想起他的父亲，EVAN想做和他父亲一样的生物学家，很想很想。EVAN回忆的时候夜晚的黑色就会慢慢的渗入他的皮肤。 
可是EVAN内心依然有绝望，只是连他自己都说不出来那是什么，他只有在耳朵里充满暴烈的音乐和痛苦的呐喊，在看到一幅扭曲的油画，在陌生的路上看到一张陌生却隐忍着痛苦的面容，在满是霓虹的街上一直晃荡却找不到方向，在拿起电话却不知道该打给谁最终轻轻地放下的时候，他才会看见那些隐藏在内心的黑色从胸膛中汹涌着穿行而出，在他的眼前徜徉成一条黑色的河。 
哗啦啦，哗啦啦，绝望地向前跑。 

***从下面开始，玩家就可以控制了*** 
EVAN出现在自己的实验室，白大褂，可以戴眼镜，正在摆弄显微镜，玩家操纵他调整显微镜，调整粗细准焦，直到刚刚好。 
然后视野就出现一堆一堆挂了的细菌的尸体，但是1分钟后，又开始活跃，EVAN自言自语，这细菌的繁殖能力真是越来越强了。完全是几何级数的上升。 
（然后暂时可以去公司内部溜跶）

(以下对话是ABO捏造的)
ABO：写不下去了，我不知道玩家控制什么。 
FFB：控制EVAN和细菌打架，最后打最大的细菌BOSS。 
ABO：晕，老土了。而且不现实。 
FFB：那就打生化人。 
ABO：不是说不能玩恐怖的吗？ 
FFB：那，怎么办啊。 
ABO：大家讨论一下吧。 



缘起无名:
2002-8-10 10:52
 “要科幻，要微生物的，而且不能玩恐怖，而且要包含哲理”……那就这样好了： 
    我们的主人公为了消灭那些细菌，决定以菌治菌，于是他就合成了一种菌——就假设叫Tom吧：） 
    Tom刚开始时当然很弱，而且只会物理攻击，在通过与细菌的战斗中积累经验，慢慢变强，同时Even的研究也使它学会各种新的能力[比如放毒，分身（无性繁殖），金钢不坏身（加个蛋白质外壳）……]最后终于消灭了所有其它的细菌……[在此强烈建议开头过场时要说明那些细菌的来历：公元XXXX年，地球上的科学家发现了一种从未见过的细菌，它对人类社会有着严重危害性。（细菌和危害性……不太好说，因为这玩意儿太小……有了！就说公元前XXXX年，地球上某个地区新婚夫妇无一生下孩子的，这件事在记者报导后，引起了科学家的注意。经科学家们研究发现，这种细菌可以让人在无形中丧失繁殖能力——这词儿不太好听，不过也就这回事——但怎么杀死这种细菌呢？科学家们都束手无策……咚咚咚咚、咚咚咚咚，画面切换，我们的主角Evan出场！）] 
    当然，这一切都是在实验室里一个密卦的培养皿中进行，实验成功后，Evan公布了他的研究成果，因此获得了第XXX界诺贝尔生物学奖，于是Tom就被大量复制投放到全球，很快，地球上的那种可以让人失去繁殖能力的细菌被清除了，科学家们举杯庆辛，Even和他的心上人也结了婚。但是，没过多长时间，Even在研究中突然发现，Tom出现了一种有害的变种……（游戏在此时结束，如果反应还好的话，正好借着这个悬念做继集） 
    好了，四个条件在此时全部满足！ 
    但是主人公好象变成Tom了……：） 
