namespace Classroom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeEntityPropertiesNonNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Class", "ClassName", c => c.String(nullable: false));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "LastName", c => c.String());
            AlterColumn("dbo.Student", "FirstName", c => c.String());
            AlterColumn("dbo.Class", "ClassName", c => c.String());
        }
    }
}
