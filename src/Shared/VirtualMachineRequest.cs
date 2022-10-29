using System.ComponentModel.DataAnnotations;

namespace Shared;

public class VirtualMachineRequest
{
    [Required]
    public DateTime Date { get; set; }

    [StringLength(100, ErrorMessage = "Het veld reden is te lang")]
    public string? Reason { get; set; }

    [Required]
    [ValidateComplexType]
    public VMInfo VMInfo { get; set; } = new();

    public ERequestStatus Status { get; set; }
}
