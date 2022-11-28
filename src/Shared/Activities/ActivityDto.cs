using Shared.VirtualMachines;
namespace Shared.Activities;

public static class ActivityDto
{
    public class Index
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public VirtualMachineDto.Index? VirtualMachine { get; set; }
        public EActivity Type { get; set; }
    }
}
