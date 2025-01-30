﻿using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using ApiApplication.Database.Repositories.Abstractions;
using System.Collections.Generic;

namespace ApiApplication.Database.Repositories
{
    public class AuditoriumsRepository : IAuditoriumsRepository
    {
        private readonly CinemaContext _context;

        public AuditoriumsRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<AuditoriumEntity> GetAsync(int auditoriumId, CancellationToken cancel)
        {
            return await _context.Auditoriums
                .Include(x => x.Seats)
                .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancel);
        }

        public async Task<List<AuditoriumEntity>> GetAllAsync(CancellationToken cancel)
        {
            return await _context.Auditoriums
                .Include(x => x.Seats)
                .ToListAsync(cancel);
        }
    }
}
