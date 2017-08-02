namespace RESTful_API_OAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitRESTful_API_DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        Roles_Id = c.Int(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.Roles_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.Users", "Roles_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Status_Id" });
            DropIndex("dbo.Users", new[] { "Roles_Id" });
            DropTable("dbo.Status");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
