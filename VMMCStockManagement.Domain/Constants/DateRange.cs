using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Constants
{
	public static class DateRange
	{
		public static IEnumerable<DateTime> GetLastDays(this DateTime date, int days)
		{
			var tempDate = date.AddDays(-days);
			var startDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);

			while (startDate.Date < date.Date)
			{
				yield return startDate;
				startDate = startDate.AddDays(1);
			}
		}

		public static IEnumerable<DateTime> GetlastMonth(this DateTime date, int months)
		{
			var tempDate = date.AddMonths(-months);
			tempDate = tempDate.AddMonths(1);
			tempDate = tempDate.AddDays(1 - tempDate.Day);

			var startDate = new DateTime(tempDate.Year, tempDate.Month, 1);

			while (startDate < date)
			{
				yield return startDate;
				startDate = startDate.AddMonths(1);
			}
		}

		public static IEnumerable<DateTime> GetCurrentYearMonths(this DateTime date)
		{
			var startDate = new DateTime(date.Year, 1,1);
			var tempDate = startDate.AddMonths(12);

			while (startDate < date)
			{
				yield return startDate;
				startDate = startDate.AddMonths(1);
			}
		}

		public static IEnumerable<DateTime> GetLastYears(this DateTime date, int years)
		{			
			var tempDate = date.AddMonths(-years);
			var startDate = new DateTime(tempDate.Year, 1, 1);

			while (startDate < date)
			{
				yield return startDate;
				startDate = startDate.AddYears(1);
			}
		}
	}
}
