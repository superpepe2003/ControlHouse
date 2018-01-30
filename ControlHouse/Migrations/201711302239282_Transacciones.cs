namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transacciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransaccionModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        CuentaIdOrigen = c.Int(nullable: false),
                        CuentaIdDestino = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaModel", t => t.CuentaIdDestino, cascadeDelete: false)
                .ForeignKey("dbo.CuentaModel", t => t.CuentaIdOrigen, cascadeDelete: false)
                .Index(t => t.CuentaIdOrigen)
                .Index(t => t.CuentaIdDestino);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransaccionModel", "CuentaIdOrigen", "dbo.CuentaModel");
            DropForeignKey("dbo.TransaccionModel", "CuentaIdDestino", "dbo.CuentaModel");
            DropIndex("dbo.TransaccionModel", new[] { "CuentaIdDestino" });
            DropIndex("dbo.TransaccionModel", new[] { "CuentaIdOrigen" });
            DropTable("dbo.TransaccionModel");
        }
    }
}
