#pragma checksum "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9710dcdae742e49d9cd6c521536d56645d425a5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Document\VSproject\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity;

#line default
#line hidden
#line 2 "C:\Document\VSproject\NewCity\NewCity\Views\_ViewImports.cshtml"
using NewCity.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9710dcdae742e49d9cd6c521536d56645d425a5", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"428dca41bb5514e614701b69b27a8dec3a48fd5f", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("ASP.NET"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("title-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-md-12  container-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height:200px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    
    List<HomeNews> silde = ViewBag.Silde as List<HomeNews>;
    List<HomeNews> content = ViewBag.Content as List<HomeNews>;
    HomeNews publicity = ViewBag.Publicity as HomeNews;


#line default
#line hidden
            BeginContext(236, 2374, true);
            WriteLiteral(@"<script>
    window.onload = function () {        
        whilescroll();
    }

    function whilescroll() {
        window.onscroll = function (e) {
            x = window.scrollY;
            if (x <= 600) {
                let y = -0.5 * x + 300;
                document.getElementById(""introduced1"").style.transform = ""translateY("" + y + ""px)""
            }
            if (x <= 855) {
                let y = -0.5 * x + 450;
                document.getElementById(""introduced2"").style.transform = ""translateY("" + y + ""px)""
            }
            if (x <= 1135) {
                let y = -0.5 * x + 600;
                document.getElementById(""introduced3"").style.transform = ""translateY("" + y + ""px)""
            }
            if (x <= 1361) {
                let y = -0.5 * x + 750;
                document.getElementById(""introduced4"").style.transform = ""translateY("" + y + ""px)""
            }


            let y = -0.5 * x;
            document.getElementById(""title-img"").style.t");
            WriteLiteral(@"ransform = ""translateY("" + y + ""px)""

            this.console.log(x);

            
        }
    }
</script>
<style>

    #body {
        background-color:rgb(27,33,23)
    }
    #title {
        position:relative;
        height:350px;
        overflow:hidden;
    }
    #content h1 {
        color: white;
        font-style: italic;
        font-size:20px;
    }
    #content p {
        color: ivory;
        font-style: italic;
        font-size: 20px;
        width:50%;
    }
    #note {
        padding-top: 300px
    }
    #title-content {
        position: absolute;
        text-align: center;
        color: white;
        top:100px;
        left: 0px;
        right: 0px;
        bottom: 0px;
    }
    .img-responsive{
        width:100%;               
    }

</style>




