﻿// <auto-generated />
using System;
using AspnetCoreStarter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspnetCoreStarter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241205232742_Initialization")]
    partial class Initialization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("ArticleLang", b =>
                {
                    b.Property<Guid>("ArticlesId")
                        .HasColumnType("TEXT");

                    b.Property<int>("LanguagesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticlesId", "LanguagesId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("ArticleLang");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Articles.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArticleUri")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Articles.ArticlePost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PostId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("PostId");

                    b.ToTable("ArticlePost");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryURI")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Keywords.Keyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("KeywordURI")
                        .HasColumnType("TEXT");

                    b.Property<string>("Schema")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Lang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRTL")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.CategoryLocals", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<int>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CategoryLocals");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.KeywordLocals", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("KeywordId")
                        .HasColumnType("TEXT");

                    b.Property<int>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("KeywordId");

                    b.HasIndex("LanguageId");

                    b.ToTable("KeywordLocals");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.PostAttributeLocals", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PostAttributeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PostAttributeId");

                    b.ToTable("PostAttributeLocals");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.UserLocals", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLocals");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.Attribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BaseType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Max")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Min")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReturnType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PostTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostURI")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostTypeId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ValueBoolean")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("ValueNumber")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("TypeId");

                    b.ToTable("PostAttribute");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PostType");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostTypeAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsList")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNullable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUnique")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Max")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Min")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PostTypeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostTypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("PostTypeAttribute");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SelectedPlan")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelectedRole")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ArticleLang", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Articles.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspnetCoreStarter.Entities.Lang", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Articles.Article", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Articles.ArticlePost", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Articles.Article", null)
                        .WithMany("ArticlePosts")
                        .HasForeignKey("ArticleId");

                    b.HasOne("AspnetCoreStarter.Entities.Posts.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.CategoryLocals", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Categories.Category", null)
                        .WithMany("Locales")
                        .HasForeignKey("CategoryId");

                    b.HasOne("AspnetCoreStarter.Entities.Lang", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.KeywordLocals", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Keywords.Keyword", null)
                        .WithMany("Locales")
                        .HasForeignKey("KeywordId");

                    b.HasOne("AspnetCoreStarter.Entities.Lang", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.PostAttributeLocals", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Lang", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspnetCoreStarter.Entities.Posts.PostAttribute", null)
                        .WithMany("Locales")
                        .HasForeignKey("PostAttributeId");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Locals.UserLocals", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Lang", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspnetCoreStarter.Entities.User", null)
                        .WithMany("Locales")
                        .HasForeignKey("UserId");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.Post", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Posts.PostType", "PostType")
                        .WithMany()
                        .HasForeignKey("PostTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostType");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostAttribute", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Posts.Post", null)
                        .WithMany("Attributes")
                        .HasForeignKey("PostId");

                    b.HasOne("AspnetCoreStarter.Entities.Posts.Attribute", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostTypeAttribute", b =>
                {
                    b.HasOne("AspnetCoreStarter.Entities.Posts.PostType", null)
                        .WithMany("Attributes")
                        .HasForeignKey("PostTypeId");

                    b.HasOne("AspnetCoreStarter.Entities.Posts.Attribute", "Type")
                        .WithMany("PostTypeAttributes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Articles.Article", b =>
                {
                    b.Navigation("ArticlePosts");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Categories.Category", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Keywords.Keyword", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.Attribute", b =>
                {
                    b.Navigation("PostTypeAttributes");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.Post", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostAttribute", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.Posts.PostType", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("AspnetCoreStarter.Entities.User", b =>
                {
                    b.Navigation("Locales");
                });
#pragma warning restore 612, 618
        }
    }
}
