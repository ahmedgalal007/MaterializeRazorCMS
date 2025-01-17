using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreStarter.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    BaseType = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnType = table.Column<string>(type: "TEXT", nullable: false),
                    Max = table.Column<int>(type: "INTEGER", nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryURI = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    KeywordURI = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    Schema = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LocalName = table.Column<string>(type: "TEXT", nullable: false),
                    TwoLettersCode = table.Column<string>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRTL = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostType_PostType_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PostType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedRole = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedPlan = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArticleUri = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryLocals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LanguageID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLocals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLocals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryLocals_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordLocals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    KeywordId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LanguageID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordLocals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeywordLocals_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KeywordLocals_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PostURI = table.Column<string>(type: "TEXT", nullable: true),
                    PostTypeID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_PostType_PostTypeID",
                        column: x => x.PostTypeID,
                        principalTable: "PostType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTypeAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TypeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Max = table.Column<int>(type: "INTEGER", nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: false),
                    IsNullable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsList = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsUnique = table.Column<bool>(type: "INTEGER", nullable: false),
                    PostTypeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypeAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTypeAttribute_Attributes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTypeAttribute_PostType_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLocals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    LanguageID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLocals_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLocals_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticleLanguage",
                columns: table => new
                {
                    ArticlesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguagesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLanguage", x => new { x.ArticlesId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_ArticleLanguage_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleLanguage_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticlePost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    PostID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArticleId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlePost_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticlePost_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TypeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValueNumber = table.Column<double>(type: "REAL", nullable: false),
                    ValueBoolean = table.Column<bool>(type: "INTEGER", nullable: false),
                    PostId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAttribute_Attributes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostAttribute_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostAttributeLocals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValueText = table.Column<string>(type: "TEXT", nullable: false),
                    PostAttributeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LanguageID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttributeLocals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAttributeLocals_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostAttributeLocals_PostAttribute_PostAttributeId",
                        column: x => x.PostAttributeId,
                        principalTable: "PostAttribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLanguage_LanguagesId",
                table: "ArticleLanguage",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePost_ArticleId",
                table: "ArticlePost",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePost_PostID",
                table: "ArticlePost",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocals_CategoryId",
                table: "CategoryLocals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocals_LanguageID",
                table: "CategoryLocals",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordLocals_KeywordId",
                table: "KeywordLocals",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordLocals_LanguageID",
                table: "KeywordLocals",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostTypeID",
                table: "Post",
                column: "PostTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttribute_PostId",
                table: "PostAttribute",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttribute_TypeID",
                table: "PostAttribute",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttributeLocals_LanguageID",
                table: "PostAttributeLocals",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttributeLocals_PostAttributeId",
                table: "PostAttributeLocals",
                column: "PostAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostType_ParentId",
                table: "PostType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTypeAttribute_PostTypeId",
                table: "PostTypeAttribute",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTypeAttribute_TypeID",
                table: "PostTypeAttribute",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocals_LanguageID",
                table: "UserLocals",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocals_UserId",
                table: "UserLocals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleLanguage");

            migrationBuilder.DropTable(
                name: "ArticlePost");

            migrationBuilder.DropTable(
                name: "CategoryLocals");

            migrationBuilder.DropTable(
                name: "KeywordLocals");

            migrationBuilder.DropTable(
                name: "PostAttributeLocals");

            migrationBuilder.DropTable(
                name: "PostTypeAttribute");

            migrationBuilder.DropTable(
                name: "UserLocals");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "PostAttribute");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "PostType");
        }
    }
}
