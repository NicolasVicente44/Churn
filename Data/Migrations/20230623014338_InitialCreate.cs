using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Churn.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Account_InterestRate = table.Column<double>(type: "float", nullable: true),
                    MaximumWithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditCard_InterestRate = table.Column<double>(type: "float", nullable: true),
                    MinimumPaymentAmount = table.Column<double>(type: "float", nullable: true),
                    AnnualFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalanceTransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumInvestmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReturnRate = table.Column<double>(type: "float", nullable: true),
                    InvestmentTerm = table.Column<int>(type: "int", nullable: true),
                    Loan_InterestRate = table.Column<double>(type: "float", nullable: true),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanTermMonths = table.Column<int>(type: "int", nullable: true),
                    Collateral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterestRate = table.Column<double>(type: "float", nullable: true),
                    MortgageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MortgageTermYears = table.Column<int>(type: "int", nullable: true),
                    DownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
