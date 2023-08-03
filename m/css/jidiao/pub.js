

var vcity={ 11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",
        	21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",
        	33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",
        	42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",
        	51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",
        	63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"
           };

function checkCard(card){
	//是否为空
	if(card === '')	{
		return false;
	}
	//校验长度，类型
	if(isCardNo(card) === false)	{
		return false;
	}
	//检查省份
	if(checkProvince(card) === false){
		return false;
	}
	//校验生日
	if(checkBirthday(card) === false)	{
		return false;
	}
	//检验位的检测
	if(checkParity(card) === false)	{
		return false;
	}
	return true;
}




//检查号码是否符合规范，包括长度，类型
function isCardNo(card){
	//身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
	var reg = /(^\d{15}$)|(^\d{17}(\d|X)$)/;
	if(reg.test(card) === false){
		return false;
	}
	return true;
}

//取身份证前两位,校验省份
function checkProvince(card){
	var province = card.substr(0,2);
	if(vcity[province] == undefined){
		return false;
	}
	return true;
}

//检查生日是否正确
function checkBirthday(card){
	var len = card.length;
	//身份证15位时，次序为省（3位）市（3位）年（2位）月（2位）日（2位）校验位（3位），皆为数字
	if(len == '15')	{
		var re_fifteen = /^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/; 
		var arr_data = card.match(re_fifteen);
		var year = arr_data[2];
		var month = arr_data[3];
		var day = arr_data[4];
		var birthday = new Date('19'+year+'/'+month+'/'+day);
		return verifyBirthday('19'+year,month,day,birthday);
	}
	//身份证18位时，次序为省（3位）市（3位）年（4位）月（2位）日（2位）校验位（4位），校验位末尾可能为X
	if(len == '18')	{
		var re_eighteen = /^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/;
		var arr_data = card.match(re_eighteen);
		var year = arr_data[2];
		var month = arr_data[3];
		var day = arr_data[4];
		var birthday = new Date(year+'/'+month+'/'+day);
		return verifyBirthday(year,month,day,birthday);
	}
	return false;
}

//校验日期
function verifyBirthday(year,month,day,birthday){
	var now = new Date();
	var now_year = now.getFullYear();
	//年月日是否合理
	if(birthday.getFullYear() == year && (birthday.getMonth() + 1) == month && birthday.getDate() == day){
		//判断年份的范围（3岁到100岁之间)
		var time = now_year - year;
		if(time >= 3 && time <= 100){
			return true;
		}
		return false;
	}
	return false;
}

//校验位的检测
function checkParity(card){
	//15位转18位
	card = changeFivteenToEighteen(card);
	var len = card.length;
	if(len == '18')	{
		var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2); 
		var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2'); 
		var cardTemp = 0, i, valnum; 
		for(i = 0; i < 17; i ++) 
		{ 
			cardTemp += card.substr(i, 1) * arrInt[i]; 
		} 
		valnum = arrCh[cardTemp % 11]; 
		if (valnum == card.substr(17, 1)) 
		{
			return true;
		}
		return false;
	}
	return false;
}

