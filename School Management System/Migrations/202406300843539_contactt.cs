namespace School_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactForms", "MessageCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactForms", "MessageCount", c => c.Int(nullable: false));
        }
    }
}
