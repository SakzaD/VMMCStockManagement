using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Interfaces.Files;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Services;
using VMMCStockManagement.Domain.Utils;
using VMMCStockManagement.Infrastructure.DbContexts;
using VMMCStockManagement.Infrastructure.Files;
using VMMCStockManagement.Infrastructure.Repositories;
using VMMCStockManagement.Infrastructure.Repositories.BaseRepositories;
using VMMCStockManagement.Infrastructure.Utils;

namespace VMMCStockManagement.Infrastructure.Extensions
{
	public static class InfrastructureExtension
	{

		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{Environments.Development}.json", optional: true, reloadOnChange: true)
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddTransient<UserStore<User>>();
			services.AddDbContext<VMMCStockManagementDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			services.AddTransient<DbContext, VMMCStockManagementDbContext>();

			services.AddSingleton<IConfiguration>(configuration);
			services.AddUtils();
			services.AddServices();
			services.AddCommandRepositories();
			services.AddQueryRepositories();

			return services;
		}

		private static IServiceCollection AddCommandRepositories(this IServiceCollection services)
		{
			services.AddTransient<ICommandRepository<SubDistrict>, CommandRepository<SubDistrict>>();
			services.AddTransient<ICommandRepository<District>, CommandRepository<District>>();
			services.AddTransient<ICommandRepository<Facility>, CommandRepository<Facility>>();
			services.AddTransient<ICommandRepository<Province>, CommandRepository<Province>>();
			services.AddTransient<ICommandRepository<Country>, CommandRepository<Country>>();

			services.AddTransient<ICommandRepository<Stock>, CommandRepository<Stock>>();
			services.AddTransient<ICommandRepository<Department>, CommandRepository<Department>>();
			services.AddTransient<ICommandRepository<JobTitle>, CommandRepository<JobTitle>>();
			services.AddTransient<ICommandRepository<Location>, CommandRepository<Location>>();
			services.AddTransient<ICommandRepository<Grant>, CommandRepository<Grant>>();
			services.AddTransient<ICommandRepository<Make>, CommandRepository<Make>>();
			services.AddTransient<ICommandRepository<Model>, CommandRepository<Model>>();


			
			services.AddTransient<ICommandRepository<HardwareModel>, CommandRepository<HardwareModel>>();
			services.AddTransient<ICommandRepository<HardwareType>, CommandRepository<HardwareType>>();
			services.AddTransient<ICommandRepository<StockRequest>, CommandRepository<StockRequest>>();
			
			services.AddTransient<ICommandRepository<Reason>, CommandRepository<Reason>>();
			services.AddTransient<ICommandRepository<Ticket>, CommandRepository<Ticket>>();
			services.AddTransient<ICommandRepository<Reference>, CommandRepository<Reference>>();
			services.AddTransient<ICommandRepository<StaffManager>, CommandRepository<StaffManager>>();
			services.AddTransient<ICommandRepository<RequestApproval>, CommandRepository<RequestApproval>>();
			services.AddTransient<ICommandRepository<Category>, CommandRepository<Category>>();
			services.AddTransient<ICommandRepository<StockRequestAssetCategory>, CommandRepository<StockRequestAssetCategory>>();		
			services.AddTransient<ICommandRepository<Model>, CommandRepository<Model>>();
			services.AddTransient<ICommandRepository<Make>, CommandRepository<Make>>();
			services.AddTransient<ICommandRepository<Supplier>, CommandRepository<Supplier>>();
			services.AddTransient<ICommandRepository<Attachment>, CommandRepository<Attachment>>();
			services.AddTransient<ICommandRepository<StockByReference>, CommandRepository<StockByReference>>();
			services.AddTransient<ICommandRepository<UserAsset>, CommandRepository<UserAsset>>();
			services.AddTransient<ICommandRepository<ReasonCategory>, CommandRepository<ReasonCategory>>();
			services.AddTransient<ICommandRepository<StockRequestAssetCategoryItem>, CommandRepository<StockRequestAssetCategoryItem>>();
			


			return services;
		}

