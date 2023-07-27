using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicket.Web.api_h
{
    public class JsonResult<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get;
            set;
        }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Msg
        {
            get;
            set;
        }

        private int _pageSize = 25;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        private int _pageIndex = 1;
        /// <summary>
        /// 页号
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

       
        /// <summary>
        /// 分页结果
        /// </summary>
        public IList<T> List { get; set; }

    }
}