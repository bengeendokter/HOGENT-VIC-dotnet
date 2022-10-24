﻿namespace Shared
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly List<VirtualMachine> _vms = new List<VirtualMachine>();

        public VirtualMachineService()
        {
            SetDummyVirtualMachineList();
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
                Name = "VM-IT-2",
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

            _vms.Add(vm1);

            // TODO vm2
        }
    }
}