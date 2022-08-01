using Bunit;
using NUnit.Framework;
using BlazorApp.Pages.SamplePages;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp.Tests
{
    public class SamplePagesTest : BunitTestContext
    {
        [Test]
        // Matches HTML content
        public void ComponentRendersCorrectly()
        {
            var cut = RenderComponent<RendersComponent>();
            // Assert
            cut.MarkupMatches("<h3>Sample Page - RendersComponent</h3>");
        }

        [Test]
        // Accepts non-Blazor type parameters
        public void AcceptsNonBlazorTypesParams()
        {
            var lines = new List<string> { "Tom", "Brian", "Jason" };

            var cut = RenderComponent<AcceptsNonBlazorTypesParams>(parameters => parameters
              .Add(p => p.Numbers, 42)
              .Add(p => p.Lines, lines)
            );

            var element1 = cut.Find("#numbers").TextContent;
            element1.MarkupMatches("42");

            var element2 = cut.Find("#lines").TextContent;
            element2.MarkupMatches("TomBrianJason");
        }

        [Test]
        public void AcceptsEventCallbackParams()
        {
            Action<MouseEventArgs> onClickHandler = _ => { };
            Action onSomethingHandler = () => { };

            var cut = RenderComponent<AcceptsEventCallbackParams>(parameters => parameters
              .Add(p => p.OnClick, onClickHandler)
              .Add(p => p.OnSomething, onSomethingHandler)
            );
        }
    }
}
