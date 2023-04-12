
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Constants;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Enums;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Models;
using VMMCStockManagement.Domain.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VMMCStockManagement.Domain.Services
{
	public class AuthenticateService : IAuthenticateService
	{
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly ILogger<AuthenticateService> logger;
		private readonly IConfiguration configuration;
		private readonly UserStore<User> userStore;
		private readonly IEmailService emailService;
		private readonly IUserUploadService userUploadService;
		private readonly ICommandRepository<JobTitle> jobTitleCommandRepo;
		private readonly IQueryRepository<JobTitle, JobTitleFilter> jobTitleQueryRepo;

		private readonly ICommandRepository<Grant> grantCommandRepo;
		private readonly IQueryRepository<Grant, GrantFilter> grantQueryRepo;
		//private readonly IUserFacilityCommandService userFacilityCommandService;
		// private readonly UserStore<User> userStore;

		public AuthenticateService(UserManager<User> userManager,
			IUserUploadService userUploadService,
			ICommandRepository<JobTitle> jobTitleCommandRepo, ICommandRepository<Grant> grantCommandRepo,
			IQueryRepository<JobTitle, JobTitleFilter> jobTitleQueryRepo, IQueryRepository<Grant, GrantFilter> grantQueryRepo,
			IConfiguration configuration, SignInManager<User> signInManager,
			ILogger<AuthenticateService> logger, UserStore<User> userStore, RoleManager<Role> roleManager,
			IEmailService emailService
			/*IUserFacilityCommandService userFacilityCommandService*/)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.logger = logger;
			this.configuration = configuration;
			this.userStore = userStore;
			this.roleManager = roleManager;
			this.emailService = emailService;
			this.userUploadService = userUploadService;
			this.jobTitleCommandRepo = jobTitleCommandRepo;
			this.jobTitleQueryRepo = jobTitleQueryRepo;
			this.grantCommandRepo = grantCommandRepo;
			this.grantQueryRepo = grantQueryRepo;
			//this.userFacilityCommandService = userFacilityCommandService;
		}

		public async Task<BaseResponse> CreateNewRole(RoleRequest request)
		{
			var response = new BaseResponse();

			var isRoleExist = await roleManager.RoleExistsAsync(request.Name.Trim());

			if (!isRoleExist)
			{
				var res = await roleManager.CreateAsync(new Role(request.Name));

				if (!res.Succeeded)
				{
					return new BaseResponse
					{
						CodeStatus = ResponseStatus.Fail,
						Message = "Failed to create role, please try again later."
					};
				}

				response.CodeStatus = ResponseStatus.Success;
			}
			else
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "This role already exist.";

			}

			return response;
		}
		public async Task<ObjectListResponse<UserResponse>> Filter(UserFilter filter)
		{
			var response = new ObjectListResponse<UserResponse>();

			var userRes = new List<UserResponse>();

			var res = userManager
				.Users
				.Include(x => x.UserRoles)
				.ThenInclude(x => x.Role)
				.Where(x => x.Status == EntityStatus.Active)
				.ToList();

			foreach (var user in res)
			{
				
				var roles = user.UserRoles.Select(x => x.Role.Name).ToList();

				userRes.Add(
					new UserResponse
					{
						Id = user.Id,
						Username = user.UserName,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Email = user.Email,
						PhoneNumber = user.PhoneNumber,
						RoleId = (user.UserRoles == null || user.UserRoles.Count() == 0) ? "" : user.UserRoles.FirstOrDefault().RoleId,
						Role = (user.UserRoles == null || user.UserRoles.Count() == 0) ? "Not Assigned" : string.Join(",", roles),
						EmployeeNumber = user.EmployeeNumber ?? string.Empty,
					});
			}
			response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}
		public async Task<ObjectResponse<UserResponse>> GetUserById(string userId)
		{
			var response = new ObjectResponse<UserResponse>();

			var userRes = new UserResponse();

			var user = await userManager
				.Users
				.FirstOrDefaultAsync(x => x.Id == userId);

			var roles = await userManager.GetRolesAsync(user);


			userRes = new UserResponse
			{
				Id = user.Id,
				Username = user.UserName,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				EmployeeNumber = user.EmployeeNumber,
				Role = (roles == null || roles.Count() == 0) ? RoleConstants.Requester : string.Join(",", roles)
			};

			response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}
		public async Task<ObjectListResponse<UserResponse>> GetManagers(UserFilter filter)
		{
			var response = new ObjectListResponse<UserResponse>();

			var userRes = new List<UserResponse>();

			var res = (await userManager.GetUsersInRoleAsync(filter.Role))
				.Where(x => x.Status == EntityStatus.Active);

			foreach (var user in res)
			{

				userRes.Add(
					new UserResponse
					{
						Id = user.Id,
						Username = user.UserName,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Email = user.Email,
						PhoneNumber = user.PhoneNumber
					});
			}
			response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}

		public async Task<BaseResponse> DeleteUser(UserDeleteRequest request)
		{
			var response = new BaseResponse();

			try
			{
				var user = await userManager
				.Users
				.FirstOrDefaultAsync(x => x.Id == request.Id);

				if (user == null)
					return new BaseResponse
					{
						Message = "Make sure you have selected the right user.",
						CodeStatus = ResponseStatus.Fail,
					};

				user.Status = EntityStatus.Deleted;
				user.UpdatedAt = DateTime.Now;
				user.UpdatedBy = request.UserId;


				var updateRes = await userManager.UpdateAsync(user);

				if (!updateRes.Succeeded)
				{
					string message = "Failed to delete user, please try again.";

					if (updateRes.Errors != null && updateRes.Errors.Any())
					{
						StringBuilder stringBuilder = new StringBuilder();

						foreach (var error in updateRes.Errors)
						{
							stringBuilder.AppendLine(error.Description);
						}

						message = stringBuilder.ToString();
					}

					response.Message = message;
					response.CodeStatus = ResponseStatus.Fail;
					return response;
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			response.Message = "User has been successfully deleted.";
			response.CodeStatus = ResponseStatus.Success;
			return response;
		}


		public async Task<ObjectResponse<UserResponse>> GetUserByIdAsync(string userId)
		{
			var response = new ObjectResponse<UserResponse>();

			var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

			var role = await userManager.GetRolesAsync(user);

			var userRes = new UserResponse
			{
				Id = user.Id,
				Username = user.UserName,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber
			};

			response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}
		public User? GetById(string userId)
		{
			return userManager.Users.FirstOrDefault(x => x.Id == userId);
		}

		public async Task<ObjectResponse<UserResponse>> Register(UserRequest request)
		{
			ObjectResponse<UserResponse> response = new ObjectResponse<UserResponse>();

			try
			{

				var checkUserResults = (await userManager.FindByEmailAsync(request.Email));

				if (checkUserResults != null)
				{
					response.Message = $"User by this email '{request.Email}' already exist.";
					response.CodeStatus = Enums.ResponseStatus.Fail;
					return response;
				}

				var user = new User
				{
					FirstName = request.FirstName.Trim(),
					LastName = request.LastName.Trim(),
					UserName = request.Email.Trim(),
					EmployeeNumber = request.EmployeeNumber.Trim(),
					PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? "" : request.PhoneNumber.Trim(),
					Email = request.Email.Trim(),
					EmailConfirmed = true,
					CreatedBy = "22636619-6ee7-4729-96a0-5c31cffe75bc",
					CreatedAt = DateTime.Now,
                    UpdatedBy = "22636619-6ee7-4729-96a0-5c31cffe75bc",
                    UpdatedAt = DateTime.Now
                };


				request.Password = "P@ssword!12";

				var results = await userManager.CreateAsync(user, request.Password);

				if (!results.Succeeded)
				{
					string message = "Failed to create user, please try again.";

					if (results.Errors != null && results.Errors.Any())
					{
						StringBuilder stringBuilder = new StringBuilder();

						foreach (var error in results.Errors)
						{
							stringBuilder.AppendLine(error.Description);
						}

						message = stringBuilder.ToString();
					}


					response.Message = message;
					response.CodeStatus = ResponseStatus.Fail;
					return response;
				}

				var roles = roleManager.Roles.ToList();

				if (request.Roles == null || request.Roles.Count() == 0)
				{
					var role = roles
						.FirstOrDefault(
						x => x.Name.Contains(RoleConstants.Requester,
						StringComparison.InvariantCultureIgnoreCase));

					var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
				}
				else
				{
					foreach (var roleId in request.Roles)
					{
						var role = roles.FirstOrDefault(x => x.Id == roleId);
						var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
					}
				}

				
				response.CodeStatus = ResponseStatus.Success;
				response.Message = "You have been registered successfully.";

				SendEmail(
					request.Email.Trim(),
					string.Format("{0} {1}",
					request.FirstName,
					request.LastName),
					request.Password);

			}
			catch (Exception ex)
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "An exception has occured trying to register user, please try again later.";
				logger.LogError(ex.Message);
			}

			return response;
		}

		public async Task<User> AddNoneEmployeeUser(UserRequest request)
		{

			User? user = null;

			try
			{

				user = new User
				{
					FirstName = request.FirstName.Trim(),
					LastName = request.LastName.Trim(),
					UserName = request.Email.Trim(),
					EmployeeNumber = request.EmployeeNumber.Trim(),
					PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? "" : request.PhoneNumber.Trim(),
					Email = request.Email.Trim(),
					IdNumber = request.IdNumber,
					EmailConfirmed = true,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now,
					CreatedBy = request.UserId,
					UpdatedBy = request.UserId,
					Status = EntityStatus.Active,

				};


				var results = await userManager.CreateAsync(user, "P@sWf74234f");

				if (!results.Succeeded)
				{

					string message = "Failed to create user, please try again.";

					if (results.Errors != null && results.Errors.Any())
					{
						StringBuilder stringBuilder = new StringBuilder();

						foreach (var error in results.Errors)
						{
							stringBuilder.AppendLine(error.Description);
						}

						message = stringBuilder.ToString();
					}

				}
				else
				{
					var addRoleResult = await userManager.AddToRoleAsync(user, RoleConstants.Requester);
				}


			}
			catch (Exception ex)
			{

				var msg = ex.Message;
				logger.LogError(msg);
			}

			return user;
		}
		public async Task<BaseResponse> RemoveUserRole(RoleDeleteRequest request)
		{
			var response = new ObjectResponse<UserResponse>();


			var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);
			var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

			if (role == null)
			{
				response.Message = "Failed to remove this role from this user.";
				response.CodeStatus = ResponseStatus.Fail;

				return response;
			}

			var res = await userManager.RemoveFromRoleAsync(user, role.Name);

			if (!res.Succeeded)
			{
				response.Message = "Failed to remove this role from this user.";
				response.CodeStatus = ResponseStatus.Fail;
			}

			//response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}
		public async Task<BaseResponse> AddUserToRole(RoleAddRequest request)
		{
			var response = new ObjectResponse<UserResponse>();


			var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);
			var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

			var res = await userManager.AddToRoleAsync(user, role.Name);

			if (!res.Succeeded)
			{
				response.Message = "Failed to add this role to the user.";
				response.CodeStatus = ResponseStatus.Fail;
			}

			//response.Data = userRes;
			response.CodeStatus = Enums.ResponseStatus.Success;

			return response;
		}
		public async Task<ObjectResponse<UserResponse>> Update(UserRequest request)
		{
			ObjectResponse<UserResponse> response = new ObjectResponse<UserResponse>();
			var userObj = await userManager.FindByIdAsync(request.Id);
			var role = await userManager.GetRolesAsync(userObj);
			try
			{

				var user = await userManager
					.Users
					.Include(x => x.UserRoles)
					.ThenInclude(x => x.Role)
					.FirstOrDefaultAsync(x => x.Id == request.Id);

				if (user == null)
				{
					response.Message = "User was not found";
					response.CodeStatus = ResponseStatus.Fail;

					return response;
				}

				user.FirstName = request.FirstName;
				user.LastName = request.LastName.Trim();
				user.UserName = request.Email.Trim();
				user.PhoneNumber = request.PhoneNumber == null ? null : request.PhoneNumber.Trim();
				user.EmployeeNumber = request.EmployeeNumber == null ? null : request.EmployeeNumber.Trim();
				user.Email = request.Email.Trim();
				user.EmailConfirmed = true;
				user.UpdatedAt = DateTime.Now;
				user.UpdatedBy = request.UserId;

				if (role.Count() > 0)
				{
					foreach (var item in role.ToList())
					{
						await userManager.RemoveFromRoleAsync(user, item);
					}
				}

				var roles = roleManager.Roles.ToList();

				if (request.Roles == null || request.Roles.Count() == 0)
				{
					var newRole = roles
						.FirstOrDefault(x => x.Name
						.Contains(RoleConstants.Requester,
						StringComparison.InvariantCultureIgnoreCase));

					var addRoleResult = await userManager.AddToRoleAsync(user, newRole.Name);
				}
				else
				{
					foreach (var roleId in request.Roles)
					{
						var newRole = roles.FirstOrDefault(x => x.Id == roleId);
						var addRoleResult = await userManager.AddToRoleAsync(user, newRole.Name);
					}
				}


				var updateRes = await userManager.UpdateAsync(user);

				if (!updateRes.Succeeded)
				{
					string message = "Failed to update user, please try again.";

					if (updateRes.Errors != null && updateRes.Errors.Any())
					{
						StringBuilder stringBuilder = new StringBuilder();

						foreach (var error in updateRes.Errors)
						{
							stringBuilder.AppendLine(error.Description);
						}

						message = stringBuilder.ToString();
					}

					response.Message = message;
					response.CodeStatus = ResponseStatus.Fail;
					return response;
				}



				//var roles = roleManager.Roles.ToList();

				//if (user.UserRoles != null && user.UserRoles.Count > 0)
				//{
				//                //var currentRole = user.UserRoles.FirstOrDefault();
				//                //var newRoleId = request.RoleId;

				//                bool isFound = false;

				//                foreach(var currentRole in user.UserRoles)
				//                {

				//                    foreach (var newRoleId in request.Roles)
				//                    {
				//                       if(newRoleId == currentRole.RoleId)
				//                        {
				//                            isFound = true;
				//                            break;
				//                        }
				//                    }


				//                }

				//                //if (currentRole.RoleId != newRoleId)
				//                //{
				//                //    var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == newRoleId);
				//                //    await userManager.RemoveFromRoleAsync(user, currentRole.Role.Name);
				//                //    await userManager.AddToRoleAsync(user, role.Name);
				//                //}
				//            }
				//else
				//{
				//	var roleId = request.RoleId;
				//                var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
				//	await userManager.AddToRoleAsync(user, role.Name);
				//}

				//if (request.Roles != null && request.Roles.Count() > 0)
				//{
				//    //foreach (var roleId in request.Roles)
				//    //{
				//    //    var role = roles.FirstOrDefault(x => x.Id == roleId);
				//    //    var addRoleResult = await userManager.AddToRoleAsync(user, role.Id);
				//    //}
				//}

				response.CodeStatus = ResponseStatus.Success;
				response.Message = "User was successfully updated.";

			}
			catch (Exception ex)
			{
				response.CodeStatus = ResponseStatus.Fail;
				response.Message = "An exception has occurred trying to register user, please try again later.";
				logger.LogError(ex.Message);
			}

			return response;
		}
		private void SendEmail(string emailAddress, string name, string password)
		{
			var to = new Dictionary<string, string>();
			to.Add(emailAddress, emailAddress);

			var callbackUrl = configuration.GetValue<string>("SystemConfig:URL");

			string systemName = configuration.GetValue<string>("SystemConfig:Name");

			var email = Email.Create(to, null, "Registered Successful",
				$"Hi {name} <br> You've been registered on {systemName}, you can follow this <br>" +
				$"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a> to login to the system. <br> <br>Here is your temporary password: {password}<br> <br>");
			this.emailService.Send(email);
		}


		public async Task<BaseResponse> UploadUser(UserUploadRequest request)
		{
			var response = new ObjectResponse<UserResponse>();

			List<ErrorModel> errors = null;

			try
			{

				var fileName = await userUploadService.SaveFileAsync(request.UserFile);

				var userList = userUploadService.ReadFile(new FileInfo(fileName));




				foreach (var user in userList)
				{
					if (userManager.Users.FirstOrDefault(x => x.UserName == user.Email || x.EmployeeNumber == user.EmployeeCode) != null)
					{
						continue;
					}

					var jobTitle = jobTitleQueryRepo.GetAll().FirstOrDefault(x => x.Name == user.JobTitle);

					if (jobTitle == null)
					{
						var newJobtitle = new JobTitle
						{
							CreatedAt = DateTime.Now,
							UpdatedAt = DateTime.Now,
							CreatedBy = request.UserId,
							UpdatedBy = request.UserId,
							Name = user.JobTitle
						};

						if (user.RowNumber >= 1994)
						{
							var newUserRe = newJobtitle;

						}
						jobTitle = jobTitleCommandRepo.Create(newJobtitle);
					}

					var grant = grantQueryRepo.GetAll().FirstOrDefault(x => x.Name == user.Grant);

					if (grant == null)
					{
						var newGrant = new Grant
						{
							CreatedAt = DateTime.Now,
							UpdatedAt = DateTime.Now,
							CreatedBy = request.UserId,
							UpdatedBy = request.UserId,
							Name = user.Grant
						};

						if (user.RowNumber >= 1994)
						{
							var newUserRe = newGrant;

						}
						grant = grantCommandRepo.Create(newGrant);
					}

					UserRequest userRequest = new UserRequest
					{
						EmployeeNumber = user.EmployeeCode,
						KnownAsName = user.KnownAsName,
						FirstName = user.FirstName,
						LastName = user.LastName,
						PhoneNumber = user.ContactNumber,
						Email = user.Email,
						JobTitleId = jobTitle.Id,
						GrantId = grant.Id,

					};

					if (user.RowNumber >= 1994)
					{
						var newUserRe = userRequest;

					}
					await AddNoneEmployeeUser(userRequest);

				}

				//response.Data = userRes;
				response.CodeStatus = Enums.ResponseStatus.Success;

			}
			catch (Exception ex)
			{
				logger.LogError($"{ex.Message}");
			}

			return response;
		}

	}
}
