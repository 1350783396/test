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
    public partial class get_news : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JsonResult<News> jsonModel = new JsonResult<News>();
            string jsonStr = "";

            int moduleID = PubFun.QueryInt("moduleid");
            if (moduleID <= 0)
                moduleID = 10;

            string action = PubFun.QueryString("action");
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
                sb.AppendFormat(" it.ModuleID={0}", moduleID);

                ETicket.Utility.PageInfo<EFEntity.ArtContent> pi = null;
                pi = BLL.ArtContentBLL.Instance.GetPageList(pageIndex, pageSize, sb.ToString(), "it.ArtID DESC");

                List<News> resultList = new List<News>();
                //转换为接口Model
                foreach (var newsItem in pi.List)
                {
                    var resultItem = ApiHelper.News2Result(newsItem, false);
                    resultList.Add(resultItem);
                }

                //生成json
                jsonModel.Status = "1";
                jsonModel.RecordCount = pi.RecordCount;
                jsonModel.PageCount = pi.PageCount;
                jsonModel.PageIndex = pageIndex;
                jsonModel.PageSize = pageSize;
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                jsonStr = JsonHelper.GetJson<JsonResult<News>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
            else if (action == "item")
            {
                
                #region 获取详细产品
                int artID = PubFun.QueryInt("artid");
                var newItem = BLL.ArtContentBLL.Instance.GetEntity(p => p.ArtID == artID);
                if (newItem == null)
                {
                    //生成json
                    jsonModel.Status = "0";
                    jsonModel.Msg = "该数据不存在";

                    jsonStr = JsonHelper.GetJson<JsonResult<News>>(jsonModel);
                    Response.Write(jsonStr);
                }

                var resultItem = ApiHelper.News2Result(newItem, true);
                List<News> resultList = new List<News>();
                resultList.Add(resultItem);

                //生成json

                jsonModel.Status = "1";
                jsonModel.Msg = "";
                jsonModel.List = resultList;

                jsonStr = JsonHelper.GetJson<JsonResult<News>>(jsonModel);
                Response.Write(jsonStr);
                Response.End();
                #endregion
            }
        }


    }
}