using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models;

namespace VMMCStockManagement.Domain.Utils
{
	public interface IEmailService
	{
		void Send(Email email);
	}
}
