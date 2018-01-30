namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientosTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientosModel", "Tipo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientosModel", "Tipo");
        }
    }
}
