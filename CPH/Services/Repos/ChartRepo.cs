using CPH.Data;
using CPH.Models;
using CPH.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var check = await Read(item.ChartId);

            if (check != null)
                return item;

            await _db.Chart.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(Chart item)
        {
            var check = await Read(item.ChartId);

            if (check != null)
            {
                _db.Chart.Remove(item);
                _db.SaveChanges();
            }
        }

        public async Task<Chart> Read(string Id)
        {
            return await _db.Chart.FirstOrDefaultAsync(x => x.ChartId == Id);
        }

        public async Task<ICollection<Chart>> ReadAll()
        {
            return await _db.Chart.ToListAsync();
        }

        public async Task<Chart> Update(Chart item)
        {
            var check = await Read(item.ChartId);

            if (check == null)
                return null;

            check = item;
            _db.SaveChanges();

            return check;
        }
    }
}
