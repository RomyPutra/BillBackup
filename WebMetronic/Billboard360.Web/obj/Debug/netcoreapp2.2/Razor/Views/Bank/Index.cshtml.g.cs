#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a6fda009d8a6200ee4ab8f99d84e4a25aa082e43"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Bank_Index), @"mvc.1.0.view", @"/Views/Bank/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Bank/Index.cshtml", typeof(AspNetCore.Views_Bank_Index))]
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
#line 5 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a6fda009d8a6200ee4ab8f99d84e4a25aa082e43", @"/Views/Bank/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Bank_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Billboard360.Web.Models.InfoBankDetailModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/add-create.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 15px;width: 15px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("padding: 5px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Bank", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(118, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 7 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
  
	ViewData["Title"] = "Bank Listing";
	var roles = @HttpContextAccessor.HttpContext.Session.GetString("_ROLE");

#line default
#line hidden
            BeginContext(389, 136, true);
            WriteLiteral("<div class=\"row\">\r\n\t<div class=\"col-12\">\r\n\t\t<div class=\"box\">\r\n\t\t\t<div class=\"box-header\">\r\n\t\t\t\t<h3 class=\"box-title\">Manage Bank</h3>\r\n");
            EndContext();
#line 16 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                 if (roles == "SPV" || roles == "ADM")
				{

#line default
#line hidden
            BeginContext(576, 30, true);
            WriteLiteral("\t\t\t\t\t<p align=\"right\">\r\n\t\t\t\t\t\t");
            EndContext();
            BeginContext(606, 180, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a6fda009d8a6200ee4ab8f99d84e4a25aa082e436693", async() => {
                BeginContext(703, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a6fda009d8a6200ee4ab8f99d84e4a25aa082e436956", async() => {
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
                BeginContext(773, 9, true);
                WriteLiteral(" Add Bank");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(786, 13, true);
            WriteLiteral("\r\n\t\t\t\t\t</p>\r\n");
            EndContext();
#line 21 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
				}

#line default
#line hidden
            BeginContext(806, 37, true);
            WriteLiteral("\t\t\t</div>\r\n\t\t\t<!-- /.box-header -->\r\n");
            EndContext();
#line 24 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
             if (Html.ViewData.ModelState.ContainsKey(string.Empty))
			{

#line default
#line hidden
            BeginContext(910, 56, true);
            WriteLiteral("\t\t\t\t<div class=\"alert alert-danger\" role=\"alert\">\r\n\t\t\t\t\t");
            EndContext();
            BeginContext(967, 72, false);
#line 27 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
               Write(Html.ValidationSummary(true, "Terjadi kesalahan saat mendapatkan data."));

#line default
#line hidden
            EndContext();
            BeginContext(1039, 14, true);
            WriteLiteral("\r\n\t\t\t\t</div>\r\n");
            EndContext();
#line 29 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
			}

#line default
#line hidden
            BeginContext(1059, 154, true);
            WriteLiteral("\t\t\t<div class=\"box-body auto-table\">\r\n\t\t\t\t<table id=\"example1\" class=\"table table-bordered table-striped\" style=\"width:100%;\">\r\n\t\t\t\t\t<thead>\r\n\t\t\t\t\t\t<tr>\r\n");
            EndContext();
            BeginContext(1237, 44, true);
            WriteLiteral("\t\t\t\t\t\t\t<th>Kode</th>\r\n\t\t\t\t\t\t\t<th>Bank</th>\r\n");
            EndContext();
#line 37 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                             if (roles == "SPV" || roles == "ADM")
							{

#line default
#line hidden
            BeginContext(1338, 25, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<th>Action</th>\r\n");
            EndContext();
#line 40 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
							}

#line default
#line hidden
            BeginContext(1373, 42, true);
            WriteLiteral("\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t</thead>\r\n\t\t\t\t\t<tbody>\r\n");
            EndContext();
#line 44 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                         foreach (var item in Model)
						{

#line default
#line hidden
            BeginContext(1460, 13, true);
            WriteLiteral("\t\t\t\t\t\t\t<tr>\r\n");
            EndContext();
            BeginContext(1538, 12, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1551, 39, false);
#line 48 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Kode));

#line default
#line hidden
            EndContext();
            BeginContext(1590, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1610, 39, false);
#line 49 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Bank));

#line default
#line hidden
            EndContext();
            BeginContext(1649, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 50 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                                 if (roles == "SPV" || roles == "ADM")
								{

#line default
#line hidden
            BeginContext(1715, 15, true);
            WriteLiteral("\t\t\t\t\t\t\t\t\t<td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1730, "\"", 1761, 2);
            WriteAttributeValue("", 1737, "Bank/Update/", 1737, 12, true);
#line 52 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
WriteAttributeValue("", 1749, item.BankID, 1749, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1762, 24, true);
            WriteLiteral(">Update</a> | <a href=\"\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1786, "\"", 1818, 3);
            WriteAttributeValue("", 1796, "Delete(\'", 1796, 8, true);
#line 52 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
WriteAttributeValue("", 1804, item.BankID, 1804, 12, false);

#line default
#line hidden
            WriteAttributeValue("", 1816, "\')", 1816, 2, true);
            EndWriteAttribute();
            BeginContext(1819, 18, true);
            WriteLiteral(">Delete</a></td>\r\n");
            EndContext();
#line 53 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
								}

#line default
#line hidden
            BeginContext(1848, 14, true);
            WriteLiteral("\t\t\t\t\t\t\t</tr>\r\n");
            EndContext();
#line 55 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
						}

#line default
#line hidden
            BeginContext(1871, 132, true);
            WriteLiteral("\t\t\t\t</table>\r\n\t\t\t</div>\r\n\t\t\t<!-- /.box-body -->\r\n\t\t</div>\r\n\t\t<!-- /.box -->\r\n\t</div>\r\n\t<!-- /.col -->\r\n</div>\r\n<!-- DataTables -->\r\n");
            EndContext();
            BeginContext(2155, 328, true);
            WriteLiteral(@"<!-- page script -->
<script>
    //$(function () {
    //    $('#example1').DataTable();
    //});
    function Delete(id){
        var txt;
        var r = confirm(""Are you sure you want to Delete?"");
        if (r == true) {

            $.ajax(
            {
                type: ""POST"",
                url: '");
            EndContext();
            BeginContext(2484, 28, false);
#line 80 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Index.cshtml"
                 Write(Url.Action("Delete", "Bank"));

#line default
#line hidden
            EndContext();
            BeginContext(2512, 567, true);
            WriteLiteral(@"',
                data: {
                    id: id
                },
                error: function (result) {
                    alert(""error"");
                },
                success: function (result) {
                    if (result == true) {
                        var baseUrl=""/Bank"";
                        window.location.reload();
                    }
                    else {
                        alert(""There is a problem, Try Later!"");
                    }
                }
            });
        }
    }
</script>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Billboard360.Web.Models.InfoBankDetailModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
