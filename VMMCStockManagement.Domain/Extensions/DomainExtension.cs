using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Services;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Services.CommandServices;
using VMMCStockManagement.Domain.Services.QueryServices;
using VMMCStockManagement.Domain.Services.GeneralServices;

namespace VMMCStockManagement.Domain.Extensions
{
	public static class DomainExtension
	{
		public static IServiceCollection AddDomain(this IServiceCollection services)
		{
			services.AddCommandServices();
			services.AddQueryServices();
			services.AddGeneralServices();

			return services;
		}

		public static IServiceCollection AddCommandServices(this IServiceCollection services)
		{
			services.AddTransient<ICountryCommandService, CountryCommandService>();
			services.AddTransient<IProvinceCommandService, ProvinceCommandService>();
			services.AddTransient<ICategoryCommandService, CategoryCommandService>();
			services.AddTransient<IDepartmentCommandService, DepartmentCommandService>();
			services.AddTransient<IDistrictCommandService, DistrictCommandService>();
			services.AddTransient<ISubDistrictCommandService, SubDistrictCommandService>();
			services.AddTransient<IGrantCommandService, GrantCommandService>();
			services.AddTransient<IJobTitleCommandService, JobTitleCommandService>();
			services.AddTransient<ILocationCommandService, LocationCommandService>();
			services.AddTransient<IMakeCommandService, MakeCommandService>();
			services.AddTransient<IModelCommandService, ModelCommandService>();
			services.AddTransient<IStaffManagerCommandService, StaffManagerCommandService>();
			services.AddTransient<ISupplierCommandService, SupplierCommandService>();
			services.AddTransient<IReferenceCommandService, ReferenceCommandService>();
			services.AddTransient<IFacilityCommandService, FacilityCommandService>();
			
			services.AddTransient<ITicketCommandService, TicketCommandService>();
			services.AddTransient<IReasonCategoryCommandService, ReasonCategoryCommandService>();
			services.AddTransient<IHardwareModelCommandService, HardwareModelCommandService>();
			services.AddTransient<IHardwareTypeCommandService, HardwareTypeCommandService>();
			services.AddTransient<IAttachmentCommandService, AttachmentCommandService>();
			//services.AddTransient<IStockByReferenceCommandService, StockByReferenceCommandService>();
			services.AddTransient<IUserAssetCommandService, UserAssetCommandService>();


			return services;
		}

		public static IServiceCollection AddQueryServices(this IServiceCollection services)
		{
			services.AddTransient<ICountryQueryService, CountryQueryService>();
			services.AddTransient<IProvinceQueryService, ProvinceQueryService>();
			services.AddTransient<IDepartmentQueryService, DepartmentQueryService>();
			services.AddTransient<IDistrictQueryService, DistrictQueryService>();
			services.AddTransient<ISubDistrictQueryService, SubDistrictQueryService>();
			services.AddTransient<IFacilityQueryService, FacilityQueryService>();
			services.AddTransient<IGrantQueryService, GrantQueryService>();
			services.AddTransient<IJobTitleQueryService, JobTitleQueryService>();
			services.AddTransient<ILocationQueryService, LocationQueryService>();
			services.AddTransient<IRequestApprovalQueryService, RequestApprovalQueryService>();
			//services.AddTransient<IStockCategoryQueryService, StockCategoryQueryService>();
			//services.AddTransient<IStockRequestAssetCategoryQueryService, StockRequestAssetCategoryQueryService>();
			services.AddTransient<IMakeQueryService, MakeQueryService>();
			services.AddTransient<IModelQueryService, ModelQueryService>();
			services.AddTransient<ISupplierQueryService, SupplierQueryService>();
			services.AddTransient<IStockRequestReportQueryService, StockRequestReportQueryService>();
			services.AddTransient<IReportQueryService, ReportQueryService>();
			services.AddTransient<ITicketQueryService, TicketQueryService>();
			services.AddTransient<IReasonCategoryQueryService, ReasonCategoryQueryService>();
			services.AddTransient<IReferenceQueryService, ReferenceQueryService>();
			services.AddTransient<IStaffManagerQueryService, StaffManagerQueryService>();



			return services;
		}

		public static IServiceCollection AddGeneralServices(this IServiceCollection services)
		{
			services.AddTransient<IAuthenticateService, AuthenticateService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IDashboardQueryService, DashboardQueryService>();
			services.AddScoped<ITrackingService, TrackingService>();
			services.AddScoped<IReminderService, ReminderService>();

			return services;
		}
	}
}
