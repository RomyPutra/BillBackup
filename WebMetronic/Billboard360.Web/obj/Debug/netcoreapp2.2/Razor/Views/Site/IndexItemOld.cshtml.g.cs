#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "991e389d81505a83aa333c4f36e9395295d18f15"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Site_IndexItemOld), @"mvc.1.0.view", @"/Views/Site/IndexItemOld.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Site/IndexItemOld.cshtml", typeof(AspNetCore.Views_Site_IndexItemOld))]
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
#line 5 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"991e389d81505a83aa333c4f36e9395295d18f15", @"/Views/Site/IndexItemOld.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Site_IndexItemOld : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Billboard360.Web.Models.SiteDetailItemModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/add-create.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 15px;width: 15px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("padding: 5px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Site", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "IndexPrice", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "IndexImage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 7 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
  
	ViewData["Title"] = "Site Detial Listing";

#line default
#line hidden
            BeginContext(314, 241, true);
            WriteLiteral("<div class=\"row\">\r\n\t<div class=\"col-12\">\r\n\t\t<div class=\"box\">\r\n\t\t\t<div class=\"box-header\">\r\n\t\t\t\t<h3 class=\"box-title\">Manage Site Detail</h3>\r\n\t\t\t\t<p align=\"right\">\r\n\t\t\t\t\t<a class=\"arrowLeft\" id=\"backLink\" style=\"float:left;\">Back</a>\r\n\t\t\t\t\t");
            EndContext();
            BeginContext(555, 191, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "991e389d81505a83aa333c4f36e9395295d18f157403", async() => {
                BeginContext(656, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "991e389d81505a83aa333c4f36e9395295d18f157666", async() => {
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
                BeginContext(726, 16, true);
                WriteLiteral(" Add Site Detail");
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
            BeginContext(746, 23, true);
            WriteLiteral("\r\n\t\t\t\t</p>\r\n\t\t\t</div>\r\n");
            EndContext();
#line 20 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
             if (Html.ViewData.ModelState.ContainsKey(string.Empty))
			{

#line default
#line hidden
            BeginContext(836, 56, true);
            WriteLiteral("\t\t\t\t<div class=\"alert alert-danger\" role=\"alert\">\r\n\t\t\t\t\t");
            EndContext();
            BeginContext(893, 72, false);
#line 23 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
               Write(Html.ValidationSummary(true, "Terjadi kesalahan saat mendapatkan data."));

#line default
#line hidden
            EndContext();
            BeginContext(965, 14, true);
            WriteLiteral("\r\n\t\t\t\t</div>\r\n");
            EndContext();
#line 25 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
			}

#line default
#line hidden
            BeginContext(985, 286, true);
            WriteLiteral(@"			<!-- /.box-header -->
			<div class=""box-body auto-table"">
				<table id=""example1"" class=""table table-bordered table-striped"" style=""width:100%;"">
					<thead>
						<tr>
							<th>Arah Lokasi</th>
							<th>Kode Lokasi</th>
							<th>Panjang</th>
							<th>Lebar</th>
");
            EndContext();
#line 35 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                             if (HttpContextAccessor.HttpContext.Session.GetString("_ROLE") != "SPV")
							{

#line default
#line hidden
            BeginContext(1363, 48, true);
            WriteLiteral("\t\t\t\t\t\t\t\t<th>View</th>\r\n\t\t\t\t\t\t\t\t<th>Action</th>\r\n");
            EndContext();
#line 39 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
							}

#line default
#line hidden
            BeginContext(1421, 42, true);
            WriteLiteral("\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t</thead>\r\n\t\t\t\t\t<tbody>\r\n");
            EndContext();
#line 43 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                         foreach (var item in Model)
						{

#line default
#line hidden
            BeginContext(1508, 25, true);
            WriteLiteral("\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1534, 45, false);
#line 46 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                               Write(Html.DisplayFor(modelItem => item.ArahLokasi));

#line default
#line hidden
            EndContext();
            BeginContext(1579, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1599, 45, false);
#line 47 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                               Write(Html.DisplayFor(modelItem => item.KodeLokasi));

#line default
#line hidden
            EndContext();
            BeginContext(1644, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1664, 42, false);
#line 48 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Panjang));

#line default
#line hidden
            EndContext();
            BeginContext(1706, 19, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1726, 40, false);
#line 49 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Lebar));

#line default
#line hidden
            EndContext();
            BeginContext(1766, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 50 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                                 if (HttpContextAccessor.HttpContext.Session.GetString("_ROLE") != "SPV")
								{

#line default
#line hidden
            BeginContext(1867, 13, true);
            WriteLiteral("\t\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(1880, 95, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "991e389d81505a83aa333c4f36e9395295d18f1514959", async() => {
                BeginContext(1961, 10, true);
                WriteLiteral("View Price");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 52 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                                                                                           WriteLiteral(item.SiteItemID);

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
            BeginContext(1975, 3, true);
            WriteLiteral(" | ");
            EndContext();
            BeginContext(1978, 95, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "991e389d81505a83aa333c4f36e9395295d18f1517562", async() => {
                BeginContext(2059, 10, true);
                WriteLiteral("View Image");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 52 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                                                                                                                                                                                             WriteLiteral(item.SiteItemID);

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
            BeginContext(2073, 20, true);
            WriteLiteral("</td>\r\n\t\t\t\t\t\t\t\t\t<td>");
            EndContext();
            BeginContext(2093, 91, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "991e389d81505a83aa333c4f36e9395295d18f1520292", async() => {
                BeginContext(2174, 6, true);
                WriteLiteral("Update");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 53 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                                                                                           WriteLiteral(item.SiteItemID);

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
            BeginContext(2184, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 54 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
								}
								

#line default
#line hidden
            BeginContext(2327, 14, true);
            WriteLiteral("\t\t\t\t\t\t\t</tr>\r\n");
            EndContext();
#line 57 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
						}

#line default
#line hidden
            BeginContext(2350, 454, true);
            WriteLiteral(@"					</tbody>
				</table>
			</div>
			<!-- /.box-body -->
		</div>
		<!-- /.box -->
	</div>
	<!-- /.col -->
</div>
<!-- page script -->
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
            BeginContext(2805, 28, false);
#line 80 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Site\IndexItemOld.cshtml"
                 Write(Url.Action("Delete", "Site"));

#line default
#line hidden
            EndContext();
            BeginContext(2833, 567, true);
            WriteLiteral(@"',
                data: {
                    id: id
                },
                error: function (result) {
                    alert(""error"");
                },
                success: function (result) {
                    if (result == true) {
                        var baseUrl=""/Site"";
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Billboard360.Web.Models.SiteDetailItemModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
