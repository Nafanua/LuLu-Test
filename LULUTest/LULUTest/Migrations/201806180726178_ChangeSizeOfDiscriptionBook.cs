namespace LULUTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSizeOfDiscriptionBook : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
