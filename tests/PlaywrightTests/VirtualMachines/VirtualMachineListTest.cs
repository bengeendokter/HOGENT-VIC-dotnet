using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIC.IntegrationTests.VirtualMachines;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class VirtualMachineListTest : PageTest
{

    [Test]
    public async Task Test1_Show_20_virtualmachines()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/");
        await Page.WaitForSelectorAsync("h1");

        var amount = await Page.GetByTestId("table-row").CountAsync();
        Assert.That(amount, Is.EqualTo(20));
    }

    //[Test]
    //public async Task Show_virtualmachines_filtered()
    //{
    //    await Page.GotoAsync($"{TestHelper.BaseUri}/vm/");

    //    var cpu = await Page.Locator("data-test-id=table-row").First.TextContentAsync();
    //    var cpu2 = await Page.Locator("data-test-id=txt-cpu").Last.TextContentAsync();
    //    Assert.GreaterOrEqual(cpu, cpu2);

    //}

    [Test]
    public async Task Test2_Go_To_DetailPage()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/");
        await Page.WaitForSelectorAsync("td");

        await Page.GetByTestId("vm-name").First.ClickAsync();

        await Expect(Page.GetByTestId("div-detailgroup")).ToBeVisibleAsync();
    }
}
