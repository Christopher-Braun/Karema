namespace Mvc4WebRole.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class IngredientOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngredientModels", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IngredientModels", "Order");
        }
    }
}
