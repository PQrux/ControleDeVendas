using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleDeVendasAPI.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Produto_Produtoid1",
                table: "VendaProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Venda_Vendaid1",
                table: "VendaProduto");

            migrationBuilder.DropIndex(
                name: "IX_VendaProduto_Produtoid1",
                table: "VendaProduto");

            migrationBuilder.DropIndex(
                name: "IX_VendaProduto_Vendaid1",
                table: "VendaProduto");

            migrationBuilder.DropColumn(
                name: "Produtoid1",
                table: "VendaProduto");

            migrationBuilder.DropColumn(
                name: "Vendaid1",
                table: "VendaProduto");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Venda",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_Vendaid",
                table: "VendaProduto",
                column: "Vendaid");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Produto_Produtoid",
                table: "VendaProduto",
                column: "Produtoid",
                principalTable: "Produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Venda_Vendaid",
                table: "VendaProduto",
                column: "Vendaid",
                principalTable: "Venda",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Produto_Produtoid",
                table: "VendaProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_VendaProduto_Venda_Vendaid",
                table: "VendaProduto");

            migrationBuilder.DropIndex(
                name: "IX_VendaProduto_Vendaid",
                table: "VendaProduto");

            migrationBuilder.AddColumn<string>(
                name: "Produtoid1",
                table: "VendaProduto",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Vendaid1",
                table: "VendaProduto",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Venda",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_Produtoid1",
                table: "VendaProduto",
                column: "Produtoid1");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_Vendaid1",
                table: "VendaProduto",
                column: "Vendaid1");

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Produto_Produtoid1",
                table: "VendaProduto",
                column: "Produtoid1",
                principalTable: "Produto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendaProduto_Venda_Vendaid1",
                table: "VendaProduto",
                column: "Vendaid1",
                principalTable: "Venda",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
