using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Enums
{
	public enum AccessRole
	{
		Requester = 1,
		HOApprover,
		DistrictCoordinator,
		ProgramAdministrator,		
		Admin,
		Unknown
	}
}
