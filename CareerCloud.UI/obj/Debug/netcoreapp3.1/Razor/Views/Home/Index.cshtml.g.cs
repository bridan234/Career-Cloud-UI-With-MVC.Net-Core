#pragma checksum "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb161bdb5049dba9d3ee0bdc106515711f2c3bd8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\_ViewImports.cshtml"
using CareerCloud.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\_ViewImports.cshtml"
using CareerCloud.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb161bdb5049dba9d3ee0bdc106515711f2c3bd8", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26f554771a6adc883e59ee1dff849807941d518e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CareerCloud.UI.Models.SearchVM>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-sm btn-secondary btnApply"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "applicantjobapplication", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Index";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<style>\r\n    .btnApply:hover{\r\n        background-color: white; /* Green */\r\n        color: #4CAF50;\r\n        box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);\r\n    }\r\n</style>\r\n    \r\n<h1>Jobs</h1>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb161bdb5049dba9d3ee0bdc106515711f2c3bd86113", async() => {
                WriteLiteral("\r\n    <input name=\"SearchValue\"");
                BeginWriteAttribute("value", " value=\"", 419, "\"", 427, 0);
                EndWriteAttribute();
                WriteLiteral(" type=\"search\" placeholder=\"Job Title, Location, Company Name\" style = \"width: 500px; height: 45px\"/>\r\n    <button class=\"btn btn-primary\" type=\"submit\" value=\"Search\">Search</button>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<br />\r\n\r\n<table class=\"table table-bordered table-hover table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n");
#nullable restore
#line 28 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                  int i = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("                #\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.JobCreatedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th align=\"center\">\r\n                ");
#nullable restore
#line 35 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Job));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th >\r\n                ");
#nullable restore
#line 38 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Company));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Locations));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ...\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n\r\n");
#nullable restore
#line 50 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
         foreach (var item in Model)
        {   
                

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n");
#nullable restore
#line 54 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
             if (Model.Count() < 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h1>Sorry! No Job Record was found, Refine your search word and try again. Thanks</h1>\r\n");
#nullable restore
#line 57 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                return;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>\r\n                ");
#nullable restore
#line 60 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
            Write(++i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 63 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.JobCreatedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 66 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.ActionLink(item.Job, "Details", "CompanyJobDescription", new { Id = item.JobDescId }, null));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 69 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Company));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td width=\"250px\">\r\n");
#nullable restore
#line 72 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                 foreach (var loc in item.Locations)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
#nullable restore
#line 74 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                 Write(string.Concat(loc.City, loc.Province, loc.CountryCode));

#line default
#line hidden
#nullable disable
            WriteLiteral(", <br />\r\n");
#nullable restore
#line 75 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb161bdb5049dba9d3ee0bdc106515711f2c3bd813180", async() => {
                WriteLiteral("Apply");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Job", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 79 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
                                                                                                                                       WriteLiteral(item.JobDescId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Job"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Job", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Job"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 82 "C:\Users\Bridan\source\repos\CareerCloud2\CareerCloud.UI\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CareerCloud.UI.Models.SearchVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
