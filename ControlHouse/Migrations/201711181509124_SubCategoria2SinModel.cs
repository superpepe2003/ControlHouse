namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoria2SinModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SubCategoriaModel", name: "CategoriaModelId", newName: "CategoriaId");
            RenameColumn(table: "dbo.MovimientosModel", name: "CuentaModelId", newName: "CuentaId");
            RenameColumn(table: "dbo.MovimientosModel", name: "CategoriaModelId", newName: "CategoriaId");
            RenameColumn(table: "dbo.MovimientosModel", name: "SubCategoriaModelId", newName: "SubCategoriaId");
            RenameIndex(table: "dbo.SubCategoriaModel", name: "IX_CategoriaModelId", newName: "IX_CategoriaId");
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_CategoriaModelId", newName: "IX_CategoriaId");
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_SubCategoriaModelId", newName: "IX_SubCategoriaId");
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_CuentaModelId", newName: "IX_CuentaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_CuentaId", newName: "IX_CuentaModelId");
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_SubCategoriaId", newName: "IX_SubCategoriaModelId");
            RenameIndex(table: "dbo.MovimientosModel", name: "IX_CategoriaId", newName: "IX_CategoriaModelId");
            RenameIndex(table: "dbo.SubCategoriaModel", name: "IX_CategoriaId", newName: "IX_CategoriaModelId");
            RenameColumn(table: "dbo.MovimientosModel", name: "SubCategoriaId", newName: "SubCategoriaModelId");
            RenameColumn(table: "dbo.MovimientosModel", name: "CategoriaId", newName: "CategoriaModelId");
            RenameColumn(table: "dbo.MovimientosModel", name: "CuentaId", newName: "CuentaModelId");
            RenameColumn(table: "dbo.SubCategoriaModel", name: "CategoriaId", newName: "CategoriaModelId");
        }
    }
}
