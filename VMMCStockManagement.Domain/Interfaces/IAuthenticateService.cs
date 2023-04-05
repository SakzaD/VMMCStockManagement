using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces
{
	public interface IAuthenticateService
	{
		Task<ObjectResponse<UserResponse>> Register(UserRequest request);
		Task<ObjectResponse<UserResponse>> Update(UserRequest request);
		Task<ObjectListResponse<UserResponse>> Filter(UserFilter filter);
		Task<ObjectListResponse<UserResponse>> GetManagers(UserFilter filter);
		Task<ObjectResponse<UserResponse>> GetUserByIdAsync(string userId);
		User? GetById(string userId);
		Task<BaseResponse> CreateNewRole(RoleRequest request);
		Task<BaseResponse> RemoveUserRole(RoleDeleteRequest request);
		Task<BaseResponse> AddUserToRole(RoleAddRequest request);
		Task<User> AddNoneEmployeeUser(UserRequest request);
		Task<BaseResponse> DeleteUser(UserDeleteRequest request);
		Task<BaseResponse> UploadUser(UserUploadRequest request);
		Task<ObjectResponse<UserResponse>> GetUserById(string userId);

	}
}
