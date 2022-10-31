using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

internal class VirtualMachineRequestService : IVirtualMachineRequestService
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
            projectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "test@help.be"
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
            Date = DateTime.Now,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor een DevOps opdracht.",
            projectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "test@help.be"
        };
        var request3 = new VirtualMachineRequest
        {
            Id = 3,
            Date = DateTime.Now,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            Reason = "Virtual machine voor iets online te zetten",
            projectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "test@help.be"
        };
    }
}
