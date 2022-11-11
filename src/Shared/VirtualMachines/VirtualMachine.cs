namespace Shared.VirtualMachines;

public class VirtualMachine
{
    public int Id { get; set; }
    public Client? Client { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Name { get; set; }
    public string? HostName { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public bool IsHighlyAvailable { get; set; }
    public string? FQDN { get; set; }
    public EBackupFrequency BackupFrequency { get; set; }
    public EDay Availability { get; set; }
    public Template? Template { get; set; }
    public bool IsActive { get; set; }

}
