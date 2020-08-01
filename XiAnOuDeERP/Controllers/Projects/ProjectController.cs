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
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Dto.InputDto.Projecct;
using XiAnOuDeERP.Models.Dto.OutputDto.Projects;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.Projects
{
    /// <summary>
    /// 项目服务
    /// </summary>
    [AppAuthentication]
    public class ProjectController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(ProjectInputDto input)
        {
            if (await db.Projects.AnyAsync(m => m.Name == input.Name && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该项目名称已存在" }))
                });
            }

            if (!await db.ProjectStates.AnyAsync(m => m.Id == input.ProjectStateId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该项目状态不存在" }))
                });
            }

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "名称不允许为空" }))
                });
            }

            db.Projects.Add(new Project()
            {
                Id = IdentityManager.NewId(),
                Name = input.Name,
                Number = input.Number,
                ProjectStateId = input.ProjectStateId,
                IsDelete = false,
                Desc = input.Desc
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
        /// 修改项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(ProjectInputDto input)
        {
            if (await db.Projects.AnyAsync(m=>m.Name == input.Name && m.Id != input.ProjectId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该项目名称已存在" }))
                });
            }

            if (!await db.ProjectStates.AnyAsync(m => m.Id == input.ProjectStateId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该项目状态不存在" }))
                });
            }

            var data = await db.Projects.SingleOrDefaultAsync(m => m.Id == input.ProjectId);

            data.Name = input.Name;
            data.Number = input.Number;
            data.ProjectStateId = input.ProjectStateId;
            data.Desc = input.Desc;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="ProjectIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> ProjectIds)
        {

            var list = new List<string>();
            foreach (var item in ProjectIds)
            {
                var data = await db.Projects.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }
            await db.SaveChangesAsync();

            return list;

        }

        /// <summary>
        /// 添加项目状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddProjeState(ProjectStateInputDto input)
        {
            if (await db.ProjectStates.AnyAsync(m=>m.Name == input.ProjectStateName && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ProjectStateName+"该状态名称已存在" }))
                });
            }

            var data = new ProjectState()
            {
                Id = IdentityManager.NewId(),
                Name = input.ProjectStateName,
                IsDelete = false
            };

            db.ProjectStates.Add(data);

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新项目状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateProjeState(ProjectStateInputDto input)
        {
            if (await db.ProjectStates.AnyAsync(m => m.Name == input.ProjectStateName && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ProjectStateName + "该状态名称已存在" }))
                });
            }

            var data = await db.ProjectStates.SingleOrDefaultAsync(m => m.Id == input.ProjectStateId);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = input.ProjectStateName + input.ProjectStateId+"该项目状态不存在" }))
                });
            }

            data.Name = input.ProjectStateName;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除项目状态
        /// </summary>
        /// <param name="ProjectStateIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteProjectStateList(List<long> ProjectStateIds)
        {

            var list = new List<string>();
            foreach (var item in ProjectStateIds)
            {
                var data = await db.ProjectStates.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }
            await db.SaveChangesAsync();

            return list;

        }

        /// <summary>
        /// 获取项目状态列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ProjectStateOutputDto>> GetProjectStateList(GetProjectStateListInputDto input)
        {
            var data = await db.ProjectStates
                    .Where(m => !m.IsDelete)
                    .ToListAsync();

            if (input.ProjectStateId != null)
            {
                data = data.Where(m => m.Id == input.ProjectStateId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.Name)).ToList();
            }

            var list = new List<ProjectStateOutputDto>();

            foreach (var item in data)
            {
                list.Add(new ProjectStateOutputDto
                {
                    ProjectStateId = item.Id.ToString(),
                    ProjectStateName = item.Name
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.ProjectStateId)
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

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ProjectOutputDto>> GetList(GetProjectInputDto input)
        {
            var data = await db.Projects
                .Where(M=>!M.IsDelete)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(input.ProjectName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.ProjectName)).ToList();
            }

            if (input.ProjectId != null)
            {
                data = data.Where(m => m.Id == input.ProjectId).ToList();
            }

            if (input.ProjectStateId != null)
            {
                data = data.Where(m => m.ProjectStateId == input.ProjectStateId).ToList();
            }

            if (input.IsComplete)
            {
                data = data.Where(m => m.ProjectState.Name == "完成").ToList();
            }

            var list = new List<ProjectOutputDto>();

            foreach (var item in data)
            {
                list.Add(new ProjectOutputDto()
                {
                    CreateDate = item.CreateDate,
                    Name = item.Name,
                    Number = item.Number,
                    ProjectId = item.Id.ToString(),
                    UpdateDate = item.UpdateDate,
                    ProjectStateId = item.ProjectStateId.ToString(),
                    ProjectState = item.ProjectState,
                    Desc = item.Desc
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.ProjectId)
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



        /// <summary>
        /// 历史项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ProjectOutputDto>> GetHositryList(GetProjectInputDto input)
        {
            var data = await db.Projects
                .Where(M => !M.IsDelete&&M.ProjectStateId== 43275373866225664)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(input.ProjectName))
            {
                data = data.Where(m => m.Name != null && m.Name.Contains(input.ProjectName)).ToList();
            }

            if (input.ProjectId != null)
            {
                data = data.Where(m => m.Id == input.ProjectId).ToList();
            }

            if (input.ProjectStateId != null)
            {
                data = data.Where(m => m.ProjectStateId == input.ProjectStateId).ToList();
            }

            if (input.IsComplete)
            {
                data = data.Where(m => m.ProjectState.Name == "完成").ToList();
            }

            var list = new List<ProjectOutputDto>();

            foreach (var item in data)
            {
                list.Add(new ProjectOutputDto()
                {
                    CreateDate = item.CreateDate,
                    Name = item.Name,
                    Number = item.Number,
                    ProjectId = item.Id.ToString(),
                    UpdateDate = item.UpdateDate,
                    ProjectStateId = item.ProjectStateId.ToString(),
                    ProjectState = item.ProjectState,
                    Desc = item.Desc
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.ProjectId)
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
