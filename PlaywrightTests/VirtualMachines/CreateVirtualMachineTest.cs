using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIC.IntegrationTests.VirtualMachines;

[Parallelizable(ParallelScope.Self)]
public class CreateVirtualMachineTest : PageTest
{
    [Test]
    public async Task Create_VM()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/manage/");

        await Page.FillAsync("data-test-id=input-name", "vm1");
        await Page.FillAsync("data-test-id=input-hostname", "hostname");
        await Page.FillAsync("data-test-id=input-fqdn", "fqdn");
        await Page.FillAsync("data-test-id=input-startdate", "2024-01-01");
        await Page.FillAsync("data-test-id=input-enddate", "2024-02-02");
        await Page.FillAsync("data-test-id=input-host", "host");
        await Page.FillAsync("data-test-id=input-ports", "80, 200");
        await Page.Locator("data-test-id=btn-day").First.ClickAsync();

        await Page.Locator("data-test-id=btn-template").First.ClickAsync();

        await Page.Locator("data-test-id=btn-submit").ClickAsync();
    }
}
