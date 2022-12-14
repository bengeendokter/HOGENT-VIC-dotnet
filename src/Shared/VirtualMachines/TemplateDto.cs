using FluentValidation;

namespace Shared.VirtualMachines;

public static class TemplateDto
{
    public class Detail
    {
        public int Id { get; set; }
        public ETemplate Type { get; set; }
        public ESoftware Software { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
    }

    public class Create
    {
        public ETemplate Type { get; set; }
        public ESoftware Software { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                RuleFor(x => x.Type).IsInEnum().WithMessage("Ongeldig type");
                RuleFor(x => x.Software).IsInEnum().WithMessage("Ongeldig mode");
                RuleFor(x => x.CPU).GreaterThanOrEqualTo(1).WithMessage("CPU moet strikt positief zijn");
                RuleFor(x => x.RAM).GreaterThanOrEqualTo(1).WithMessage("RAM moet strikt positief zijn");
                RuleFor(x => x.Storage).GreaterThanOrEqualTo(1).WithMessage("Storage moet strikt positief zijn");
            }
        }
    }
}
