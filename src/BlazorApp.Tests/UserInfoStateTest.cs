using BlazorApp.Pages.SamplePages;
using Bunit;
using Bunit.TestDoubles;

namespace BlazorApp.Tests
{
    public class UserInfoStateTest : BunitTestContext
    {
        [Test]
        // Unauthicated and unauthorized
        public void UnauthicatedUnauthorized()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            ctx.AddTestAuthorization();

            var cut = ctx.RenderComponent<UserInfo>();

            // Assert
            cut.MarkupMatches(@"<h1>Please log in</h1>
                    <p>State: Not authorized</p>");
        }

        [Test]
        public void AuthenticatingAuthorizing()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorizing();

            var cut = ctx.RenderComponent<UserInfo>();

            // Assert
            cut.MarkupMatches(@"<h1>Please log in</h1>
                    <p>State: Authorizing</p>");
        }

        [Test]
        public void AuthenticatedUnAuthorized()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER", AuthorizationState.Unauthorized);

            var cut = ctx.RenderComponent<UserInfo>();

            // Assert
            cut.MarkupMatches(@"<h1>Welcome TEST USER</h1>
                    <p>State: Not authorized</p>");
        }

        [Test]
        public void AuthenticatedAuthorized()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");

            var cut = ctx.RenderComponent<UserInfo>();

            // Assert
            cut.MarkupMatches(@"<h1>Welcome TEST USER</h1>
                    <p>State: Authorized</p>");
        }


    }
}
