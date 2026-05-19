using PJATK_APBD_Cw7_s33133.DTOs;

namespace PJATK_APBD_Cw7_s33133.Services;

public interface IPCService
{
    Task<IEnumerable<PCDto>> GetAllPCsAsync();
    Task<PCDetailsDto?> GetPCWithComponentsAsync(int id);
    Task<PCDto> CreatePCAsync(PCRequestDto dto);
    Task<bool> UpdatePCAsync(int id, PCRequestDto dto);
    Task<bool> DeletePCAsync(int id);
}