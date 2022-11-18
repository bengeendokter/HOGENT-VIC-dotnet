namespace Shared.VirtualMachines;
using System.ComponentModel.DataAnnotations;
public class Template
{
    public static readonly List<Template> TEMPLATES = new()
    {

            new ()
            {
                Type = ETemplate.ArtificialIntelligence,
                Mode = EMode.PaaS,
                CPU = 2,
                RAM = 32,
                Storage = 8
            },


            new ()
            {
                Type = ETemplate.Database,
                Mode = EMode.IaaS,
                CPU = 4,
                RAM = 4,
                Storage = 200
            },
        

            new ()
            {
                Type = ETemplate.MachineLearning,
                Mode = EMode.SaaS,
                CPU = 8,
                RAM = 64,
                Storage = 80

            },
       
    };
    public ETemplate Type {get; set; }
    [Required(ErrorMessage = "Dit veld is verplicht")]
    public EMode Mode { get; set; }
    [Required]
    public int CPU { get; set; }
    [Required]
    public int RAM { get; set; }
    [Required]
    public int Storage { get; set; }
}
