﻿// <auto-generated />
using System;
using BlogApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlogApi.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlogApi.Entity.AccessTokenEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("expiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("BlackTokenList");
                });

            modelBuilder.Entity("BlogApi.Entity.AuthorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("likesCount")
                        .HasColumnType("integer");

                    b.Property<int>("postsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BlogApi.Entity.CommentEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentCommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("authorId")
                        .HasColumnType("uuid");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("deleteDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("modifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("subCommentsCount")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BlogApi.Entity.CommunityEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("subscribersCount")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("BlogApi.Entity.LikeEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BlogApi.Entity.PostEntity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("addressId")
                        .HasColumnType("uuid");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("authorId")
                        .HasColumnType("uuid");

                    b.Property<int>("commentsCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("communityId")
                        .HasColumnType("uuid");

                    b.Property<string>("communityName")
                        .HasColumnType("text");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("likesCount")
                        .HasColumnType("integer");

                    b.Property<int>("readingTime")
                        .HasColumnType("integer");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("AuthorEntityId");

                    b.HasIndex("communityId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogApi.Entity.PostTagsEntity", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("BlogApi.Entity.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogApi.Entity.UserCommunityEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "CommunityId");

                    b.HasIndex("CommunityId");

                    b.ToTable("UserCommunities");
                });

            modelBuilder.Entity("BlogApi.Entity.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PostEntityTagEntity", b =>
                {
                    b.Property<Guid>("Postsid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("tagsId")
                        .HasColumnType("uuid");

                    b.HasKey("Postsid", "tagsId");

                    b.HasIndex("tagsId");

                    b.ToTable("PostEntityTagEntity");
                });

            modelBuilder.Entity("BlogApi.Entity.AuthorEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.UserEntity", "User")
                        .WithOne("Author")
                        .HasForeignKey("BlogApi.Entity.AuthorEntity", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogApi.Entity.CommentEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.CommentEntity", "ParentComment")
                        .WithMany()
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BlogApi.Entity.PostEntity", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Entity.UserEntity", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentComment");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogApi.Entity.LikeEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.PostEntity", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogApi.Entity.PostEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.AuthorEntity", null)
                        .WithMany("Posts")
                        .HasForeignKey("AuthorEntityId");

                    b.HasOne("BlogApi.Entity.CommunityEntity", "Community")
                        .WithMany()
                        .HasForeignKey("communityId");

                    b.Navigation("Community");
                });

            modelBuilder.Entity("BlogApi.Entity.PostTagsEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.PostEntity", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Entity.TagEntity", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("BlogApi.Entity.UserCommunityEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.CommunityEntity", "Community")
                        .WithMany("UserCommunities")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Entity.UserEntity", "User")
                        .WithMany("UserCommunities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PostEntityTagEntity", b =>
                {
                    b.HasOne("BlogApi.Entity.PostEntity", null)
                        .WithMany()
                        .HasForeignKey("Postsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Entity.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("tagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogApi.Entity.AuthorEntity", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("BlogApi.Entity.CommunityEntity", b =>
                {
                    b.Navigation("UserCommunities");
                });

            modelBuilder.Entity("BlogApi.Entity.PostEntity", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("BlogApi.Entity.UserEntity", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("Comments");

                    b.Navigation("UserCommunities");
                });
#pragma warning restore 612, 618
        }
    }
}
