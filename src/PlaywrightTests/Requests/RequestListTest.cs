using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIC.IntegrationTests.Requests;

[Parallelizable(ParallelScope.Self)]
public class RequestListTest : PageTest
{

    [Test]
    public async Task Show_Request_with_20_items()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/list/");

        var amount = await Page.Locator("data-test-id=table-row").CountAsync();
        Assert.That(amount, Is.EqualTo(20));

    }

    [Test]
    public async Task Show_Request_after_filter()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/list/");

        await Page.Locator("data-test-id=filter-status").SelectOptionAsync(new[] { new SelectOptionValue() { Label = "Denied" } });
        var amount = await Page.Locator("data-test-id=table-row").CountAsync();
        amount.Equals(0);

    }
}
