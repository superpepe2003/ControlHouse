namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientosCuentasCascada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovimientosModel", "CuentaId", "dbo.CuentaModel");
            AddForeignKey("dbo.MovimientosModel", "CuentaId", "dbo.CuentaModel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientosModel", "CuentaId", "dbo.CuentaModel");
            AddForeignKey("dbo.MovimientosModel", "CuentaId", "dbo.CuentaModel", "Id", cascadeDelete: true);
        }
    }
}
