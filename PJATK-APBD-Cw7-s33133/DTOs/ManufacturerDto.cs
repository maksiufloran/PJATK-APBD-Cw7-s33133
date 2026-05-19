namespace PJATK_APBD_Cw7_s33133.DTOs;

public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string FoundationDate { get; set; } = null!; // Zwracane jako string YYYY-MM-DD
}