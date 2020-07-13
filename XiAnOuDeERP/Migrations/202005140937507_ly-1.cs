namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Leave",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ApprovalType = c.Int(nullable: false),
                        LeaveType = c.Int(nullable: false),
                        ApprovalDesc = c.String(),
                        ApprovelId = c.Long(),
                        Desc = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApprovelId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApprovelId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        RealName = c.String(maxLength: 255),
                        DateOfBirth = c.DateTime(),
                        SexType = c.Int(nullable: false),
                        IdCard = c.String(maxLength: 255),
                        Nation = c.String(maxLength: 255),
                        Education = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        WeiXin = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        PortraitPath = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 80),
                        UserType = c.Int(nullable: false),
                        DepartmentId = c.Long(),
                        PositionType = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.OutOfStock",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        Number = c.Double(nullable: false),
                        ProjectId = c.Long(),
                        ApprovalType = c.Int(nullable: false),
                        ApplicantId = c.Long(nullable: false),
                        WarehouseKeeperId = c.Long(),
                        IsCollarUse = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.RawMaterial", t => t.RawMaterialId)
                .ForeignKey("dbo.UserDetails", t => t.WarehouseKeeperId)
                .Index(t => t.RawMaterialId)
                .Index(t => t.ProjectId)
                .Index(t => t.ApplicantId)
                .Index(t => t.WarehouseKeeperId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        Number = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RawMaterial",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        Abbreviation = c.String(maxLength: 255),
                        BeCommonlyCalled = c.String(maxLength: 255),
                        CASNumber = c.String(maxLength: 255),
                        WarehousingType = c.Int(),
                        RawMaterialType = c.String(maxLength: 255),
                        MolecularWeight = c.String(maxLength: 255),
                        MolecularFormula = c.String(maxLength: 255),
                        StructuralFormula = c.String(maxLength: 255),
                        AppearanceState = c.String(maxLength: 255),
                        EntryPersonId = c.Long(),
                        DepartmentId = c.Long(),
                        ProjectId = c.Long(),
                        WarehouseKeeperId = c.Long(),
                        CompanyId = c.Long(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.UserDetails", t => t.EntryPersonId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.UserDetails", t => t.WarehouseKeeperId)
                .Index(t => t.EntryPersonId)
                .Index(t => t.DepartmentId)
                .Index(t => t.ProjectId)
                .Index(t => t.WarehouseKeeperId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Overtime",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Duration = c.String(),
                        OverTimeDate = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ApprovalType = c.Int(nullable: false),
                        DepartmentLeaderId = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.DepartmentLeaderId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DepartmentLeaderId);
            
            CreateTable(
                "dbo.PersonnelRts",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Position = c.String(),
                        AddbyId = c.Long(nullable: false),
                        ApprovalType = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        RecruitedNumber = c.Int(nullable: false),
                        SexType = c.Int(nullable: false),
                        Age = c.String(),
                        SkillRequirements = c.String(),
                        ReviewedById = c.Long(),
                        Education = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.AddbyId)
                .ForeignKey("dbo.UserDetails", t => t.ReviewedById)
                .Index(t => t.AddbyId)
                .Index(t => t.ReviewedById);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Purpose = c.String(maxLength: 255),
                        ExpectArrivalTime = c.DateTime(),
                        ApplyNumber = c.Double(),
                        QuasiPurchaseNumber = c.Double(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Enclosure = c.String(maxLength: 255),
                        ApplicantRemarks = c.String(maxLength: 255),
                        ApplyTime = c.DateTime(),
                        PurchaseTime = c.DateTime(),
                        WaybillNumber = c.String(maxLength: 255),
                        PurchaseContract = c.String(maxLength: 255),
                        ArrivalTime = c.DateTime(),
                        ApprovalType = c.Int(),
                        ApprovalDesc = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        ApplicantId = c.Long(),
                        RawMaterialId = c.Long(),
                        ProjectId = c.Long(),
                        DepartmentLeaderId = c.Long(),
                        GeneralManagerId = c.Long(),
                        PurchasingSpecialistId = c.Long(),
                        SupplierId = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.UserDetails", t => t.DepartmentLeaderId)
                .ForeignKey("dbo.UserDetails", t => t.GeneralManagerId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.UserDetails", t => t.PurchasingSpecialistId)
                .ForeignKey("dbo.RawMaterial", t => t.RawMaterialId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .Index(t => t.ApplicantId)
                .Index(t => t.RawMaterialId)
                .Index(t => t.ProjectId)
                .Index(t => t.DepartmentLeaderId)
                .Index(t => t.GeneralManagerId)
                .Index(t => t.PurchasingSpecialistId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Score",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Fraction = c.Int(nullable: false),
                        SupplierId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.StorageRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RawMaterialId = c.Long(),
                        Number = c.Double(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RawMaterial", t => t.RawMaterialId)
                .Index(t => t.RawMaterialId);
            
            CreateTable(
                "dbo.Wage",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        BasePay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PostSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeritPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConfidentialSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeniorityPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EducationSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrafficAndMealSupplement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BonusPaidIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OvertimeExpenses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalManagementSystem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WagesPayable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EndowmentInsurance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedicalInsurance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LargeInsurance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnemploymentInsurance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxDeductible = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaidWages = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SignId = c.Long(nullable: false),
                        Desc = c.String(maxLength: 255),
                        TotalBeforeTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentIssueCurrentTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousPeriodWithholdingTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentIssueWithholdingTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.SignId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SignId);
            
            CreateTable(
                "dbo.Warehousing",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Number = c.Double(nullable: false),
                        ApplicantId = c.Long(nullable: false),
                        WarehouseKeeperId = c.Long(),
                        ApprovalType = c.Int(nullable: false),
                        PurchaseId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.Purchase", t => t.PurchaseId)
                .ForeignKey("dbo.UserDetails", t => t.WarehouseKeeperId)
                .Index(t => t.ApplicantId)
                .Index(t => t.WarehouseKeeperId)
                .Index(t => t.PurchaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehousing", "WarehouseKeeperId", "dbo.UserDetails");
            DropForeignKey("dbo.Warehousing", "PurchaseId", "dbo.Purchase");
            DropForeignKey("dbo.Warehousing", "ApplicantId", "dbo.UserDetails");
            DropForeignKey("dbo.Wage", "UserId", "dbo.User");
            DropForeignKey("dbo.Wage", "SignId", "dbo.User");
            DropForeignKey("dbo.StorageRoom", "RawMaterialId", "dbo.RawMaterial");
            DropForeignKey("dbo.Score", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Purchase", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Purchase", "RawMaterialId", "dbo.RawMaterial");
            DropForeignKey("dbo.Purchase", "PurchasingSpecialistId", "dbo.UserDetails");
            DropForeignKey("dbo.Purchase", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Purchase", "GeneralManagerId", "dbo.UserDetails");
            DropForeignKey("dbo.Purchase", "DepartmentLeaderId", "dbo.UserDetails");
            DropForeignKey("dbo.Purchase", "ApplicantId", "dbo.UserDetails");
            DropForeignKey("dbo.PersonnelRts", "ReviewedById", "dbo.UserDetails");
            DropForeignKey("dbo.PersonnelRts", "AddbyId", "dbo.UserDetails");
            DropForeignKey("dbo.Overtime", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.Overtime", "DepartmentLeaderId", "dbo.UserDetails");
            DropForeignKey("dbo.OutOfStock", "WarehouseKeeperId", "dbo.UserDetails");
            DropForeignKey("dbo.OutOfStock", "RawMaterialId", "dbo.RawMaterial");
            DropForeignKey("dbo.RawMaterial", "WarehouseKeeperId", "dbo.UserDetails");
            DropForeignKey("dbo.RawMaterial", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.RawMaterial", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.RawMaterial", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.RawMaterial", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.OutOfStock", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.OutOfStock", "ApplicantId", "dbo.UserDetails");
            DropForeignKey("dbo.Leave", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.Leave", "ApprovelId", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Warehousing", new[] { "PurchaseId" });
            DropIndex("dbo.Warehousing", new[] { "WarehouseKeeperId" });
            DropIndex("dbo.Warehousing", new[] { "ApplicantId" });
            DropIndex("dbo.Wage", new[] { "SignId" });
            DropIndex("dbo.Wage", new[] { "UserId" });
            DropIndex("dbo.StorageRoom", new[] { "RawMaterialId" });
            DropIndex("dbo.Score", new[] { "SupplierId" });
            DropIndex("dbo.Purchase", new[] { "SupplierId" });
            DropIndex("dbo.Purchase", new[] { "PurchasingSpecialistId" });
            DropIndex("dbo.Purchase", new[] { "GeneralManagerId" });
            DropIndex("dbo.Purchase", new[] { "DepartmentLeaderId" });
            DropIndex("dbo.Purchase", new[] { "ProjectId" });
            DropIndex("dbo.Purchase", new[] { "RawMaterialId" });
            DropIndex("dbo.Purchase", new[] { "ApplicantId" });
            DropIndex("dbo.PersonnelRts", new[] { "ReviewedById" });
            DropIndex("dbo.PersonnelRts", new[] { "AddbyId" });
            DropIndex("dbo.Overtime", new[] { "DepartmentLeaderId" });
            DropIndex("dbo.Overtime", new[] { "UserId" });
            DropIndex("dbo.RawMaterial", new[] { "CompanyId" });
            DropIndex("dbo.RawMaterial", new[] { "WarehouseKeeperId" });
            DropIndex("dbo.RawMaterial", new[] { "ProjectId" });
            DropIndex("dbo.RawMaterial", new[] { "DepartmentId" });
            DropIndex("dbo.RawMaterial", new[] { "EntryPersonId" });
            DropIndex("dbo.OutOfStock", new[] { "WarehouseKeeperId" });
            DropIndex("dbo.OutOfStock", new[] { "ApplicantId" });
            DropIndex("dbo.OutOfStock", new[] { "ProjectId" });
            DropIndex("dbo.OutOfStock", new[] { "RawMaterialId" });
            DropIndex("dbo.User", new[] { "DepartmentId" });
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropIndex("dbo.Leave", new[] { "ApprovelId" });
            DropIndex("dbo.Leave", new[] { "UserId" });
            DropTable("dbo.Warehousing");
            DropTable("dbo.Wage");
            DropTable("dbo.StorageRoom");
            DropTable("dbo.Score");
            DropTable("dbo.Supplier");
            DropTable("dbo.Purchase");
            DropTable("dbo.PersonnelRts");
            DropTable("dbo.Overtime");
            DropTable("dbo.RawMaterial");
            DropTable("dbo.Project");
            DropTable("dbo.OutOfStock");
            DropTable("dbo.User");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Leave");
            DropTable("dbo.Department");
            DropTable("dbo.Company");
        }
    }
}
