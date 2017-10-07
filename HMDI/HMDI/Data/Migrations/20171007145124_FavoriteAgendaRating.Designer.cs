using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HMDI.Data;
using HMDI.Entities;

namespace HMDI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171007145124_FavoriteAgendaRating")]
    partial class FavoriteAgendaRating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HMDI.Entities.Agenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgendaCategoryId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AgendaCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Agendas");
                });

            modelBuilder.Entity("HMDI.Entities.AgendaCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AgendaCategories");
                });

            modelBuilder.Entity("HMDI.Entities.AgendaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgendaId");

                    b.Property<string>("Todo");

                    b.HasKey("Id");

                    b.HasIndex("AgendaId");

                    b.ToTable("AgendaItems");
                });

            modelBuilder.Entity("HMDI.Entities.AgendaTag", b =>
                {
                    b.Property<int>("AgendaId");

                    b.Property<int>("TagId");

                    b.HasKey("AgendaId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("AgendaTag");
                });

            modelBuilder.Entity("HMDI.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HMDI.Entities.Checklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DueDate");

                    b.Property<DateTime?>("FinishedAt");

                    b.Property<bool>("IsFinished");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("HMDI.Entities.ChecklistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CheckedAt");

                    b.Property<int>("ChecklistId");

                    b.Property<bool>("IsChecked");

                    b.Property<string>("Todo");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("HMDI.Entities.FavoriteAgenda", b =>
                {
                    b.Property<int>("AgendaId");

                    b.Property<string>("UserId");

                    b.Property<int>("Grade");

                    b.Property<bool>("HasRated");

                    b.HasKey("AgendaId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteAgenda");
                });

            modelBuilder.Entity("HMDI.Entities.Rating", b =>
                {
                    b.Property<int>("Id");

                    b.Property<double>("Avg");

                    b.Property<int>("Five");

                    b.Property<int>("Four");

                    b.Property<int>("One");

                    b.Property<int>("Three");

                    b.Property<int>("TotalVotes");

                    b.Property<int>("Two");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("HMDI.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgendaId");

                    b.Property<string>("Text");

                    b.Property<DateTime>("TimeCreated");

                    b.Property<string>("UserID");

                    b.HasKey("Id");

                    b.HasIndex("AgendaId");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HMDI.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HMDI.Entities.Agenda", b =>
                {
                    b.HasOne("HMDI.Entities.AgendaCategory", "AgendaCategory")
                        .WithMany("Agendas")
                        .HasForeignKey("AgendaCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HMDI.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HMDI.Entities.AgendaCategory", b =>
                {
                    b.HasOne("HMDI.Entities.ApplicationUser", "User")
                        .WithMany("AgendaCategories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HMDI.Entities.AgendaItem", b =>
                {
                    b.HasOne("HMDI.Entities.Agenda", "Agenda")
                        .WithMany("Items")
                        .HasForeignKey("AgendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HMDI.Entities.AgendaTag", b =>
                {
                    b.HasOne("HMDI.Entities.Agenda", "Agenda")
                        .WithMany("AgendaTags")
                        .HasForeignKey("AgendaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HMDI.Entities.Tag", "Tag")
                        .WithMany("AgendaTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HMDI.Entities.Checklist", b =>
                {
                    b.HasOne("HMDI.Entities.ApplicationUser", "User")
                        .WithMany("Checklists")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HMDI.Entities.ChecklistItem", b =>
                {
                    b.HasOne("HMDI.Entities.Checklist", "Checklist")
                        .WithMany("Items")
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HMDI.Entities.FavoriteAgenda", b =>
                {
                    b.HasOne("HMDI.Entities.Agenda", "Agenda")
                        .WithMany("Favorites")
                        .HasForeignKey("AgendaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HMDI.Entities.ApplicationUser", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HMDI.Entities.Rating", b =>
                {
                    b.HasOne("HMDI.Entities.Agenda", "Agenda")
                        .WithOne("Rating")
                        .HasForeignKey("HMDI.Entities.Rating", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HMDI.Entities.Review", b =>
                {
                    b.HasOne("HMDI.Entities.Agenda", "Agenda")
                        .WithMany("Reviews")
                        .HasForeignKey("AgendaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HMDI.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HMDI.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HMDI.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HMDI.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
