using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForThePeople.Migrations
{
    public partial class NotesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_AspNetUsers_ApplicationUserId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_ApplicationUserId",
                table: "Note");

            migrationBuilder.RenameColumn(
                name: "LegislatureId",
                table: "Note",
                newName: "ResultMember_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Note",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResultMember_Id",
                table: "Note",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegislationId",
                table: "Note",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Legislature",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legislature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Member_Id = table.Column<string>(nullable: false),
                    NoteId = table.Column<int>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Date_of_Birth = table.Column<string>(nullable: true),
                    LegislatureId = table.Column<string>(nullable: true),
                    ResultMember_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Member_Id);
                    table.ForeignKey(
                        name: "FK_Result_Legislature_LegislatureId",
                        column: x => x.LegislatureId,
                        principalTable: "Legislature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_Result_ResultMember_Id",
                        column: x => x.ResultMember_Id,
                        principalTable: "Result",
                        principalColumn: "Member_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Bill_Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Short_Title = table.Column<string>(nullable: true),
                    Sponsor_Title = table.Column<string>(nullable: true),
                    Sponsor_Name = table.Column<string>(nullable: true),
                    Sponsor_State = table.Column<string>(nullable: true),
                    Sponsor_Party = table.Column<string>(nullable: true),
                    CongressDotGov_Url = table.Column<string>(nullable: true),
                    Govtrack_Url = table.Column<string>(nullable: true),
                    Introduced_Date = table.Column<string>(nullable: true),
                    Last_Vote = table.Column<string>(nullable: true),
                    Primary_Subject = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Summary_Short = table.Column<string>(nullable: true),
                    Latest_Major_Action_Date = table.Column<string>(nullable: true),
                    Latest_Major_Action = table.Column<string>(nullable: true),
                    ResultMember_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Bill_Id);
                    table.ForeignKey(
                        name: "FK_Bill_Result_ResultMember_Id",
                        column: x => x.ResultMember_Id,
                        principalTable: "Result",
                        principalColumn: "Member_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Committee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ResultMember_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committee_Result_ResultMember_Id",
                        column: x => x.ResultMember_Id,
                        principalTable: "Result",
                        principalColumn: "Member_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Date_of_Birth = table.Column<DateTime>(nullable: false),
                    Chamber = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Seniority = table.Column<int>(nullable: false),
                    District = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Contact_Form = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Twitter_Account = table.Column<string>(nullable: true),
                    Facebook_Account = table.Column<string>(nullable: true),
                    Party = table.Column<string>(nullable: true),
                    ResultMember_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Result_ResultMember_Id",
                        column: x => x.ResultMember_Id,
                        principalTable: "Result",
                        principalColumn: "Member_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Chamber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Seniority = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Party = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Office = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Missed_Votes_Pct = table.Column<double>(nullable: false),
                    Votes_with_Party_Pct = table.Column<double>(nullable: false),
                    Contact_Form = table.Column<string>(nullable: true),
                    ResultMember_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Result_ResultMember_Id",
                        column: x => x.ResultMember_Id,
                        principalTable: "Result",
                        principalColumn: "Member_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "92034bef-c9ca-4ed6-8f21-3bfb7045d9fc", "AQAAAAEAACcQAAAAEPkFtPkHTwCl8HQbRM6yzkp/01bVhAjgnou57guPW7SmgoUfBH1doaFF0KiC80VhUA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Note_ResultMember_Id",
                table: "Note",
                column: "ResultMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ResultMember_Id",
                table: "Bill",
                column: "ResultMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Committee_ResultMember_Id",
                table: "Committee",
                column: "ResultMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ResultMember_Id",
                table: "Member",
                column: "ResultMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Result_LegislatureId",
                table: "Result",
                column: "LegislatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_NoteId",
                table: "Result",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_ResultMember_Id",
                table: "Result",
                column: "ResultMember_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ResultMember_Id",
                table: "Role",
                column: "ResultMember_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Result_ResultMember_Id",
                table: "Note",
                column: "ResultMember_Id",
                principalTable: "Result",
                principalColumn: "Member_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Result_ResultMember_Id",
                table: "Note");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Committee");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Legislature");

            migrationBuilder.DropIndex(
                name: "IX_Note_ResultMember_Id",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "LegislationId",
                table: "Note");

            migrationBuilder.RenameColumn(
                name: "ResultMember_Id",
                table: "Note",
                newName: "LegislatureId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Note",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Note",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LegislatureId",
                table: "Note",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63b54cdc-ebc6-4938-849c-28b4619d82ee", "AQAAAAEAACcQAAAAEOJkoFu2iLxcKivzB2wS/3bgm5DRRzjmd0LmFHbdR6TloRtAkUpthf1Vtzj7mKSg9Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Note_ApplicationUserId",
                table: "Note",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_AspNetUsers_ApplicationUserId",
                table: "Note",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
