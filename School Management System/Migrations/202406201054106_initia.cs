namespace School_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initia : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "RollNo", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ContactForms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ContactForms", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ContactForms", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.ContactForms", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.Notices", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.Notices", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "TeacherDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "Email", c => c.String());
            AlterColumn("dbo.Teachers", "TeacherDescription", c => c.String());
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String());
            AlterColumn("dbo.Notices", "Description", c => c.String());
            AlterColumn("dbo.Notices", "Subject", c => c.String());
            AlterColumn("dbo.ContactForms", "Message", c => c.String());
            AlterColumn("dbo.ContactForms", "Subject", c => c.String());
            AlterColumn("dbo.ContactForms", "Email", c => c.String());
            AlterColumn("dbo.ContactForms", "Name", c => c.String());
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "Address", c => c.String());
            AlterColumn("dbo.Students", "RollNo", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
        }
    }
}
