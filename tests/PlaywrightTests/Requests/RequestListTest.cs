using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace VIC.IntegrationTests.Requests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class RequestListTest : PageTest
{

    [Test]
    public async Task Test1_Show_Request_with_15_items()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/list/");
        await Page.WaitForSelectorAsync("table");

        var amount = await Page.GetByTestId("table-row").CountAsync();

        // 15 items
        Assert.That(amount, Is.EqualTo(15));

    }

    [Test]
    public async Task Test2_Show_Request_after_filter()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/list/");
        await Page.WaitForSelectorAsync("table");

        await Page.GetByTestId("filter-status").SelectOptionAsync(new[] { new SelectOptionValue() { Label = "Denied" } });

        var amount = await Page.GetByTestId("table-row").CountAsync();

        // no items
        Assert.That(amount, Is.EqualTo(0));

    }
}
