namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CuentasMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuentaModel", "Nombre", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuentaModel", "Nombre", c => c.String(nullable: false));
        }
    }
}
