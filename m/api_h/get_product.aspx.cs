using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using NJiaSu.Libraries;

namespace ETicket.Web.api_h
{
    /// <summary>
    /// 获取产品列表
    /// </summary>
    public partial class get_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //查询参数
            string categoryID = PubFun.QueryString("categoryid");
            string action = PubFun.QueryString("action");
            string token = PubFun.QueryString("token");
            EFEntity.User user =new EFEntity.User();
            if (token!="")
                user = BLL.UserBLL.Instance.GetUserForCookie(token);

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
                sb.AppendFormat(" it.SaleFlag={0}", true);
                sb.AppendFormat(" and it.CategoryID='{0}'", categoryID);

                ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
                pi = BLL.ProductBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), "it.ProductID DESC");

                List<Product> resultList = new List<Product>();
                //转换为接口Model
                foreach (var proItem in pi.List)
                {
                    var resultItem = ApiHelper.Product2Result(proItem, user, false);
                    resultList.Add(resultItem);
                }

                //生成json
                JsonResult<Product> jsonModel = new JsonResult<Product>();
                jsonModel.Status = "1";
                jsonModel.RecordCount = pi.RecordCount;
                jsonModel.PageCount = pi.PageCount;
                jsonModel.PageIndex = pageIndex;
                jsonModel.PageSize = pageSize;
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                string jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
            else if(action=="item")
            {
                #region 获取详细产品
                JsonResult<Product> jsonModel = new JsonResult<Product>();
                string jsonStr="";

                int productID = PubFun.QueryInt("productid");
                var pro= BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
                if(pro==null)
                {
                    //生成json
                    jsonModel.Status = "0";
                    jsonModel.Msg = "该产品不存在";

                    jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                    Response.Write(jsonStr);
                }

                var resultItem = ApiHelper.Product2Result(pro, user, true);
                List<Product> resultList = new List<Product>();
                resultList.Add(resultItem);

                //生成json
                
                jsonModel.Status = "1";
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
            if (action == "list_fx")
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
                sb.AppendFormat(" it.SaleFlag={0}", true);
                sb.AppendFormat(" and it.CategoryID='{0}'", categoryID);

                ETicket.Utility.PageInfo<EFEntity.Product> pi = null;
                pi = BLL.ProductBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), "it.ProductID DESC");

                List<Product> resultList = new List<Product>();
                //转换为接口Model
                foreach (var proItem in pi.List)
                {
                    var resultItem = ApiHelper.Product2Result(proItem, user, false,true);
                    resultList.Add(resultItem);
                }

                //生成json
                JsonResult<Product> jsonModel = new JsonResult<Product>();
                jsonModel.Status = "1";
                jsonModel.RecordCount = pi.RecordCount;
                jsonModel.PageCount = pi.PageCount;
                jsonModel.PageIndex = pageIndex;
                jsonModel.PageSize = pageSize;
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                string jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
            else if (action == "item_fx")
            {
                #region 获取详细产品
                JsonResult<Product> jsonModel = new JsonResult<Product>();
                string jsonStr = "";

                int productID = PubFun.QueryInt("productid");
                var pro = BLL.ProductBLL.Instance.GetEntity(p => p.ProductID == productID);
                if (pro == null)
                {
                    //生成json
                    jsonModel.Status = "0";
                    jsonModel.Msg = "该产品不存在";

                    jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                    Response.Write(jsonStr);
                }

                var resultItem = ApiHelper.Product2Result(pro, user, true,true);

                //var webHost="http://192.168.2.105";
                var webHost = PubFun.ServerHost();
                var imgPath = HtmlController.Instance.CreateFXQRCode(webHost, user.UserID.ToString(), pro.ProductID.ToString());
                resultItem.TitleImg_B=webHost+imgPath;

                List<Product> resultList = new List<Product>();
                resultList.Add(resultItem);

                //生成json

                jsonModel.Status = "1";
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                jsonStr = JsonHelper.GetJson<JsonResult<Product>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
        }
    }
}