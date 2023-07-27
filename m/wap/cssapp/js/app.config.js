//var siteUrl="http://192.168.2.105";
var siteUrl="http://www.yangshuo.cm";
var pageSize=20;
var md5Key="21232f297a57a5a743894a0e4a801fc3";
var softVersion="20.00.1008";
var chkVersion=3;

var partnerID = "2088021612876241";
var sellerID = "530678888@qq.com";
var privateKey="MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCOb31XUgvKMmSNi8wQ2Z6JtInZ6AEC8e1T06uCm0Y1hNfKsk+vwD1y43pP1zYMVGslEOIbV3NVivm8rfswFp0BCJEI3RE8TIYrVzxLlB9gkEKJRnbw89AyxeIDIjPHktuddsikPs3Tj3uwYX7GqyV3djPp0z3BuZ6s0Bs8jVZ6KwIDAQAB";
var publicKey="MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCOb31XUgvKMmSNi8wQ2Z6JtInZ6AEC8e1T06uCm0Y1hNfKsk+vwD1y43pP1zYMVGslEOIbV3NVivm8rfswFp0BCJEI3RE8TIYrVzxLlB9gkEKJRnbw89AyxeIDIjPHktuddsikPs3Tj3uwYX7GqyV3djPp0z3BuZ6s0Bs8jVZ6KwIDAQAB";

var notifyUrl = siteUrl+"/payapi/alipay_kj/notify_url.aspx";

document.write('<script src="'+siteUrl+'/api_h/app.ext.js"> <\/script>');
function getKFPhone(){
	if(typeof(kfPhone) == "undefined"||kfPhone=="")
	{
		return "07732300400";
	}
	else{
		return kfPhone;
	}
}
