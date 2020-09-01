using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<byte[]> GenerateSowsListPdfAttachmentAsync(string userId);
    }
}
