#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcd89f263e59b48d7aea962a56299097a836e308"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_City_Update), @"mvc.1.0.view", @"/Views/City/Update.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/City/Update.cshtml", typeof(AspNetCore.Views_City_Update))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcd89f263e59b48d7aea962a56299097a836e308", @"/Views/City/Update.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_City_Update : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Billboard360.Web.Models.CityOutputModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/City/UpdateCity"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
  
    ViewData["Title"] = "Perbarui Kota";

#line default
#line hidden
            BeginContext(97, 201, true);
            WriteLiteral("\r\n<div class=\"col-md-6 offset-md-3\">\r\n\t<div class=\"card card-custom\">\r\n\t\t<div class=\"card-header\">\r\n\t\t\t<div class=\"card-title\">\r\n\t\t\t\t<h3 class=\"card-label\">Perbarui Kota</h3>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\r\n\t\t");
            EndContext();
            BeginContext(298, 1907, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcd89f263e59b48d7aea962a56299097a836e3084588", async() => {
                BeginContext(345, 30, true);
                WriteLiteral("\r\n\t\t\t<div class=\"card-body\">\r\n");
                EndContext();
#line 16 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
                 if (Html.ViewData.ModelState.ContainsKey(string.Empty))
				{

#line default
#line hidden
                BeginContext(444, 58, true);
                WriteLiteral("\t\t\t\t\t<div class=\"alert alert-danger\" role=\"alert\">\r\n\t\t\t\t\t\t");
                EndContext();
                BeginContext(503, 64, false);
#line 19 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
                   Write(Html.ValidationSummary(true, "Terjadi kesalahan saat mengirim."));

#line default
#line hidden
                EndContext();
                BeginContext(567, 15, true);
                WriteLiteral("\r\n\t\t\t\t\t</div>\r\n");
                EndContext();
#line 21 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
				}

#line default
#line hidden
                BeginContext(589, 52, true);
                WriteLiteral("\r\n\t\t\t\t<input type=\"hidden\" id=\"CityID\" name=\"CityID\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 641, "\"", 662, 1);
#line 23 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
WriteAttributeValue("", 649, Model.CityID, 649, 13, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(663, 158, true);
                WriteLiteral(">\r\n\r\n\t\t\t\t<div class=\"form-group\">\r\n\t\t\t\t\t<label>Kode Kota</label>\r\n\t\t\t\t\t<input type=\"text\" class=\"form-control\" id=\"kodes\" name=\"kodes\" placeholder=\"Kode Kota\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 821, "\"", 840, 1);
#line 27 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
WriteAttributeValue("", 829, Model.Kode, 829, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(841, 189, true);
                WriteLiteral(" data-val=\"false\" data-val-required=\"The city code is required\" disabled />\r\n\t\t\t\t\t<input type=\"text\" class=\"form-control\" style=\"display:none;\" id=\"kode\" name=\"kode\" placeholder=\"Kode Kota\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1030, "\"", 1049, 1);
#line 28 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
WriteAttributeValue("", 1038, Model.Kode, 1038, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1050, 365, true);
                WriteLiteral(@" data-val=""true"" data-val-required=""The city code is required"" visible=""false""/>
					<span style=""display:block;"" class=""invalid-feedback"" data-valmsg-for=""kode"" data-valmsg-replace=""true""></span>
				</div>

				<div class=""form-group"">
					<label>Nama Kota</label>
					<input type=""text"" class=""form-control"" id=""nama"" name=""nama"" placeholder=""Nama Kota""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1415, "\"", 1434, 1);
#line 34 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\City\Update.cshtml"
WriteAttributeValue("", 1423, Model.Kota, 1423, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1435, 763, true);
                WriteLiteral(@" data-val=""true"" data-val-required=""The city name is required""/>
					<span style=""display:block;"" class=""invalid-feedback"" data-valmsg-for=""nama"" data-valmsg-replace=""true""></span>
				</div>

				<div class=""form-group"">
					<label>Pilih Provinsi</label>
					<select class=""form-control"" id=""lstprovince"" name=""lstprovince"" data-val=""true"" data-val-required=""The Province field is required.""></select>
					<span style=""display:block;"" class=""invalid-feedback"" data-valmsg-for=""Detail.Provinsi"" data-valmsg-replace=""true""></span>
				</div>
			</div>
			<div class=""card-footer"">
				<input type=""submit"" class=""btn btn-primary mr-2"" value=""Perbarui"">
				<button type=""button"" id=""backLink"" class=""btn btn-secondary"">Batal</button>
			</div>
		");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2205, 21, true);
            WriteLiteral("\r\n\t</div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Billboard360.Web.Models.CityOutputModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
