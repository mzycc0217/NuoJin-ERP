using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XiAnOuDeERP.Models.Db;
using XiAnOuDeERP.Models.Dto.InputDto.AtlasManagements;
using XiAnOuDeERP.Models.Dto.OutputDto.AtlasManagements;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.AtlasManagement
{
    /// <summary>
    /// 图谱服务
    /// </summary>
    [AppAuthentication]
    public class AtlasController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加图谱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(AtlasInputDto input)
        {
            if (await db.Atlas.AnyAsync(m => m.Name == input.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "已有该名称的图谱，请勿重复添加" }))
                });
            }

            if (string.IsNullOrWhiteSpace(input.Url))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "Url不允许为空" }))
                });
            }

            db.Atlas.Add(new Models.Db.Aggregate.AtlasManagements.Atlas
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                BatchNumber = input.BatchNumber,
                Desc = input.Desc,
                Url = input.Url,
                TestingTime = input.TestingTime
            });

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新图谱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(AtlasInputDto input)
        {
            if (await db.Atlas.AnyAsync(m => m.Name == input.Name && m.Id != input.AtlasId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该图谱名称已存在" }))
                });
            }

            var data = await db.Atlas.SingleOrDefaultAsync(m=>m.Id == input.AtlasId);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该谱图不存在" }))
                });
            }

            data.Name = input.Name;
            data.BatchNumber = input.BatchNumber;
            data.Desc = input.Desc;
            data.TestingTime = input.TestingTime;
            data.Url = input.Url;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "修改失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除图谱
        /// </summary>
        /// <param name="AtlasIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> AtlasIds)
        {
            var list = new List<string>();

            foreach (var item in AtlasIds)
            {
                var data = await db.Atlas.SingleOrDefaultAsync(m => m.Id == item);

                if (data != null)
                {
                    db.Atlas.Remove(data);
                }
                else
                {
                    list.Add("【" + item + "】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取图谱列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AtlasOutputDto>> GetAtlasList(GetAtlasInputDto input)
        {
            var data = await db.Atlas
                    .ToListAsync();

            if (input.AtlasId != null)
            {
                data = data.Where(m => m.Id == input.AtlasId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.Name)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.BatchNumber))
            {
                data = data.Where(m => m.BatchNumber != null && m.BatchNumber.Contains(input.BatchNumber)).ToList();
            }

            var list = new List<AtlasOutputDto>();

            foreach (var item in data)
            {
                list.Add(new AtlasOutputDto
                {
                    AtlasId = item.Id.ToString(),
                    BatchNumber = item.BatchNumber,
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    Name = item.Name,
                    UpdateDate = item.UpdateDate,
                    TestingTime = item.TestingTime,
                    Url = item.Url
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.AtlasId)
                .ToList();

            if (input.PageIndex != null && input.PageSize != null
                && input.PageIndex != 0 && input.PageSize != 0)
            {
                list = list
                    .Skip((input.PageIndex - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .ToList();
            }

            if (list != null && list.Count > 0)
            {
                list[0].Count = count;
            }

            return list;
        }

    }
}
