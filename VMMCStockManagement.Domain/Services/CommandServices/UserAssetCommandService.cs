using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Interfaces;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class UserAssetCommandService : BaseCommandService<UserAsset>, IUserAssetCommandService
	{

		private readonly IQueryRepository<Stock, StockFilter> assetQueryRepo;
		private readonly IQueryRepository<StockRequest, StockRequestFilter> assetRequestQueryRepo;
		private readonly IQueryRepository<UserAsset, UserAssetFilter> queryRepository;
		private readonly IUserQueryRepository userQueryRepository;
		private readonly IAuthenticateService authenticateService;
		public UserAssetCommandService(ICommandRepository<UserAsset> commandRepository, IUserQueryRepository userQueryRepository, IAuthenticateService authenticateService,
			ILogger<BaseCommandService<UserAsset>> logger, IQueryRepository<Stock, StockFilter> assetQueryRepo,
			IQueryRepository<StockRequest, StockRequestFilter> assetRequestQueryRepo,
			IQueryRepository<UserAsset, UserAssetFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
			this.userQueryRepository = userQueryRepository;
			this.authenticateService = authenticateService;
			this.assetQueryRepo = assetQueryRepo;
			this.assetRequestQueryRepo = assetRequestQueryRepo;
		}

		public override string ServiceName => nameof(UserAssetCommandService);

		private User? GetUserByEmployee(string employeeNumber)
		{
			var user = userQueryRepository
				.GetUserByEmployeeNumber(employeeNumber);

			return user;
		}
		private User? GetUserByIdNumber(string idNumber)
		{
			var user = userQueryRepository
				.GetUserByIdNumber(idNumber);

			return user;
		}

		private User GetUserByUserName(string email)
		{
			var user = userQueryRepository
				.GetUserByUsername(email);

			return user;
		}
		public BaseResponse Add(UserAssetRequest request)
		{
			User? user = null;

			if (request.IsEmployee)
			{
				if (string.IsNullOrEmpty(request.EmployeeNumber))
				{
					return new BaseResponse
					{
						CodeStatus = ResponseStatus.Fail,
						Message = "Employee number is required."
					};
				}
				else
				{
					//Check if employee number doesn't exist and return a message
					user = GetUserByEmployee(request.EmployeeNumber);
					if (user == null)
					{
						return new BaseResponse
						{
							CodeStatus = ResponseStatus.Fail,
							Message = "Invalid employee number provided."
						};
					}
				}
			}
			else
			{
				if (string.IsNullOrEmpty(request.IdNumber))
				{
					return new BaseResponse
					{
						CodeStatus = ResponseStatus.Fail,
						Message = "Please provide Id Number."
					};
				}
			}



			if (request.IsEmployee == true)
			{
				user = GetUserByEmployee(request.EmployeeNumber);
			}
			else
			{
				user = GetUserByIdNumber(request.IdNumber);

				if (user == null)
				{
					user = GetUserByUserName(request.Email);
				}

				if (user == null)
				{
					var newUser = new UserRequest
					{
						FirstName = request.FirstName.Trim(),
						LastName = request.LastName.Trim(),
						//UserName = request.Email.Trim(),
						IdNumber = request.IdNumber.Trim(),
						PhoneNumber = string.IsNullOrWhiteSpace(request.Phone) ? "" : request.Phone.Trim(),
						Email = request.Email.Trim(),

					};

					user = authenticateService.AddNoneEmployeeUser(newUser).Result;
				}
			}

			request.AssigneeUserId = user.Id;

			var results = queryRepository
				.GetAll()
				.FirstOrDefault(x => x.AssetId == request.AssetId);

			if (results != null)
			{
				return new BaseResponse
				{
					CodeStatus = ResponseStatus.Fail,
					Message = "Asset has been assigned to someone"
				};
			}

			var asset = assetQueryRepo.GetById(request.AssetId.Value);
			var assetRequest = assetRequestQueryRepo.GetById(request.AssetRequestId.Value);

			if (asset != null && assetRequest != null)
			{

				if (asset.GrantId != assetRequest.GrantId &&
					!assetRequest.Grant.Name.Contains("NICRA", StringComparison.OrdinalIgnoreCase))
				{

					return new BaseResponse
					{
						CodeStatus = ResponseStatus.Fail,
						Message = $"You cannot assign asset from this grant [{asset.Grant.Name}] to this grant [{assetRequest.Grant.Name}]."
					};

				}

			}

			request.ReferenceNumber = asset.ReferenceNumber.Trim();

			var userAsset = UserAsset.Create(request);
			return Create(userAsset);
		}

		public override void AfterCreation(UserAsset entity)
		{

		}
		public BaseResponse Delete(BaseRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
			{
				return new BaseResponse
				{
					CodeStatus = ResponseStatus.Fail,
					Message = "Invalid UserAsset selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}
		public BaseResponse Edit(UserAssetRequest request)
		{
			var UserAsset = queryRepository.GetById(request.Id.Value);

			UserAsset.UpdatedAt = DateTime.Now;

			var response = Update(UserAsset);

			return response;
		}
	}
}
