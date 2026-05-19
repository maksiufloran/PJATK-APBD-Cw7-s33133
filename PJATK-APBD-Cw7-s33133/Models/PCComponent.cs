using System.ComponentModel.DataAnnotations.Schema;

namespace PJATK_APBD_Cw7_s33133.Models;

public class PCComponent
{
    public int PCId { get; set; }
    [ForeignKey(nameof(PCId))]
    public PC PC { get; set; } = null!;

    [Column(TypeName = "char(10)")]
    public string ComponentCode { get; set; } = null!;
    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;

    public int Amount { get; set; }
}