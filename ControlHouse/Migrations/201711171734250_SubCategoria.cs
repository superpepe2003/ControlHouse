namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoria : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SubCategoriaModel", "CategoriaId");
            AddForeignKey("dbo.SubCategoriaModel", "CategoriaId", "dbo.CategoriaModel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategoriaModel", "CategoriaId", "dbo.CategoriaModel");
            DropIndex("dbo.SubCategoriaModel", new[] { "CategoriaId" });
        }
    }
}