<div class=""row"" id=""body"">
    <div class=""container-fluid"">
        <div id=""myCarousel"" class=""carousel slide"" data-ride=""carousel"" data-interval=""6000"">
            <ol class=""c");
            WriteLiteral(@"arousel-indicators"">
                <li data-target=""#myCarousel"" data-slide-to=""0"" class=""active""></li>
                <li data-target=""#myCarousel"" data-slide-to=""1""></li>
                <li data-target=""#myCarousel"" data-slide-to=""2""></li>
            </ol>
            <div class=""carousel-inner"" role=""listbox"">
");
            EndContext();
#line 95 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                 for (int i = 0; i < silde.Count ; i++)
                {
                    string status = string.Empty;
                    if (i == 0)
                    {
                        status = "active";
                    }

#line default
#line hidden
            BeginContext(2860, 24, true);
            WriteLiteral("                    <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 2884, "\"", 2904, 2);
            WriteAttributeValue("", 2892, "item", 2892, 4, true);
#line 102 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 2896, status, 2897, 7, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2905, 27, true);
            WriteLiteral(">\r\n                        ");
            EndContext();
            BeginContext(2932, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4c7a21a68c384797a36e9245065f9670", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2942, "~/images/", 2942, 9, true);
#line 103 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 2951, silde[i].Img, 2951, 13, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3006, 137, true);
            WriteLiteral("\r\n                        <div class=\"carousel-caption\" role=\"option\">\r\n                            <p>\r\n                                ");
            EndContext();
            BeginContext(3144, 14, false);
#line 106 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                           Write(silde[i].Title);

#line default
#line hidden
            EndContext();
            BeginContext(3158, 60, true);
            WriteLiteral("\r\n                                <a class=\"btn btn-default\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 3218, "\"", 3242, 1);
#line 107 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
WriteAttributeValue("", 3225, silde[i].Content, 3225, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3243, 175, true);
            WriteLiteral(">\r\n                                    详情\r\n                                </a>\r\n                            </p>\r\n                        </div>\r\n                    </div>\r\n");
            EndContext();
#line 113 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(3437, 600, true);
            WriteLiteral(@"            </div>
            <a class=""left carousel-control"" href=""#myCarousel"" role=""button"" data-slide=""prev"">
                <span class=""glyphicon glyphicon-chevron-left"" aria-hidden=""true""></span>
                <span class=""sr-only"">Previous</span>
            </a>
            <a class=""right carousel-control"" href=""#myCarousel"" role=""button"" data-slide=""next"">
                <span class=""glyphicon glyphicon-chevron-right"" aria-hidden=""true""></span>
                <span class=""sr-only"">Next</span>
            </a>
        </div>


        <div id=""title"">
            ");
            EndContext();
            BeginContext(4037, 90, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e7eb5c79f3d640478b0cd454dc64e3c7", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute(")", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4100, "~/images/", 4100, 9, true);
#line 127 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 4109, publicity.Img, 4109, 14, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4127, 37, true);
            WriteLiteral("\r\n            <h1 id=\"title-content\">");
            EndContext();
            BeginContext(4165, 17, false);
#line 128 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                              Write(publicity.Content);

#line default
#line hidden
            EndContext();
            BeginContext(4182, 76, true);
            WriteLiteral("</h1>\r\n        </div>\r\n        <div style=\"padding-top:10px\" id=\"content\">\r\n");
            EndContext();
#line 131 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
             for (int i = 0; i < content.Count; i++)
            {
                string id = "introduced" + (i + 1).ToString();

#line default
#line hidden
            BeginContext(4391, 16, true);
            WriteLiteral("            <div");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 4407, "\"", 4415, 1);
#line 134 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
WriteAttributeValue("", 4412, id, 4412, 3, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4416, 46, true);
            WriteLiteral(" class=\"container\" style=\"padding-top:40px\">\r\n");
            EndContext();
#line 135 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                 if (i % 2 == 0)
                {

#line default
#line hidden
            BeginContext(4515, 130, true);
            WriteLiteral("                    <div class=\"col-md-9\" style=\"text-align:center\">\r\n                        <p>\r\n                            <b>");
            EndContext();
            BeginContext(4646, 16, false);
#line 139 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                          Write(content[i].Title);

#line default
#line hidden
            EndContext();
            BeginContext(4662, 34, true);
            WriteLiteral("</b>\r\n                            ");
            EndContext();
            BeginContext(4697, 18, false);
#line 140 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                       Write(content[i].Content);

#line default
#line hidden
            EndContext();
            BeginContext(4715, 128, true);
            WriteLiteral("\r\n                        </p>\r\n                    </div>\r\n                    <div class=\"col-md-3\">\r\n                        ");
            EndContext();
            BeginContext(4843, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "824ddd99785146138dfbd1cb5070fc86", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4874, "~/images/", 4874, 9, true);
#line 144 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 4883, content[i].Img, 4883, 15, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4902, 30, true);
            WriteLiteral("\r\n                    </div>\r\n");
            EndContext();
#line 146 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(4992, 69, true);
            WriteLiteral("                    <div class=\"col-md-3\"> \r\n                        ");
            EndContext();
            BeginContext(5061, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7695a6ed665349588a003059ea0a8c81", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 5092, "~/images/", 5092, 9, true);
#line 150 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 5101, content[i].Img, 5101, 15, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5120, 160, true);
            WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-md-9\" style=\"text-align:center\">\r\n                        <p>\r\n                            <b>");
            EndContext();
            BeginContext(5281, 16, false);
#line 154 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                          Write(content[i].Title);

#line default
#line hidden
            EndContext();
            BeginContext(5297, 34, true);
            WriteLiteral("</b>\r\n                            ");
            EndContext();
            BeginContext(5332, 18, false);
#line 155 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                       Write(content[i].Content);

#line default
#line hidden
            EndContext();
            BeginContext(5350, 60, true);
            WriteLiteral("\r\n                        </p>\r\n                    </div>\r\n");
            EndContext();
#line 158 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"

                }

#line default
#line hidden
            BeginContext(5431, 22, true);
            WriteLiteral("\r\n            </div>\r\n");
            EndContext();
#line 162 "C:\Document\VSproject\NewCity\NewCity\Views\Home\Index.cshtml"
                
            }

#line default
#line hidden
            BeginContext(5486, 96, true);
            WriteLiteral("\r\n\r\n        </div>\r\n        <div id=\"note\">\r\n           \r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
