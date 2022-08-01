using BlazorApp.Pages.SamplePages;
using Bunit;
using Bunit.TestDoubles;
using System.Security.Claims;

namespace BlazorApp.Tests
{
    public class UserRightsStateTest : BunitTestContext
    {
        [Test]
        public void AuthenticatedHasRole()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
                authContext.SetAuthorized("TEST USER");
                authContext.SetRoles("superuser");

                var cut = ctx.RenderComponent<UserRights>();

                // Assert
                var element = cut.Find("#thelist").InnerHtml;
                element.MarkupMatches(@"<li>You have the role SUPER USER</li>");
        }

        [Test]
        public void AuthenticatedHasRoleMatchTextContent()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
                authContext.SetAuthorized("TEST USER");
                authContext.SetRoles("superuser");

                // Act
                var cut = ctx.RenderComponent<UserRights>();

                // Assert
                var element = cut.Find("#thelist").TextContent;
                element.MarkupMatches(@"You have the role SUPER USER");
        }

        [Test]
        public void AuthenticatedHasRoles()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetRoles("admin", "superuser");

            // Act
            var cut = ctx.RenderComponent<UserRights>();

            // Assert
            var element = cut.Find("#thelist").InnerHtml;
            element.MarkupMatches(@"
                      <li>You have the role SUPER USER</li>
                      <li>You have the role ADMIN</li>");
        }

        [Test]
        public void AuthenticatedHasPolicy()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetPolicies("content-editor");

            // Act
            var cut = ctx.RenderComponent<UserRights>();

            // Assert
            var element = cut.Find("#thelist").InnerHtml;
            element.MarkupMatches(@"<li>You are a CONTENT EDITOR</li>");
        }

        [Test]
        public void AuthenticatedHasClaims()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetClaims(
              new Claim(ClaimTypes.Email, "test@example.com"),
              new Claim(ClaimTypes.DateOfBirth, "01-01-1970")
            );

            // Act
            var cut = ctx.RenderComponent<UserRights>();

            // Assert
            var element = cut.Find("#thelist").InnerHtml;
            element.MarkupMatches(@"
                      <li>Emailaddress: test@example.com</li>
                      <li>Dateofbirth: 01-01-1970</li>");
        }

        [Test]
        public void AuthenticatedHasRolesPolicyClaims()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetRoles("admin", "superuser");
            authContext.SetPolicies("content-editor");
            authContext.SetClaims(new Claim(ClaimTypes.Email, "test@example.com"));

            // Act
            var cut = ctx.RenderComponent<UserRights>();

            // Assert
            var element = cut.Find("#thelist").InnerHtml;
            element.MarkupMatches(@"
                      <li>Emailaddress: test@example.com</li>
                      <li>You have the role SUPER USER</li>
                      <li>You have the role ADMIN</li>
                      <li>You are a CONTENT EDITOR</li>");
        }

        [Test]
        public void IsAuthenticatationType()
        {
            using var ctx = TestContext is not null ? TestContext : new Bunit.TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetAuthenticationType("custom-auth-type");

            // Act
            var cut = ctx.RenderComponent<UserRights>();

            // Assert
            var element = cut.Find("#thelist").InnerHtml;
            element.MarkupMatches(@"<li>You have the authentication type CUSTOM AUTH TYPE</li>");
        }

    }
}
