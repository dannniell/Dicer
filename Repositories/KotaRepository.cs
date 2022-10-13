﻿using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class KotaRepository : IKotaService
    {
        private readonly ApplicationDbContext _context;

        public KotaRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Kota>> GetAllKota(int provinsiId)
        {
            var param = new SqlParameter("@ProvinsiId", provinsiId);

            var result = _context.kotas.FromSqlRaw(Constants.Constants.getKota, param).ToList();

            return result;
        }
    }
}
