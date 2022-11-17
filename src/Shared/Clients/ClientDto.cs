namespace Shared.Clients;

public static class ClientDto
{
    public class Index
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? Email { get; set; }
    }

    public class InternalClientDetail : Index
    {
        public string? Department { get; set; }
        public string? Education { get; set; }
    }

    public class ExternalClientDetail : Index
    {
        public string? ExternalName { get; set; }
        public string? Type { get; set; }
    }
}

