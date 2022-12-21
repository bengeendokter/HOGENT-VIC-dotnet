using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIC.IntegrationTests.VirtualMachines;

[Parallelizable(ParallelScope.Self)]

public class VirtualMachineList : PageTest
{

    [Test]
    public async Task Show_20_virtualmachines()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/");

        var amount = await Page.Locator("data-test-id=table-row").CountAsync();
        amount.Equals(20);
    }

    //[Test]
    //public async Task Show_virtualmachines_filtered()
    //{
    //    await Page.GotoAsync($"{TestHelper.BaseUri}/vm/");

    //    var cpu = await Page.Locator("data-test-id=table-row").First.TextContentAsync();
    //    var cpu2 = await Page.Locator("data-test-id=txt-cpu").Last.TextContentAsync();
    //    Assert.GreaterOrEqual(cpu, cpu2);

    //}
}
