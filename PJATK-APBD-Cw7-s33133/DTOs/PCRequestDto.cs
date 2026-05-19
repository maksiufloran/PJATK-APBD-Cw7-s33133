namespace PJATK_APBD_Cw7_s33133.DTOs;

public class PCRequestDto
{
    public string Name { get; set; } = null!;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}