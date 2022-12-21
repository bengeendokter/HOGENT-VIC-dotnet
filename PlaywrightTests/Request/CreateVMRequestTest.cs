using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace VIC.IntegraionTests;

[Parallelizable(ParallelScope.Self)]
public class CreateVmRequestTest : PageTest
{
    [Test]
    public async Task Create_VMRequest()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/");

        await Page.FillAsync("data-test-id=input-projectname", "Karl jacobs");
        await Page.FillAsync("data-test-id=input-startdate", "2024-01-01");
        await Page.FillAsync("data-test-id=input-enddate", "2024-02-02");
        await Page.FillAsync("data-test-id=input-reason", "Lorem ipsum sit damet venus qua til verum terqu han requil.");

        await Page.Locator("data-test-id=btn-submit").ClickAsync();

        await Page.Locator("data-test-id=msg-succes").IsVisibleAsync();

    }

}