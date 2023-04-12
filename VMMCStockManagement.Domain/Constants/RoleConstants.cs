using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Domain.Constants
{
	public static class RoleConstants
	{	
		//VMMC Roles
		public const string Requester = "Team Leader"; 
		public const string DistrictCoordinator = "District Coordinator";	
		public const string ProgramAdministrator = "Program Administrator";
		public const string HOApprover = "HO Approver";
		public const string Administrator = "Administrator";		


		public static AccessRole GetRoleEnum(this string role)
		{
			switch (role)
			{
				case Requester:
					return AccessRole.Requester;
				case DistrictCoordinator:
					return AccessRole.DistrictCoordinator;
				case ProgramAdministrator:
					return AccessRole.ProgramAdministrator;
				case HOApprover:
					return AccessRole.HOApprover;				
				default:
					return AccessRole.Admin;
			}
		}
	}
}
