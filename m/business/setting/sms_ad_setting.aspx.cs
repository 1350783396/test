using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NJiaSu.Libraries; 

namespace ETicket.Web.business.setting
{
    public partial class sms_ad_setting : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Click += btnSave_Click;
            this.btnCancel.Click += btnCancel_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.repList.ItemDataBound += repList_ItemDataBound;
            this.repList.ItemCommand += repList_ItemCommand;

            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }


        void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int pkid = int.Parse(e.CommandArgument.ToString());
                EFEntity.SMS_AD ad = BLL.SMSADBLL.Instance.GetEntity(p => p.PKID == pkid);

                lblPKID.Text = ad.PKID.ToString();
                txtStart.Value = ad.StartTime.Value.ToString("yyyy-MM-dd");
                txtEnd.Value = ad.StartTime.Value.ToString("yyyy-MM-dd");
                txtAD.Value = ad.ADContent.ToString();

                this.btnSave.Text = "更新";
                btnCancel.Visible = true;

            }
        }

        void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                CheckBox checkALL = e.Item.FindControl("chkAll") as CheckBox;
                checkALL.Attributes.Add("onclick", string.Format("javascript:FormSelectAll('{0}','chkItem',this);", this.form1.ClientID));
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ad = e.Item.DataItem as EFEntity.SMS_AD;
                LinkButton lbtnEdit = e.Item.FindControl("lbtnEdit") as LinkButton;
                lbtnEdit.CommandArgument = ad.PKID.ToString();
                lbtnEdit.CommandName = "Edit";
            }
        }

        //保存
        void btnSave_Click(object sender, EventArgs e)
        {
            
            //主键
            int PKID = 0;
            if (lblPKID.Text != "")
                PKID = int.Parse(lblPKID.Text);

            EFEntity.SMS_AD ad = null;

            string startTime = txtStart.Value.Trim();
            string endTime = txtEnd.Value.Trim();
            string adContent = txtAD.Value.Trim();

            #region 验证
            if (startTime == "")
            {
                PubFun.ShowMsg(this, "请选择开始日期");
                return;
            }
            if (endTime == "")
            {
                PubFun.ShowMsg(this, "请选选择结束日期");
                return;
            }
            if (adContent == "")
            {
                PubFun.ShowMsg(this, "请填写广告内容");
                return;
            }
            try
            {
                Convert.ToDateTime(startTime + " 00:00:00");
               
            }
            catch
            {
                PubFun.ShowMsg(this, "开始日期格式填写不正确");
                return;
            }
            try
            {
                Convert.ToDateTime(endTime + " 00:00:00");
            }
            catch
            {
                PubFun.ShowMsg(this, "结束日期格式填写不正确");
                return;
            }
            
            #endregion

            bool isAdd = true;
            if (PKID > 0)
            {
                isAdd = false;
                ad = BLL.SMSADBLL.Instance.GetEntity(p => p.PKID == PKID);
            }
            else
            {
                ad = new EFEntity.SMS_AD();
            }


            ad.StartTime = Convert.ToDateTime(startTime + " 00:00:00");
            ad.EndTime = Convert.ToDateTime(endTime + " 00:00:00");
            ad.ADContent = adContent;
            ad.UserID = BLL.UserBLL.Instance.GetLoginModel().UserID;
         
        

            if (isAdd)
            {
                int count= BLL.SMSADBLL.Instance.GetCount();
                if (count>=50)
                {
                    PubFun.ShowMsg(this, "为了不影响发送短信的性能，最多设置50条配置。请先删除在添加");
                    return;
                }
                ad.AddTime = DateTime.Now;
                BLL.SMSADBLL.Instance.AddObject(ad);
                ClearInput();
            }
            else
            {
                BLL.SMSADBLL.Instance.UpdateObject(ad);
                ClearInput();
            }


            LoadData();
        }
        //取消编辑
        void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            bool delete = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chkItem = this.repList.Items[i].FindControl("chkItem") as CheckBox;
                if (chkItem != null && chkItem.Checked == true)
                {
                    Label lblPKID = this.repList.Items[i].FindControl("lblPKID") as Label;
                    if (lblPKID != null)
                    {
                        int id = int.Parse(lblPKID.Text);
                        try
                        {
                            EFEntity.SMS_AD ad = new EFEntity.SMS_AD();
                            ad.PKID = id;
                            BLL.SMSADBLL.Instance.DeleteObject(ad);

                            delete = true;
                        }
                        catch
                        {

                        }
                    }
                }
            }

            if (delete)
            {
                LoadData();
            }
        }
        //清除输入
        void ClearInput()
        {
            lblPKID.Text = "";
            txtStart.Value = "";
            txtEnd.Value = "";
            txtAD.Value = "";

            this.btnSave.Text = "新增";
            btnCancel.Visible = false;
        }

        void LoadData()
        {
            IEnumerable<EFEntity.SMS_AD> adList =BLL.SMSADBLL.Instance.GetEntities().OrderByDescending(p=>p.PKID);
            this.repList.DataSource = adList;
            this.repList.DataBind();

            this.litCount.Text = string.Format("共{0}条记录", adList.Count<EFEntity.SMS_AD>());
        }
    }
}