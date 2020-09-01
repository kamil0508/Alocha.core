using Alocha.Domain.Interfaces;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.SowVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateSowsListPdfAttachmentAsync(string userId)
        {
            var sows = await _unitOfWork.Sow.FindAllAsync(s => s.UserId == userId && !s.IsRemoved);
            var sowVMs = _mapper.Map<IEnumerable<SowVM>>(sows);
            var pdfHelper = new PdfDocument(sowVMs);

            return pdfHelper.Generate();
        }
    }
}
