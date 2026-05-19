using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw7_s33133.Data;
using PJATK_APBD_Cw7_s33133.DTOs;
using PJATK_APBD_Cw7_s33133.Models;

namespace PJATK_APBD_Cw7_s33133.Services;

public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PCDto>> GetAllPCsAsync()
    {
        return await _context.PCs
            .Select(p => new PCDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            }).ToListAsync();
    }

    public async Task<PCDetailsDto?> GetPCWithComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.Manufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.Type)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null) return null;

        return new PCDetailsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents.Select(c => new PCComponentDto
            {
                Amount = c.Amount,
                Component = new ComponentDto
                {
                    Code = c.Component.Code,
                    Name = c.Component.Name,
                    Description = c.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = c.Component.Manufacturer.Id,
                        Abbreviation = c.Component.Manufacturer.Abbreviation,
                        FullName = c.Component.Manufacturer.FullName,
                        FoundationDate = c.Component.Manufacturer.FoundationDate.ToString("yyyy-MM-dd")
                    },
                    Type = new TypeDto
                    {
                        Id = c.Component.Type.Id,
                        Abbreviation = c.Component.Type.Abbreviation,
                        Name = c.Component.Type.Name
                    }
                }
            }).ToList()
        };
    }

    public async Task<PCDto> CreatePCAsync(PCRequestDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PCDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdatePCAsync(int id, PCRequestDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePCAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        _context.PCs.Remove(pc); // Usunie kaskadowo powiązania w PCComponents dzięki konfiguracji EF Core
        await _context.SaveChangesAsync();
        return true;
    }
}