//15位转18位身份证号
function changeFivteenToEighteen(card){
	if(card.length == '15')	{
		var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2); 
		var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2'); 
		var cardTemp = 0, i;   
		card = card.substr(0, 6) + '19' + card.substr(6, card.length - 6);
		for(i = 0; i < 17; i ++) { 
			cardTemp += card.substr(i, 1) * arrInt[i]; 
		} 
		card += arrCh[cardTemp % 11]; 
		return card;
	}
	return card;
}
function chkmobile(no){
	if(no=="")return;
	if(no.length<11){
		return false;
	}
    reg=/(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}/g;

    var telReg = !!no.match(/^(0|86|17951)?(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$/);
    if(telReg == false){
		return false;
	}
	return true;
}



function chkcnname(val){ 
   var reg = /^[\u4E00-\u9FA5]{2,4}$/; 
   if(!reg.test(val)){ 
	  if(chkenname(val)){
          return true;
	  }else{
		  return false;
	  }
   }else{ 
      return true;
   } 
} 
function chkenname(ms){
   var reg = /^[a-zA-Z\s]{4,50}$/; 
   if(!reg.test(ms)){ 
      return false;
   }else{ 
      return true;
   }
}
function getcard(ms,callback){
	var sfz="",tel="",mob="",name1="",name2="",totc=0,dd="",manc=0,boyc=0;
	ms=ms.replace(/\r\n/g,"");
	
	//找身份证
	var reg = /(\d{17}(\d|X))/g;
	var data = ms.match(reg);
    if(data){
		for(var one in data){
			if(checkCard(data[one])){
				//alert("身份证号："+data[one]);
				sfz=data[one];
				break;
			}
		}
	}
	
    //找电话
    //reg=/(1[35789][0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}/g;
	//13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}
	//reg=/(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}/g;

    reg=/(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}/g;

	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("手机号码："+data[one]);
			tel=data[one];
			break;
		}
	}
	
	//找姓名
	reg=/[赵钱孙李周吴郑王冯陈褚卫蒋沈韩杨朱秦尤许何吕施张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧尹姚邵湛汪祁毛禹狄米贝明臧计伏成戴谈宋茅庞熊纪舒屈项祝董粱杜阮蓝闵席季麻强贾路娄危江童颜郭梅盛林刁钟徐邱骆高夏蔡田樊胡凌霍虞万支柯昝管卢莫经房裘缪干解应宗丁宣贲邓郁单杭洪包诸左石崔吉钮龚程嵇邢滑裴陆荣翁荀羊於惠甄麴家封芮羿储靳汲邴糜松井段富巫乌焦巴弓牧隗山谷车侯宓蓬全郗班仰秋仲伊宫宁仇栾暴甘钭厉戎祖武符刘景詹束龙叶幸司韶郜黎蓟薄印宿白怀蒲邰从鄂索咸籍赖卓蔺屠蒙池乔阴欎胥能苍双闻莘党翟谭贡劳逄姬申扶堵冉宰郦雍舄璩桑桂濮牛寿通边扈燕冀郏浦尚农温别庄晏柴瞿阎充慕连茹习宦艾鱼容向古易慎戈廖庾终暨居衡步都耿满弘匡国文寇广禄阙东殴殳沃利蔚越夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾毋沙乜养鞠须丰巢关蒯相查後荆红游竺权逯盖益桓公万俟司马上官欧阳夏侯诸葛闻人东方赫连皇甫尉迟公羊澹台公冶宗政濮阳淳于单于太叔申屠公孙仲孙轩辕令狐钟离宇文长孙慕容鲜于闾丘司徒司空亓官司寇仉督子车颛孙端木巫马公西漆雕乐正壤驷公良拓跋夹谷宰父谷梁晋楚闫法汝鄢涂钦段干百里东郭南门呼延归海羊舌微生岳帅缑亢况后有琴梁丘左丘东门西门商牟佘佴伯赏南宫墨哈谯笪年爱阳佟第五言福百家姓终卓蔺屠蒙池乔阳郁胥能苍双闻莘党翟谭贡劳逄姬申扶堵冉宰郦雍却璩桑桂濮牛寿通边扈燕冀僪浦尚农温别庄晏柴瞿阎充慕连茹习宦艾鱼容向古易慎戈庾终暨居衡步都耿满弘匡国文寇广禄阙东欧殳沃利蔚越夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾毋沙乜养鞠须丰巢关蒯相查后荆红游竺权逮盍益桓公唱万俟司马上官欧阳夏侯诸葛闻人东方赫连皇甫尉迟公羊澹台公冶宗政濮阳淳于单于太叔申屠公孙仲孙轩辕令狐钟离宇文长孙慕容司徒司空召有舜丛岳寸贰皇侨彤竭端赫实甫集象翠狂辟典良函芒苦其京中夕之蹇称诺来多繁戊朴回毓税荤靖绪愈硕牢买但巧枚撒泰秘亥绍以壬森斋释奕姒朋求羽用占真穰翦闾漆贵代贯旁崇栋告休褒谏锐皋闳在歧禾示委钊频嬴呼大威昂律冒保系抄定化莱校么抗祢綦悟宏功庚务敏捷拱兆丑丙畅苟随类卯俟友答乙允甲留尾佼玄乘裔延植环矫赛昔侍度旷遇偶前由咎塞敛受泷袭衅叔圣御夫仆镇藩邸府掌首员焉戏可智尔凭悉进笃厚仁业肇资合仍九衷哀刑俎仵圭夷徭蛮汗孛乾帖罕洛淦洋邶郸郯邗邛剑虢隋蒿茆菅苌树桐锁钟机盘铎斛玉线针箕庹绳磨蒉瓮弭刀疏牵浑恽势世仝同蚁止戢睢冼种凃肖己泣潜卷脱谬蹉赧浮顿说次错念夙斯完丹表聊源姓吾寻展出不户闭才无书学愚本性雪霜烟寒少字桥板斐独千诗嘉扬善揭祈析赤紫青柔刚奇拜佛陀弥阿素长僧隐仙隽宇祭酒淡塔琦闪始星南天接波碧速禚腾潮镜似澄潭謇纵渠奈风春濯沐茂英兰檀藤枝检生折登驹骑貊虎肥鹿雀野禽飞节宜鲜粟栗豆帛官布衣藏宝钞银门盈庆喜及普建营巨望希道载声漫犁力贸勤革改兴亓睦修信闽北守坚勇汉练尉士旅五令将旗军行奉敬恭仪母堂丘义礼慈孝理伦卿问永辉位让尧依犹介承市所苑杞剧第零谌招续达忻六鄞战迟候宛励粘萨邝覃辜初楼城区局台原考妫纳泉老清德卑过麦曲竹百福言第五佟爱年笪谯哈墨南宫赏伯佴佘牟商西门东门左丘梁丘琴后况亢缑帅微生羊舌海归呼延南门东郭百里钦鄢汝法闫楚晋谷梁宰父夹谷拓跋壤驷乐正漆雕公西巫马端木颛孙子车督仉司寇亓官鲜于锺离盖逯库郏逢阴薄厉稽闾丘公良段干开光操瑞眭泥运摩伟铁迮荔菲辗迟]{1}[\u4E00-\u9FA5]{1,3}[\s+\(\（\,\，\;\d]/g;
	var s=ms.replace(/(联系|日期|行程|电话|人数|手机|旅游|旅行|入园|千古情|团队|姓名|酒店|接送|贵宾|明天)/g,"@");
	data=s.match(reg);
	if(data){
		for(var one in data){
			//alert("姓名："+data[one]);
			if(name1==""){
				name1=data[one].replace(/[\s+\(\,\，\;\（\d]/gi,"");
			}else{
				name2=data[one];
				break;
			}
		}
	}
	
	//日期
	reg=/(\d{1,2}月\d{1,2}日|\d{1,2}\-\d{1,2})/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("日期："+data[one]);
			dd=data[one].replace("月","-").replace("日","");
			break;
		}
	}
    if(dd==""){
	}else{
		dd="2019-"+dd;
	}
	//人数 全
	reg=/(\d{1,5}人|\d{1,5}大|\d{1,5}全|\d{1,5}成|\d{1,5}位|\d{1,5}个)/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("人数："+data[one]);
			manc=val(data[one]);
			break;
		}
	}
	//人数 半
	reg=/(\d{1,5}小|\d{1,5}优|\d{1,5}半)/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("人数："+data[one]);
			boyc=val(data[one]);
			break;
		}
	}
	totc=manc+boyc;
	var aa={"name":name1,"date":dd,"manc":manc,"boyc":boyc,"sfz":sfz,"tel":tel};
	/*
	var ms='';
	ms+='<br>姓名：'+name1;
	if(name2==""){
	}else{
		//ms+='('+name2+')';
	}
	ms+='<br>手机：'+tel;
	ms+='<br>身份证：'+sfz;
	ms+='<br>人数：'+totc;
	ms+='<br>入园日期：'+dd;
	
    $("#info").html(ms);
	*/
	try{callback(aa)}catch(e){}
}



