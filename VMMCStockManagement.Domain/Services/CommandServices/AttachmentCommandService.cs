using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Interfaces.Repositories;
using VMMCStockManagement.Domain.Interfaces.Services.CommandServices;
using VMMCStockManagement.Domain.Models.Requests.Filters;
using VMMCStockManagement.Domain.Models.Requests;
using VMMCStockManagement.Domain.Models.Responses;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Services.CommandServices
{
	public class AttachmentCommandService : BaseCommandService<Attachment>, IAttachmentCommandService
	{

		private readonly IQueryRepository<Attachment, AttachmentFilter> queryRepository;
		public AttachmentCommandService(ICommandRepository<Attachment> commandRepository,
			ILogger<BaseCommandService<Attachment>> logger,
			IQueryRepository<Attachment, AttachmentFilter> queryRepository) : base(commandRepository, logger)
		{
			this.queryRepository = queryRepository;
		}

		public override string ServiceName => nameof(AttachmentCommandService);


		public BaseResponse Add(AttachmentRequest request)
		{

			Attachment attachment = Attachment.Create(request);

			return Create(attachment);
		}

		public override void AfterCreation(Attachment entity)
		{

		}

		public BaseResponse Delete(BaseRequest request)
		{
			var district = queryRepository.GetById(request.Id.Value);

			if (district == null)
			{
				return new BaseResponse
				{
					CodeStatus = Enums.ResponseStatus.Fail,
					Message = "Invalid asset selected"
				};
			}


			district.UpdatedAt = DateTime.Now;
			district.UpdatedBy = request.UserId;
			district.Status = Enums.EntityStatus.Deleted;
			var response = Delete(district);

			return response;
		}

		public BaseResponse Edit(AttachmentRequest request)
		{
			var asset = queryRepository.GetById(request.Id.Value);


			asset.UpdatedAt = DateTime.Now;


			var response = Update(asset);

			return response;
		}
	}
}
