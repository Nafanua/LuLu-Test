namespace LULUTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropQuantityInBookTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookModels", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookModels", "Quantity");
        }
    }
}
