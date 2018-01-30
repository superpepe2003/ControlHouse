namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientosTipo1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MovimientosModel", "Tipo", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovimientosModel", "Tipo", c => c.Boolean(nullable: false));
        }
    }
}
