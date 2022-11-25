namespace Shared.VirtualMachines;

public interface ITemplateService
{
    List<TemplateDto.Detail> GetAll();

    TemplateDto.Detail? Create(TemplateDto.Create template);
}