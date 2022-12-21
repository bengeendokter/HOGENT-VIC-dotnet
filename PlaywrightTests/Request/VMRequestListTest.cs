using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIC.IntegraionTests;


namespace VIC.IntegrationTests.Request;

[Parallelizable(ParallelScope.Self)]
internal class VMRequestListTest: PageTest
{

    [Test]
    public async Task Show_Request_with_20_items()
    {
        await Page.GotoAsync($"{TestHelper.BaseUri}/vm/request/list/");

        await Page.Locator("data-test-id=table-row").CountAsync();

    }

    //public async Task Show_Request_after_filter()
    //{

    //}
}
