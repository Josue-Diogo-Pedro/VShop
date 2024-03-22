using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Products(Name, Price, Description, Stock, ImagemURL, CategoryId) 
                                   VALUES 
                                   ('Caderno', 7.55, 'Caderno', 10, 'caderno1.jpg', 1)");

            migrationBuilder.Sql(@"INSERT INTO Products(Name, Price, Description, Stock, ImagemURL, CategoryId) 
                                   VALUES 
                                   ('Lapis', 3.45, 'Lapis Preto', 20, 'lapis1.jpg', 1)");

            migrationBuilder.Sql(@"INSERT INTO Products(Name, Price, Description, Stock, ImagemURL, CategoryId) 
                                   VALUES 
                                   ('Clips', 5.33, 'Clips para papel', 50, 'clips1.jpg', 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FORM Products");
        }
    }
}
