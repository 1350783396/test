using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using NJiaSu.Libraries;

namespace ETicket.Web.business.product
{
    public partial class line_price : AdminBase
    {
        #region 模板
        const string trTempl = @"<tr>
                                    <td class='label'>
                                        <s>*</s>{level}：
                                    </td>
                                    <td>
                                        <input type='text' id='txtCost_{id}' name='txtCost_{id}' value='{value}'/>元
                                        &nbsp;&nbsp;
                                        <label class='error' for='txtCost_{id}'></label>
                                    </td>
                                     <td class='label'>
                                        <s>*</s>返积分：
                                    </td>
                                    <td>
                                        <input type='text' id='txtRebate_{id}' name='txtRebate_{id}' value='{rebate}'/>
                                         &nbsp;&nbsp;
                                        <label class='error' for='txtRebate_{id}'></label>
                                    </td>
                                    <td class='label'>
                                        <s>*</s>最低分销价：
                                    </td>
                                    <td>
                                        <input type='text' id='txtFXMini_{id}' name='txtFXMini_{id}' value='{FxPriceMini}'/>
                                         &nbsp;&nbsp;
                                        <label class='error' for='txtFXMini_{id}'></label>
                                    </td>
                                    <td class='label'>
                                        <s>*</s>建议分销价：
                                    </td>
                                    <td>
                                        <input type='text' id='txtFXSuggest_{id}' name='txtFXSuggest_{id}' value='{FxPriceSuggest}'/>
                                         &nbsp;&nbsp;
                                        <label class='error' for='txtFXSuggest_{id}'></label>
                                    </td>
                                </tr>";

        const string ruleTempl = @" txtCost_{id}: {required: true,isPrice: true }, txtRebate_{id}:{required: true},txtFXMini_{id}: {required: true,isPrice: true },txtFXSuggest_{id}: {required: true,isPrice: true }";
        const string msgTempl = "txtCost_{id}:{required: \"请输入{level}的价格\"}, txtRebate_{id}:{required: \"请输入{level}的返积分\"},txtFXMini_{id}:{required: \"请输入{level}的最低分销价格\"},txtFXSuggest_{id}:{required: \"请输入{level}建议分销价格\"}";
        #endregion

        public string DataHtml = "";
        public string ruleResult = "";
        public string msgResult = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int productID = PubFun.QueryInt("productid");
            IEnumerable<EFEntity.UserLevel> userLevelList = BLL.UserLevelBLL.Instance.GetEntities(p => p.UserCategory == "user" | p.UserCategory == "partner").OrderBy(p => p.OrderValue);

            if (!Page.IsPostBack)
            {
                InitNav(productID);
            }

            #region POST保存
            if (Request.RequestType=="POST")
            {
                Dictionary<int, decimal[]> priceList = new Dictionary<int, decimal[]>();
                foreach (var userLevel in userLevelList)
                {
                    int id = userLevel.UserLevelID;
                    string formKey = "txtCost_" + id;
                    string rebateKey = "txtRebate_" + id;
                    string fxMiniKey = "txtFXMini_" + id;
                    string fxSuggKey = "txtFXSuggest_" + id;

                    decimal value = PubFun.QueryDecimal(formKey);
                    decimal rebateValue = PubFun.QueryDecimal(rebateKey);
                    decimal fxMiniValue = PubFun.QueryDecimal(fxMiniKey);
                    decimal fxSuggValue = PubFun.QueryDecimal(fxSuggKey);
                    if (value <= 0)
                    {
                        PubFun.ShowMsgRedirect(this, userLevel.UserLevelName + "的价格应大于0",this.Request.RawUrl);
                        return;
                    }
                    if (fxMiniValue < value)
                    {
                        PubFun.ShowMsgRedirect(this, userLevel.UserLevelName + "的【分销最低价格】大于其购买的【价格】", this.Request.RawUrl);
                        return;
                    }
                    if (fxSuggValue <fxMiniValue)
                    {
                        PubFun.ShowMsgRedirect(this, userLevel.UserLevelName + "的【建议分销价格】应大于【最低分销价格】", this.Request.RawUrl);
                        return;
                    }

                    decimal[] ppp = { value, rebateValue, fxMiniValue, fxSuggValue };
                    priceList.Add(id, ppp);
                }

                foreach (var kv in priceList)
                {
                    ETicket.EFEntity.ProductSUK suk = null;
                    suk=ETicket.BLL.ProductSUKBLL.Instance.GetEntity(p => p.UserLevelID == kv.Key & p.ProductID == productID);
                    bool isAdd = false;
                    if (suk == null) {
                        isAdd = true;
                        suk = new EFEntity.ProductSUK();
                    }
                  
                    suk.ProductID = productID;
                    suk.UserLevelID = kv.Key;

                    decimal[] ppp= kv.Value;
                    suk.ProductPrice = ppp[0];
                    suk.Rebate = ppp[1];
                    suk.FXPrice_Mini = ppp[2];
                    suk.FXPrice_Recommend = ppp[3];

                    if (isAdd)
                        BLL.ProductSUKBLL.Instance.AddObject(suk);
                    else
                        BLL.ProductSUKBLL.Instance.UpdateObject(suk);
                }

                PubFun.ShowMsgRedirect(this, "保存成功", Request.RawUrl);
            }
            #endregion

