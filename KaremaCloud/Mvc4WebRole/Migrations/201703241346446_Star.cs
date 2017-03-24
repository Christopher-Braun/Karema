namespace Mvc4WebRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Star : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecipeModels", "Star", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecipeModels", "Star");
        }
    }
}
