namespace ControlHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hashtag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimientosModel", "HashTag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimientosModel", "HashTag");
        }
    }
}
