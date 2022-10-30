using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public interface IVirtualMachineRequestService
{
    List<VirtualMachineRequest> GetAll();

    VirtualMachineRequest? Get(int id);

    //TODO: DTO
    VirtualMachineRequest? Update(int id, VirtualMachineRequest request);
}
