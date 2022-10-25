using System.Collections.Generic;

namespace Shared
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly List<VirtualMachine> _vms = new List<VirtualMachine>();

        public VirtualMachineService()
        {
            SetDummyVirtualMachineList();
        }

        public VirtualMachine? Get(int id)
        {
            return _vms.FirstOrDefault(v => v.Id == id);
        }

        public List<VirtualMachine> GetAll()
        {
            return _vms;
        }

        // TODO: Dto
        public VirtualMachine? Update(int id, VirtualMachine updatedVm)
        {
            var vm = _vms.FirstOrDefault(v => v.Id == id);
            if (vm == null) return null;
            // TODO: enkel de mogelijke properties
            vm = updatedVm;
            return vm;
        }

        private void SetDummyVirtualMachineList()
        {
            var vm1 = new VirtualMachine
            {
                Id = 1,
                Name = "VM-IT-1",
                Hostname = "VM_JN58CE_2354",
                FQDN = "TBD",
                IsHighlyAvailable = true,
                StartDate = new DateTime(2022, 01, 15),
                EndDate = new DateTime(2023, 02, 14),
                IsCreated = true,
                IsActive = true,
                Template = Template.TEMPLATES[ETemplate.ArtificialIntelligence],
                AvailableDays = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
                BackupFrequency = EBackupFrequency.Daily
            };
            
            var vm2 = new VirtualMachine
            {
                Id = 2,
                Name = "VM-IT-2",
                Hostname = "VM_GH35ZR_5436",
                FQDN = "TBD",
                IsHighlyAvailable = false,
                StartDate = new DateTime(2022, 02, 16),
                EndDate = new DateTime(2023, 03, 17),
                IsCreated = true,
                IsActive = false,
                Template = Template.TEMPLATES[ETemplate.Database],
                AvailableDays = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday + (int)EDay.Saturday + (int)EDay.Sunday),
                BackupFrequency = EBackupFrequency.Weekly
            };

            // TODO more vms

            _vms.Add(vm1);
            _vms.Add(vm2);
        }
    }
}
