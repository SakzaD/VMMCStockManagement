using VMMCStockManagement.Domain.Enums;

namespace VMMCStockManagement.Web.Utils
{
	public static class ParameterValidator	{

		public static AccessRole GetRole(this string type)
		{

			if (string.IsNullOrEmpty(type)) return AccessRole.Unknown;

			if (type.Equals("in", StringComparison.InvariantCultureIgnoreCase))
			{
				return AccessRole.DataCapture;
			}
            //else if (type.Equals("ln", StringComparison.InvariantCultureIgnoreCase))
            //{
            //	return AccessRole.SiteManager;
            //}

            //else if (type.Equals("im", StringComparison.InvariantCultureIgnoreCase))
            //{
            //	return AccessRole.StockManager;
            //}
            //else if (type.Equals("is", StringComparison.InvariantCultureIgnoreCase))
            //{
            //	return AccessRole.DistrictManager;
            //}

            return AccessRole.Unknown;
		}

	}
}
