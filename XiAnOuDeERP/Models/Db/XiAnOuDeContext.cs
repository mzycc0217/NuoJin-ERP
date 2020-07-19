using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using XiAnOuDeERP.Models.Db.Aggregate.Projects;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WarehouseManagements;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Users;
using XiAnOuDeERP.Models.Util;
using System.Data.Entity.ModelConfiguration.Conventions;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.WageManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.PurchasingManagements;
using XiAnOuDeERP.Models.Db.Aggregate.FinancialManagement.DeviceManagement;
using XiAnOuDeERP.Models.Db.Aggregate.PersonnelMatters.Assetss;
using XiAnOuDeERP.Models.Db.Aggregate.AtlasManagements;
using XiAnOuDeERP.Models.Db.Aggregate.ApprovalMangement;
using XiAnOuDeERP.Models.Db.Aggregate.Departent_User;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoomst;
using XiAnOuDeERP.Models.Db.Aggregate.StrongRoom;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt;
using XiAnOuDeERP.Models.Db.Aggregate.OutEntropt.Outenport;

namespace XiAnOuDeERP.Models.Db
{
    public class XiAnOuDeContext : DbContext
    {
        public XiAnOuDeContext() : base("name=XiAnOuDeEntities")
        {

        }


        #region  mzy各类表单申请表
        public virtual DbSet<ChemistryMonad> ChemistryMonad { get; set; }
        public virtual DbSet<OfficeMonad> OfficeMonad { get; set; }
        public virtual DbSet<FnishedProductMonad> FnishedProductMonad { get; set; }

        #endregion





        #region mzy新建基础信息表
        public virtual DbSet<Z_Raw> Z_Raw { get; set; }

        public virtual DbSet<Z_Supplies> Z_Supplies { get; set; }

        public virtual DbSet<Z_FnishedProduct> Z_FnishedProduct { get; set; }

        public virtual DbSet<Z_Chemistry> Z_Chemistry { get; set; }

       public virtual DbSet<Z_Office> Z_Office { get; set; }

        public virtual DbSet<Z_MaterialCode> Z_MaterialCode { get; set; }

        public virtual DbSet<Z_RowType> Z_RowType { get; set; }

        public virtual DbSet<Z_SuppliesType> Z_SuppliesType { get; set; }
        public virtual DbSet<Z_FinshedProductType> Z_FinshedProductType { get; set; }
        public virtual DbSet<Z_ChemistryType> Z_ChemistryType { get; set; }
        public virtual DbSet<Z_OfficeType> Z_OfficeType { get; set; }
        /// <summary>
        /// 领料表
        /// </summary>
        public virtual DbSet<Raw_UserDetils> Raw_UserDetils { get; set; }
        public virtual DbSet<Chemistry_UserDetils> Chemistry_UserDetils { get; set; }
        public virtual DbSet<FnishedProduct_UserDetils> FnishedProduct_UserDetils { get; set; }
        public virtual DbSet<Office_UsrDetils> Office_UsrDetils { get; set; }

        #region 领料记录表
        public virtual DbSet<FinshedProduct_User> FinshedProduct_Users { get; set; }
        public virtual DbSet<Chmistry_User> Chmistry_Users { get; set; }
        public virtual DbSet<Office_User> Office_Users { get; set; }
        #endregion

        /// <summary>
        /// 领料表
        /// </summary>

        public virtual DbSet<Entrepot> Entrepots { get; set; }
        /// <summary>
        /// 物料仓库对应表
        /// </summary>

        public virtual DbSet<RawRoom> RawRooms { get; set; }

        public virtual DbSet<FnishedProductRoom> FnishedProductRooms { get; set; }

        public virtual DbSet<ChemistryRoom> ChemistryRooms { get; set; }

        public virtual DbSet<OfficeRoom> Offices { get; set; }


      //  public virtual DbSet<RawRoom> RawRooms { get; set; }
        #endregion



        //   public virtual DbSet<Content_Usersd> Pursh_User { get; set; }

        //采购申请签核表
        public virtual DbSet<Pursh_User> Pursh_User { get; set; }
        #region 实体

        /// <summary>
        /// 审批表
        /// </summary>
        public virtual DbSet<Approval> Approvals { get; set; }

