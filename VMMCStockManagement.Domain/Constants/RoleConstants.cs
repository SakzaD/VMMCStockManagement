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
		public const string Administrator = "Administrator";
		public const string DataCapture = "DataCapture";
		public const string Supplier = "Supplier";

		public const string StockManager = "StockManager";
		public const string DistrictManager = "DistrictManager";
		public const string SiteManager = "SiteManager";


		public static AccessRole GetRoleEnum(this string role)
		{
			switch (role)
			{
				case DataCapture:
					return AccessRole.DataCapture;
				case Supplier:
					return AccessRole.Supplier;
				case StockManager:
					return AccessRole.StockManager;
				case DistrictManager:
					return AccessRole.DistrictManager;
				case SiteManager:
					return AccessRole.SiteManager;
				default:
					return AccessRole.Admin;
			}
		}
	}
}
