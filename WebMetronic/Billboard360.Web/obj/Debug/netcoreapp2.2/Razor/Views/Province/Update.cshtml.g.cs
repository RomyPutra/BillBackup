#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f16c0c83e5ec7ebddc0bed90e2ea745618c50b96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Province_Update), @"mvc.1.0.view", @"/Views/Province/Update.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Province/Update.cshtml", typeof(AspNetCore.Views_Province_Update))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f16c0c83e5ec7ebddc0bed90e2ea745618c50b96", @"/Views/Province/Update.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Province_Update : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Billboard360.Web.Models.ProvinceOutputModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/Province/UpdateProvince"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
  
    ViewData["Title"] = "Perbarui Provinsi";

#line default
#line hidden
            BeginContext(105, 205, true);
            WriteLiteral("\r\n<div class=\"col-md-6 offset-md-3\">\r\n\t<div class=\"card card-custom\">\r\n\t\t<div class=\"card-header\">\r\n\t\t\t<div class=\"card-title\">\r\n\t\t\t\t<h3 class=\"card-label\">Perbarui Provinsi</h3>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\r\n\t\t");
            EndContext();
            BeginContext(310, 1373, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f16c0c83e5ec7ebddc0bed90e2ea745618c50b964641", async() => {
                BeginContext(365, 30, true);
                WriteLiteral("\r\n\t\t\t<div class=\"card-body\">\r\n");
                EndContext();
#line 16 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
                 if (Html.ViewData.ModelState.ContainsKey(string.Empty))
				{

#line default
#line hidden
                BeginContext(464, 58, true);
                WriteLiteral("\t\t\t\t\t<div class=\"alert alert-danger\" role=\"alert\">\r\n\t\t\t\t\t\t");
                EndContext();
                BeginContext(523, 67, false);
#line 19 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
                   Write(Html.ValidationSummary(true, "Terjadi kesalahan saat memperbarui."));

#line default
#line hidden
                EndContext();
                BeginContext(590, 15, true);
                WriteLiteral("\r\n\t\t\t\t\t</div>\r\n");
                EndContext();
#line 21 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
				}

#line default
#line hidden
                BeginContext(612, 60, true);
                WriteLiteral("\r\n\t\t\t\t<input type=\"hidden\" id=\"provinceID\" name=\"provinceID\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 672, "\"", 697, 1);
#line 23 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
WriteAttributeValue("", 680, Model.ProvinceID, 680, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(698, 164, true);
                WriteLiteral(">\r\n\r\n\t\t\t\t<div class=\"form-group\">\r\n\t\t\t\t\t<label>Kode Provinsi</label>\r\n\t\t\t\t\t<input type=\"text\" class=\"form-control\" id=\"kode\" name=\"kode\" placeholder=\"Kode Provinsi\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 862, "\"", 881, 1);
#line 27 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
WriteAttributeValue("", 870, Model.Kode, 870, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(882, 361, true);
                WriteLiteral(@" data-val=""true"" data-val-required=""The province code is required""/>
					<span style=""display:block;"" class=""invalid-feedback"" data-valmsg-for=""kode"" data-valmsg-replace=""true""></span>
				</div>

				<div class=""form-group"">
					<label>Nama Provinsi</label>
					<input type=""text"" class=""form-control"" id=""nama"" name=""nama"" placeholder=""Nama Provinsi""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1243, "\"", 1266, 1);
#line 33 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Province\Update.cshtml"
WriteAttributeValue("", 1251, Model.Provinsi, 1251, 15, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1267, 409, true);
                WriteLiteral(@" data-val=""true"" data-val-required=""The province name is required""/>
					<span style=""display:block;"" class=""invalid-feedback"" data-valmsg-for=""nama"" data-valmsg-replace=""true""></span>
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
            BeginContext(1683, 23, true);
            WriteLiteral("\r\n\t</div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Billboard360.Web.Models.ProvinceOutputModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
