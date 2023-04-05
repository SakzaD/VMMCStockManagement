using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Interfaces.Services;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class DepartmentCommandService : BaseCommandService<Department>, IDepartmentCommandService
	{

		private readonly IQueryRepository<Department, DepartmentFilter> queryRepository;
		public DepartmentCommandService(ICommandRepository<Department> commandRepository,
			ILogger<BaseCommandService<Department>> logger,
			IQueryRepository<Department, DepartmentFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(DepartmentCommandService);

		public BaseResponse Add(DepartmentRequest request)
		{
			Department location = Department.Create(request);

			return Create(location);
		}

		public override void AfterCreation(Department entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(DepartmentRequest request)
		{
			var department = queryRepository.GetById(request.Id.Value);

			if (department == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid district selected"
				};

			department.Name = request.Name;
			department.UpdatedAt = DateTime.Now;
			department.UpdatedBy = request.UserId;

			var response = Update(department);

			return response;
		}
	}
}
