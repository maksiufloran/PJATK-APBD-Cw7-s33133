namespace PJATK_APBD_Cw7_s33133.DTOs;

public class ComponentDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ManufacturerDto Manufacturer { get; set; } = null!;
    public TypeDto Type { get; set; } = null!;
}