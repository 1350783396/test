using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    public partial class get_order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JsonResult<Order> jsonModel = new JsonResult<Order>();
            string jsonStr = "";

            //查询参数
            string ordertype = PubFun.QueryString("ordertype");
            string action = PubFun.QueryString("action");
            string token = PubFun.QueryString("token");
          

            EFEntity.User user = new EFEntity.User();
            if (token != "")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);

            if (user == null || user.UserID <= 0)
            {
                jsonModel.Status = "0";
                jsonModel.Msg = "请先登录系统后再提交";
                jsonStr = "";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                return;
            }

            if (action == "list")
            {
                #region 列表
                int pageIndex = PubFun.QueryInt("pageindex");
                int pageSize = PubFun.QueryInt("pagesize");
                if (pageIndex <= 0)
                    pageIndex = 1;
                if (pageSize <= 0)
                    pageSize = ApiHelper.DefaultPageSize;

                //取数条件
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" it.UserID={0}", user.UserID);
                if (ordertype == "") 
                {
                    sb.Append("and it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
                }
                else if (ordertype == "back")
                {
                    sb.Append("and (it.OrderStatus ='退款审核中' or it.OrderStatus ='退款审核通过' or it.OrderStatus ='已退款' or it.OrderStatus ='退款审核不通过')");
                }
                else
                {
                    sb.Append("and it.OrderStatus!='退款审核中' and it.OrderStatus!='退款审核通过' and it.OrderStatus!='已退款' and it.OrderStatus!='退款审核不通过'");
                }

                ETicket.Utility.PageInfo<EFEntity.OrderSheet> pi = null;
                pi = BLL.OrderSheetBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), " it.OrderTime DESC");

                List<Order> resultList = new List<Order>();
                //转换为接口Model
                foreach (var orderItem in pi.List)
                {
                    var resultItem = ApiHelper.OrderSheet2Result(orderItem, user,false);
                    resultList.Add(resultItem);
                }

                //生成json
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "1";
                jsonModel.RecordCount = pi.RecordCount;
                jsonModel.PageCount = pi.PageCount;
                jsonModel.PageIndex = pageIndex;
                jsonModel.PageSize = pageSize;
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                jsonStr = "";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
            else if (action == "item")
            {
                #region 获取某一条订单详细

                int orderID = PubFun.QueryInt("orderid");
                var orderModel = BLL.OrderSheetBLL.Instance.GetEntity(p => p.OrderID == orderID&&p.UserID==user.UserID);
                if (orderModel == null)
                {
                    jsonModel = new JsonResult<Order>();
                    //生成json
                    jsonModel.Status = "0";
                    jsonModel.Msg = "该订单不存在，或已被删除";

                    jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                    Response.Write(jsonStr);
                }
               
                var resultItem = ApiHelper.OrderSheet2Result(orderModel, user,true);
                List<Order> resultList = new List<Order>();
                resultList.Add(resultItem);

                //生成json
                jsonModel = new JsonResult<Order>();
                jsonModel.Status = "1";
                jsonModel.Msg = user.Account == null ? "0" : user.Account.Value.ToString();
                jsonModel.List = resultList;

                jsonStr = "";
                jsonStr = JsonHelper.GetJson<JsonResult<Order>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
        }
    }
}