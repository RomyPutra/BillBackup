#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9c86d933beaaa0bdf653604f1dfd30107558ce5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Wish_Index), @"mvc.1.0.view", @"/Views/Wish/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Wish/Index.cshtml", typeof(AspNetCore.Views_Wish_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9c86d933beaaa0bdf653604f1dfd30107558ce5", @"/Views/Wish/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Wish_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Billboard360.Web.Models.WishListOutputModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/NoImage.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:125px;height:85px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Site", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ViewDetail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(118, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 6 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
  
	ViewData["Title"] = "Wishlist";

#line default
#line hidden
            BeginContext(288, 63, true);
            WriteLiteral("<div class=\"row\">\r\n\t<div class=\"col-12\">\r\n\t\t<div class=\"box\">\r\n");
            EndContext();
            BeginContext(436, 107, true);
            WriteLiteral("\t\t\t<div class=\"shopping-cart\">\r\n\t\t\t\t<!-- Title -->\r\n\t\t\t\t<div class=\"title\">\r\n\t\t\t\t\tWish List\r\n\t\t\t\t</div>\r\n\r\n");
            EndContext();
#line 21 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                 foreach (var item in Model)
				{

#line default
#line hidden
            BeginContext(584, 116, true);
            WriteLiteral("\t\t\t\t<div class=\"item\">\r\n\t\t\t\t\t<div class=\"buttons\">\r\n\t\t\t\t\t\t<span class=\"delete-btn btn-wish\" style=\"margin-top:-10px\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 700, "\"", 785, 6);
            WriteAttributeValue("", 710, "deleteWish(\'", 710, 12, true);
#line 25 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
WriteAttributeValue("", 722, Url.Action("DeleteFromWishList", "Wish"), 722, 41, false);

#line default
#line hidden
            WriteAttributeValue("", 763, "\',", 763, 2, true);
            WriteAttributeValue(" ", 765, "\'", 766, 2, true);
#line 25 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
WriteAttributeValue("", 767, item.WishlistID, 767, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 783, "\')", 783, 2, true);
            EndWriteAttribute();
            BeginContext(786, 10, true);
            WriteLiteral("></span>\r\n");
            EndContext();
            BeginContext(889, 39, true);
            WriteLiteral("\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"image\">\r\n");
            EndContext();
#line 29 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                         if (item.Image.Count > 0)
						{

#line default
#line hidden
            BeginContext(971, 11, true);
            WriteLiteral("\t\t\t\t\t\t\t<img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 982, "\"", 1002, 1);
#line 31 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
WriteAttributeValue("", 988, item.Image[0], 988, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1003, 45, true);
            WriteLiteral(" alt=\"\" style=\"width:125px;height:85px;\" />\r\n");
            EndContext();
#line 32 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
						}
						else
						{

#line default
#line hidden
            BeginContext(1078, 7, true);
            WriteLiteral("\t\t\t\t\t\t\t");
            EndContext();
            BeginContext(1085, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f9c86d933beaaa0bdf653604f1dfd30107558ce58084", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1158, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 36 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
						}

#line default
#line hidden
            BeginContext(1169, 6, true);
            WriteLiteral("\t\t\t\t\t\t");
            EndContext();
            BeginContext(1252, 59, true);
            WriteLiteral("\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"description\">\r\n\t\t\t\t\t\t<span>");
            EndContext();
            BeginContext(1312, 16, false);
#line 40 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                         Write(item.NoBillboard);

#line default
#line hidden
            EndContext();
            BeginContext(1328, 21, true);
            WriteLiteral("</span>\r\n\t\t\t\t\t\t<span>");
            EndContext();
            BeginContext(1350, 18, false);
#line 41 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                         Write(item.BillboardType);

#line default
#line hidden
            EndContext();
            BeginContext(1368, 66, true);
            WriteLiteral("</span>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"description\">\r\n\t\t\t\t\t\t<span>");
            EndContext();
            BeginContext(1435, 9, false);
#line 44 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                         Write(item.Kota);

#line default
#line hidden
            EndContext();
            BeginContext(1444, 21, true);
            WriteLiteral("</span>\r\n\t\t\t\t\t\t<span>");
            EndContext();
            BeginContext(1466, 13, false);
#line 45 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                         Write(item.Provinsi);

#line default
#line hidden
            EndContext();
            BeginContext(1479, 87, true);
            WriteLiteral("</span>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"total-price\">\r\n\t\t\t\t\t\t<p>Rp. <span class=\"price\">");
            EndContext();
            BeginContext(1567, 10, false);
#line 48 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                                              Write(item.Harga);

#line default
#line hidden
            EndContext();
            BeginContext(1577, 82, true);
            WriteLiteral("</span>,-</p>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"buttons\" style=\"margin:0px;\">\r\n\t\t\t\t\t\t");
            EndContext();
            BeginContext(1659, 140, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9c86d933beaaa0bdf653604f1dfd30107558ce511936", async() => {
                BeginContext(1736, 59, true);
                WriteLiteral("\r\n\t\t\t\t\t\t\t<span class=\"text-grey\"> View Site </span>\r\n\t\t\t\t\t\t");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 51 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
                                                                           WriteLiteral(item.SiteID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1799, 27, true);
            WriteLiteral("\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n");
            EndContext();
#line 56 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Wish\Index.cshtml"
				}

#line default
#line hidden
            BeginContext(1833, 73, true);
            WriteLiteral("\t\t\t</div>\r\n\t\t</div>\r\n\t\t<!-- /.box -->\r\n\t</div>\r\n\t<!-- /.col -->\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Billboard360.Web.Models.WishListOutputModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
