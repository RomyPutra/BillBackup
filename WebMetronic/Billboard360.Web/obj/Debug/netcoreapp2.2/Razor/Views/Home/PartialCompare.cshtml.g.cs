#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54d55bf73d8e400529bcd158cb5fc32828e2cc12"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_PartialCompare), @"mvc.1.0.view", @"/Views/Home/PartialCompare.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/PartialCompare.cshtml", typeof(AspNetCore.Views_Home_PartialCompare))]
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
#line 1 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\_ViewImports.cshtml"
using Billboard360.Web;

#line default
#line hidden
#line 2 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\_ViewImports.cshtml"
using Billboard360.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54d55bf73d8e400529bcd158cb5fc32828e2cc12", @"/Views/Home/PartialCompare.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_PartialCompare : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Billboard360.Web.Models.ForMaps>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/NoImage.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("listimage"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(40, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
 foreach (var item in Model.BillBoard)
{

#line default
#line hidden
            BeginContext(85, 8, true);
            WriteLiteral("    <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 93, "\"", 119, 2);
            WriteAttributeValue("", 101, "column4", 101, 7, true);
#line 5 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
WriteAttributeValue(" ", 108, item.Tipe, 109, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(120, 3, true);
            WriteLiteral(">\r\n");
            EndContext();
#line 6 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
         if (item.Img != "-")
        {

#line default
#line hidden
            BeginContext(165, 16, true);
            WriteLiteral("            <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 181, "\"", 196, 1);
#line 8 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
WriteAttributeValue("", 187, item.Img, 187, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(197, 23, true);
            WriteLiteral(" class=\"listimage\" />\r\n");
            EndContext();
#line 9 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(256, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(268, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "54d55bf73d8e400529bcd158cb5fc32828e2cc125717", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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
            BeginContext(319, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 13 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
        }

#line default
#line hidden
            BeginContext(332, 66, true);
            WriteLiteral("        <div class=\"detailprod\">\r\n            <h3 class=\"pheader\">");
            EndContext();
            BeginContext(399, 16, false);
#line 15 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                           Write(item.NoBillBoard);

#line default
#line hidden
            EndContext();
            BeginContext(415, 48, true);
            WriteLiteral("</h3>\r\n            <p class=\"pdetail\">Tipe    : ");
            EndContext();
            BeginContext(464, 9, false);
#line 16 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                    Write(item.Tipe);

#line default
#line hidden
            EndContext();
            BeginContext(473, 47, true);
            WriteLiteral("</p>\r\n            <p class=\"pdetail\">Ukuran  : ");
            EndContext();
            BeginContext(521, 8, false);
#line 17 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                    Write(item.Pjg);

#line default
#line hidden
            EndContext();
            BeginContext(529, 3, true);
            WriteLiteral(" X ");
            EndContext();
            BeginContext(533, 8, false);
#line 17 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                                Write(item.Lbr);

#line default
#line hidden
            EndContext();
            BeginContext(541, 53, true);
            WriteLiteral(" Meter</p>\r\n            <p class=\"pdetail\">H/V     : ");
            EndContext();
            BeginContext(595, 9, false);
#line 18 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                    Write(item.HorV);

#line default
#line hidden
            EndContext();
            BeginContext(604, 47, true);
            WriteLiteral("</p>\r\n            <p class=\"pdetail\">Alamat  : ");
            EndContext();
            BeginContext(652, 11, false);
#line 19 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                    Write(item.Alamat);

#line default
#line hidden
            EndContext();
            BeginContext(663, 133, true);
            WriteLiteral("</p>\r\n        </div>\r\n        <div class=\"ml-auto\" style=\"font-size: 12px;padding: 0 15px 0px 15px;text-align: right;height:20px;\">\r\n");
            EndContext();
#line 22 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
               int score = (int)Math.Round(item.RateScoreAvg); 

#line default
#line hidden
            BeginContext(862, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 23 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
             for (int i = 0; i < 5; i++)
            {
                if (i < score)
                {

#line default
#line hidden
            BeginContext(970, 62, true);
            WriteLiteral("                    <span class=\"fa fa-star checked\"></span>\r\n");
            EndContext();
#line 28 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(1092, 54, true);
            WriteLiteral("                    <span class=\"fa fa-star\"></span>\r\n");
            EndContext();
#line 32 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                }
            }

#line default
#line hidden
            BeginContext(1180, 46, true);
            WriteLiteral("            <p style=\"display:inline-block;\">(");
            EndContext();
            BeginContext(1227, 17, false);
#line 34 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                         Write(item.RateScoreAvg);

#line default
#line hidden
            EndContext();
            BeginContext(1244, 97, true);
            WriteLiteral(")</p>\r\n        </div>\r\n        <div class=\"detailprice\">\r\n            <p>Rp. <span class=\"price\">");
            EndContext();
            BeginContext(1342, 9, false);
#line 37 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
                                  Write(item.Hawl);

#line default
#line hidden
            EndContext();
            BeginContext(1351, 210, true);
            WriteLiteral("</span>,-</p>\r\n        </div>\r\n        <div class=\"detailbtn\">\r\n            <input type=\"button\" value=\"Compare\" style=\"padding: 5px 10px;background-color: #ED3239;width: 80%; font-size: 13px; margin: 2px 5px;\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1561, "\"", 1656, 9);
            WriteAttributeValue("", 1571, "addCompare(\'", 1571, 12, true);
#line 40 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
WriteAttributeValue("", 1583, Url.Action("AddToCompare", "Rest"), 1583, 35, false);

#line default
#line hidden
            WriteAttributeValue("", 1618, "\',", 1618, 2, true);
            WriteAttributeValue(" ", 1620, "\'", 1621, 2, true);
#line 40 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
WriteAttributeValue("", 1622, item.SiteID, 1622, 12, false);

#line default
#line hidden
            WriteAttributeValue("", 1634, "\',", 1634, 2, true);
            WriteAttributeValue(" ", 1636, "\'", 1637, 2, true);
#line 40 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
WriteAttributeValue("", 1638, item.SiteItemID, 1638, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 1654, "\')", 1654, 2, true);
            EndWriteAttribute();
            BeginContext(1657, 33, true);
            WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");
            EndContext();
#line 43 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Home\PartialCompare.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Billboard360.Web.Models.ForMaps> Html { get; private set; }
    }
}
#pragma warning restore 1591
