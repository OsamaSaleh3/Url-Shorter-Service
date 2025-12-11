using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UrlRepository
    {
        private readonly ApplicationDbContext _db;
        public UrlRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> AddUrl(Url url, CancellationToken ct)
        {
            await _db.Url.AddAsync(url, ct);
            await _db.SaveChangesAsync(ct);
            return url.Id;
        }

        public async Task<string> GetLongUrlByShortCode(string shortCode, CancellationToken ct)
        {
            var url = await _db.Url.SingleOrDefaultAsync(u => u.ShortCode == shortCode, ct);
            if (url is not null)
                return url.LongUrl;
            return "";
        }

        public async Task<bool> CheckIfShortCodeExists(string shortCode, CancellationToken ct)
        {
            return await _db.Url.AnyAsync(u => u.ShortCode == shortCode, ct);
        }
    }
}
