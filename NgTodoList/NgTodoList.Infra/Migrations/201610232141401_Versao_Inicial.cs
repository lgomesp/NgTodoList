namespace NgTodoList.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versao_Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 160),
                        Done = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 160),
                        Password = c.String(nullable: false, maxLength: 32, fixedLength: true),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "IX_USER_EMAIL");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todo", "UserId", "dbo.User");
            DropIndex("dbo.User", "IX_USER_EMAIL");
            DropIndex("dbo.Todo", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Todo");
        }
    }
}
