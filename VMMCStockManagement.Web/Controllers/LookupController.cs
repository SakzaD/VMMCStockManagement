using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMMCStockManagement.Domain.Interfaces.Services.QueryServices;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LookupController : ControllerBase
	{
		private readonly IRoleService roleService;
		private readonly ICountryQueryService countryQueryService;
		private readonly IDistrictQueryService districtQueryService;
		private readonly IFacilityQueryService facilityQueryService;
		private readonly IProvinceQueryService provinceQueryService;
		private readonly ISubDistrictQueryService subDistrictQueryService;
		private readonly IAuthenticateService authenticateService;
		private readonly ILocationQueryService locationQueryService;
		private readonly IDepartmentQueryService departmentQueryService;
		private readonly IGrantQueryService grantQueryService;
		private readonly IJobTitleQueryService jobTitleQueryService;
		private readonly IStockQueryService assetQueryService;
		private readonly IReasonQueryService reasonQueryService;
		private readonly ICategoryQueryService assetCategoryQueryService;
		private readonly IMakeQueryService makeQueryService;
		private readonly IModelQueryService modelQueryService;
		private readonly ISupplierQueryService supplierQueryService;
		private readonly IReasonCategoryQueryService reasonCategoryQueryService;


		public LookupController(ICountryQueryService countryQueryService, IStockQueryService assetQueryService,

			IDistrictQueryService districtQueryService, IDepartmentQueryService departmentQueryService,
			IGrantQueryService grantQueryService, IJobTitleQueryService jobTitleQueryService,
			IFacilityQueryService facilityQueryService, ILocationQueryService locationQueryService,
			IProvinceQueryService provinceQueryService, ISubDistrictQueryService subDistrictQueryService,
			 IAuthenticateService authenticateService, IRoleService roleService,
			 IModelQueryService modelQueryService, IMakeQueryService makeQueryService,
			 IReasonQueryService reasonQueryService, ICategoryQueryService assetCategoryQueryService,
			 ISupplierQueryService supplierQueryService, IReasonCategoryQueryService reasonCategoryQueryService)
		{
			this.countryQueryService = countryQueryService;
			this.districtQueryService = districtQueryService;
			this.facilityQueryService = facilityQueryService;
			this.provinceQueryService = provinceQueryService;
			this.provinceQueryService = provinceQueryService;
			this.subDistrictQueryService = subDistrictQueryService;
			this.authenticateService = authenticateService;
			this.locationQueryService = locationQueryService;
			this.roleService = roleService;
			this.departmentQueryService = departmentQueryService;
			this.grantQueryService = grantQueryService;
			this.jobTitleQueryService = jobTitleQueryService;
			this.assetQueryService = assetQueryService;
			this.reasonQueryService = reasonQueryService;
			this.assetCategoryQueryService = assetCategoryQueryService;
			this.makeQueryService = makeQueryService;
			this.modelQueryService = modelQueryService;
			this.supplierQueryService = supplierQueryService;
			this.reasonCategoryQueryService = reasonCategoryQueryService;

		}

		[HttpGet("location/filter")]
		public IActionResult FilterLocation([FromQuery] LocationFilter filter)
		{
			return Ok(locationQueryService.Filter(filter));
		}

		[HttpGet("department/filter")]
		public IActionResult FilterDepartment([FromQuery] DepartmentFilter filter)
		{
			return Ok(departmentQueryService.Filter(filter));
		}

		[HttpGet("grant/filter")]
		public IActionResult FilterGrant([FromQuery] GrantFilter filter)
		{
			return Ok(grantQueryService.Filter(filter));
		}

		[HttpGet("jobtitle/filter")]
		public IActionResult FilterJobTitle([FromQuery] JobTitleFilter filter)
		{
			return Ok(jobTitleQueryService.Filter(filter));
		}

		[HttpGet("stock/filter")]
		public IActionResult FilterAsset([FromQuery] StockFilter filter)
		{
			return Ok(assetQueryService.Filter(filter));
		}

		[HttpGet("stock-category/filter")]
		public IActionResult FilterAssetCategory([FromQuery] CategoryFilter filter)
		{
			return Ok(assetCategoryQueryService.Filter(filter));
		}

		[HttpGet("country/filter")]
		public IActionResult FilterCountries([FromQuery] CountryFilter filter)
		{
			return Ok(countryQueryService.Filter(filter));
		}

		[HttpGet("province/filter")]
		public IActionResult FilterProvinces([FromQuery] ProvinceFilter filter)
		{
			return Ok(provinceQueryService.Filter(filter));
		}

		[HttpGet("district/filter")]
		public IActionResult FilterDistricts([FromQuery] DistrictFilter filter)
		{
			return Ok(districtQueryService.Filter(filter));
		}

		[HttpGet("supplier/filter")]
		public IActionResult FilterSuppliers([FromQuery] SupplierFilter filter)
		{
			return Ok(supplierQueryService.Filter(filter));
		}

		[HttpGet("sub-district/filter")]
		public IActionResult FilterSubDistricts([FromQuery] SubDistrictFilter filter)
		{
			return Ok(subDistrictQueryService.Filter(filter));
		}

		[HttpGet("facility/filter")]
		public IActionResult FilterFacilities([FromQuery] FacilityFilter filter)
		{
			return Ok(facilityQueryService.Filter(filter));
		}

		[HttpGet("reason/filter")]
		public IActionResult FilterReason([FromQuery] ReasonFilter filter)
		{
			return Ok(reasonQueryService.Filter(filter));
		}

		[HttpGet("reason-category/filter")]
		public IActionResult FilterReasonCategory([FromQuery] ReasonCategoryFilter filter)
		{
			return Ok(reasonCategoryQueryService.Filter(filter));
		}


		[HttpGet("users/filter")]
		public async Task<IActionResult> FilterUsers([FromQuery] UserFilter filter)
		{
			var response = await authenticateService.Filter(filter);

			return Ok(response);
		}

		[HttpGet("managers/filter")]
		public async Task<IActionResult> FilterManagers([FromQuery] UserFilter filter)
		{
			var response = await authenticateService.GetManagers(filter);
			return Ok(response);
		}

		[HttpGet("all-roles/filter")]
		public IActionResult FilterRoles([FromQuery] RoleFilter filter)
		{

			var response = roleService.Filter(filter);
			return Ok(response);
		}

		[HttpGet("user/roles/filter")]
		public async Task<IActionResult> GetUser([FromQuery] RoleFilter filter)
		{
			var response = await roleService.GetUserRoles(filter);
			return Ok(response);
		}

		[HttpGet("make/filter")]
		public async Task<IActionResult> FilterMake([FromQuery] MakeFilter filter)
		{
			var response = makeQueryService.Filter(filter);
			return Ok(response);
		}

		[HttpGet("model/filter")]
		public async Task<IActionResult> FilterModel([FromQuery] ModelFilter filter)
		{
			var response = modelQueryService.Filter(filter);
			return Ok(response);
		}

		[HttpGet("user/get-by-id")]
		public async Task<IActionResult> GetUserById([FromQuery] string userId)
		{
			var response = await authenticateService.GetUserById(userId);
			return Ok(response);
		}
	}
}
