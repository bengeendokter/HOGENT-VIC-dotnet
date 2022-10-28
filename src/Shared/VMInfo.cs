using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Shared;

public class VMInfo
{
    private ValidationMessageStore? messageStore;

    [Required]
    [StringLength(30, ErrorMessage = "De naam is te lang")]
    [MinLength(3, ErrorMessage = "De naam moet langer dan 3 characters zijn")]
    public string? Name { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "De hostname moet langer dan 3 characters zijn")]
    public string? HostName { get; set; }
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now;
    [Required]
    public bool IsHighAvailable { get; set; }
    [Required]
    public string? FQDN { get; set; }
    [Required]
    public string? Poorten { get; set; }
    [Required]
    public EBackupFrequency EBackupFrequency { get; set; }
    [Required]
    public EDay? Availability { get; set; }
    [Required]
    public Template? Template { get; set; }
}
