using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MaakVMForm
{

    [Required]
    [StringLength(40, ErrorMessage = "Naam is te lang")]
    public string? VMNaam { get; set; }

    [Required]
    [StringLength(60, ErrorMessage = "Naam is te lang")]
    public string? HostName { get; set; }

    [Required]
    [StringLength(60, ErrorMessage = "Naam is te lang")]
    public string? FQDN { get; set; }

    [Required]
    public TemplateType TemplateType { get; set; }

    [Required]
    public ModeType ModeType { get; set; }
    [Required]
    public int CPUCores { get; set; }

    [Required]
    public int RAM { get; set; }

    [Required]
    public int Storage { get; set; }

    [Required]
    public bool IsHighAvailable { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string? Aanvrager { get; set; }

    [Required]
    public string? TypeBeschikbaarheid { get; set; }

    [Required]
    public string? Gebruiker { get; set; }

    [Required]
    public string[] poorten { get; set; }

    [Required]
    public string? host { get; set; }



}
