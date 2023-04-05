using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Interfaces.Services
{
	public interface IReminderService
	{
		Task ProcessReminder(List<string> roles, int numOfDays = 7);
	}
}
