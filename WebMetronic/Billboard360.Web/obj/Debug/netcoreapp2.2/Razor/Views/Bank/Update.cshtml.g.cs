#pragma checksum "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "45c751c558d5ce73a78d67373d11164a5322d732"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Bank_Update), @"mvc.1.0.view", @"/Views/Bank/Update.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Bank/Update.cshtml", typeof(AspNetCore.Views_Bank_Update))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45c751c558d5ce73a78d67373d11164a5322d732", @"/Views/Bank/Update.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2664fbb93c39c104a9cee248e26ee1e5ef9116a", @"/Views/_ViewImports.cshtml")]
    public class Views_Bank_Update : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Billboard360.Web.Models.InfoBankDetailModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/Bank/UpdateBank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
  
    ViewData["Title"] = "Update Bank";

#line default
#line hidden
            BeginContext(99, 297, true);
            WriteLiteral(@"
<div class=""wrapper fadeInDown"">
	<div id=""formContent"" class=""regis"">
		<div style=""text-align:right;"">
			<button type=""button"" id=""backLink"" style=""background:red;""><span aria-hidden=""true"">&times;</span></button>
		</div>
		<h2 class=""active""> Update Bank </h2>
		<!-- Login Form -->
");
            EndContext();
#line 13 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
         if (Html.ViewData.ModelState.ContainsKey(string.Empty))
		{

#line default
#line hidden
            BeginContext(461, 54, true);
            WriteLiteral("\t\t\t<div class=\"alert alert-danger\" role=\"alert\">\r\n\t\t\t\t");
            EndContext();
            BeginContext(516, 64, false);
#line 16 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
           Write(Html.ValidationSummary(true, "Terjadi kesalahan saat mengirim."));

#line default
#line hidden
            EndContext();
            BeginContext(580, 13, true);
            WriteLiteral("\r\n\t\t\t</div>\r\n");
            EndContext();
#line 18 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
		}

#line default
#line hidden
            BeginContext(598, 2, true);
            WriteLiteral("\t\t");
            EndContext();
            BeginContext(600, 943, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45c751c558d5ce73a78d67373d11164a5322d7325643", async() => {
                BeginContext(647, 51, true);
                WriteLiteral("\r\n\t\t\t<input type=\"hidden\" id=\"bankID\" name=\"bankID\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 698, "\"", 719, 1);
#line 20 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
WriteAttributeValue("", 706, Model.BankID, 706, 13, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(720, 99, true);
                WriteLiteral(">\r\n\t\t\t<input type=\"text\" id=\"kode\" class=\"fadeIn first form-control\" name=\"kode\" placeholder=\"kode\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 819, "\"", 838, 1);
#line 21 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
WriteAttributeValue("", 827, Model.Kode, 827, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(839, 291, true);
                WriteLiteral(@" data-val=""true"" data-val-required=""The bank code is required"" />
			<span style=""display:block;"" class=""field-validation-valid"" data-valmsg-for=""kode"" data-valmsg-replace=""true""></span>
			<input type=""text"" id=""nama"" class=""fadeIn second form-control"" name=""nama"" placeholder=""nama bank""");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1130, "\"", 1149, 1);
#line 23 "D:\Me\BillboardNewAdmin\WebMetronic\Billboard360.Web\Views\Bank\Update.cshtml"
WriteAttributeValue("", 1138, Model.Bank, 1138, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1150, 189, true);
                WriteLiteral(" data-val=\"true\" data-val-required=\"The bank name is required\" />\r\n\t\t\t<span style=\"display:block;\" class=\"field-validation-valid\" data-valmsg-for=\"nama\" data-valmsg-replace=\"true\"></span>\r\n");
                EndContext();
                BeginContext(1471, 65, true);
                WriteLiteral("\t\t\t<input type=\"submit\" class=\"fadeIn fourth\" value=\"Update\">\r\n\t\t");
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
            BeginContext(1543, 23, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Billboard360.Web.Models.InfoBankDetailModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
