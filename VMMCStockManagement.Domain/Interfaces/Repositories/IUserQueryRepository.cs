using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Repositories
{
	public interface IUserQueryRepository
	{
		IQueryable<User> Filter(UserFilter filter);
		User? GetUserById(string userId);
		User? GetUserByEmployeeNumber(string employeeNumber);
		Task<IList<User>> GetUsersByRole(string role);
		Task<IList<User>> GetUsersByRoles(List<string> roles);
		IList<User> GetUsersByIds(List<string> ids);
		User? GetUserByIdNumber(string idNumber);
		User? GetUserByUsername(string email);

		ObjectResponse<UserResponse> SearchByEmployeeNumber(string? searchValue);

	}
}
