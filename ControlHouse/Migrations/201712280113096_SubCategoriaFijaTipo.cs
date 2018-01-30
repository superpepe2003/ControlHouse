namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoriaFijaTipo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategoriaModel", "Tipo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategoriaModel", "Tipo");
        }
    }
}