            #region 生成动态表单
            StringBuilder sbHtml = new StringBuilder();
            string sbRule = "";
            string sbMsg = "";

            foreach (var userLevel in userLevelList)
            {
                //动态html
                string value = "";
                decimal rebateValue = 0, FxPriceMini=0, FxPriceSuggest=0;
                if (productID > 0)
                {
                    ETicket.EFEntity.ProductSUK suk = ETicket.BLL.ProductSUKBLL.Instance.GetEntity(p => p.UserLevelID == userLevel.UserLevelID & p.ProductID == productID);
                    if (suk != null)
                    {
                        value = suk.ProductPrice.Value.ToString();
                        rebateValue=(suk.Rebate == null) ? 0 : suk.Rebate.Value;
                        FxPriceMini = (suk.FXPrice_Mini == null) ? 0 : suk.FXPrice_Mini.Value;
                        FxPriceSuggest = (suk.FXPrice_Recommend == null) ? 0 : suk.FXPrice_Recommend.Value;
                    }
                }

                string item = "";
                item = trTempl;
                item = item.Replace("{level}", userLevel.UserLevelName);
                item = item.Replace("{id}", userLevel.UserLevelID.ToString());
                item = item.Replace("{value}", value);
                item = item.Replace("{rebate}", rebateValue.ToString());
                item = item.Replace("{FxPriceMini}", FxPriceMini.ToString());
                item = item.Replace("{FxPriceSuggest}", FxPriceSuggest.ToString());
                sbHtml.Append(item);

                //验证规则
                string itemRule = "";
                itemRule = ruleTempl;
                itemRule = itemRule.Replace("{id}", userLevel.UserLevelID.ToString());
                if (sbRule == "")
                {
                    sbRule = itemRule;
                }
                else
                {
                    sbRule = sbRule + "," + itemRule;
                }

                //验证提示
                string itemMsg = "";
                itemMsg = msgTempl;
                itemMsg = itemMsg.Replace("{level}", userLevel.UserLevelName);
                itemMsg = itemMsg.Replace("{id}", userLevel.UserLevelID.ToString());
                if (sbMsg == "")
                {
                    sbMsg = itemMsg;
                }
                else
                {
                    sbMsg = sbMsg + "," + itemMsg;
                }
            }

            DataHtml = sbHtml.ToString();
            ruleResult = sbRule;
            msgResult = sbMsg;
            #endregion
        }

        void InitNav(int productID)
        {
            hyProduct.NavigateUrl = "line_add.aspx?productid=" + productID;
            //hyPrice.NavigateUrl = "#";
            hyTickTime.NavigateUrl = "line_ticktime.aspx?productid=" + productID;
            hyStock.NavigateUrl = "line_stock.aspx?productid=" + productID;
            hyAddress.NavigateUrl = "line_address.aspx?productid=" + productID;
            hyPublish.NavigateUrl = "line_publish.aspx?productid=" + productID;

            divProduct.Attributes.Add("class", "tab01");
            divPrice.Attributes.Add("class", "tab02");
            divTickTime.Attributes.Add("class", "tab01");
            divStock.Attributes.Add("class", "tab01");
            divAddress.Attributes.Add("class", "tab01");
            divPublish.Attributes.Add("class", "tab01");
        }
    }
}