using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIC.IntegrationTests.Requests;

[TestFixture]
public class CreateRequestTest: PageTest
{
    [Test]
    public async Task Test1_Create_VMRequest()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/");

        await Page.GetByTestId("input-projectname").FillAsync("Karl jacobs");
        await Page.GetByTestId("input-startdate").FillAsync("2024-01-01");
        await Page.GetByTestId("input-enddate").FillAsync("2024-02-02");
        await Page.GetByTestId("input-reason").FillAsync("Lorem ipsum sit damet venus qua til verum terqu han requil.");

        await Page.GetByTestId("btn-submit").ClickAsync();

        await Expect(Page.GetByTestId("msg-succes")).ToBeVisibleAsync();

    }
}
