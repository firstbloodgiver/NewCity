#pragma checksum "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "304759cf641f79fb2a577908e39de8a887ed4754"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Main_Index), @"mvc.1.0.view", @"/Views/Main/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Main/Index.cshtml", typeof(AspNetCore.Views_Main_Index))]
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
#line 1 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity;

#line default
#line hidden
#line 2 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"304759cf641f79fb2a577908e39de8a887ed4754", @"/Views/Main/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"428dca41bb5514e614701b69b27a8dec3a48fd5f", @"/Views/_ViewImports.cshtml")]
    public class Views_Main_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NewCity.Models.StoryCard>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(33, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
  
    ViewData["Title"] = "Main";

#line default
#line hidden
            BeginContext(75, 194, true);
            WriteLiteral("\r\n<h2>Main</h2>\r\n<div class=\"container-fluid\">\r\n    <div class=\"col-md-3\" style=\"background-color:aquamarine\">\r\n\r\n\r\n    </div>\r\n    <div class=\"col-md-6\" style=\"background-color:aqua\">\r\n        ");
            EndContext();
            BeginContext(270, 41, false);
#line 14 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
   Write(Html.DisplayFor(model => model.StoryName));

#line default
#line hidden
            EndContext();
            BeginContext(311, 22, true);
            WriteLiteral("\r\n        ||\r\n        ");
            EndContext();
            BeginContext(334, 36, false);
#line 16 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
   Write(Html.DisplayFor(model => model.Text));

#line default
#line hidden
            EndContext();
            BeginContext(370, 40, true);
            WriteLiteral("\r\n        <div>\r\n            <!--选项-->\r\n");
            EndContext();
#line 19 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
             foreach (StoryOption storyOption in Model.StoryOptions)
            {

#line default
#line hidden
            BeginContext(495, 78, true);
            WriteLiteral("            <button type=\"button\" class=\"btn btn-secondary\">\r\n                ");
            EndContext();
            BeginContext(574, 16, false);
#line 22 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
           Write(storyOption.Text);

#line default
#line hidden
            EndContext();
            BeginContext(590, 26, true);
            WriteLiteral(";\r\n            </button>\r\n");
            EndContext();
#line 24 "C:\Users\ryr\Source\Repos\NewCity\NewCity\Views\Main\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(631, 117, true);
            WriteLiteral("        </div>\r\n\r\n    </div>\r\n    <div class=\"col-md-3\" style=\"background-color:burlywood\">\r\n\r\n\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NewCity.Models.StoryCard> Html { get; private set; }
    }
}
#pragma warning restore 1591
