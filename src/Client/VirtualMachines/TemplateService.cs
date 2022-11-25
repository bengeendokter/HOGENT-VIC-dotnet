namespace Client.VirtualMachines;

public class TemplateService : ITemplateService
{
    private readonly List<TemplateDto.Detail> _templates = new()
    {
        new ()
        {
            Id = 1,
            Type = ETemplate.ArtificialIntelligence,
            Mode = EMode.PaaS,
            CPU = 2,
            RAM = 32,
            Storage = 8
        },
        new ()
        {
            Id = 2,
            Type = ETemplate.Database,
            Mode = EMode.IaaS,
            CPU = 4,
            RAM = 4,
            Storage = 200
        },
        new ()
        {
            Id = 3,
            Type = ETemplate.MachineLearning,
            Mode = EMode.SaaS,
            CPU = 8,
            RAM = 64,
            Storage = 80
        }
    };

    public List<TemplateDto.Detail> GetAll()
    {
        return _templates;
    }

    public TemplateDto.Detail? Create(TemplateDto.Create template)
    {
        var lastTemplate = _templates.LastOrDefault();
        var newTemplate = new TemplateDto.Detail
        {
            Id = lastTemplate!.Id + 1,
            Type = template.Type,
            Mode = template.Mode,
            CPU = template.CPU,
            RAM = template.RAM,
            Storage = template.Storage,
        };

        _templates.Add(newTemplate);
        return newTemplate;
    }
}
