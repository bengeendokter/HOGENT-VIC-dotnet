namespace Shared.Clients;

public static class ClientDto
{
    public class Index 
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? PhoneNumber { get; set; }
        public EClientType ClientType { get; set; }
        public string? ClientOrganisation { get; set; }
    }

    public class Detail : Index
    {
        public string? Email { get; set; }
        public string? BackupContact { get; set; }
        public string? Education { get; set; }

        public string? ExternalType { get; set; }
    }

}

