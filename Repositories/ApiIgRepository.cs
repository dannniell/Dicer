using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class ApiIgRepository : IApiIgService
    {
        private readonly ApplicationDbContext _context;

        public ApiIgRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<ApiIGModel>> GetDataIg()
        {
            var result = _context.apiIGModels.FromSqlRaw(Constants.Constants.getDataIg).ToList();
            return result;
        }
    }
}
