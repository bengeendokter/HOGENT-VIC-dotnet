namespace Shared.VirtualMachines;

public static class VirtualMachineDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
