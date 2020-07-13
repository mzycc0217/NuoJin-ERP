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
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WageManagements;
using XiAnOuDeERP.Models.Dto.InputDto.FinancialManagement.WageManagementDto;
using XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto;
using XiAnOuDeERP.Models.Enum;
using XiAnOuDeERP.Models.Util;

namespace XiAnOuDeERP.Controllers.FinancialManagement.WageManagementAppService
{
    /// <summary>
    /// 工资服务
    /// </summary>
    [AppAuthentication]
    public class WageController : ApiController
    {
        XiAnOuDeContext db = new XiAnOuDeContext();

        /// <summary>
        /// 添加工资记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Add(WageInputDto input)
        {
            if (input.StartTime > input.EndTime)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "开始时间不能大于结束时间" }))
                });
            }

            var data = await db.Wages.SingleOrDefaultAsync(m => m.StartTime <= input.StartTime && m.EndTime >= input.EndTime && m.UserId == input.UserId && !m.IsDelete);

            if (data != null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户本月已添加工资记录" }))
                });
            }

            if (!await db.UserDetails.AnyAsync(m => m.Id == input.UserId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在" }))
                });
            }

            db.Wages.Add(new Wage()
            {
                Id = IdentityManager.NewId(),
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                BasePay = input.BasePay,
                BonusPaidIn = input.BonusPaidIn,
                ConfidentialSalary = input.ConfidentialSalary,
                Desc = input.Desc,
                EducationSalary = input.EducationSalary,
                EndowmentInsurance = input.EndowmentInsurance,
                GrossPay = input.GrossPay,
                LargeInsurance = input.LargeInsurance,
                MedicalInsurance = input.MedicalInsurance,
                MeritPay = input.MeritPay,
                OtherSum = input.OtherSum,
                OvertimeExpenses = input.OvertimeExpenses,
                PaidWages = input.PaidWages,
                PostSalary = input.PostSalary,
                SeniorityPay = input.SeniorityPay,
                TaxDeductible = input.TaxDeductible,
                TotalManagementSystem = input.TotalManagementSystem,
                TrafficAndMealSupplement = input.TrafficAndMealSupplement,
                UnemploymentInsurance = input.UnemploymentInsurance,
                UserId = input.UserId,
                WagesPayable = input.WagesPayable,
                IsDelete = false
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
        /// 修改工资记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Update(WageInputDto input)
        {
            var data = await db.Wages.SingleOrDefaultAsync(m => m.Id == input.WageId && !m.IsDelete);

            if (data == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "工资记录不存在" }))
                });
            }

            if (!await db.UserDetails.Include(m => m.User).AnyAsync(m => m.Id == input.UserId && !m.IsDelete))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "该用户不存在" }))
                });
            }

            var user = await db.UserDetails.Include(m=>m.User).SingleOrDefaultAsync(m => m.Id == input.SignId && !m.IsDelete);

            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "签字人无效" }))
                });
            }

            data.GrossPay = input.GrossPay;
            data.LargeInsurance = input.LargeInsurance;
            data.MedicalInsurance = input.MedicalInsurance;
            data.MeritPay = input.MeritPay;
            data.OtherSum = input.OtherSum;
            data.OvertimeExpenses = input.OvertimeExpenses;
            data.PaidWages = input.PaidWages;
            data.PostSalary = input.PostSalary;
            data.SeniorityPay = input.SeniorityPay;
            data.SignId = input.SignId;
            data.StartTime = input.StartTime;
            data.TaxDeductible = input.TaxDeductible;
            data.TotalManagementSystem = input.TotalManagementSystem;
            data.TrafficAndMealSupplement = input.TrafficAndMealSupplement;
            data.UnemploymentInsurance = input.UnemploymentInsurance;
            data.UserId = input.UserId;
            data.WagesPayable = input.WagesPayable;
            data.EndowmentInsurance = input.EndowmentInsurance;
            data.EndTime = input.EndTime;
            data.EducationSalary = input.EducationSalary;
            data.Desc = input.Desc;
            data.ConfidentialSalary = input.ConfidentialSalary;
            data.BonusPaidIn = input.BonusPaidIn;
            data.BasePay = input.BasePay;

            if (await db.SaveChangesAsync() <= 0)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ResponseApi() { Code = EExceptionType.Implement, Message = "更新失败" }))
                });
            }
        }

        /// <summary>
        /// 批量删除工资记录
        /// </summary>
        /// <param name="WageIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> DeleteList(List<long> WageIds)
        {
            var list = new List<string>();
            foreach (var item in WageIds)
            {
                var data = await db.Wages.SingleOrDefaultAsync(m => m.Id == item && !m.IsDelete);

                if (data != null)
                {
                    data.IsDelete = true;
                }
                else
                {
                    list.Add(item + "不存在");
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取工资记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<WageOutputDto>> GetList(GetWageInputDto input)
        {
            var data = await db.Wages
                .Include(m => m.User)
                .Include(m => m.Sign)
                .Where(m => !m.IsDelete)
                .ToListAsync();

            if (input.WageId != null)
            {
                data = data.Where(m => m.Id == input.WageId).ToList();
            }

            if (input.UserId != null)
            {
                data = data.Where(m => m.UserId == input.UserId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                data = data.Where(m => m.User.RealName != null && m.User.RealName.Contains(input.UserName)).ToList();
            }

            if (input.SignId != null)
            {
                data = data.Where(m => m.SignId == input.SignId).ToList();
            }

            if (input.StartTime != null)
            {
                data = data.Where(m => m.StartTime <= input.StartTime).ToList();
            }

            if (input.EndTime != null)
            {
                data = data.Where(m => m.EndTime >= input.EndTime).ToList();
            }

            var list = new List<WageOutputDto>();

            foreach (var item in data)
            {
                list.Add(new WageOutputDto()
                {
                    BasePay = item.BasePay,
                    BonusPaidIn = item.BonusPaidIn,
                    ConfidentialSalary = item.ConfidentialSalary,
                    CreateDate = item.CreateDate,
                    Desc = item.Desc,
                    EducationSalary = item.EducationSalary,
                    EndowmentInsurance = item.EndowmentInsurance,
                    EndTime = item.EndTime,
                    GrossPay = item.GrossPay,
                    LargeInsurance = item.LargeInsurance,
                    MedicalInsurance = item.MedicalInsurance,
                    MeritPay = item.MeritPay,
                    OtherSum = item.OtherSum,
                    OvertimeExpenses = item.OvertimeExpenses,
                    PaidWages = item.PaidWages,
                    PostSalary = item.PostSalary,
                    SeniorityPay = item.SeniorityPay,
                    Sign = item.Sign,
                    SignId = item.SignId.ToString(),
                    StartTime = item.StartTime,
                    TaxDeductible = item.TaxDeductible,
                    UserId = item.UserId.ToString(),
                    TotalManagementSystem = item.TotalManagementSystem,
                    TrafficAndMealSupplement = item.TrafficAndMealSupplement,
                    UnemploymentInsurance = item.UnemploymentInsurance,
                    UpdateDate = item.UpdateDate,
                    User = item.User,
                    WageId = item.Id.ToString(),
                    WagesPayable = item.WagesPayable
                });
            }


            var count = list.Count;

            list = list
                .OrderByDescending(m => m.WageId)
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
