using Domain.VirtualMachines;
using Fakers.Clients;
using Fakers.Common;

namespace Fakers.VirtualMachines;

public class VirtualMachineRequestFaker : EntityFaker<VirtualMachineRequest>
{

    public VirtualMachineRequestFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(
            f =>
               new VirtualMachineRequest(
                   f.Date.Future(1, DateTime.Now.AddDays(1)).ToUniversalTime(),
                   f.Date.Future(1, DateTime.Now.AddYears(1)).ToUniversalTime(),
                   f.Lorem.Paragraph(),
                   f.Lorem.Word(),
                   new ClientFaker(locale).AsTransient(),
                   null,
                   ERequestStatus.Requested
                   )
        );
    }

}
