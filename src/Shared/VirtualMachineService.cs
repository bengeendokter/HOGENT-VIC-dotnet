namespace Shared
{
    public class VirtualMachineService
    {
        private List<VirtualMachine> _vms = new List<VirtualMachine>();

        public VirtualMachineService()
        {
            setDummyVirtualMachineList();
        }

        public List<VirtualMachine> getAll()
        {
            return _vms;
        }

        private void setDummyVirtualMachineList()
        {
            var vm1 = new VirtualMachine();
            vm1.Name = "VM-IT-2";
            vm1.Hostname = "VM_JN58CE_2354";
            vm1.FQDN = "TBD";
            vm1.IsHighlyAvailable = true;
            vm1.StartDate = new DateTime(2022, 01, 15);
            vm1.EndDate = new DateTime(2023, 02, 14);
            vm1.IsCreated = true;
            vm1.IsActive = true;
            vm1.Cpu = 6;
            vm1.Ram = 32;
            vm1.Storage = 50;
            vm1.Template = ETemplate.ArtificialIntelligence;
            vm1.Mode = EMode.IaaS;
            vm1.AvailableDays = (EDay)((int) EDay.Monday + (int) EDay.Tuesday + (int) EDay.Wednesday + (int) EDay.Thursday + (int) EDay.Friday);
            vm1.BackupFrequency = EBackupFrequency.Daily;

            _vms.Add(vm1);

            // TODO vm2
        }
    }
}
