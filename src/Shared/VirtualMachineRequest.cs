using System.ComponentModel.DataAnnotations;

namespace Shared;

public class VirtualMachineRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now;
    [Required]
    [StringLength(300, ErrorMessage = "Het veld reden is te lang")]
    [MinLength(30, ErrorMessage = "Geef voldoende informatie bij het aanvragen van de VM.")]
    public string? Reason { get; set; }
    [Required]
    public string? EmailAanvrager { get; set; }

    public string? NummerAanvrager { get; set; }

    [Required]
    public string projectNaam { get; set; }

    public ERequestStatus Status { get; set; }
}
