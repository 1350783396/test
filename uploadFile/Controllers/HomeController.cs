﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using uploadFile.Models;

namespace uploadFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult testView()
        {
            return View();
        }

        public IActionResult UploadFiles(IFormFile file)
        {

            if (file.Length > 0)
            {
                DataTable dt = new DataTable();
                string strMsg;
                //利用IFormFile里面的OpenReadStream()方法直接读取文件流
                dt = ExcelToDatatable(file.OpenReadStream(), Path.GetExtension(file.FileName), out strMsg);

                if (dt.Rows.Count > 0)
                {
                    //取列名
                    List<string> cnameList = new List<string>();
                    foreach (DataColumn dcn in dt.Columns)
                    {
                        cnameList.Add(dcn.ColumnName);

                    }
                    //根据列名来读取行数据
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (string cname in cnameList)
                        {
                            int? args = cnameList.Contains("访客数") ? row["访客数"].ToString()==""?1 : 0:0;
                        }

                    }
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    foreach (DataColumn column in dt.Columns)
                    //    {
                    //        var asd = row[column];
                    //    }
                    //}

                }

                return Json("asd");
            }
            return Json("asd");
            //以下多文件上传
            //var date = Request;
            //var files = Request.Form.Files;
            //foreach (var item in files)
            //{
            //    if (item.Length > 0)
            //    {
            //        DataTable dt = new DataTable();
            //        string strMsg;
            //        //利用IFormFile里面的OpenReadStream()方法直接读取文件流
            //        dt = ExcelToDatatable(item.OpenReadStream(), Path.GetExtension(item.FileName), out strMsg);

            //        if (dt.Rows.Count > 0)
            //        {
            //        }
            //        return Json("asd");
            //    }
            //}

        }


        public static DataTable ExcelToDatatable(Stream stream, string fileType, out string strMsg, string sheetName = null)
        {
            strMsg = "";
            DataTable dt = new DataTable();
            ISheet sheet = null;
            IWorkbook workbook = null;
            try
            {
                #region 判断excel版本
                //2007以上版本excel
                if (fileType == ".xlsx")
                {
                    workbook = new XSSFWorkbook(stream);
                }
                //2007以下版本excel
                else if (fileType == ".xls")
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    throw new Exception("传入的不是Excel文件！");
                }
                #endregion
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum;
                    for (int i = firstRow.FirstCellNum; i < cellCount; i++)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue.Trim();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                DataColumn dataColumn = new DataColumn(cellValue);
                                dt.Columns.Add(dataColumn);
                            }
                        }
                    }
                    DataRow dataRow = null;
                    //遍历行
                    for (int j = sheet.FirstRowNum + 1; j <= sheet.LastRowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        dataRow = dt.NewRow();
                        if (row == null || row.FirstCellNum < 0)
                        {
                            continue;
                        }
                        //遍历列
                        for (int i = row.FirstCellNum; i < cellCount; i++)
                        {
                            ICell cellData = row.GetCell(i);
                            if (cellData != null)
                            {
                                //判断是否为数字型，必须加这个判断不然下面的日期判断会异常
                                if (cellData.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cellData))
                                    {
                                        dataRow[i] = cellData.DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[i] = cellData.ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[i] = cellData.ToString().Trim();
                                }
                            }
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
                else
                {
                    throw new Exception("没有获取到Excel中的数据表！");
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
            }
            return dt;
        }

        [HttpPost]　　　　//上传文件是 post 方式，这里加不加都可以
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);       //统计所有文件的大小

            var filepath = Directory.GetCurrentDirectory() + "\\file";  //存储文件的路径
            ViewBag.log = "日志内容为：";     //记录日志内容

            foreach (var item in files)     //上传选定的文件列表
            {
                if (item.Length > 0)        //文件大小 0 才上传
                {
                    var thispath = filepath + "\\" + item.FileName;     //当前上传文件应存放的位置

                    if (System.IO.File.Exists(thispath) == true)        //如果文件已经存在,跳过此文件的上传
                    {
                        ViewBag.log += "\r\n文件已存在：" + thispath.ToString();
                        continue;
                    }

                    //上传文件
                    using (var stream = new FileStream(thispath, FileMode.Create))      //创建特定名称的文件流
                    {
                        try
                        {
                            await item.CopyToAsync(stream);     //上传文件
                        }
                        catch (Exception ex)        //上传异常处理
                        {
                            ViewBag.log += "\r\n" + ex.ToString();
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult GetJson()
        {
            
            List<Dictionary<string, int>> keyValues = new List<Dictionary<string, int>>();

            Dictionary<string, int> keyValuePairs1 = new Dictionary<string, int>() { { "asd", 1 }, { "ads", 2 } };
            keyValues.Add(keyValuePairs1);
            Dictionary<string, int> keyValuePairs2 = new Dictionary<string, int>() { { "a1sd", 1 }, { "ad1s", 2 } };
            keyValues.Add(keyValuePairs2);
            // var json1 = JsonConvert.SerializeObject(keyValuePairs1);
            return Json(new { data = keyValues, code = 0 });
        }

    }
}
