using CPH.Data;
using CPH.Models;
using CPH.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Repos
{
    public class RegionRepo : IRegion
    {
        private ApplicationDbContext _db;

        public RegionRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Region> Create(Region item)
        {
            var check = await ReadByName(item.Name);

            if (check != null)
                throw new Exception("Region already exists.");

            await _db.Region.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public Task Delete(Region item)
        {
            throw new NotImplementedException();
        }

        public Task<Region> Read(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Region>> ReadAll()
        {
            return await _db.Region.ToListAsync();
        }

        public async Task<Region> ReadByName(string regionName)
        {
            return await _db.Region.FirstOrDefaultAsync(x => x.Name == regionName);
        }

        public Task<Region> Update(Region item)
        {
            throw new NotImplementedException();
        }
    }
}
