﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class SupplierFilter : BaseFilter
	{
		public string? Name { get; set; }
	}
}
