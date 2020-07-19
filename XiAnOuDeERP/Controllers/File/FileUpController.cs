using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.File
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
   // [AppAuthentication]
    public class FileUpController : ApiController
    {
        /// <summary>
        /// 结构文件上传
        /// </summary>
        /// <returns></returns>
        public dynamic ConstructionFileUp()
        {
            //获取主机ip
            //   System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            var host = string.Empty;
            foreach (System.Net.IPAddress ip in ips.AddressList)
            {
                host = ip.ToString();
            }

            var data1 = HttpContext.Current.Request.Files["file"];
            var Sign = HttpContext.Current.Request.Headers["sign"];
            #region 创建文件夹
            string path4 = System.AppDomain.CurrentDomain.BaseDirectory;

            var path = path4 /*"C:\\Users\\冬天捡到的徒弟xin\\source\\repos\\FileCaozuo\\FileData\\"*/;
            var dateFolder = DateTime.Now.ToString("yyyyMMdd");
            var direcPath = Path.Combine(path, dateFolder);

            var dataFil = path4 + dateFolder;         /*"C:\\Users\\冬天捡到的徒弟xin\\source\\repos\\FileCaozuo\\FileData\\" + dateFolder + "";*/
            var dataF = dateFolder + "分子";
            var direcPatht = Path.Combine(dataFil, dataF);

            var dataFils = path4 + dateFolder;     /*"C:\\Users\\冬天捡到的徒弟xin\\source\\repos\\FileCaozuo\\FileData\\" + dateFolder + "";*/
            var dataFs = dateFolder + "结构";
            var direcPathts = Path.Combine(dataFils, dataFs);

            var dataFilst = path4 + dateFolder;
            var dataFst = dateFolder + "图谱";
            var direcPathtst = Path.Combine(dataFilst, dataFst);


            var dataFilstk = path4 + dateFolder;
            var dataFstk = dateFolder + "合同";
            var direcPathtstk = Path.Combine(dataFilstk, dataFstk);
            if (!Directory.Exists(direcPath))
            {
                Directory.CreateDirectory(direcPath);
                if (!Directory.Exists(direcPatht))
                {
                    Directory.CreateDirectory(direcPatht);
                }
                if (!Directory.Exists(direcPathts))
                {
                    Directory.CreateDirectory(direcPathts);
                }
                if (!Directory.Exists(direcPathtst))
                {
                    Directory.CreateDirectory(direcPathtst);
                }
                if (!Directory.Exists(direcPathtstk))
                {
                    Directory.CreateDirectory(direcPathtstk);
                }
            }
            #endregion
            #region 保存文件
            var arr = data1.FileName.Split('.');
            var ext = arr.LastOrDefault();//获取后缀
                                          // var dateFolders = DateTime.Now.ToString("yyyyMMdd");
            var fileName = Guid.NewGuid() + "." + ext;//新的文件名
            if (int.Parse(Sign) == 1)//分子
            {
                var savePath = Path.Combine(direcPatht, fileName);
                data1.SaveAs(savePath);
                var url = "/" + dateFolder + "/" + dataF + "/" + fileName;
                return new
                {
                    PreFileName = data1.FileName,
                    FileName = dateFolder + "/" + fileName,//文件夹下的文件
                    Url = url
                };
            }
            if (int.Parse(Sign) == 2)//结构
            {
                var savePath = Path.Combine(direcPathts, fileName);
                data1.SaveAs(savePath);

                var url = "/" + dateFolder + "/" + dataFs + "/" + fileName;
                return new
                {
                    PreFileName = data1.FileName,
                    FileName = dateFolder + "/" + fileName,//文件夹下的文件
                    Url = url
                };
            }
            if (int.Parse(Sign) == 3)//图谱
            {
                var savePath = Path.Combine(direcPathtst, fileName);
                data1.SaveAs(savePath);

                var url = "/" + dateFolder + "/" + dataFst + "/" + fileName;
                return new
                {
                    PreFileName = data1.FileName,
                    FileName = dateFolder + "/" + fileName,//文件夹下的文件
                    Url = url
                };
            }
            if (int.Parse(Sign) == 4)//合同
            {
                var savePath = Path.Combine(direcPathtstk, fileName);
                data1.SaveAs(savePath);

                var url = "/" + dateFolder + "/" + dataFstk + "/" + fileName;
                return new
                {
                    PreFileName = data1.FileName,
                    FileName = dateFolder + "/" + fileName,//文件夹下的文件
                    Url = url
                };
            }
            return new { msg = "失败" };
            #endregion
            //var userId = ((UserIdentity)User.Identity).UserId;

            //var userId = 42966847102676992;

            //var data1 = HttpContext.Current.Request.Files;

            //if (data1 != null && data1.Count > 0)
            //{
            //    var data = data1[0];

            //    var arr = data.FileName.Split('.');
            //    var ext = arr.LastOrDefault();
            //    var dateFolder = DateTime.Now.ToString("yyyyMMdd");
            //    var fileName = Guid.NewGuid() + "." + ext;

            //    var host = "http://192.168.3.26:9100/";
            //    //var path = "C:\\dv\\Api\\FileData\\";

            //    var path = "~/FileData/";
            //    var url = host + "FileData" + "/" + dateFolder + "/" + fileName;

            //    var direcPath = Path.Combine(path, dateFolder);
            //    var savePath = Path.Combine(direcPath, fileName);

            //    if (!Directory.Exists(direcPath))
            //        Directory.CreateDirectory(direcPath);

            //    data.SaveAs(savePath);

            //using (FileStream fs = new FileStream(savePath, FileMode.Create))
            //{
            //    //await data.CopyToAsync(fs);
            //}

            //return new
            //{
            //    PreFileName = data.FileName,
            //    FileName = dateFolder + "/" + fileName,
            //    Url = url
            //};
        }
        //else
        //{
        //    throw new HttpResponseException(new HttpResponseMessage()
        //    {
        //        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "请上传文件" }))
        //    });
        //}
    }

}





