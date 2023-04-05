using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using System.Reflection.Metadata;

namespace VMMCStockManagement.Infrastructure.DbContexts
{
	public class VMMCStockManagementDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
	   UserRole, IdentityUserLogin<string>,
	   IdentityRoleClaim<string>, IdentityUserToken<string>>
	{

		public VMMCStockManagementDbContext(DbContextOptions<VMMCStockManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>(b =>
			{
				b.ToTable("AspNetUsers");
				b.HasMany(u => u.UserRoles)
				 .WithOne(ur => ur.User)
				 .HasForeignKey(ur => ur.UserId)
				 .IsRequired();
			});

			builder.Entity<Role>(role =>
			{
				role.ToTable("AspNetRoles");
				role.HasKey(r => r.Id);
				role.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
				role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

				role.Property(u => u.Name).HasMaxLength(256);
				role.Property(u => u.NormalizedName).HasMaxLength(256);

				role.HasMany<UserRole>()
					.WithOne(ur => ur.Role)
					.HasForeignKey(ur => ur.RoleId)
					.IsRequired();
				role.HasMany<IdentityRoleClaim<string>>()
					.WithOne()
					.HasForeignKey(rc => rc.RoleId)
					.IsRequired();
			});

			builder.Entity<IdentityRoleClaim<string>>(roleClaim =>
			{
				roleClaim.HasKey(rc => rc.Id);
				roleClaim.ToTable("AspNetRoleClaims");
			});

			builder.Entity<UserRole>(userRole =>
			{
				userRole.ToTable("AspNetUserRoles");
				userRole.HasKey(r => new { r.UserId, r.RoleId });
			});

			builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
			builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
			builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
		}

		public DbSet<Country> Country { get; set; }
		public DbSet<Province> Province { get; set; }
		public DbSet<District> District { get; set; }
		public DbSet<SubDistrict> SubDistrict { get; set; }
		public DbSet<Facility> Facility { get; set; }
		public DbSet<Stock> Stock { get; set; }
		public DbSet<Department> Department { get; set; }
		public DbSet<Grant> Grant { get; set; }
		public DbSet<HardwareModel> HardwareModel { get; set; }
		public DbSet<HardwareType> HardwareType { get; set; }
		public DbSet<JobTitle> JobTitle { get; set; }
		public DbSet<Location> Location { get; set; }
		public DbSet<StockRequest> StockRequest { get; set; }
		public DbSet<Reason> Reason { get; set; }
		public DbSet<Ticket> Ticket { get; set; }
		public DbSet<Reference> Reference { get; set; }
		public DbSet<RequestApproval> RequestApproval { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<StockRequestAssetCategory> StockRequestAssetCategory { get; set; }
		public DbSet<Make> Make { get; set; }
		public DbSet<Model> Model { get; set; }
		public DbSet<Supplier> Supplier { get; set; }
		public DbSet<Attachment> Attachment { get; set; }
		public DbSet<StockByReference> StockByReference { get; set; }
		public DbSet<UserAsset> UserAsset { get; set; }
	}
	}