function getallykinfo(ms,callback){
	var yksfz=[],yktel=[],ykname=[],totc=0,dd="",manc=0,boyc=0;
	ms=ms.replace(/\r\n/g,"");
	
	//找姓名
	reg=/[赵钱孙李周吴郑王冯陈褚卫蒋沈韩杨朱秦尤许何吕施张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧尹姚邵湛汪祁毛禹狄米贝明臧计伏成戴谈宋茅庞熊纪舒屈项祝董粱杜阮蓝闵席季麻强贾路娄危江童颜郭梅盛林刁钟徐邱骆高夏蔡田樊胡凌霍虞万支柯昝管卢莫经房裘缪干解应宗丁宣贲邓郁单杭洪包诸左石崔吉钮龚程嵇邢滑裴陆荣翁荀羊於惠甄麴家封芮羿储靳汲邴糜松井段富巫乌焦巴弓牧隗山谷车侯宓蓬全郗班仰秋仲伊宫宁仇栾暴甘钭厉戎祖武符刘景詹束龙叶幸司韶郜黎蓟薄印宿白怀蒲邰从鄂索咸籍赖卓蔺屠蒙池乔阴欎胥能苍双闻莘党翟谭贡劳逄姬申扶堵冉宰郦雍舄璩桑桂濮牛寿通边扈燕冀郏浦尚农温别庄晏柴瞿阎充慕连茹习宦艾鱼容向古易慎戈廖庾终暨居衡步都耿满弘匡国文寇广禄阙东殴殳沃利蔚越夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾毋沙乜养鞠须丰巢关蒯相查後荆红游竺权逯盖益桓公万俟司马上官欧阳夏侯诸葛闻人东方赫连皇甫尉迟公羊澹台公冶宗政濮阳淳于单于太叔申屠公孙仲孙轩辕令狐钟离宇文长孙慕容鲜于闾丘司徒司空亓官司寇仉督子车颛孙端木巫马公西漆雕乐正壤驷公良拓跋夹谷宰父谷梁晋楚闫法汝鄢涂钦段干百里东郭南门呼延归海羊舌微生岳帅缑亢况后有琴梁丘左丘东门西门商牟佘佴伯赏南宫墨哈谯笪年爱阳佟第五言福百家姓终卓蔺屠蒙池乔阳郁胥能苍双闻莘党翟谭贡劳逄姬申扶堵冉宰郦雍却璩桑桂濮牛寿通边扈燕冀僪浦尚农温别庄晏柴瞿阎充慕连茹习宦艾鱼容向古易慎戈庾终暨居衡步都耿满弘匡国文寇广禄阙东欧殳沃利蔚越夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾毋沙乜养鞠须丰巢关蒯相查后荆红游竺权逮盍益桓公唱万俟司马上官欧阳夏侯诸葛闻人东方赫连皇甫尉迟公羊澹台公冶宗政濮阳淳于单于太叔申屠公孙仲孙轩辕令狐钟离宇文长孙慕容司徒司空召有舜丛岳寸贰皇侨彤竭端赫实甫集象翠狂辟典良函芒苦其京中夕之蹇称诺来多繁戊朴回毓税荤靖绪愈硕牢买但巧枚撒泰秘亥绍以壬森斋释奕姒朋求羽用占真穰翦闾漆贵代贯旁崇栋告休褒谏锐皋闳在歧禾示委钊频嬴呼大威昂律冒保系抄定化莱校么抗祢綦悟宏功庚务敏捷拱兆丑丙畅苟随类卯俟友答乙允甲留尾佼玄乘裔延植环矫赛昔侍度旷遇偶前由咎塞敛受泷袭衅叔圣御夫仆镇藩邸府掌首员焉戏可智尔凭悉进笃厚仁业肇资合仍九衷哀刑俎仵圭夷徭蛮汗孛乾帖罕洛淦洋邶郸郯邗邛剑虢隋蒿茆菅苌树桐锁钟机盘铎斛玉线针箕庹绳磨蒉瓮弭刀疏牵浑恽势世仝同蚁止戢睢冼种凃肖己泣潜卷脱谬蹉赧浮顿说次错念夙斯完丹表聊源姓吾寻展出不户闭才无书学愚本性雪霜烟寒少字桥板斐独千诗嘉扬善揭祈析赤紫青柔刚奇拜佛陀弥阿素长僧隐仙隽宇祭酒淡塔琦闪始星南天接波碧速禚腾潮镜似澄潭謇纵渠奈风春濯沐茂英兰檀藤枝检生折登驹骑貊虎肥鹿雀野禽飞节宜鲜粟栗豆帛官布衣藏宝钞银门盈庆喜及普建营巨望希道载声漫犁力贸勤革改兴亓睦修信闽北守坚勇汉练尉士旅五令将旗军行奉敬恭仪母堂丘义礼慈孝理伦卿问永辉位让尧依犹介承市所苑杞剧第零谌招续达忻六鄞战迟候宛励粘萨邝覃辜初楼城区局台原考妫纳泉老清德卑过麦曲竹百福言第五佟爱年笪谯哈墨南宫赏伯佴佘牟商西门东门左丘梁丘琴后况亢缑帅微生羊舌海归呼延南门东郭百里钦鄢汝法闫楚晋谷梁宰父夹谷拓跋壤驷乐正漆雕公西巫马端木颛孙子车督仉司寇亓官鲜于锺离盖逯库郏逢阴薄厉稽闾丘公良段干开光操瑞眭泥运摩伟铁迮荔菲辗迟]{1}[\u4E00-\u9FA5]{1,3}[\s+\(\（\,\，\;\d]/g;
	var s=ms.replace(/(联系|日期|行程|电话|人数|手机|旅游|旅行|入园|千古情|团队|姓名|酒店|接送|贵宾|明天)/g,"@");
	data=s.match(reg);
	if(data){
		for(var one in data){
			ykname.push(data[one].replace(/[\s+\(\,\，\;\（\d]/gi,""));
		}
	}

	//找身份证
	var reg = /(\d{17}(\d|X))/g;
	var data = ms.match(reg);
    if(data){
		for(var one in data){
			if(checkCard(data[one])){
				yksfz.push(data[one]);
			}
		}
	}
	
    //找电话
    reg=/(13[0-9]|13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			yktel.push(data[one]);
		}
	}


	
	//日期
	reg=/(\d{1,2}月\d{1,2}日|\d{1,2}\-\d{1,2})/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("日期："+data[one]);
			dd=data[one].replace("月","-").replace("日","");
			break;
		}
	}
    if(dd==""){
	}else{
		dd="2019-"+dd;
	}
	//人数 全
	reg=/(\d{1,5}人|\d{1,5}大|\d{1,5}全|\d{1,5}成|\d{1,5}位|\d{1,5}个)/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("人数："+data[one]);
			manc=val(data[one]);
			break;
		}
	}
	//人数 半
	reg=/(\d{1,5}小|\d{1,5}优|\d{1,5}半)/g;
	data=ms.match(reg);
	if(data){
		for(var one in data){
			//alert("人数："+data[one]);
			boyc=val(data[one]);
			break;
		}
	}
	var aa={"name":ykname,"date":dd,"manc":manc,"boyc":boyc,"sfz":yksfz,"tel":yktel};
	try{callback(aa)}catch(e){}
}



	function setdiary(ms){
		    var nowdate=new Date();
			var opt={};
			opt.date = {preset : 'date'};
			opt.datetime = {preset : 'datetime'};
			opt.time = {preset : 'time'};
			opt.default = {
				theme: 'android-ics light', //皮肤样式
		        display: 'modal', //显示方式 
		        mode: 'scroller', //日期选择模式
				dateFormat: 'yy-mm-dd',
				lang: 'zh',
				showNow: false,
				nowText: "今天",
		        startYear: nowdate.getFullYear(), //开始年份
		        endYear: nowdate.getFullYear()+2 //结束年份
			};
		  	$("#"+ms).mobiscroll($.extend(opt['date'], opt['default']));
	}

