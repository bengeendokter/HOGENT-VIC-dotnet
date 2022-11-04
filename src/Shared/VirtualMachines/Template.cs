namespace Shared.VirtualMachines;

public class Template
{
    public static readonly Dictionary<ETemplate, Template> TEMPLATES = new()
    {
        { ETemplate.ArtificialIntelligence,
            new ()
            {
                Name = "Artificial Intelligence",
                Type = ETemplate.ArtificialIntelligence,
                Mode = EMode.PaaS,
                Stats = new ()
                {
                    Cpu = 6,
                    Ram = 32,
                    Storage = 25
                }
            }
        },
        { ETemplate.Database,
            new ()
            {
                Name = "Database",
                Type = ETemplate.Database,
                Mode = EMode.IaaS,
                Stats = new ()
                {
                    Cpu = 2,
                    Ram = 16,
                    Storage = 60
                }
            }
        },
        { ETemplate.MachineLearning,
            new ()
            {
                Name = "Machine Learning",
                Type = ETemplate.MachineLearning,
                Mode = EMode.SaaS,
                Stats = new ()
                {
                    Cpu = 8,
                    Ram = 64,
                    Storage = 80
                }
            }
        }
    };

    public string? Name { get; set; }
    public ETemplate Type {get; set; }
    public EMode Mode { get; set; }
    public Stats? Stats { get; set; }
}
