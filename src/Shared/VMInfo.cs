using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class VMInfo
{
    [Required]
    [StringLength(10, ErrorMessage = "Naam is te lang")]
    public string? Name { get; set; }
    [Required]
    public string? HostName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public bool IsHighAvailable { get; set; }
    [Required]
    public string? FQDN { get; set; }
    [Required]
    public int[]? Poorten { get; set; }
    [Required]
    public EBackupFrequency EBackupFrequency { get; set; }
    [Required]
    public EDay[] Availability { get; set; }

}
