namespace School_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class late1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "IsNew", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "IsNew");
        }
    }
}
