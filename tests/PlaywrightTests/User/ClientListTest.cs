using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using VIC.IntegrationTests;

namespace VIC.IntegrationTests.User;

[Parallelizable(ParallelScope.Self)]
public class ClientListTest : PageTest
{
    [Test]
    public async Task Test1_Show_25_Items_In_ClientTable()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/klanten/");

        await Page.WaitForSelectorAsync("td");

        var rows = await Page.GetByTestId("table-row").CountAsync();

        // 25 Rows
        Assert.That(rows, Is.EqualTo(25));
    }

    [Test]
    public async Task Test2_Show_Only_Internal()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/klanten/");

        await Page.WaitForSelectorAsync("td");

        await Page.GetByTestId("select-type").SelectOptionAsync(new[] { new SelectOptionValue() { Label = "Interne" } });

        var rows =  await Page.GetByTestId("table-row").AllInnerTextsAsync();

        Assert.That(rows.All(row => row.Contains("Intern")), Is.EqualTo(true));
    }
}
