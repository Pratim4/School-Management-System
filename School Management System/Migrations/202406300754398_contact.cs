namespace School_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "MessageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "MessageCount");
        }
    }
}
