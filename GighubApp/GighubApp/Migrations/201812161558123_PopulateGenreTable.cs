namespace GighubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (GenreId, GenreName) VALUES(1, 'Blues')");
            Sql("INSERT INTO Genres (GenreId, GenreName) VALUES(2, 'Folk')");
            Sql("INSERT INTO Genres (GenreId, GenreName) VALUES(3, 'Jazz')");
            Sql("INSERT INTO Genres (GenreId, GenreName) VALUES(4, 'Rock')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres Where GenreId IN (1,2,3,4)");
        }
    }
}
