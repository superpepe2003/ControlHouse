namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movimientosinhora : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MovimientosModel", "Fecha", c => c.DateTime());
            DropColumn("dbo.MovimientosModel", "Hora");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovimientosModel", "Hora", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MovimientosModel", "Fecha", c => c.DateTime(nullable: false));
        }
    }
}
