using System.ComponentModel.DataAnnotations;

namespace Shared;

public class VirtualMachine
{
    public int Id { get; set; }
    [Required]
    public string Host { get; set; }
    [Required]
    [ValidateComplexType]
    public VMInfo? VMInfo { get; set; } = new();
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}
