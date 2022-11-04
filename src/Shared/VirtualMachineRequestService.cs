using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

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
            Date = new DateTime(2022, 01, 01),
            StartDate = new DateTime(2022, 02, 01),
            EndDate = new DateTime(2022, 03, 01),
            Reason = "Virtual machine voor een bachlerproef onderzoek ivm AI.",
            projectNaam = "bachlerproef AI",
            Status = ERequestStatus.Accepted,
            EmailAanvrager = "test@help.be"
        };
        var request2 = new VirtualMachineRequest
        {
            Id = 2,
            Date = new DateTime(2022, 01, 02),
            StartDate = new DateTime(2022, 02, 02),
            EndDate = new DateTime(2022, 03, 02),
            Reason = "Virtual machine voor een DevOps opdracht.",
            projectNaam = "Opdracht",
            Status = ERequestStatus.Handled,
            EmailAanvrager = "test@help.be"
        };
        var request3 = new VirtualMachineRequest
        {
            Id = 3,
            Date = new DateTime(2022, 04, 05),
            StartDate = new DateTime(2022, 05, 05),
            EndDate = new DateTime(2022, 05, 20),
            Reason = "Virtual machine voor iets online te zetten",
            projectNaam = "Online",
            Status = ERequestStatus.Denied,
            EmailAanvrager = "test@help.be"
        };

        _requests.Add(request1);
        _requests.Add(request2);
        _requests.Add(request3);
    }
}
