namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movimientosinhora2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MovimientosModel", "Fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovimientosModel", "Fecha", c => c.DateTime());
        }
    }
}
