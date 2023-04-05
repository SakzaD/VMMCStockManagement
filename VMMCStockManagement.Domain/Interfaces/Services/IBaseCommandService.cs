using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;

namespace VMMCStockManagement.Domain.Interfaces.Services
{
	public interface IBaseCommandService<Ent> where Ent : BaseEntity
	{
		CreationResponse<Ent> Create(Ent entity);
		BaseResponse Update(Ent entity);
		BaseResponse Delete(long id);
		BaseResponse Delete(Ent entity);



	}
}
