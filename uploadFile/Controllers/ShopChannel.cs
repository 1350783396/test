using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace uploadFile.Controllers
{
    public class ShopChannel
    {
        public int id { get; set; }
        //上传人
        public Guid userid { get; set; }
        public long shopid { get; set; }

        public DateTime thedate { get; set; }
        [Description("流量来源")]
        public string channel { get; set; }
        [Description("来源明细")]
        public string channelSon { get; set; }
        [Description("访客数")]
        public int uv { get; set; }
        [Description("下单金额")]
        public decimal orderMoney { get; set; }
        [Description("下单买家数")]
        public int orderBuyer { get; set; }
        [Description("下单转化率")]
        public string orderConvertRate { get; set; }
        [Description("支付金额")]
        public decimal payMoney { get; set; }
        [Description("支付买家数")]
        public int payBuyer { get; set; }
        [Description("支付转化率")]
        public string payConvertRate { get; set; }
        [Description("客单价")]
        public decimal payPrice { get; set; }
        [Description("UV价值")]
        public string uvValue { get; set; }
        [Description("关注店铺买家数")]
        public int concernShop { get; set; }
        [Description("收藏商品买家数")]
        public int collectArticle { get; set; }
        [Description("加购人数")]
        public int collectPerson { get; set; }
        [Description("新访客")]
        public int newUv { get; set; }
    }
}
