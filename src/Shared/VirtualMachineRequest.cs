using System.ComponentModel.DataAnnotations;

namespace Shared;

public class VirtualMachineRequest
{
    //[Required]
    //public DateTime Date { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "Naam is te lang")]
    [MinLength(10, ErrorMessage = "Te klein")]
    public string? Reason { get; set; }

    //    [Required]
    //    public VMInfo? VMInfo { get; set; }
}
