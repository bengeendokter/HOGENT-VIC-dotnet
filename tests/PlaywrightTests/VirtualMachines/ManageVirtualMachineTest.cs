using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace VIC.IntegrationTests.VirtualMachines;

[TestFixture]
public class ManageVirtualMachineTest : PageTest
{
    private string vm = "vm1";

    [Test]
    public async Task Test1_Create_VM()
    {

        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/manage/");

        await Page.GetByTestId("input-name").FillAsync(vm);
        await Page.GetByTestId("input-hostname").FillAsync("hostname");
        await Page.GetByTestId("input-fqdn").FillAsync("fqdn");
        await Page.GetByTestId("input-startdate").FillAsync("2024-01-01");
        await Page.GetByTestId("input-enddate").FillAsync("2024-02-02");
        await Page.GetByTestId("input-host").FillAsync("host");
        await Page.GetByTestId("input-ports").FillAsync("80, 200");
        await Page.GetByTestId("btn-day").First.ClickAsync();
        await Page.GetByTestId("btn-template").First.ClickAsync();

        await Page.GetByTestId("btn-submit").ClickAsync();

        await Page.GotoAsync($"{TestHelper.BaseUri}/vm?searchTerm={vm}");
        await Page.WaitForSelectorAsync("h1");

        var rows = await Page.GetByTestId("table-row").CountAsync();
        Assert.That(rows, Is.EqualTo(1));

        var name = await Page.GetByTestId("vm-name").InnerTextAsync();
        Assert.That(name, Is.EqualTo(vm));
    }

    [Test]
    public async Task Test2_Edit_VM()
    {

        await Page.GotoAsync($"{TestHelper.BaseUri}/vm?searchTerm={vm}");
        await Page.GetByTestId("vm-name").First.ClickAsync();
        await Page.WaitForSelectorAsync("h1");

        await Page.GetByTestId("link-editvm").ClickAsync();
        await Page.WaitForSelectorAsync("h1");

        await Page.GetByTestId("input-enddate").FillAsync("2024-02-02");

        await Page.GetByTestId("btn-submit").ClickAsync();
        await Page.WaitForSelectorAsync("h1");

        var date = await Page.GetByTestId("enddate").InnerTextAsync();
        Assert.That(date, Is.EqualTo("02-02-2024"));

    }

    [Test]
    public async Task Test3_DeleteVM()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm?searchTerm={vm}");
        await Page.GetByTestId("vm-name").First.ClickAsync();
        await Page.WaitForSelectorAsync("h1");

        await Page.GetByTestId("btn-delete").ClickAsync();
        await Page.GetByTestId("btn-delete-confirmation").ClickAsync();

        await Page.GotoAsync($"{TestHelper.BaseUri}/vm?searchTerm={vm}");
        await Page.WaitForSelectorAsync("h1");

        var rows = await Page.GetByTestId("table-row").CountAsync();
        Assert.That(rows, Is.EqualTo(0));
    }
}
