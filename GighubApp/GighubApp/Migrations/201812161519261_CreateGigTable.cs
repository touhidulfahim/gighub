namespace GighubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Byte(nullable: false),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        GigId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(),
                        Artist_Id = c.String(maxLength: 128),
                        Genre_GenreId = c.Byte(),
                    })
                .PrimaryKey(t => t.GigId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreId)
                .Index(t => t.Artist_Id)
                .Index(t => t.Genre_GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "Genre_GenreId", "dbo.Genres");
            DropForeignKey("dbo.Gigs", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "Genre_GenreId" });
            DropIndex("dbo.Gigs", new[] { "Artist_Id" });
            DropTable("dbo.Gigs");
            DropTable("dbo.Genres");
        }
    }
}
