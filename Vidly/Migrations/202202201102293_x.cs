namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Movies SET NumberAvailable=NumberInStock");
        }
        
        public override void Down()
        {
        }
    }
}
