#pragma checksum "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15f03bf3f9c078938931834ee3c21f9220bba56b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_StoryCards_Index), @"mvc.1.0.view", @"/Views/StoryCards/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/StoryCards/Index.cshtml", typeof(AspNetCore.Views_StoryCards_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "F:\VisualStudioProject\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity;

#line default
#line hidden
#line 2 "F:\VisualStudioProject\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15f03bf3f9c078938931834ee3c21f9220bba56b", @"/Views/StoryCards/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"428dca41bb5514e614701b69b27a8dec3a48fd5f", @"/Views/_ViewImports.cshtml")]
    public class Views_StoryCards_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NewCity.Models.StoryCard>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Main", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
  
    ViewData["Title"] = "StoryCard";

#line default
#line hidden
            BeginContext(93, 29, true);
            WriteLiteral("\r\n<h2>Index</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(122, 102, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf82e39ca9e545d5a6b8c3ca3a95489f", async() => {
                BeginContext(204, 16, true);
                WriteLiteral("Start this story");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-StorySeriesID", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 10 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
                                                             WriteLiteral(ViewBag.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["StorySeriesID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-StorySeriesID", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["StorySeriesID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(224, 92, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(317, 45, false);
#line 16 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.StoryName));

#line default
#line hidden
            EndContext();
            BeginContext(362, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(418, 40, false);
#line 19 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Text));

#line default
#line hidden
            EndContext();
            BeginContext(458, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(514, 39, false);
#line 22 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.IMG));

#line default
#line hidden
            EndContext();
            BeginContext(553, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(609, 49, false);
#line 25 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BackgroundIMG));

#line default
#line hidden
            EndContext();
            BeginContext(658, 124, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                option\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 33 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(814, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(863, 44, false);
#line 36 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.StoryName));

#line default
#line hidden
            EndContext();
            BeginContext(907, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(963, 39, false);
#line 39 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Text));

#line default
#line hidden
            EndContext();
            BeginContext(1002, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1058, 38, false);
#line 42 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.IMG));

#line default
#line hidden
            EndContext();
            BeginContext(1096, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1152, 48, false);
#line 45 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.BackgroundIMG));

#line default
#line hidden
            EndContext();
            BeginContext(1200, 39, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
            EndContext();
#line 48 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
                 foreach(StoryOption option in item.StoryOptions)
                {

#line default
#line hidden
            BeginContext(1325, 49, true);
            WriteLiteral("                   <div>\r\n                       ");
            EndContext();
            BeginContext(1375, 11, false);
#line 51 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
                  Write(option.Text);

#line default
#line hidden
            EndContext();
            BeginContext(1386, 52, true);
            WriteLiteral("\r\n                       ||\r\n                       ");
            EndContext();
            BeginContext(1439, 20, false);
#line 53 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
                  Write(option.ID.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(1459, 29, true);
            WriteLiteral("\r\n                   </div>\r\n");
            EndContext();
#line 55 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1507, 54, true);
            WriteLiteral("\r\n                \r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 60 "F:\VisualStudioProject\NewCity\NewCity\Views\StoryCards\Index.cshtml"
}

#line default
#line hidden
            BeginContext(1564, 38, true);
            WriteLiteral("\r\n      \r\n    </tbody>\r\n\r\n\r\n</table>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<NewCity.Models.StoryCard>> Html { get; private set; }
    }
}
#pragma warning restore 1591
