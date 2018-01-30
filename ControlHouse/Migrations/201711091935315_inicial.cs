namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CuentaModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Saldo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovimientosModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        SubCategoriaId = c.Int(nullable: false),
                        CuentaId = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        Hora = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaModel", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.CuentaModel", t => t.CuentaId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategoriaModel", t => t.SubCategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId)
                .Index(t => t.SubCategoriaId)
                .Index(t => t.CuentaId);
            
            CreateTable(
                "dbo.SubCategoriaModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientosModel", "SubCategoriaId", "dbo.SubCategoriaModel");
            DropForeignKey("dbo.MovimientosModel", "CuentaId", "dbo.CuentaModel");
            DropForeignKey("dbo.MovimientosModel", "CategoriaId", "dbo.CategoriaModel");
            DropIndex("dbo.MovimientosModel", new[] { "CuentaId" });
            DropIndex("dbo.MovimientosModel", new[] { "SubCategoriaId" });
            DropIndex("dbo.MovimientosModel", new[] { "CategoriaId" });
            DropTable("dbo.SubCategoriaModel");
            DropTable("dbo.MovimientosModel");
            DropTable("dbo.CuentaModel");
            DropTable("dbo.CategoriaModel");
        }
    }
}
