using Bogus;
using Domain.VirtualMachines;
using Fakers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakers.VirtualMachines;

public class VirtualMachineRequestFaker: EntityFaker<VirtualMachineRequest>
{

    public VirtualMachineRequestFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(
            f =>
               new VirtualMachineRequest(
                   f.Date.Future(1, DateTime.Now.AddDays(1)),
                   f.Date.Future(1, DateTime.Now.AddYears(1)),
                   f.Lorem.Paragraph(),
                   f.Lorem.Word(),
                   //Client
                   f.Random.Enum<ERequestStatus>(),
                   f.Lorem.Paragraph()
                   )
        );
    }

}
