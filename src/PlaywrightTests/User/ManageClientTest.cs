using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace VIC.IntegrationTests.User;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ManageClientTest : PageTest
{
    [Test] 
    public async Task Test1_Create_Client_With_Empty_Fields_Gives_Validation_Errors()
    {
        await Page.GotoAsync("https://localhost:7257/klanten/aanmaken");

        await Page.GetByTestId("btn-add").ClickAsync();

        await Expect(Page.GetByTestId("err-surname")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-name")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-phone")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-email")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-backup")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-org")).ToBeVisibleAsync();
        await Expect(Page.GetByTestId("err-edu")).ToBeVisibleAsync();

    }

    [Test]
    public async Task Test2_Create_Client()
    {
        await Page.GotoAsync("https://localhost:7257/klanten");

        await Page.GetByTestId("btn-add").ClickAsync();

        await Page.GetByTestId("in-surname").FillAsync("Voornaam");
        await Page.GetByTestId("in-name").FillAsync("Achternaam");
        await Page.GetByTestId("in-phone").FillAsync("0123 45 67 89");
        await Page.GetByTestId("in-email").FillAsync("test@test.be");
        await Page.GetByTestId("in-backup").FillAsync("0123 45 67 89");
        await Page.GetByTestId("in-org").FillAsync("Organisatie");
        await Page.GetByTestId("in-type").SelectOptionAsync(new[] { "External" });
        await Page.GetByTestId("in-etype").FillAsync("EType");

        await Page.GetByTestId("btn-add").ClickAsync();

        var email = await Page.GetByTestId("dtl-email").TextContentAsync();
        Assert.That(email, Is.EqualTo("test@test.be"));

        var phone = await Page.GetByTestId("dtl-phone").TextContentAsync();
        Assert.That(phone, Is.EqualTo("0123 45 67 89"));

        var backup = await Page.GetByTestId("dtl-backup").TextContentAsync();
        Assert.That(backup, Is.EqualTo("0123 45 67 89"));

        var organisation = await Page.GetByTestId("dtl-org").TextContentAsync();
        Assert.That(organisation, Is.EqualTo("Organisatie"));

        var type = await Page.GetByTestId("dtl-type").TextContentAsync();
        Assert.That(type, Is.EqualTo("EType"));
    }

    [Test]
    public async Task Test3_Edit_Client()
    {
        await Page.GotoAsync("https://localhost:7257/klanten");
        await Page.GetByTestId("searchbar").FillAsync("Voornaam");

        await Page.GetByTestId("btn-edit-callback").ClickAsync();

        await Page.GetByTestId("in-surname").FillAsync("Surname");
        await Page.GetByTestId("in-name").FillAsync("Lastname");
        await Page.GetByTestId("in-phone").FillAsync("0321 54 76 98");
        await Page.GetByTestId("in-email").FillAsync("aangepast@test.be");
        await Page.GetByTestId("in-backup").FillAsync("0321 54 76 98");
        await Page.GetByTestId("in-org").FillAsync("Nieuwe organisatie");
        await Page.GetByTestId("in-type").SelectOptionAsync(new[] { "Internal" });

        await Expect(Page.GetByTestId("in-edu")).ToBeVisibleAsync();
        await Page.GetByTestId("in-edu").FillAsync("DIT");

        await Expect(Page.GetByTestId("btn-edit")).ToBeVisibleAsync();
        await Page.GetByTestId("btn-edit").ClickAsync();

        await Page.GetByTestId("searchbar").FillAsync("Surname");

        await Expect(Page.GetByTestId("btn-info-callback")).ToBeVisibleAsync();
        await Page.GetByTestId("btn-info-callback").ClickAsync();

        var email = await Page.GetByTestId("dtl-email").TextContentAsync();
        Assert.That(email, Is.EqualTo("aangepast@test.be"));

        var phone = await Page.GetByTestId("dtl-phone").TextContentAsync();
        Assert.That(phone, Is.EqualTo("0321 54 76 98"));

        var backup = await Page.GetByTestId("dtl-backup").TextContentAsync();
        Assert.That(backup, Is.EqualTo("0321 54 76 98"));

        var organisation = await Page.GetByTestId("dtl-org").TextContentAsync();
        Assert.That(organisation, Is.EqualTo("Nieuwe organisatie"));

        var type = await Page.GetByTestId("dtl-edu").TextContentAsync();
        Assert.That(type, Is.EqualTo("DIT"));
    }

    [Test]
    public async Task Test4_Delete_Client()
    {
        await Page.GotoAsync("https://localhost:7257/klanten");
        await Page.GetByTestId("searchbar").FillAsync("Surname");

        await Page.GetByTestId("btn-delete-callback").ClickAsync();
        await Page.GetByTestId("btn-delete-confirmation").ClickAsync();

        await Page.GotoAsync("https://localhost:7257/klanten");
        await Page.GetByTestId("searchbar").FillAsync("Surname");

        var rows = await Page.Locator("tr").CountAsync();

        // only header row
        Assert.That(rows, Is.EqualTo(1));

    }
}
