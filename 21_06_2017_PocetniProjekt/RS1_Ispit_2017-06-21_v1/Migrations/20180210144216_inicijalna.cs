using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_2017_06_21_v1.Migrations
{
    public partial class inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImePrezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucenik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImePrezime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucenik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavnikId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odjeljenje_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UpisUOdjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUDnevniku = table.Column<int>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: false),
                    OpciUspjeh = table.Column<int>(nullable: false),
                    UcenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisUOdjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Ucenik_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "Ucenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspitStavka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<float>(nullable: true),
                    MaturskiIspitId = table.Column<int>(nullable: false),
                    Oslobodjen = table.Column<bool>(nullable: false),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspitStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_MaturskiIspit_MaturskiIspitId",
                        column: x => x.MaturskiIspitId,
                        principalTable: "MaturskiIspit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "UpisUOdjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_NastavnikId",
                table: "MaturskiIspit",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_OdjeljenjeId",
                table: "MaturskiIspit",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_MaturskiIspitId",
                table: "MaturskiIspitStavka",
                column: "MaturskiIspitId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_UpisUOdjeljenjeId",
                table: "MaturskiIspitStavka",
                column: "UpisUOdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Odjeljenje_NastavnikId",
                table: "Odjeljenje",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_UcenikId",
                table: "UpisUOdjeljenje",
                column: "UcenikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaturskiIspitStavka");

            migrationBuilder.DropTable(
                name: "MaturskiIspit");

            migrationBuilder.DropTable(
                name: "UpisUOdjeljenje");

            migrationBuilder.DropTable(
                name: "Odjeljenje");

            migrationBuilder.DropTable(
                name: "Ucenik");

            migrationBuilder.DropTable(
                name: "Nastavnik");
        }
    }
}
