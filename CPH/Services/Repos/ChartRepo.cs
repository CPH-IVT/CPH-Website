using CPH.Data;
using CPH.Models;
using CPH.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Repos
{
    public class ChartRepo : IChart
    {
        private ApplicationDbContext _db;

        public ChartRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Chart> Create(Chart item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Chart item)
        {
            throw new NotImplementedException();
        }

        public async Task<Chart> Read(string Id)
        {
            return _db.Chart.FirstOrDefault(x => x.id == Id);
        }

        public async Task<ICollection<Chart>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Chart> Update(Chart item)
        {
            throw new NotImplementedException();
        }
    }
}