        /// <summary>
        /// 模块关联审核流表
        /// </summary>
        public virtual DbSet<RelatedApproval> RelatedApprovals { get; set; }

        #region Projects-项目
        /// <summary>
        /// 项目表
        /// </summary>
        public virtual DbSet<Project> Projects { get; set; }

        /// <summary>
        /// 项目状态表
        /// </summary>
        public virtual DbSet<ProjectState> ProjectStates { get; set; }

        #endregion
        /// <summary>
        /// 审核人员表
        /// </summary>
        #region
        public virtual DbSet<Departent_User> Departent_Users { get; set; }


        #endregion
        #region PurchasingManagements-采购管理
        /// <summary>
        /// 签核记录表
        /// </summary>
        public virtual DbSet<Content_User> Content_User { get; set; }
        /// <summary>
        /// 采购申请表
        /// </summary>
        public virtual DbSet<Purchase> Purchases { get; set; }

        /// <summary>
        /// 采购订单审核记录表
        /// </summary>
        public virtual DbSet<PurchaseApproval> PurchaseApprovals { get; set; }

        /// <summary>
        /// 设备表
        /// </summary>
        public virtual DbSet<Device> Devices { get; set; }

        /// <summary>
        /// 设备维修表
        /// </summary>
        public virtual DbSet<DeviceRepair> DeviceRepairs { get; set; }

        /// <summary>
        /// 设备维修记录表
        /// </summary>
        public virtual DbSet<DeviceRepairApproval> DeviceRepairApprovals { get; set; }

        /// <summary>
        /// 供应商表
        /// </summary>
        public virtual DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// 评分表
        /// </summary>
        public virtual DbSet<Score> Scores { get; set; }

        /// <summary>
        /// 加班表
        /// </summary>
        public virtual DbSet<Overtime> Overtimes { get; set; }

        /// <summary>
        /// 请假表
        /// </summary>
        public virtual DbSet<Leave> Leaves { get; set; }

        /// <summary>
        /// 请假审核记录表
        /// </summary>
        public virtual DbSet<LeaveApproval> LeaveApprovals { get; set; }

        /// <summary>
        /// 人员需求表
        /// </summary>
        public virtual DbSet<PersonnelRts> PersonnelRts { get; set; }

        /// <summary>
        /// 人员需求审核记录表
        /// </summary>
        public virtual DbSet<PersonnelRtsApproval> PersonnelRtsApprovals { get; set; }
        #endregion

        #region WarehouseManagements-库存管理
        /// <summary>
        /// 原材料表
        /// </summary>
        public virtual DbSet<RawMaterial> RawMaterials { get; set; }

        /// <summary>
        /// 出库表
        /// </summary>
        public virtual DbSet<OutOfStock> OutOfStocks { get; set; }

        /// <summary>
        /// 出库表
        /// </summary>
        public virtual DbSet<OutOfStockType> OutOfStockTypes { get; set; }

        /// <summary>
        /// 出库审核记录表
        /// </summary>
        public virtual DbSet<OutOfStockApproval> OutOfStockApprovals { get; set; }

        /// <summary>
        /// 入库表
        /// </summary>
        public virtual DbSet<Warehousing> Warehousings { get; set; }

        /// <summary>
        /// 入库审核记录表
        /// </summary>
        public virtual DbSet<WarehousingApproval> WarehousingApprovals { get; set; }

        /// <summary>
        /// 单位表
        /// </summary>
        public virtual DbSet<Company> Companys { get; set; }
        /// <summary>
        /// 库存表
        /// </summary>
        public virtual DbSet<StorageRoom> StorageRooms { get; set; }

        /// <summary>
        /// 销售表
        /// </summary>
        public virtual DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// 销售类型表
        /// </summary>
        public virtual DbSet<SaleType> SaleTypes { get; set; }

        /// <summary>
        /// 入库类型表
        /// </summary>
        public virtual DbSet<WarehousingType> WarehousingTypes { get; set; }

        #endregion

        #region WageManagements-工资管理

        /// <summary>
        /// 工资表
        /// </summary>
        public virtual DbSet<Wage> Wages { get; set; }

        #endregion

        #region PersonnelMatters

