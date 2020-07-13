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
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Dto.InputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.PersonnelMatters.UserAppService
{
    /// <summary>
    /// 人员需求服务
    /// </summary>
    [AppAuthentication]
    public class PersonnelRtsController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加人员需求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthentication]
        public async Task Add(PersonnelRtsInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;

            var related = await db.RelatedApprovals.SingleOrDefaultAsync(m => m.RelatedKey == "PersonnelRts");

            if (related == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "人员需求申请未绑定审批流，添加失败" }))
                });
            }

            var approval = await db.Approvals.Where(m => m.Key == related.ApprovalKey).ToListAsync();

            if (approval == null && approval.Count <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = related.ApprovalKey + "该审批流不存在，添加失败" }))
                });
            }

            var data = new PersonnelRts()
            {
                Id = IdentityManager.NewId(),
                AddbyId = userId,
                Age = input.Age,
                ApprovalType = EApprovalType.UnderReview,
                Education = input.Education,
                IsDelete = false,
                Number = input.Number,
                Position = input.Position,
                RecruitedNumber = input.RecruitedNumber,
                Sex = input.Sex,
                SkillRequirements = input.SkillRequirements,
                ApprovalIndex = 0,
                ApprovalKey = related.ApprovalKey
            };

            db.PersonnelRts.Add(data);

            var userTypeKey = await db.Approvals.SingleOrDefaultAsync(m => m.Deis == 1 && m.Key == data.ApprovalKey);

            if (userTypeKey != null)
            {
                db.PersonnelRtsApprovals.Add(new PersonnelRtsApproval
                {
                    Id = IdentityManager.NewId(),
                    PersonnelRtsId = data.Id,
                    IsApproval = false,
                    UserTypeKey = userTypeKey.UserTypeKey,
                    ApprovalIndex = 1
                });
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流异常" }))
                });
            }

            if (await db.SaveChangesAsync() <=0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "添加失败" }))
                });
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(PersonnelRtsInputDto input)
        {
            var data = await db.PersonnelRts.SingleOrDefaultAsync(m => m.Id == input.PersonnelRtsId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该需求不存在" }))
                });
            }

            data.Age = input.Age;
            data.Education = input.Education;
            data.Number = input.Number;
            data.Position = input.Position;
            data.RecruitedNumber += input.RecruitedNumber;
            data.Sex = input.Sex;
            data.SkillRequirements = input.SkillRequirements;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 更新人员需求审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdatePersonnelRtsApprovalType(PersonnelRtsInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.PersonnelRts.SingleOrDefaultAsync(m => m.Id == input.PersonnelRtsId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该需求不存在" }))
                });
            }

            data.ApprovalType = (EApprovalType)input.ApprovalType;

            if (data.ApprovalType == EApprovalType.Rejected)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该申请已被驳回" }))
                });
            }

            var approval = await db.Approvals.Where(m => m.Key == data.ApprovalKey).ToListAsync();

            if (approval == null && approval.Count <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "该审批流不存在，请重新提交申请" }))
                });
            }

            var index = data.ApprovalIndex + 1;

            var maxIndex = approval.Max(m => m.Deis);

            var userApproval = approval.SingleOrDefault(m => m.Deis == index);

            if (userApproval != null && input.ApprovalType == null)
            {
                if (!userTypeKeys.Contains(userApproval.UserTypeKey))
                {

                //}
                //if (userApproval.UserTypeKey != token.UserTypeKey)
                //{
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "无权操作" }))
                    });
                }
                else
                {
                    data.ApprovalIndex += 1;

                    data.ApprovalType = EApprovalType.InExecution;

                    //var personnelRtsApproval = await db.PersonnelRtsApprovals.SingleOrDefaultAsync(m => m.UserTypeKey == token.UserTypeKey && m.ApprovalIndex == index && m.PersonnelRtsId == data.Id);

                    var personnelRtsApproval = await db.PersonnelRtsApprovals.SingleOrDefaultAsync(m => userTypeKeys.Contains(m.UserTypeKey) && m.ApprovalIndex == index && m.PersonnelRtsId == data.Id);

                    if (personnelRtsApproval == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = data.ApprovalKey + "审批流程异常" }))
                        });
                    }

                    personnelRtsApproval.IsApproval = true;
                    personnelRtsApproval.UserId = token.UserId;

                    var userTypeKey = approval.SingleOrDefault(m => m.Deis == index + 1);

                    if (userTypeKey != null)
                    {
                        db.PersonnelRtsApprovals.Add(new PersonnelRtsApproval
                        {
                            Id = IdentityManager.NewId(),
                            PersonnelRtsId = data.Id,
                            IsApproval = false,
                            UserTypeKey = userTypeKey.UserTypeKey,
                            ApprovalIndex = index + 1
                        });
                    }

                    if (maxIndex == data.ApprovalIndex)
                    {
                        data.ApprovalType = EApprovalType.Reviewed;
                    }
                }
            }
            else if (maxIndex == data.ApprovalIndex && input.ApprovalType == null)
            {
                data.ApprovalType = EApprovalType.Reviewed;
            }
            else
            {
                if (input.ApprovalType == EApprovalType.Rejected)
                {
                    data.ApprovalType = (EApprovalType)input.ApprovalType;
                }
            }


            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> Ids)
        {
            var list = new List<string>();
            foreach (var item in Ids)
            {
                var data = await db.PersonnelRts.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add("【"+item+"】不存在");
                }
            }

            await db.SaveChangesAsync();

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<PersonnelRtsOutputDto>> GetList(GetPersonnelRtsInputDto input)
        {
            var userId = ((UserIdentity)User.Identity).UserId;
            var token = ((UserIdentity)User.Identity).Token;

            var userTypeKeys = token.UserTypeKey;

            var data = await db.PersonnelRts
                .Include(m=>m.Addby)
                .Where(m=>!m.IsDelete)
                .ToListAsync();

            if (input.IsApproval)
            {
                var personnelRts = await db.PersonnelRtsApprovals
                    .Include(m => m.PersonnelRts)
                    .Include(m => m.PersonnelRts.Addby)
                    //.Where(m => m.UserTypeKey == token.UserTypeKey && !m.IsApproval && m.UserId == null && !m.PersonnelRts.IsDelete && m.PersonnelRts.ApprovalType != EApprovalType.Rejected)
                    .Where(m => userTypeKeys.Contains(m.UserTypeKey) && !m.IsApproval && m.UserId == null && !m.PersonnelRts.IsDelete && m.PersonnelRts.ApprovalType != EApprovalType.Rejected)
                    .Select(m=>m.PersonnelRts)
                    .ToListAsync();

                data = personnelRts;
            }

            if (input.UserId != null)
            {
                var personnelRts = await db.PersonnelRtsApprovals
                    .Include(m => m.PersonnelRts)
                    .Include(m => m.PersonnelRts.Addby)
                    .Where(m => m.UserId == input.UserId && !m.PersonnelRts.IsDelete && m.PersonnelRts.ApprovalType == EApprovalType.Reviewed)
                    .Select(m => m.PersonnelRts)
                    .ToListAsync();

                data = personnelRts;
            }

            if (!string.IsNullOrWhiteSpace(input.AddbyName))
            {
                data = data.Where(m => m.Addby.RealName != null && m.Addby.RealName.Contains(input.AddbyName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.Position))
            {
                data = data.Where(m => m.Position != null && m.Position.Contains(input.Position)).ToList();
            }

            if (input.AddbyId != null)
            {
                data = data.Where(m => m.AddbyId == input.AddbyId).ToList();
            }

            if (input.PersonnelRtsId != null)
            {
                data = data.Where(m => m.Id == input.PersonnelRtsId).ToList();
            }

            var list = new List<PersonnelRtsOutputDto>();

            foreach (var item in data)
            {
                list.Add(new PersonnelRtsOutputDto()
                {
                    PersonnelRtsId = item.Id.ToString(),
                    Addby = item.Addby,
                    AddbyId = item.AddbyId.ToString(),
                    Age = item.Age,
                    ApprovalType = item.ApprovalType,
                    ApprovalTypeStr = item.ApprovalType.GetDescription(),
                    CreateDate = item.CreateDate,
                    Education = item.Education,
                    Number = item.Number,
                    Position = item.Position,
                    RecruitedNumber = item.RecruitedNumber,
                    Sex = item.Sex,
                    SkillRequirements = item.SkillRequirements,
                    UpdateDate = item.UpdateDate
                });
            }

            var count = list.Count;

            list = list
                .OrderByDescending(m => m.PersonnelRtsId)
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
