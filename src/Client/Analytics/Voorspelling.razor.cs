using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Analytics
{
    public partial class Voorspelling
    {
        [Inject] public IVirtualMachineService VirtualMachineService { get; set; } = default!;

        public List<UsageDto.Index> Usages
        {
            get => new()
            {
                new UsageDto.Index { UsageType = EUsage.Cpu, Unit = EUsageUnit.Cores, Total = DefinitieftotaalCPU, Used = DefinitiefgebruikCPU },
                new UsageDto.Index { UsageType = EUsage.Ram, Unit = EUsageUnit.GB, Total = DefinitieftotaalRAM, Used = DefinitiefgebruikRAM },
                new UsageDto.Index { UsageType = EUsage.Storage, Unit = EUsageUnit.GB, Total = DefinitieftotaalStorage, Used = DefinitiefgebruikStorage }
            };
            set => Usages = value;
        }

        private List<VirtualMachineDto.Index> _vms = new();
        private DataItem[] historiek => GrafiekWeergave();
        private DateTime datum = DateTime.Now;
        private int evolutie = 5;

        private bool error = false;
        private string errorMessage = string.Empty;
        private bool loading = false;
        //totaal
        private int TotaalVMCPU(DateTime datum)
        {
            return _vms.Where(v => (v.StartDate <= datum)).ToList().Aggregate(0, (acc, v) => acc + v.CPU);
        }

        private int TotaalVMRAM(DateTime datum)
        {
            return _vms.Where(v => (v.StartDate <= datum)).ToList().Aggregate(0, (acc, v) => acc + v.RAM);
        }

        private int TotaalVMStorage(DateTime datum)
        {
            return _vms.Where(v => (v.StartDate <= datum)).ToList().Aggregate(0, (acc, v) => acc + v.Storage);
        }

        //vrij capaciteit vm
        private int[] VrijVMS(DateTime datum)
        {
            int[] lijst = new int[] { 0, 0, 0 };
            _vms.Where(v => (v.StartDate <= datum && v.EndDate <= datum)).ToList().ForEach(v =>
            {
                int tempCPU = lijst[0];
                int tempRAM = lijst[1];
                int tempStorage = lijst[2];
                lijst[0] = tempCPU + v.CPU;
                lijst[1] = tempRAM + v.RAM;
                lijst[2] = tempStorage + v.Storage;
            });
            return lijst;
        }

        //berekenen
        private int DefinitieftotaalCPUfunc(DateTime datum)
        {
            return TotaalVMCPU(datum);
        }

        private int DefinitiefvrijCPUfunc(DateTime datum)
        {
            return VrijVMS(datum)[0];
        }

        private int DefinitiefgebruikCPUfunc(DateTime datum)
        {
            return (TotaalVMCPU(datum) - VrijVMS(datum)[0]);
        }

        private int DefinitieftotaalRAMfunc(DateTime datum)
        {
            return TotaalVMRAM(datum);
        }

        private int DefinitiefvrijRAMfunc(DateTime datum)
        {
            return VrijVMS(datum)[1];
        }

        private int DefinitiefgebruikRAMfunc(DateTime datum)
        {
            return (TotaalVMRAM(datum) - VrijVMS(datum)[1]);
        }

        private int DefinitieftotaalStoragefunc(DateTime datum)
        {
            return TotaalVMStorage(datum);
        }

        private int DefinitiefvrijStoragefunc(DateTime datum)
        {
            return VrijVMS(datum)[2];
        }

        private int DefinitiefgebruikStoragefunc(DateTime datum)
        {
            return (TotaalVMStorage(datum) - VrijVMS(datum)[2]);
        }

        private int DefinitieftotaalCPU => DefinitieftotaalCPUfunc(datum);
        private int DefinitiefvrijCPU => DefinitiefvrijCPUfunc(datum);
        private int DefinitiefgebruikCPU => DefinitiefgebruikCPUfunc(datum);
        private int DefinitieftotaalRAM => DefinitieftotaalRAMfunc(datum);
        private int DefinitiefvrijRAM => DefinitiefvrijRAMfunc(datum);
        private int DefinitiefgebruikRAM => DefinitiefgebruikRAMfunc(datum);
        private int DefinitieftotaalStorage => DefinitieftotaalStoragefunc(datum);
        private int DefinitiefvrijStorage => DefinitiefvrijStoragefunc(datum);
        private int DefinitiefgebruikStorage => DefinitiefgebruikStoragefunc(datum);
        /*GRAFIEK*/
        class DataItem
        {
            public DateTime Date { get; set; }

            public double CPU { get; set; }

            public double RAM { get; set; }

            public double Storage { get; set; }
        }

        string FormatAsMonth(object value)
        {
            if (value != null)
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }

        private DataItem[] GrafiekWeergave()
        {
            //gekozen datum
            DateTime begin = datum;
            //tot x maanden (evolutie)
            int tot = evolutie;
            DateTime end = datum.AddMonths(tot);
            //alle datums opvullen
            var datums = new List<DateTime>();
            for (var dt = begin; dt <= end; dt = dt.AddMonths(1))
            {
                datums.Add(dt);
            }

            DataItem[] data = new DataItem[datums.Count()];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new DataItem { Date = datums[i], CPU = (double)DefinitiefgebruikCPUfunc(datums[i]), RAM = (double)DefinitiefgebruikRAMfunc(datums[i]), Storage = (double)DefinitiefgebruikStoragefunc(datums[i]) };
            }

            return data;
        }

        protected override async Task OnInitializedAsync()
        {

            VirtualMachineReq.Index request = new()
            {
                PageSize = 1000,
            };

            try
            {
                loading = true;
                _vms = await VirtualMachineService.GetIndexAsync(request);
                loading = false;
            }
            catch (Exception ex)
            {
                loading = false;
                error = true;
                errorMessage = ex.Message;
            }
        }
    }
}