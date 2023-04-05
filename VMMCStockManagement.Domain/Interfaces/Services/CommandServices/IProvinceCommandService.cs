﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services.CommandServices
{
	public interface IProvinceCommandService : IBaseCommandService<Province>
	{
		BaseResponse Add(ProvinceRequest request);
		BaseResponse Edit(ProvinceRequest request);
		BaseResponse Delete(BaseRequest request);
	}
}
