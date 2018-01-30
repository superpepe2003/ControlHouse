namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimientosCuentasCascada2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovimientosModel", "CategoriaId", "dbo.CategoriaModel");
            DropForeignKey("dbo.MovimientosModel", "SubCategoriaId", "dbo.SubCategoriaModel");
            AddForeignKey("dbo.MovimientosModel", "CategoriaId", "dbo.CategoriaModel", "Id");
            AddForeignKey("dbo.MovimientosModel", "SubCategoriaId", "dbo.SubCategoriaModel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimientosModel", "SubCategoriaId", "dbo.SubCategoriaModel");
            DropForeignKey("dbo.MovimientosModel", "CategoriaId", "dbo.CategoriaModel");
            AddForeignKey("dbo.MovimientosModel", "SubCategoriaId", "dbo.SubCategoriaModel", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovimientosModel", "CategoriaId", "dbo.CategoriaModel", "Id", cascadeDelete: true);
        }
    }
}
