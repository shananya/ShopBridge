namespace ShopBridge_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstInsert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopBridges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopBridges");
        }
    }
}
