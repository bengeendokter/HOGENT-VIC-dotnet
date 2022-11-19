using FluentValidation;

namespace Shared.VirtualMachines;

public static class TemplateDto
{
    /*public static readonly List<Template> TEMPLATES = new()
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
       
    };*/

    public class Detail
    {
        public int Id { get; set; }
        public ETemplate Type { get; set; }
        public EMode Mode { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
    }

    public class Create
    {
        public ETemplate Type { get; set; }
        public EMode Mode { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.Type).IsInEnum().WithMessage("Ongeldig type");
                RuleFor(x => x.Mode).IsInEnum().WithMessage("Ongeldig mode");
                RuleFor(x => x.CPU).GreaterThanOrEqualTo(1).WithMessage("CPU moet strikt positief zijn");
                RuleFor(x => x.RAM).GreaterThanOrEqualTo(1).WithMessage("RAM moet strikt positief zijn");
                RuleFor(x => x.Storage).GreaterThanOrEqualTo(1).WithMessage("Storage moet strikt positief zijn");
            }
        }
    }
}
