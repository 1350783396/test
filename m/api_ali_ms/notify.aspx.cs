using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;

using System.Security.Cryptography;

using System.Collections.Specialized;


namespace ETicket.Web.api_ali_ms
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Response.Write("6C1346D63291FC26939D8CC5757BE65D<br>");
            var signt = "219f9cb7eceb40fcd4aa016a61993423consume_type2item_title银子岩门票methodsendmobile18077113872num1num_iid522071639483order_id1525658026040792seller_nickhaaron_johnsend_type2sku_properties门票种类:成人票;门票类型:电子票sms_template验证码$code.您已成功订购haaron_john提供的银子岩门票,有效期2016/04/12至2016/04/12,消费时请出示本短信以验证.如有疑问,请联系卖家.sub_method1sub_outer_iid2taobao_sid92333234timestamp2016-04-12 04:04:37tokend3077e5d86c75a2142f87161a4b4c3eatype0valid_ends2016-04-12 23:59:59valid_start2016-04-12 00:00:00weeks[1,2,3,4,5,6,7]";
            
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.GetEncoding("GBK").GetBytes(signt));

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }
            Response.Write(result);
            */

            //接口文档
            //http://ma.taobao.com/doc/standard.htm?spm=0.0.0.0.5rFAzD#partnerinterface
           
            try
            {
                var method = PubFun.QueryString("method");
                switch (method)
                {
                    case "send":
                        send();
                        break;

                    case "resend":
                        resend();
                        break;

                    case "modified":
                        modified();
                        break;

                    case "order_modify":
                        order_modify();
                        break;

                    case "cancel":
                        cancel();
                        break;

                    case "heartBeatCheck":
                        heartBeatCheck();
                        break;

                    default:
                        //NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","fail","未处理方法:" + method);
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "ex-ipm", ex.InnerException.ToString());
            }
        }

        //码商ID
        string codemerchant_id = APIHelper.codemerchant_id;
        /// <summary>
        /// 发码
        /// </summary>
        void send()
        {
            //接口只记录数据，不做逻辑处理

            #region 参数
            //接口调用时的时间：yyyy-MM-dd HH:mm:ss
            var timestamp = PubFun.QueryString("timestamp");
            var sign = PubFun.QueryString("sign");//签名
            var order_id = PubFun.QueryString("order_id");//淘宝订单交易号
            //用户通知类型：
            //0 ： 正常老流程
            //1：不传明文手机号，只传加密手机号
            //2：仅限内部使用
            var type = PubFun.QueryString("type");
            //买家的手机号码
            var mobile = PubFun.QueryString("mobile");
            //买家手机号中间四位隐藏 
            var encrypt_mobile = PubFun.QueryString("encrypt_mobile");
            //买家手机号MD5值
            var md5_mobile = PubFun.QueryString("md5_mobile");
            //购买的商品数量
            var num = PubFun.QueryString("num");
            var method = PubFun.QueryString("method");
            var taobao_sid = PubFun.QueryString("taobao_sid");//淘宝卖家seller_id
            var seller_nick = PubFun.QueryString("seller_nick");//淘宝卖家用户名（旺旺号）
            var item_title = PubFun.QueryString("item_title");//商品标题
            //发送类型：
            //0：二维码(发短信、彩信，短信只有验证码)
            //1：矩阵码(只发短信，短信包含矩阵码和验证码)
            //2：验证码(只发短信，短信只有验证码)，目前默认值为2
            var send_type = PubFun.QueryString("send_type");
            //核销类型:0：不限制,1:一码一刷,2:一码多刷,默认值为0
            var consume_type = PubFun.QueryString("consume_type");
            var sms_template = PubFun.QueryString("sms_template");
            //有效期开始时间:yyyy-MM-dd HH:mm:ss
            var valid_start = PubFun.QueryString("valid_start");
            //有效期截止时间：yyyy-MM-dd HH:mm:ss
            var valid_ends = PubFun.QueryString("valid_ends");
            //淘宝商品编号
            var num_iid = PubFun.QueryString("num_iid");
            //商家发布商品时有填写的话就传
            var outer_iid = PubFun.QueryString("outer_iid");
            //下单时选择的销售属性对应的商家编码
            var sub_outer_iid = PubFun.QueryString("sub_outer_iid");
            //下单时选择的销售属性对应的文本
            var sku_properties = PubFun.QueryString("sku_properties");
            //token验证串，商家回调时须回传，否则将验证不通过，同一个订单的token是唯一的，不会变
            var token = PubFun.QueryString("token");
            //订单实付金额
            var total_fee = PubFun.QueryString("total_fee");
            //有效期中的星期设置，如[1,2,3,4,5,6,7],表示周一到周日才能核销
            var weeks = PubFun.QueryString("weeks");

            IDictionary<string, string> parameters = GetRequestPost();
            //日志
            StringBuilder sbLog = new StringBuilder();
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            sbLog.AppendLine("sign:" + sign);

            #endregion

 
            //验证签名
            if (!validSign(parameters,sign,sbLog))
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "valid_fail", sbLog.ToString());
                //Response.End();
                return;
            }

            //订单是否存在
            var sendModel = ETicket.BLL.TaoBaoMSBLL.Instance.GetCount(p => p.order_id == order_id && p.method == "send");
            if (sendModel > 0)
            {
                sbLog.AppendLine("该订单已经存在系统");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "exits", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }


            //记录到表
            EFEntity.Taobao_MS alimsModel = new EFEntity.Taobao_MS();
            try
            {
                //淘宝数据
                alimsModel.timestamp = Convert.ToDateTime(timestamp);
                alimsModel.sign = sign;
                alimsModel.order_id = order_id;
                alimsModel.type = type;
                alimsModel.mobile = mobile;
                alimsModel.encrypt_mobile = encrypt_mobile;
                alimsModel.md5_mobile = md5_mobile;
                alimsModel.num = (num == "" ? 0 : int.Parse(num));
                alimsModel.method = method;
                alimsModel.taobao_sid = taobao_sid;
                alimsModel.seller_nick = seller_nick;
                alimsModel.item_title = item_title;
                alimsModel.send_type = send_type;
                alimsModel.consume_type = consume_type;
                alimsModel.sms_template = sms_template;
                alimsModel.valid_start = valid_start;
                alimsModel.valid_ends = valid_ends;
                alimsModel.num_iid = num_iid;
                alimsModel.outer_iid = outer_iid;
                alimsModel.sub_outer_iid = sub_outer_iid;
                alimsModel.sku_properties = sku_properties;
                alimsModel.token = token;
                alimsModel.total_fee = (total_fee == "" ? 0 : Convert.ToDecimal(total_fee));
                alimsModel.weeks = weeks;
                alimsModel.codemerchant_id = codemerchant_id;

                //系统内部状态
                alimsModel.SysTranStatus = 0;
                alimsModel.SysTranNum = 0;

                //数据
                ETicket.BLL.TaoBaoMSBLL.Instance.AddObject(alimsModel);
                sbLog.AppendLine("记录到Taobao_MS表成功。");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","succ", sbLog.ToString());

                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            catch (Exception ex)
            {
                sbLog.AppendLine("记录到Taobao_MS表发生错误。");
                sbLog.AppendLine(ex.ToString());
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","ex-ipm", sbLog.ToString());
                //Response.End();
                return;
            }

        }

        /// <summary>
        /// 重新发码
        /// </summary>
        void resend()
        {
            #region 参数
            var timestamp = PubFun.QueryString("timestamp");
            var sign = PubFun.QueryString("sign");
            var order_id = PubFun.QueryString("order_id");
            var type = PubFun.QueryString("type");
            var mobile = PubFun.QueryString("mobile");
            var encrypt_mobile = PubFun.QueryString("encrypt_mobile");
            var md5_mobile = PubFun.QueryString("md5_mobile");
            var num = PubFun.QueryString("num");
            var left_num = PubFun.QueryString("left_num");
            var method = PubFun.QueryString("method");
            var taobao_sid = PubFun.QueryString("taobao_sid");
            var seller_nick = PubFun.QueryString("seller_nick");
            var item_title = PubFun.QueryString("item_title");
            var num_iid = PubFun.QueryString("num_iid");
            var outer_iid = PubFun.QueryString("outer_iid");
            var sub_outer_iid = PubFun.QueryString("sub_outer_iid");
            var sku_properties = PubFun.QueryString("sku_properties");
            var send_type = PubFun.QueryString("send_type");
            var consume_type = PubFun.QueryString("consume_type");
            var sms_template = PubFun.QueryString("sms_template");
            var valid_start = PubFun.QueryString("valid_start");
            var valid_ends = PubFun.QueryString("valid_ends");
            var token = PubFun.QueryString("token");

            IDictionary<string, string> parameters = GetRequestPost();

            //日志
            StringBuilder sbLog = new StringBuilder();
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            sbLog.AppendLine("sign:" + sign);

            #endregion

            //验证签名
            if (!validSign(parameters,sign,sbLog))
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "valid_fail", sbLog.ToString());
                //Response.End();
                return;
            }
            if (type != "0")
            {
                sbLog.Append("type不为0，不用重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "not_do", sbLog.ToString());
                Response.Write("{\"code\":200}");
                return;
            }

            var sendModel = ETicket.BLL.TaoBaoMSBLL.Instance.GetEntity(p => p.order_id == order_id && p.method == "send");
            if (sendModel == null)
            {
                sbLog.Append("根据order_id和method找相应的订单失败，无法重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "resend_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }

            //执行重新发送短信
            var sysOrderID = sendModel.OrderID.Value;
            var sms = BLL.SMSSendOrderBLL.Instance.GetEntity(p => p.OrderID == sysOrderID);
            if (sms == null)
            {
                sbLog.Append("根据系统的OrderID无法找到相关短信的记录，无法重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "resend_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            sms.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送中.ToString();
            sms.SendNum = sms.SendNum + 1;
            sms.SendIsRead = false;
            BLL.SMSSendOrderBLL.Instance.UpdateObject(sms);

            //Response.Write("{\"code\":200}");

            /* */
            //记录到淘宝通知回调
            EFEntity.Taobao_MS alimsModel = new EFEntity.Taobao_MS();

            //本系统
            alimsModel.OrderID = sendModel.OrderID;//系统OrderID
            alimsModel.SheetID = sendModel.SheetID;
            alimsModel.UserID = sendModel.UserID;
            //淘宝数据
            alimsModel.timestamp = Convert.ToDateTime(timestamp);
            alimsModel.sign = sign;
            alimsModel.order_id = order_id;
            alimsModel.type = type;
            alimsModel.mobile = mobile;
            alimsModel.encrypt_mobile = encrypt_mobile;
            alimsModel.md5_mobile = md5_mobile;
            alimsModel.num = int.Parse(num);
            alimsModel.method = method;
            alimsModel.taobao_sid = taobao_sid;
            alimsModel.seller_nick = seller_nick;
            alimsModel.item_title = item_title;
            alimsModel.send_type = send_type;
            alimsModel.consume_type = consume_type;
            alimsModel.sms_template = sms_template;
            alimsModel.valid_start = valid_start;
            alimsModel.valid_ends = valid_ends;
            alimsModel.num_iid = num_iid;
            alimsModel.outer_iid = outer_iid;
            alimsModel.sub_outer_iid = sub_outer_iid;
            alimsModel.sku_properties = sku_properties;
            alimsModel.token = token;
            //alimsModel.total_fee = Convert.ToDecimal(total_fee);
            //alimsModel.weeks = weeks;
            alimsModel.codemerchant_id = codemerchant_id;

            //通知接口数据
            alimsModel.NotifyMethod = ETicket.Utility.AlilMSNotifyEnum.resend.ToString();
            alimsModel.NotifyStatus = 0;
            alimsModel.NotifyNum = 0;

            try
            {
                ETicket.BLL.TaoBaoMSBLL.Instance.AddObject(alimsModel);
                sbLog.AppendLine("写到淘宝码商通知表成功。");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","succ",sbLog.ToString());
                //成功接收到发码通知后商家接口须返回响应内容：{"code":200}
                Response.Write("{\"code\":200}");
            }
            catch (Exception ex)
            {
                sbLog.AppendLine("写到淘宝码商通知表发生错误。");
                sbLog.AppendLine(ex.ToString());
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","ex-ipm",sbLog.ToString());
            }
           
        }

        /// <summary>
        /// 执行接收退款成功通知
        /// </summary>
        void cancel()
        {
            #region 参数
            var timestamp = PubFun.QueryString("timestamp");
            var sign = PubFun.QueryString("sign");
            var order_id = PubFun.QueryString("order_id");
            var type = PubFun.QueryString("type");
            var mobile = PubFun.QueryString("mobile");
            var encrypt_mobile = PubFun.QueryString("encrypt_mobile");
            var md5_mobile = PubFun.QueryString("md5_mobile");
            var num = PubFun.QueryString("num");
            //var left_num = PubFun.QueryString("left_num");
            var method = PubFun.QueryString("method");
            var taobao_sid = PubFun.QueryString("taobao_sid");
            var seller_nick = PubFun.QueryString("seller_nick");
            var item_title = PubFun.QueryString("item_title");
            var num_iid = PubFun.QueryString("num_iid");
            //退款取消码的总可用数
            var cancel_num = PubFun.QueryString("cancel_num");
            var outer_iid = PubFun.QueryString("outer_iid");
            var sub_outer_iid = PubFun.QueryString("sub_outer_iid");
            var sku_properties = PubFun.QueryString("sku_properties");
            var send_type = PubFun.QueryString("send_type");
            var consume_type = PubFun.QueryString("consume_type");
            //var sms_template = PubFun.QueryString("sms_template");
            var valid_start = PubFun.QueryString("valid_start");
            var valid_ends = PubFun.QueryString("valid_ends");
            var token = PubFun.QueryString("token");

            IDictionary<string, string> parameters = GetRequestPost();
         
            //日志
            StringBuilder sbLog = new StringBuilder();
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            sbLog.AppendLine("sign:" + sign);
            #endregion

            //验证签名
            if (!validSign(parameters,sign,sbLog))
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "valid_fail", sbLog.ToString());
                //Response.End();
                return;
            }

            Response.Write("{\"code\":200}");
            return;

            //获取订单生成的ID号
            var sendModel = ETicket.BLL.TaoBaoMSBLL.Instance.GetEntity(p => p.order_id == order_id && p.method == "send");
            if (sendModel == null)
            {
                sbLog.Append("根据order_id和method找相应的订单失败，无法执行退款业务");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "cancel_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            var sysOrderID = sendModel.OrderID.Value;

            EFEntity.OrderRefund refund = new EFEntity.OrderRefund();
            refund.OrderID = sysOrderID;
            refund.RMark = "系统提示：来自淘宝的退款。如审核不通过，要跟淘宝店那边沟通，因为淘宝已经退款给买家。";
            refund.RTime = DateTime.Now;
            refund.RUserID = sendModel.UserID;

            string msg = BLL.OrderSheetBLL.Instance.RefundRequest(sysOrderID, refund);
            if (msg == "")
            {
                sbLog.Append("申请退款成功！");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "calcel_succ", sbLog.ToString());
                Response.Write("{\"code\":200}");
                return;
            }
            else
            {
                
                sbLog.Append("申请退款失败！理由：" + msg);
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "calcel_fail", sbLog.ToString());

                /*
                //记录数据表，并不执行回调淘宝通知
                EFEntity.Taobao_MS alimsModel = new EFEntity.Taobao_MS();

                //本系统
                alimsModel.OrderID = sendModel.OrderID;//系统OrderID
                alimsModel.SheetID = sendModel.SheetID;
                alimsModel.UserID = sendModel.UserID;
                //淘宝数据
                alimsModel.timestamp = Convert.ToDateTime(timestamp);
                alimsModel.sign = sign;
                alimsModel.order_id = order_id;
                alimsModel.type = type;
                alimsModel.mobile = mobile;
                alimsModel.encrypt_mobile = encrypt_mobile;
                alimsModel.md5_mobile = md5_mobile;
                alimsModel.num = (num==""?0:int.Parse(num));
                alimsModel.method = method;
                alimsModel.taobao_sid = taobao_sid;
                alimsModel.seller_nick = seller_nick;
                alimsModel.item_title = item_title;
                alimsModel.send_type = send_type;
                alimsModel.consume_type = consume_type;
                //alimsModel.sms_template = sms_template;
                alimsModel.valid_start = valid_start;
                alimsModel.valid_ends = valid_ends;
                alimsModel.num_iid = num_iid;
                alimsModel.outer_iid = outer_iid;
                alimsModel.sub_outer_iid = sub_outer_iid;
                alimsModel.sku_properties = sku_properties;
                alimsModel.token = token;
                //alimsModel.total_fee = Convert.ToDecimal(total_fee);
                //alimsModel.weeks = weeks;
                alimsModel.codemerchant_id = codemerchant_id;

                //通知接口数据
                //alimsModel.NotifyMethod = ETicket.Utility.AlilMSNotifyEnum.resend.ToString();
                alimsModel.NotifyStatus = 1;
                alimsModel.NotifyNum = 0;

                ETicket.BLL.TaoBaoMSBLL.Instance.AddObject(alimsModel);

                */

                Response.Write("{\"code\":200}");
                //Response.End();
            }
        }

        /// <summary>
        /// 修改手机号码
        /// </summary>
        void modified()
        {
            #region 参数

            var timestamp = PubFun.QueryString("timestamp");
            var sign = PubFun.QueryString("sign");
            var order_id = PubFun.QueryString("order_id");
            var type = PubFun.QueryString("type");
            var mobile = PubFun.QueryString("mobile");
            var encrypt_mobile = PubFun.QueryString("encrypt_mobile");
            var md5_mobile = PubFun.QueryString("md5_mobile");
            var num = PubFun.QueryString("num");
            var left_num = PubFun.QueryString("left_num");
            var method = PubFun.QueryString("method");
            var taobao_sid = PubFun.QueryString("taobao_sid");
            var seller_nick = PubFun.QueryString("seller_nick");
            var item_title = PubFun.QueryString("item_title");
            var num_iid = PubFun.QueryString("num_iid");
            var outer_iid = PubFun.QueryString("outer_iid");
            var sub_outer_iid = PubFun.QueryString("sub_outer_iid");
            var sku_properties = PubFun.QueryString("sku_properties");
            var send_type = PubFun.QueryString("send_type");
            var consume_type = PubFun.QueryString("consume_type");
            var sms_template = PubFun.QueryString("sms_template");
            var valid_start = PubFun.QueryString("valid_start");
            var valid_ends = PubFun.QueryString("valid_ends");
            var token = PubFun.QueryString("token");

            IDictionary<string, string> parameters = GetRequestPost();
          
            //日志
            StringBuilder sbLog = new StringBuilder();
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            sbLog.AppendLine("sign:" + sign);
            #endregion

            //验证签名
            if (!validSign(parameters,sign, sbLog))
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "valid_fail", sbLog.ToString());
                //Response.End();
                return;
            }

            if (type != "0")
            {
                sbLog.Append("type不为0，不用重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "not_do", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }

            var sendModel = ETicket.BLL.TaoBaoMSBLL.Instance.GetEntity(p => p.order_id == order_id && p.method == "send");
            if (sendModel == null)
            {
                sbLog.Append("根据order_id和method找相应的订单失败，无法重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "resend_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            //更改手机号码
            var sysOrderID = sendModel.OrderID.Value;
            var sheet = ETicket.BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == sysOrderID);
            if (sheet == null)
            {
                sbLog.Append("根据sysOrderID，无法找到相应订单");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "mod_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            sheet.Memo = sheet.Memo + "|淘宝用户更改过手机号,旧号码" + sheet.Phone;
            sheet.Phone = mobile;
            ETicket.BLL.OrderSheetBLL.Instance.UpdateObject(sheet);
            //更改短信的手机号并重新发送
            var sms = BLL.SMSSendOrderBLL.Instance.GetEntity(p => p.OrderID == sysOrderID);
            if (sms == null)
            {
                sbLog.Append("根据系统的OrderID无法找到相关短信的记录，无法重新发码");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "resend_fail", sbLog.ToString());
                Response.Write("{\"code\":200}");
                //Response.End();
                return;
            }
            sms.Phone = mobile;
            sms.SendStatus = ETicket.Utility.SMSSendStatusEnum.发送中.ToString();
            sms.SendNum = sms.SendNum + 1;
            sms.SendIsRead = false;
            BLL.SMSSendOrderBLL.Instance.UpdateObject(sms);

            #region 生成回调通知
            //记录到淘宝通知回调
            EFEntity.Taobao_MS alimsModel = new EFEntity.Taobao_MS();

            //本系统
            alimsModel.OrderID = sendModel.OrderID;//系统OrderID
            alimsModel.SheetID = sendModel.SheetID;
            alimsModel.UserID = sendModel.UserID;
            //淘宝数据
            alimsModel.timestamp = Convert.ToDateTime(timestamp);
            alimsModel.sign = sign;
            alimsModel.order_id = order_id;
            alimsModel.type = type;
            alimsModel.mobile = mobile;
            alimsModel.encrypt_mobile = encrypt_mobile;
            alimsModel.md5_mobile = md5_mobile;
            alimsModel.num = int.Parse(num);
            alimsModel.method = method;
            alimsModel.taobao_sid = taobao_sid;
            alimsModel.seller_nick = seller_nick;
            alimsModel.item_title = item_title;
            alimsModel.send_type = send_type;
            alimsModel.consume_type = consume_type;
            alimsModel.sms_template = sms_template;
            alimsModel.valid_start = valid_start;
            alimsModel.valid_ends = valid_ends;
            alimsModel.num_iid = num_iid;
            alimsModel.outer_iid = outer_iid;
            alimsModel.sub_outer_iid = sub_outer_iid;
            alimsModel.sku_properties = sku_properties;
            alimsModel.token = token;
            //alimsModel.total_fee = Convert.ToDecimal(total_fee);
            //alimsModel.weeks = weeks;
            alimsModel.codemerchant_id = codemerchant_id;

            //通知接口数据
            alimsModel.NotifyMethod = ETicket.Utility.AlilMSNotifyEnum.resend.ToString();
            alimsModel.NotifyStatus = 0;
            alimsModel.NotifyNum = 0;

            try
            {
                ETicket.BLL.TaoBaoMSBLL.Instance.AddObject(alimsModel);
                sbLog.AppendLine("写到淘宝码商通知表成功。");
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms","succ", sbLog.ToString());
                //成功接收到发码通知后商家接口须返回响应内容：{"code":200}
                Response.Write("{\"code\":200}");
            }
            catch (Exception ex)
            {
                sbLog.AppendLine("写到淘宝码商通知表发生错误。");
                sbLog.AppendLine(ex.ToString());
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "ex-ipm",sbLog.ToString());
            }
            #endregion

        }

        /// <summary>
        //以下情况下淘宝会给合作方系统发出该通知：
        //1.	淘宝上订单的使用有效期修改
        //2.	维权成功
        //3.	后续可能会增加其他情况下的通知
        /// </summary>
        void order_modify()
        {
            var timestamp = PubFun.QueryString("timestamp");
            var sign = PubFun.QueryString("sign");
            var order_id = PubFun.QueryString("order_id");
            var taobao_sid = PubFun.QueryString("taobao_sid");
            var seller_nick = PubFun.QueryString("seller_nick");
            var method = PubFun.QueryString("method");
            //通知类型:
            //1.	使用有效期修改通知
            //2.	维权成功通知
            var sub_method = PubFun.QueryString("sub_method");
            //
            var data = PubFun.QueryString("data");

            IDictionary<string, string> parameters = GetRequestPost();
          
            //日志
            StringBuilder sbLog = new StringBuilder();
            sbLog.AppendLine("进入修改订单处理");
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            sbLog.AppendLine("sign:" + sign);

            //验证签名
            if (!validSign(parameters,sign, sbLog))
            {
                NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "valid_fail", sbLog.ToString());
                //Response.End();
                return;
            }
            sbLog.AppendLine("修改订单成功。");
            NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "order_modify", sbLog.ToString());

            //订单修改
            Response.Write("{\"code\":200}");
        }

        void heartBeatCheck()
        {
            IDictionary<string, string> parameters = GetRequestPost();
            //日志
            StringBuilder sbLog = new StringBuilder();
            foreach (var dem in parameters)
            {
                string key = dem.Key;
                string value = dem.Value;
                sbLog.AppendLine(key + ":" + value);
            }
            NJiaSu.Libraries.LogHelper.LogResult("api_ali_ms", "heartBeatCheck", sbLog.ToString());

            Response.Write("{\"code\":\"isv.success-all\",\"msg\":\"成功\"}");
        }

        #region 验证签名
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="sign"></param>
        /// <param name="sbLog"></param>
        /// <returns></returns>
        bool validSign(IDictionary<string, string> parameters,string sign,StringBuilder sbLog)
        {
            var clientSign = SignTopRequest(parameters, api_ali_ms.APIHelper.secret,sbLog);
            if (clientSign.ToLower() != sign.ToLower())
            {
                sbLog.AppendLine("验证签名失败。sign：" + sign + "，cSign：" + clientSign);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取所有参数，不包括sign
        /// </summary>
        public  IDictionary<string, string>  GetRequestPost()
        {
            int i = 0;
            IDictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                if (requestItem[i]!="sign")
                    sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        public static string SignTopRequest(IDictionary<string, string> parameters, string secret, StringBuilder sbLog)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();
            query.Append(secret);
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }

            //第三步：使用MD5/HMAC加密
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.GetEncoding("GBK").GetBytes(query.ToString()));
            
            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }

        #endregion
    }
}