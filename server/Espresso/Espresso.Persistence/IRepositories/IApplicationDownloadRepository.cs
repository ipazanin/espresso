using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.Persistence.IRepositories
{
    public interface IApplicationDownloadRepository
    {
        public Task<IEnumerable<ApplicationDownload>> GetApplicationDownloads();
    }
}
