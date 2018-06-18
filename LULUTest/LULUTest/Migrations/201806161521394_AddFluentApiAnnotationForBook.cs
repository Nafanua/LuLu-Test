namespace LULUTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFluentApiAnnotationForBook : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookModels", newName: "Books");
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
            RenameTable(name: "dbo.Books", newName: "BookModels");
        }
    }
}
