﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class Make : BaseEntity
	{
		public string Name { get; set; }
		public static Make Create(MakeRequest request)
		{
			return new Make
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				Name = request.Name
			};
		}
	}
}