function selformclose(msg,callback){
	var _this=this;
	var boxid="selformboxclose";
	_this.closeme=function(){
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	var ms='';
	ms+='<div class="fade" style="max-height:90%"><div class="modal-dialog" style="height:95%"><div class="modal-content" style="height:98%;overflow-y:auto">';
    ms+='<div class="modal-body" style="font-size: 14px;background-color:#FEFFFF"><div style="text-align: left;margin-top:0px;">'+msg+'</div></div>';
    ms+='</div></div></div>';
	ms+='<div id="cmdcloseselfromclose"  style="overflow:hidden;width:30px;height:30px;line-height:30px;border-radius:30px;text-align:center;font-size:26px;background-color:#FFF;color:#333;position:absolute;top:10px;right:0px;z-index:9999;border:1px solid #333">&times;</div>';
	_this.closeme();
	var div=document.createElement("div");
	div.id=boxid;
	div.className="modal";
	div.innerHTML=ms;
	document.body.appendChild(div);
	_this.box=div;
	$("#cmdcloseselfromclose").unbind().click(function(){
		_this.closeme();
	});
	$(div).show();
	try{callback(_this);}catch(e){}
}

function noclose(msg,callback){
	//信息窗
	var bgid="noclose_bg";
	var boxid="noclose_box";
    var _this=this;
	_this.closeme=function(){
		try{document.body.removeChild(document.getElementById(bgid))}catch(e){}
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	_this.closeme();
    var w=parent.document.body.clientWidth||parent.document.documentElement.clientWidth;
    var h=parent.document.body.clientHeight||parent.document.documentElement.clientHeight;
    var st=parent.document.body.scrollTop||parent.document.documentElement.scrollTop;
	var bg=parent.document.createElement("div");
	
	bg.id=bgid;
	bg.style.position="fixed";
	bg.style.width="100%";
	bg.style.height="100%";
	bg.style.left=0+"px";
	bg.style.top=0+"px";
	bg.style.backgroundColor="#333333";
	bg.style.zIndex=9999998;
	parent.document.body.appendChild(bg);
    try{bg.style.filter="alpha(opacity=80)";}catch(e){}
	try{bg.style.opacity=0.8;}catch(e){}

	var box=parent.document.createElement("div");
	box.id=boxid;
	box.style.position="fixed";
	box.style.overflow="hidden";
	if(w>760){
	   box.style.width="40%";
	}else{
	   box.style.width="80%";
	}
	try{box.style.borderRadius=5+"px";}catch(e){}
	box.style.height="auto";
	box.style.backgroundColor="#FFF";
	box.style.zIndex=9999999;
    box.style.display="none";
	box.style.fontSize=14+"px";
	box.className="yinyin3";

	var head=parent.document.createElement("div");
	head.style.overflow="hidden";
	head.style.height=40+"px";
	head.style.position="relative";
	head.style.backgroundColor="#FFF";
	head.style.borderBottom="1px solid #ddd";
	head.style.margin=10+"px";
	box.appendChild(head);

		var cap=parent.document.createElement("span");
		cap.style.overflow="hidden";
		cap.style.display="block";
		cap.style.color="#777";
		cap.style.height=40+"px";
		cap.style.lineHeight=40+"px";
		cap.style.fontSize=16+"px";
		cap.style.fontWeight="700";
	   cap.innerHTML="<i class='fa fa-cubes'></i> 温馨提示";
    head.appendChild(cap);
	
	
	var cont=parent.document.createElement("div");
	cont.style.position="relative";
	cont.style.overflow="hidden";
	cont.style.height="auto";
	cont.style.margin=10+"px";
	cont.style.marginBottom="30px";
	cont.style.color="#555";
    try{cont.innerHTML=msg;}catch(e){}
	box.appendChild(cont);


	parent.document.body.appendChild(box);
	st=0;
	$(box).css({"top":(20+st)+"px","left":((w-$(box).outerWidth())/2)+"px"});
	$(box).fadeIn(function(){
		try{callback(_this)}catch(e){}
	});	
	
}
//-------------------------------------------------------------------------------------
function setfindkey(callback){
	$(".cmdfindkey").each(function(index,obj){
		obj.onclick=function(){
			var tab=$(obj).attr("datatab");
			var key=$("#txtkey").val();
			try{callback(tab,key)}catch(e){}
		};
	});
}
//-------------------------------------------------------------------------------------
function setfindtype(callback){
	var _this=this;
	var divid="findtypemenu_id";
	try{document.body.removeChild(document.getElementById(divid))}catch(e){}
	var div=document.createElement("div");
	div.id=divid;
	div.style.position="fixed";
	div.style.zIndex=99999;
	var w=120;
	//div.style.overflow="hidden";
	//div.style.overflowY="auto";
	div.style.width=w+"px";
	div.style.height="100%";
	div.style.top=0+"px";
	div.style.backgroundColor="transparent";
    div.style.left=-w+"px";
	
	var cont=document.createElement("div");
	cont.style.position="absolute";
	cont.style.overflow="hidden";
	cont.style.width=w+"px";
	cont.style.height="100%";
	cont.style.top=0+"px";
	cont.style.left=0+"px";
	cont.style.border="1px solid #DDD";
	cont.style.backgroundColor="#F8F8F8";
	cont.style.overflowY="auto";
	
	var onoff=document.createElement("div");
	onoff.style.position="absolute";
	onoff.style.overflow="hidden";
	onoff.style.width=30+"px";
	onoff.style.height=40+"px";
	onoff.style.right=-30+"px";
	onoff.style.top="50%";
	onoff.style.marginTop=-20+"px";
	onoff.style.border="1px solid #DDD";
	onoff.style.backgroundColor="#39C";
	onoff.style.lineHeight=40+"px";
	onoff.style.textAlign="center";
	onoff.className="onofffindtype";
	try{
		onoff.style.borderTopRightRadius="8px";
		onoff.style.borderBottomRightRadius="8px";
	}catch(e){}
	onoff.innerHTML='<i class="fa fa-angle-double-right myflash" style="font-size:20px;color:#FFF"></i>';
	onoff.onclick=function(){
		var xy=$(div).offset();
		if(xy.left>=0){
			_this.offmenu();
		}else{
			$(div).animate({"left":"0px"});
	        onoff.innerHTML='<i class="fa fa-angle-double-left" style="font-size:20px;color:#FFF"></i>';
		}
	};
	_this.offmenu=function(){
			$(div).animate({"left":"-"+w+"px"});
	        onoff.innerHTML='<i class="fa fa-angle-double-right myflash" style="font-size:20px;color:#FFF"></i>';
	};
	document.body.appendChild(div);
	div.appendChild(cont);
	div.appendChild(onoff);
	_this.box=cont;
	try{callback(_this)}catch(e){}
}
//-------------------------------------------------------------------------------------
function judgelogin(){	
	var wxid=getCookie("session_wxid");
	$.get("/9766/pub/php/ajaxbd.php?dowhat=getadmininfo&wxid="+encodeURIComponent(wxid),function(data,stat){
		eval("var aa="+data);
		window.mytype="";
		if(aa.msg=="OK"){
                setCookie("session_wxid",aa.wxid,30)

				$("#bdsysname").remove();
				var div=document.createElement("div");
				div.id="bdsysname";
				div.style.position="fixed";
				div.style.zIndex=99990;
				div.style.display="block";
				div.style.color="#F00";
				div.style.overflow="hidden";
				div.style.left="50%";
				div.style.marginLeft="-50px";
				div.style.lineHeight=1;
				div.style.textAlign="center"
				div.style.bottom=25+"px";
				div.style.padding="2px 4px";
				div.style.backgroundColor="#CC0";
				div.style.color="#393";
				try{div.style.borderRadius=5+"px"}catch(e){}
				//div.className="myflash";
                //try{div.style.filter="alpha(opacity=50)";}catch(e){}
	            //try{div.style.opacity=0.5;}catch(e){}
			
				div.innerHTML=aa.bdsysname;
				document.body.appendChild(div);
				$(div).unbind().click(function(e){
					e.stopPropagation();
					var o=$(".BM_fixBox").find(".BM_DH");
					if(o.length==3){
					   $(o[1]).click();
					}
				});
				var w=$(div).outerWidth();
				div.style.marginLeft=(-w/2)+"px";
			   initBottomMenu(aa);
			   if(("A"+aa.mtype).indexOf("系统操作员")>0){
				   $(".sysworkernone").hide();
			   }
			   window.mytype=aa.mtype;
		}else if(aa.msg=="SELECT"){
				var ms='';
				ms+='<div id="div_sel_bd"></div>';
			    noclose(ms,function(_s){
				   $.get("/9766/pub/php/ajaxbd.php?dowhat=selbdsys&sel="+aa.bdsysid,function(data,stat){
					   $("#div_sel_bd").html(data);
					   $(".selbdsys").each(function(t1,o1){
						    o1.onclick=function(){
								$.get("/9766/pub/php/ajaxbd.php?dowhat=bdsysselected&id="+$(o1).attr("dataid"),function(data,stat){
									document.location.reload();
								});
							};
					   });
				   });
			    });
		}else{
			noclose(aa.msg,function(_s){
			});
		}
	});
	
	$(".backarrow").unbind().click(function(){
        window.history.back(-1);
	});
}


function topfrm(msg,callback){
	var _this=this;
	var boxid="topfrm";
	_this.closeme=function(){
		try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
	}
	var ms='';
	ms+='<div class="fade" style="max-height:90%;"><div class="modal-dialog" style="height:95%;"><div class="modal-content" style="height:98%;overflow-y:auto;">';
    ms+='<div class="modal-body" style="font-size: 14px;background-color:#FEFFFF"><div style="text-align: left;margin-top:0px;">'+msg+'</div></div>';
    ms+='</div></div></div>';
	ms+='<div id="cmdtopfrmclose"  style="overflow:hidden;width:30px;height:30px;line-height:30px;border-radius:30px;text-align:center;font-size:26px;background-color:#FFF;color:#333;position:absolute;top:10px;right:0px;z-index:9999;border:1px solid #333">&times;</div>';
	_this.closeme();
	var div=document.createElement("div");
	div.id=boxid;
	div.className="modal";
	div.style.zIndex=99999999;
	div.innerHTML=ms;
	document.body.appendChild(div);
	_this.box=div;
	$("#cmdtopfrmclose").unbind().click(function(){
		_this.closeme();
	});
	$(div).show();
	try{callback(_this);}catch(e){}
}

//-------------------------------------------------------------------------------------
function showmovemsg(msg){
         var w=document.documentElement.clientWidth||document.body.clientWidth;
   	     var h=document.documentElement.clientHeight||document.body.clientHeight;
	     var box=document.createElement("div");
	     var w1=280;
		 var h1=0;
		 box.style.textAlign="center";
	     box.style.width=w1+"px";
	     box.style.height="auto";
	     box.style.padding="10px 0px 10px 0px";
	     box.style.lineHeight=30+"px";
	     box.style.position="fixed";
	     box.style.backgroundColor="#555";
		 box.style.color="#FFF";
		 box.style.fontSize=18+"px";
	     box.style.zIndex=99999999;
	     box.innerHTML=msg;
		 box.style.display="none";
		 box.className="yinyin";
		 try{box.style.borderRadius="8px"}catch(e){}
	     document.body.appendChild(box);
         h1=box.offsetHeight;
	     var x=(w-w1)/2;
	     var y=(h-h1)/2;
	     box.style.top=y+"px";
	     box.style.left=x+"px";
		 $(box).fadeIn(function(){
			 setTimeout(function(){
                $(box).animate({"top":(y-200)+"px","opacity":"0"},1000,function(){
		            try{document.body.removeChild(box)}catch(e){}
                });
			 },1000);
		 });
}


function sysmsg(msg){
	     var boxid="sysmsg_boxid";
         try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
         var w=document.documentElement.clientWidth||document.body.clientWidth;
   	     var h=document.documentElement.clientHeight||document.body.clientHeight;
	     var box=document.createElement("div");
	     var w1=280;
		 var h1=0;
		 box.style.textAlign="center";
		 box.id=boxid;
	     box.style.width=w1+"px";
		 box.style.overflow="hidden";
	     box.style.height="auto";
	     box.style.padding="10px 0px 10px 0px";
	     box.style.lineHeight=30+"px";
	     box.style.position="fixed";
	     box.style.backgroundColor="#555";
		 box.style.color="#FFF";
		 box.style.fontSize=18+"px";
	     box.style.zIndex=99999999;
	     box.innerHTML=msg;
		 box.style.display="none";
		 box.className="yinyin";
		 try{box.style.borderRadius="8px"}catch(e){}
	     document.body.appendChild(box);
         h1=box.offsetHeight;
	     var x=(w-w1)/2;
	     var y=(h-h1)/2-50;
	     box.style.top=y+"px";
	     box.style.left=x+"px";
		 $(box).fadeIn(function(){
			 setTimeout(function(){
                $(box).animate({"opacity":"0"},100,function(){
		            try{document.body.removeChild(document.getElementById(boxid))}catch(e){}
                });
			 },2000);
		 });
}

//-------------------------------------------------------------------------------------
    judgelogin();
//-------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------