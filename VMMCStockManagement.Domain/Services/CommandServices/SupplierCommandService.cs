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
	public class SupplierCommandService : BaseCommandService<Supplier>, ISupplierCommandService
	{

		private readonly IQueryRepository<Supplier, SupplierFilter> queryRepository;
		public SupplierCommandService(ICommandRepository<Supplier> commandRepository,
			ILogger<BaseCommandService<Supplier>> logger,
			IQueryRepository<Supplier, SupplierFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(SupplierCommandService);



		private bool IsNameExist(string supplierName)
		{
			var asset = queryRepository
			.GetAll()
			.FirstOrDefault(x => x.Name == supplierName.Trim());

			return asset != null;
		}

		public BaseResponse Add(SupplierRequest request)
		{
			var isRegistrationExistExist = IsNameExist(request.Name.Trim());

			if (isRegistrationExistExist)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = $"This name [{request.Name}] already exist on our system."
				};
			}


			Supplier supplier = Supplier.Create(request);

			return Create(supplier);
		}

		public override void AfterCreation(Supplier entity)
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
					Message = "Invalid asset selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(SupplierRequest request)
		{
			var asset = queryRepository.GetById(request.Id.Value);

			if (asset == null)
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};

			asset.UpdatedAt = DateTime.Now;
			asset.Name = request.Name;
			asset.Description = request.Description;

			var response = Update(asset);

			return response;
		}
	}
}