		private static IServiceCollection AddQueryRepositories(this IServiceCollection services)
		{
			services.AddTransient<IQueryRepository<Country, CountryFilter>, CountryQueryRepository>();
			services.AddTransient<IQueryRepository<District, DistrictFilter>, DistrictQueryRepository>();
			services.AddTransient<IQueryRepository<Facility, FacilityFilter>, FacilityQueryRepository>();
			services.AddTransient<IQueryRepository<Province, ProvinceFilter>, ProvinceQueryRepository>();
			services.AddTransient<IQueryRepository<SubDistrict, SubDistrictFilter>, SubDistrictQueryRepository>();

			services.AddTransient<IQueryRepository<Stock, StockFilter>, StockQueryRepository>();
			services.AddTransient<IQueryRepository<Department, DepartmentFilter>, DepartmentQueryRepository>();
			services.AddTransient<IQueryRepository<JobTitle, JobTitleFilter>, JobTitleQueryRepository>();
			services.AddTransient<IQueryRepository<Location, LocationFilter>, LocationQueryRepository>();
			services.AddTransient<IQueryRepository<Grant, GrantFilter>, GrantQueryRepository>();
			
			services.AddTransient<IQueryRepository<HardwareModel, HardwareModelFilter>, HardwareModelQueryRepository>();
			services.AddTransient<IQueryRepository<HardwareType, HardwareTypeFilter>, HardwareTypeQueryRepository>();
			
			services.AddTransient<IQueryRepository<Reason, ReasonFilter>, ReasonQueryRepository>();
			services.AddTransient<IQueryRepository<Ticket, TicketFilter>, TicketQueryRepository>();
			services.AddTransient<IQueryRepository<Reference, ReferenceFilter>, ReferenceQueryRepository>();


			services.AddTransient<IQueryRepository<StockRequest, StockRequestFilter>, StockRequestQueryRepository>();			
			services.AddTransient<IQueryRepository<StaffManager, StaffManagerFilter>, StaffManagerQueryRepository>();
			services.AddTransient<IQueryRepository<RequestApproval, RequestApprovalFilter>, RequestApprovalQueryRepository>();
			services.AddTransient<IQueryRepository<Category, CategoryFilter>, CategoryQueryRepository>();			
			
			services.AddTransient<IQueryRepository<StockRequest, StockRequestReportFilter>, StockRequestQueryReportRepository>();
			services.AddTransient<IUserQueryRepository, UserQueryRepository>();
			services.AddTransient<IQueryRepository<Make, MakeFilter>, MakeQueryRepository>();
			services.AddTransient<IQueryRepository<Model, ModelFilter>, ModelQueryRepository>();
			services.AddTransient<IQueryRepository<Supplier, SupplierFilter>, SupplierQueryRepository>();
			services.AddTransient<IQueryRepository<Attachment, AttachmentFilter>, AttachmentQueryRepository>();
			services.AddTransient<IQueryRepository<StockByReference, StockByReferenceFilter>, StockByReferenceQueryRepository>();
			services.AddTransient<IQueryRepository<UserAsset, UserAssetFilter>, UserAssetQueryRepository>();
			services.AddTransient<IQueryRepository<ReasonCategory, ReasonCategoryFilter>, ReasonCategoryQueryRepository>();
			services.AddTransient<IQueryRepository<StockRequestAssetCategoryItem, StockRequestAssetCategoryItemFilter>, StockRequestAssetCategoryItemQueryRepository>();
			
			
			return services;
		}
		private static IServiceCollection AddServices(this IServiceCollection services)
		{

			services.AddTransient<IFileService, FileService>();
			services.AddTransient<IBulkUploadService, BulkUploadService>();
			services.AddTransient<IUserUploadService, UserUploadService>();


			return services;
		}

		private static IServiceCollection AddUtils(this IServiceCollection services)
		{

			services.AddTransient<ISerializer, Serializer>();
			services.AddTransient<IBaseRestClient, BaseRestClient>();
			services.AddTransient<IEmailService, EmailService>();
			return services;
		}

		static IServiceCollection AddGeneralServices(this IServiceCollection services)
		{
			services.AddTransient<IAuthenticateService, AuthenticateService>();
			services.AddScoped<IRoleService, RoleService>();


			return services;
		}

	}
}
