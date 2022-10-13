using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class ProvinsiRepository : IProvinsiService
    {
        private readonly ApplicationDbContext _context;

        public ProvinsiRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Provinsi>> GetAllProvinsi()
        {
            var result = _context.provinsis.FromSqlRaw(Constants.Constants.getProvinsi).ToList();
            return result;
        }
    }
}