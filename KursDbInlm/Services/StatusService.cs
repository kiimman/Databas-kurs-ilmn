using KursDbInlm.Contexts;
using KursDbInlm.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KursDbInlm.Services;

internal class StatusService
{
    private readonly DataContext _context = new DataContext();

    public async Task CreateStatusTypesAsync()
    {
        if(!await _context.Statuses.AnyAsync())
        {
            string[] _statuses = new string[] { "Ej påbörjad", "Påbörjad", "Avslutad" };

            foreach(var status in _statuses) 
            {
                await _context.AddAsync(new StatusEntity { StatusName = status});
                await _context.SaveChangesAsync();
            }
        }
    }
}
