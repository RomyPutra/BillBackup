#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\ExportImport\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "deb1ada7f4c98afe935fce0dca27010dd0311d1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ExportImport_Index), @"mvc.1.0.razor-page", @"/Views/ExportImport/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Views/ExportImport/Index.cshtml", typeof(AspNetCore.Views_ExportImport_Index), null)]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"deb1ada7f4c98afe935fce0dca27010dd0311d1a", @"/Views/ExportImport/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_ExportImport_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(69, 282, true);
            WriteLiteral(@"
<div class=""card card-custom"">
	<div class=""card-header"">
		<div class=""card-title"">
			<h3 class=""card-label"">Unggah Site</h3>
		</div>
	</div>		

	<div class=""card-body"">
		<div id=""loader"" style=""display:none;""></div>

		<div id=""divload"" class=""animate-bottom"">
			");
            EndContext();
            BeginContext(351, 605, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "deb1ada7f4c98afe935fce0dca27010dd0311d1a4349", async() => {
                BeginContext(401, 389, true);
                WriteLiteral(@"
				<div class=""row"">
					<div class=""col-md-4"">
						<input type=""file"" id=""fUpload"" name=""files"" class=""form-control"" />
					</div>
					<div class=""col-md-4"">
						<input type=""button"" class=""btn btn-primary"" id=""btnUpload"" value=""Upload"" />
					</div>
					<div class=""col-md-4"">
						<input type=""button"" class=""btn btn-info"" id=""btnDownload"" value=""Download Template""");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 790, "\"", 855, 3);
                WriteAttributeValue("", 800, "location.href=\'", 800, 15, true);
#line 26 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\ExportImport\Index.cshtml"
WriteAttributeValue("", 815, Url.Action("Download", "ExportImport"), 815, 39, false);

#line default
#line hidden
                WriteAttributeValue("", 854, "\'", 854, 1, true);
                EndWriteAttribute();
                BeginContext(856, 93, true);
                WriteLiteral(" />\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t\t<br />\r\n\t\t\t\t<div id=\"dvData\" class=\"auto-table\"></div>\r\n\t\t\t");
                EndContext();
            }
            );
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
            BeginContext(956, 27, true);
            WriteLiteral("\r\n\t\t</div>\r\n\t</div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Billboard360.Web.Views.ExportImport.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Billboard360.Web.Views.ExportImport.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Billboard360.Web.Views.ExportImport.IndexModel>)PageContext?.ViewData;
        public Billboard360.Web.Views.ExportImport.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
