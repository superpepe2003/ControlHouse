namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoriaFija : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategoriaModel", "FijaMensualmente", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubCategoriaModel", "Monto", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategoriaModel", "Monto");
            DropColumn("dbo.SubCategoriaModel", "FijaMensualmente");
        }
    }
}
