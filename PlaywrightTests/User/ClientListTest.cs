using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using VIC.IntegraionTests;

namespace VIC.IntegrationTests.User;

[Parallelizable(ParallelScope.Self)]
public class ClientListTest : PageTest
{
    [Test]
    public async Task Show_25_Items_In_ClientTable()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/klanten/");

        await Page.WaitForSelectorAsync("h1");

        var rows = await Page.Locator("tr").CountAsync();

        // 25 Rows + header row
        Assert.AreEqual(26, rows);
    }

    [Test]
    public async Task Show_Only_Internal()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/klanten/");

        await Page.WaitForSelectorAsync("h1");

    }
}
