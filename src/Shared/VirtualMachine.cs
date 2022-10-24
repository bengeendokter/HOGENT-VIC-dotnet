namespace Shared;

public class VirtualMachine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Hostname { get; set; }
    public string? FQDN { get; set; }
    public bool IsHighlyAvailable { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCreated { get; set; }
    public bool IsActive { get; set; }
    public int Cpu { get; set; }
    public int Ram { get; set; }
    public int Storage { get; set; }
    public ETemplate Template { get; set; }
    public EMode Mode { get; set; }
    public EDay AvailableDays { get; set; }
    public EBackupFrequency BackupFrequency { get; set; }
}
