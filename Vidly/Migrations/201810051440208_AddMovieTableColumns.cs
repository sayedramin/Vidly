namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieTableColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genre", c => c.String(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "AddedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
            Sql("UPDATE Movies SET Genre ='Romantic', ReleaseDate='01/01/1990', AddedDate='01/01/2018', NumberInStock=102  WHERE Id=1 ");
            Sql("UPDATE Movies SET Genre ='Action', ReleaseDate='02/02/2000', AddedDate='02/02/2018', NumberInStock=103 WHERE Id=2 ");
            Sql("UPDATE Movies SET Genre ='Horror', ReleaseDate='03/03/2010', AddedDate='03/03/2018', NumberInStock=104 WHERE Id=3 ");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String());
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "AddedDate");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "Genre");
        }
    }
}
