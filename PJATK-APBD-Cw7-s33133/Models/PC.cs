using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJATK_APBD_Cw7_s33133.Models;

public class PC
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    [Column(TypeName = "float")]
    public float Weight { get; set; }
    
    public int Warranty { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
    
    public int Stock { get; set; }

    // Właściwość nawigacyjna
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}