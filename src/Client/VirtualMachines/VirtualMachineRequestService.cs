using Shared.VirtualMachines;

namespace Client.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly List<VirtualMachineRequest> _requests = new List<VirtualMachineRequest>();
    public VirtualMachineRequestService()
    {
        SetDummyRequestList();
    }
    
    public VirtualMachineRequest? Get(int id)
    {
        return _requests.FirstOrDefault(x => x.Id == id);
    }

    public List<VirtualMachineRequest> GetAll()
    {
        Console.WriteLine("test");
        return _requests;
    }

    public VirtualMachineRequest? Update(int id, VirtualMachineRequest request)
    {
        throw new NotImplementedException();
    }

    private void SetDummyRequestList()
    {
        var request1 = new VirtualMachineRequest
        {
            Id = 1,
            Date = DateTime.Now,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
            ProjectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "test@help.be",
            NummerAanvrager = 0469569562
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
            Date = DateTime.Now,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor een DevOps opdracht.",
            ProjectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "test@help.be",
            NummerAanvrager = 0469569562
        };
        var request3 = new VirtualMachineRequest
        {
            Id = 3,
            Date = DateTime.Now,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor iets online te zetten",
            ProjectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "test@help.be",
            NummerAanvrager = 0469569562
        };
        _requests.Add(request1);
        _requests.Add(request2);
        _requests.Add(request3);
    }
}
