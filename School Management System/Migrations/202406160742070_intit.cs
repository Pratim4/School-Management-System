namespace School_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Attendence_AttendenceId", "dbo.Attendences");
            DropForeignKey("dbo.Students", "Attendence_AttendenceId1", "dbo.Attendences");
            DropIndex("dbo.Students", new[] { "Attendence_AttendenceId" });
            DropIndex("dbo.Students", new[] { "Attendence_AttendenceId1" });
            DropColumn("dbo.Attendences", "StudentId");
            RenameColumn(table: "dbo.Attendences", name: "Student_StudentId", newName: "StudentId");
            RenameIndex(table: "dbo.Attendences", name: "IX_Student_StudentId", newName: "IX_StudentId");
            DropColumn("dbo.Students", "Attendence_AttendenceId");
            DropColumn("dbo.Students", "Attendence_AttendenceId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Attendence_AttendenceId1", c => c.Int());
            AddColumn("dbo.Students", "Attendence_AttendenceId", c => c.Int());
            RenameIndex(table: "dbo.Attendences", name: "IX_StudentId", newName: "IX_Student_StudentId");
            RenameColumn(table: "dbo.Attendences", name: "StudentId", newName: "Student_StudentId");
            AddColumn("dbo.Attendences", "StudentId", c => c.Int());
            CreateIndex("dbo.Students", "Attendence_AttendenceId1");
            CreateIndex("dbo.Students", "Attendence_AttendenceId");
            AddForeignKey("dbo.Students", "Attendence_AttendenceId1", "dbo.Attendences", "AttendenceId");
            AddForeignKey("dbo.Students", "Attendence_AttendenceId", "dbo.Attendences", "AttendenceId");
        }
    }
}