        #region Assetss
        /// <summary>
        /// 资产表
        /// </summary>
        public virtual DbSet<Assets> Assetss { get; set; }

        /// <summary>
        /// 资产类型表
        /// </summary>
        public virtual DbSet<AssetsType> AssetsTypes { get; set; }

        /// <summary>
        /// 支出记录表
        /// </summary>
        public virtual DbSet<AssetExpenditure> AssetExpenditures { get; set; }

        /// <summary>
        /// 收入记录表
        /// </summary>
        public virtual DbSet<AssetIncome> AssetIncomes { get; set; }
        #endregion

        #region Users-用户



        public virtual DbSet<User_User_Type> User_User_Types { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public virtual DbSet<User> User { get; set; }
        /// <summary>
        /// 部门表
        /// </summary>
        public virtual DbSet<Department> Departments { get; set; }
        /// <summary>
        /// 用户详情表
        /// </summary>
        public virtual DbSet<UserDetails> UserDetails { get; set; }

        /// <summary>
        /// 模块表
        /// </summary>
        public virtual DbSet<Module> Modules { get; set; }

        /// <summary>
        /// 菜单表
        /// </summary>
        public virtual DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 元素表
        /// </summary>
        public virtual DbSet<Element> Elements { get; set; }

        /// <summary>
        /// 用户模块表
        /// </summary>
        public virtual DbSet<UserModule> UserModules { get; set; }

        /// <summary>
        /// 用户菜单表
        /// </summary>
        public virtual DbSet<UserMenu> UserMenus { get; set; }

        /// <summary>
        /// 用户元素表
        /// </summary>
        public virtual DbSet<UserElement> UserElements { get; set; }

        /// <summary>
        /// 用户类型表
        /// </summary>
        public virtual DbSet<UserType> UserTypes { get; set; }

        /// <summary>
        /// 用户绑定类型表
        /// </summary>
        public virtual DbSet<UserDetailsType> UserDetailsTypes { get; set; }

        #endregion

        #endregion AtlasManagements

        /// <summary>
        /// 图谱表
        /// </summary>
        public virtual DbSet<Atlas> Atlas { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = base.ChangeTracker.Entries();

            var second = 0;

            foreach (var item in entries)
            {
                second++;

                var now = DateTime.Now.AddSeconds(second);

                var entity = (EntityBase)item.Entity;

                if (item.State == EntityState.Added)
                {
                    entity.CreateDate = now;
                    entity.UpdateDate = now;
                }
                else if (item.State == EntityState.Modified)
                {
                    entity.UpdateDate = now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //HasRequired不允许为空的外键
            //HasOptional可以为空的外键
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            #region  曾经Ly
            modelBuilder.Entity<Project>().HasRequired(p => p.ProjectState).WithMany().HasForeignKey(m => m.ProjectStateId);

            modelBuilder.Entity<User>().HasRequired(p => p.Department).WithMany().HasForeignKey(m => m.DepartmentId);

            modelBuilder.Entity<UserDetailsType>().HasRequired(p => p.UserType).WithMany().HasForeignKey(m => m.UserTypeId);
            modelBuilder.Entity<UserDetailsType>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Purchase>().HasOptional(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
           modelBuilder.Entity<Purchase>().HasOptional(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);
            modelBuilder.Entity<Purchase>().HasOptional(p => p.Z_Raw).WithMany().HasForeignKey(m => m.RawId);
            modelBuilder.Entity<Purchase>().HasRequired(p => p.Project).WithMany().HasForeignKey(m => m.ProjectId);
            modelBuilder.Entity<Purchase>().HasOptional(p => p.Supplier).WithMany().HasForeignKey(m => m.SupplierId);


            modelBuilder.Entity<PurchaseApproval>().HasOptional(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<PurchaseApproval>().HasRequired(p => p.Purchase).WithMany().HasForeignKey(m => m.PurchaseId);

            modelBuilder.Entity<Device>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<Device>().HasRequired(p => p.Department).WithMany().HasForeignKey(m => m.DepartmentId);
            modelBuilder.Entity<Device>().HasRequired(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);

            modelBuilder.Entity<DeviceRepair>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<DeviceRepair>().HasRequired(p => p.Device).WithMany().HasForeignKey(m => m.DeviceId);

            modelBuilder.Entity<Score>().HasRequired(p => p.Supplier).WithMany().HasForeignKey(m => m.SupplierId);
            modelBuilder.Entity<Score>().HasRequired(p => p.Addby).WithMany().HasForeignKey(m => m.AddbyId);

            modelBuilder.Entity<Overtime>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<Overtime>().HasOptional(p => p.DepartmentLeader).WithMany().HasForeignKey(m => m.DepartmentLeaderId);

            modelBuilder.Entity<Leave>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);

            modelBuilder.Entity<LeaveApproval>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<LeaveApproval>().HasRequired(p => p.Leave).WithMany().HasForeignKey(m => m.LeaveId);

            modelBuilder.Entity<PersonnelRts>().HasRequired(p => p.Addby).WithMany().HasForeignKey(m => m.AddbyId);

            modelBuilder.Entity<PersonnelRtsApproval>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<PersonnelRtsApproval>().HasRequired(p => p.PersonnelRts).WithMany().HasForeignKey(m => m.PersonnelRtsId);

            modelBuilder.Entity<RawMaterial>().HasRequired(p => p.EntryPerson).WithMany().HasForeignKey(m => m.EntryPersonId);
            modelBuilder.Entity<RawMaterial>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<RawMaterial>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);

            modelBuilder.Entity<OutOfStock>().HasRequired(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
            modelBuilder.Entity<OutOfStock>().HasOptional(p => p.Project).WithMany().HasForeignKey(m => m.ProjectId);
            modelBuilder.Entity<OutOfStock>().HasRequired(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);
            modelBuilder.Entity<OutOfStock>().HasOptional(p => p.OutOfStockType).WithMany().HasForeignKey(m => m.OutOfStockTypeId);

            modelBuilder.Entity<OutOfStockApproval>().HasOptional(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<OutOfStockApproval>().HasRequired(p => p.OutOfStock).WithMany().HasForeignKey(m => m.OutOfStockId);

            modelBuilder.Entity<Warehousing>().HasRequired(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
            modelBuilder.Entity<Warehousing>().HasOptional(p => p.Purchase).WithMany().HasForeignKey(m => m.PurchaseId);
            modelBuilder.Entity<Warehousing>().HasOptional(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);

            modelBuilder.Entity<WarehousingApproval>().HasOptional(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<WarehousingApproval>().HasRequired(p => p.Warehousing).WithMany().HasForeignKey(m => m.WarehousingId);

            modelBuilder.Entity<StorageRoom>().HasRequired(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);
            modelBuilder.Entity<StorageRoom>().HasOptional(p => p.WarehouseKeeper).WithMany().HasForeignKey(m => m.WarehouseKeeperId);

            modelBuilder.Entity<Sale>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<Sale>().HasRequired(p => p.OutOfStock).WithMany().HasForeignKey(m => m.OutOfStockId);
            modelBuilder.Entity<Sale>().HasOptional(p => p.SaleType).WithMany().HasForeignKey(m => m.SaleTypeId);

            modelBuilder.Entity<UserDetails>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Menu>().HasRequired(p => p.Module).WithMany().HasForeignKey(m => m.ModuleId);

            modelBuilder.Entity<Element>().HasRequired(p => p.Menu).WithMany().HasForeignKey(m => m.MenuId);

            modelBuilder.Entity<UserModule>().HasRequired(p => p.Module).WithMany().HasForeignKey(m => m.ModuleId);
            modelBuilder.Entity<UserModule>().HasRequired(p => p.UserType).WithMany().HasForeignKey(m => m.UserTypeId);

            modelBuilder.Entity<UserMenu>().HasRequired(p => p.Menu).WithMany().HasForeignKey(m => m.MenuId);
            modelBuilder.Entity<UserMenu>().HasRequired(p => p.UserType).WithMany().HasForeignKey(m => m.UserTypeId);

            modelBuilder.Entity<UserElement>().HasRequired(p => p.Element).WithMany().HasForeignKey(m => m.ElementId);
            modelBuilder.Entity<UserElement>().HasRequired(p => p.UserType).WithMany().HasForeignKey(m => m.UserTypeId);

            modelBuilder.Entity<Wage>().HasOptional(p => p.Sign).WithMany().HasForeignKey(m => m.SignId);
            modelBuilder.Entity<Wage>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Assets>().HasRequired(p => p.Department).WithMany().HasForeignKey(m => m.DepartmentId);
            modelBuilder.Entity<Assets>().HasRequired(p => p.AssetsType).WithMany().HasForeignKey(m => m.AssetsTypeId);

            modelBuilder.Entity<AssetExpenditure>().HasOptional(p => p.Purchase).WithMany().HasForeignKey(m => m.PurchaseId);
            modelBuilder.Entity<AssetExpenditure>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);

            modelBuilder.Entity<AssetIncome>().HasRequired(p => p.User).WithMany().HasForeignKey(m => m.UserId);
            modelBuilder.Entity<AssetIncome>().HasOptional(p => p.Sale).WithMany().HasForeignKey(m => m.SaleId);

            modelBuilder.Entity<Supplier>().HasRequired(p => p.RawMaterial).WithMany().HasForeignKey(m => m.RawMaterialId);
            #endregion

            #region 仓库表 -mzy
            modelBuilder.Entity<Pursh_User>().HasRequired(p => p.UserDetails).WithMany().HasForeignKey(m => m.user_Id);
            modelBuilder.Entity<Pursh_User>().HasRequired(p => p.Purchase).WithMany().HasForeignKey(m => m.Purchase_Id);

            modelBuilder.Entity<RawRoom>().HasRequired(p => p.z_Raw).WithMany().HasForeignKey(m => m.RawId);
            modelBuilder.Entity<RawRoom>().HasOptional(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
            modelBuilder.Entity<RawRoom>().HasOptional(p => p.entrepot).WithMany().HasForeignKey(m => m.EntrepotId);


            modelBuilder.Entity<FnishedProductRoom>().HasRequired(p => p.Z_FnishedProduct).WithMany().HasForeignKey(m => m.FnishedProductId);
            modelBuilder.Entity<FnishedProductRoom>().HasOptional(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
            modelBuilder.Entity<FnishedProductRoom>().HasOptional(p => p.entrepot).WithMany().HasForeignKey(m => m.EntrepotId);

            modelBuilder.Entity<ChemistryRoom>().HasRequired(p => p.Z_Chemistry).WithMany().HasForeignKey(m => m.ChemistryId);
            modelBuilder.Entity<ChemistryRoom>().HasOptional(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
            modelBuilder.Entity<ChemistryRoom>().HasOptional(p => p.entrepot).WithMany().HasForeignKey(m => m.EntrepotId);


            modelBuilder.Entity<OfficeRoom>().HasRequired(p => p.Z_Office).WithMany().HasForeignKey(m => m.OfficeId);
            modelBuilder.Entity<OfficeRoom>().HasOptional(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
            modelBuilder.Entity<OfficeRoom>().HasOptional(p => p.entrepot).WithMany().HasForeignKey(m => m.EntrepotId);


            modelBuilder.Entity<Entrepot>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
           // modelBuilder.Entity<Entrepot>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
           ///领取人表
           
            #endregion

            #region 申请表-mzy
            modelBuilder.Entity<ChemistryMonad>().HasOptional(p => p.Z_Chemistry).WithMany().HasForeignKey(m => m.ChemistryId);
            modelBuilder.Entity<ChemistryMonad>().HasOptional(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
            modelBuilder.Entity<ChemistryMonad>().HasOptional(p => p.Supplier).WithMany().HasForeignKey(m => m.SupplierId);


            modelBuilder.Entity<FnishedProductMonad>().HasOptional(p => p.Z_FnishedProduct).WithMany().HasForeignKey(m => m.FnishedProductId);
            modelBuilder.Entity<FnishedProductMonad>().HasOptional(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
            modelBuilder.Entity<FnishedProductMonad>().HasOptional(p => p.Supplier).WithMany().HasForeignKey(m => m.SupplierId);

            modelBuilder.Entity<OfficeMonad>().HasOptional(p => p.Z_Office).WithMany().HasForeignKey(m => m.OfficeId);
            modelBuilder.Entity<OfficeMonad>().HasOptional(p => p.Applicant).WithMany().HasForeignKey(m => m.ApplicantId);
            modelBuilder.Entity<OfficeMonad>().HasOptional(p => p.Supplier).WithMany().HasForeignKey(m => m.SupplierId);


            #endregion


            #region  签核记录
            modelBuilder.Entity<FinshedProduct_User>().HasRequired(p => p.UserDetails).WithMany().HasForeignKey(m => m.user_Id);
            modelBuilder.Entity<FinshedProduct_User>().HasRequired(p => p.FnishedProductMonad).WithMany().HasForeignKey(m => m.FnishedProductId);

            modelBuilder.Entity<Office_User>().HasRequired(p => p.UserDetails).WithMany().HasForeignKey(m => m.user_Id);
            modelBuilder.Entity<Office_User>().HasRequired(p => p.OfficeMonad).WithMany().HasForeignKey(m => m.OfficeId);

            modelBuilder.Entity<Chmistry_User>().HasRequired(p => p.UserDetails).WithMany().HasForeignKey(m => m.user_Id);
            modelBuilder.Entity<Chmistry_User>().HasRequired(p => p.ChemistryMonad).WithMany().HasForeignKey(m => m.ChemistryId);
            #endregion

            #region 申请记录表-mzy

            ///领取人表
            modelBuilder.Entity<Raw_UserDetils>().HasRequired(p => p.z_Raw).WithMany().HasForeignKey(m => m.RawId);
            modelBuilder.Entity<Raw_UserDetils>().HasOptional(p => p.userDetails).WithMany().HasForeignKey(m => m.entrepotid);
            modelBuilder.Entity<Raw_UserDetils>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);

            modelBuilder.Entity<Chemistry_UserDetils>().HasRequired(p => p.Z_Chemistry).WithMany().HasForeignKey(m => m.ChemistryId);
            modelBuilder.Entity<Chemistry_UserDetils>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);

            modelBuilder.Entity<FnishedProduct_UserDetils>().HasRequired(p => p.Z_FnishedProduct).WithMany().HasForeignKey(m => m.FnishedProductId);
            modelBuilder.Entity<FnishedProduct_UserDetils>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);


            modelBuilder.Entity<Office_UsrDetils>().HasRequired(p => p.Z_Office).WithMany().HasForeignKey(m => m.OfficeId);
            modelBuilder.Entity<Office_UsrDetils>().HasRequired(p => p.userDetails).WithMany().HasForeignKey(m => m.User_id);
            #endregion
            #region mzy专用
            modelBuilder.Entity<Z_Supplies>().HasRequired(p => p.Z_SuppliesType).WithMany().HasForeignKey(m => m.Z_SuppliesTypeid);
            modelBuilder.Entity<Z_Supplies>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_Supplies>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);


            modelBuilder.Entity<Z_Chemistry>().HasRequired(p => p.Z_ChemistryType).WithMany().HasForeignKey(m => m.Z_ChemistryTypeid);
            modelBuilder.Entity<Z_Chemistry>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_Chemistry>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);

            modelBuilder.Entity<Z_FnishedProduct>().HasRequired(p => p.Z_FinshedProductType).WithMany().HasForeignKey(m => m.Z_FinshedProductTypeid);
            modelBuilder.Entity<Z_FnishedProduct>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_FnishedProduct>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);

            modelBuilder.Entity<Z_Raw>().HasRequired(p => p.Z_RowType).WithMany().HasForeignKey(m => m.Z_RowTypeid);
            modelBuilder.Entity<Z_Raw>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_Raw>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);


            modelBuilder.Entity<Z_Office>().HasRequired(p => p.Z_OfficeType).WithMany().HasForeignKey(m => m.Z_OfficeTypeid);
            modelBuilder.Entity<Z_Office>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_Office>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);

            modelBuilder.Entity<Z_MaterialCode>().HasRequired(p => p.Z_RowType).WithMany().HasForeignKey(m => m.Z_RowTypeid);
            modelBuilder.Entity<Z_MaterialCode>().HasOptional(p => p.Company).WithMany().HasForeignKey(m => m.CompanyId);
            modelBuilder.Entity<Z_MaterialCode>().HasOptional(p => p.WarehousingType).WithMany().HasForeignKey(m => m.WarehousingTypeId);

          
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}