#pragma checksum "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b24696aafd0bc6f51adbae91a37389948bf4bf43"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MediaBuyer_ViewDetail), @"mvc.1.0.view", @"/Views/MediaBuyer/ViewDetail.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MediaBuyer/ViewDetail.cshtml", typeof(AspNetCore.Views_MediaBuyer_ViewDetail))]
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
#line 1 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\_ViewImports.cshtml"
using Iklan;

#line default
#line hidden
#line 2 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\_ViewImports.cshtml"
using Iklan.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b24696aafd0bc6f51adbae91a37389948bf4bf43", @"/Views/MediaBuyer/ViewDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7a2742a29fddcfb3cc72624772d704651ac96bb", @"/Views/_ViewImports.cshtml")]
    public class Views_MediaBuyer_ViewDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Iklan.Models.SiteDetailResponseModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/scripts/MediaBuyer/ViewDetail.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
  
    ViewData["Title"] = "ViewDetail";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(265, 139, true);
            WriteLiteral("<script src=\"https://maps.google.com/maps/api/js?key=AIzaSyDjrac-6MHc6-JNehr3r1lFBC-vqI_Wh88\"></script>\r\n<script>\r\n    var urlAddToCart = \'");
            EndContext();
            BeginContext(405, 37, false);
#line 10 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                   Write(Url.Action("AddtoCart", "MediaBuyer"));

#line default
#line hidden
            EndContext();
            BeginContext(442, 28, true);
            WriteLiteral("\';\r\n    var urlAddtoBook = \'");
            EndContext();
            BeginContext(471, 37, false);
#line 11 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                   Write(Url.Action("AddtoBookNew", "Booking"));

#line default
#line hidden
            EndContext();
            BeginContext(508, 15, true);
            WriteLiteral("\';\r\n</script>\r\n");
            EndContext();
            BeginContext(523, 84, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24696aafd0bc6f51adbae91a37389948bf4bf434890", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 13 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(607, 504, true);
            WriteLiteral(@"


<section class=""spd-wrap"">
    <div class=""container"">
        <div class=""row"">

            <div class=""col-lg-12 col-md-12"">

                <div class=""slide-property-detail"">

                    <div class=""slide-property-first"">
                        <div class=""row"">
                            <div class=""col-lg-8 col-md-8"" style=""margin-left:40px;"">
                                <div class=""row"">
                                    <input type=""hidden"" id=""LatitudeH""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1111, "\"", 1146, 1);
#line 28 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1119, Model.data.Detail.Latitude, 1119, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1147, 77, true);
            WriteLiteral(" />\r\n                                    <input type=\"hidden\" id=\"longitudeH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1224, "\"", 1260, 1);
#line 29 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1232, Model.data.Detail.Longitude, 1232, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1261, 74, true);
            WriteLiteral(" />\r\n                                    <input type=\"hidden\" id=\"SiteIDH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1335, "\"", 1368, 1);
#line 30 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1343, Model.data.Detail.SiteID, 1343, 25, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1369, 79, true);
            WriteLiteral(" />\r\n                                    <input type=\"hidden\" id=\"SitePriceIDH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1448, "\"", 1524, 1);
#line 31 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1456, Model.data.Item.FirstOrDefault().Price.FirstOrDefault().SitePriceID, 1456, 68, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1525, 80, true);
            WriteLiteral(" />\r\n\r\n                                    <input type=\"hidden\" id=\"SiteItemIDH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1605, "\"", 1657, 1);
#line 33 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1613, Model.data.Item.FirstOrDefault().SiteItemID, 1613, 44, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1658, 77, true);
            WriteLiteral(" />\r\n                                    <input type=\"hidden\" id=\"HargaAwalH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1735, "\"", 1809, 1);
#line 34 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 1743, Model.data.Item.FirstOrDefault().Price.FirstOrDefault().HargaAwal, 1743, 66, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1810, 613, true);
            WriteLiteral(@" />

                                    <!-- Single Items -->
                                    <div class=""col-xs-6 col-lg-3 col-md-6"">
                                        <div class=""singles_item"">
                                            <div class=""icon"">
                                                <i class=""icofont-eye""></i>
                                            </div>
                                            <div class=""info"">
                                                <h4 class=""name"">View(s)</h4>
                                                <p class=""value"">");
            EndContext();
            BeginContext(2424, 27, false);
#line 44 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                            Write(Model.data.Detail.TotalView);

#line default
#line hidden
            EndContext();
            BeginContext(2451, 761, true);
            WriteLiteral(@"</p>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Single Items -->
                                    <div class=""col-xs-6 col-lg-3 col-md-6"">
                                        <div class=""singles_item"">
                                            <div class=""icon"">
                                                <i class=""icofont-billboard""></i>
                                            </div>
                                            <div class=""info"">
                                                <h4 class=""name"">Type</h4>
                                                <p class=""value"">");
            EndContext();
            BeginContext(3213, 22, false);
#line 57 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                            Write(Model.data.Detail.Tipe);

#line default
#line hidden
            EndContext();
            BeginContext(3235, 723, true);
            WriteLiteral(@"</p>
                                            </div>
                                        </div>
                                    </div>


                                    <!-- Single Items -->
                                    <div class=""col-xs-6 col-lg-3 col-md-6"" style=""max-width:100%;flex:100;"">
                                        <div class=""singles_item"">
                                            <div class=""icon"">
                                                <i class=""icofont-location-pin""></i>
                                            </div>
                                            <div class=""info"">
                                                <h4 class=""name"">");
            EndContext();
            BeginContext(3959, 24, false);
#line 70 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                            Write(Model.data.Detail.Alamat);

#line default
#line hidden
            EndContext();
            BeginContext(3983, 72, true);
            WriteLiteral("</h4>\r\n                                                <p class=\"value\">");
            EndContext();
            BeginContext(4056, 22, false);
#line 71 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                            Write(Model.data.Detail.Kota);

#line default
#line hidden
            EndContext();
            BeginContext(4078, 2, true);
            WriteLiteral(", ");
            EndContext();
            BeginContext(4081, 26, false);
#line 71 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                     Write(Model.data.Detail.Provinsi);

#line default
#line hidden
            EndContext();
            BeginContext(4107, 946, true);
            WriteLiteral(@"</p>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
</section>
<!-- ============================ Hero Banner End ================================== -->
<!-- ============================ Property Detail Start ================================== -->
<section class=""gray pt-5"">
    <div class=""container"">
        <div class=""row"">

            <!-- property main detail -->
            <div class=""col-lg-8 col-md-12 col-sm-12 order-lg-1 order-md-2 order-2"">

                <div class=""block-wrap"">

                    <div class=""block-body"">
                        <div id=""owl-demo"" class=""owl-carousel owl-theme"">
");
            EndContext();
#line 100 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                             if (Model.data.Item.FirstOrDefault().Image != null)
                            {
                                

#line default
#line hidden
#line 102 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                 foreach (var img in Model.data.Item.FirstOrDefault().Image)
                                {

#line default
#line hidden
            BeginContext(5295, 43, true);
            WriteLiteral("                                    <div><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 5338, "\"", 5355, 1);
#line 104 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 5345, img.Image, 5345, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5356, 26, true);
            WriteLiteral(" class=\"mfp-gallery \"><img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 5382, "\"", 5398, 1);
#line 104 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 5388, img.Image, 5388, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5399, 46, true);
            WriteLiteral(" class=\" img-responsive\" alt=\"\" /></a></div>\r\n");
            EndContext();
#line 105 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                }

#line default
#line hidden
#line 105 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                 
                            }

#line default
#line hidden
            BeginContext(5511, 360, true);
            WriteLiteral(@"                        </div>
                    </div>
                </div>

                <!-- Single Block Wrap -->
                <div class=""block-wrap"">

                    <div class=""block-header"">
                        <h4 class=""block-title"">Details</h4>
                    </div>

                    <div class=""block-body"">
");
            EndContext();
#line 119 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                         if (Model.data != null)
                        {


#line default
#line hidden
            BeginContext(5950, 260, true);
            WriteLiteral(@"                            <div class=""col-md-12"">

                                <div style=""display: flex;flex-wrap: nowrap;"">
                                    <div style=""border: 1px solid #e7eff9; padding: 10px; text-align: center; width: 20%;"">
");
            EndContext();
#line 126 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                         if (Model.data.Item.Count > 0)
                                        {

#line default
#line hidden
            BeginContext(6326, 163, true);
            WriteLiteral("                                            <p style=\"margin-bottom:5px;\">Ukuran</p>\r\n                                            <p style=\"margin-bottom:0px;\"><b>");
            EndContext();
            BeginContext(6490, 26, false);
#line 129 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                        Write(Model.data.Item[0].Panjang);

#line default
#line hidden
            EndContext();
            BeginContext(6516, 3, true);
            WriteLiteral(" X ");
            EndContext();
            BeginContext(6520, 24, false);
#line 129 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                      Write(Model.data.Item[0].Lebar);

#line default
#line hidden
            EndContext();
            BeginContext(6544, 16, true);
            WriteLiteral(" Meter</b></p>\r\n");
            EndContext();
#line 130 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
            BeginContext(6692, 180, true);
            WriteLiteral("                                            <p style=\"margin-bottom:5px;\">Ukuran</p>\r\n                                            <p style=\"margin-bottom:0px;\"><b>0 Meter</b></p>\r\n");
            EndContext();
#line 135 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                        }

#line default
#line hidden
            BeginContext(6915, 334, true);
            WriteLiteral(@"                                    </div>
                                    <div style=""border: 1px solid #e7eff9; padding: 10px; text-align: center; width: 20%;"">
                                        <p style=""margin-bottom:5px;"">Bentuk</p>
                                        <p style=""margin-bottom:0px;"" id=""HorV""><b>");
            EndContext();
            BeginContext(7250, 22, false);
#line 139 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                              Write(Model.data.Detail.HorV);

#line default
#line hidden
            EndContext();
            BeginContext(7272, 342, true);
            WriteLiteral(@"</b></p>
                                    </div>
                                    <div style=""border: 1px solid #e7eff9; padding: 10px; text-align: center; width: 20%;"">
                                        <p style=""margin-bottom:5px;"">Tipe</p>
                                        <p style=""margin-bottom:0px;"" id=""tipe""><b>");
            EndContext();
            BeginContext(7615, 22, false);
#line 143 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                              Write(Model.data.Detail.Tipe);

#line default
#line hidden
            EndContext();
            BeginContext(7637, 130, true);
            WriteLiteral("</b></p>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n");
            EndContext();
#line 147 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                        }

#line default
#line hidden
            BeginContext(7794, 1039, true);
            WriteLiteral(@"                    </div>

                </div>

                <!-- Single Block Wrap -->
                <div class=""block-wrap"">

                    <div class=""block-header"">
                        <h4 class=""block-title"">Maps</h4>
                    </div>

                    <div class=""home-map fl-wrap"">
                        <div class=""map-container fw-map"">
                            <div id=""map-main""></div>
                        </div>
                    </div>

                </div>
            </div>

            <!-- property Sidebar -->
            <div class=""col-lg-4 col-md-12 col-sm-12 order-lg-2 order-md-1 order-1"">

                <div class=""side-booking-wraps "">
                    <div class=""side-booking-wrap hotel-booking"">

                        <div class=""side-booking-header light"">
                            <div class=""author-with-rate"">
                                <div class=""head-author"">
                                    <");
            WriteLiteral("h5 id=\"alamat\">");
            EndContext();
            BeginContext(8834, 24, false);
#line 177 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                               Write(Model.data.Detail.Alamat);

#line default
#line hidden
            EndContext();
            BeginContext(8858, 90, true);
            WriteLiteral("</h5>\r\n                                    <span id=\"kota\"><i class=\"ti-location-pin\"></i>");
            EndContext();
            BeginContext(8949, 22, false);
#line 178 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                              Write(Model.data.Detail.Kota);

#line default
#line hidden
            EndContext();
            BeginContext(8971, 2, true);
            WriteLiteral(", ");
            EndContext();
            BeginContext(8974, 26, false);
#line 178 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                       Write(Model.data.Detail.Provinsi);

#line default
#line hidden
            EndContext();
            BeginContext(9000, 109, true);
            WriteLiteral("</span>\r\n                                </div>\r\n                                <div class=\"head-ratting\">\r\n");
            EndContext();
            BeginContext(9678, 66, true);
            WriteLiteral("                                    <h4 class=\"head-list-titleup\">");
            EndContext();
            BeginContext(9745, 22, false);
#line 189 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                             Write(Model.data.Detail.Tipe);

#line default
#line hidden
            EndContext();
            BeginContext(9767, 73, true);
            WriteLiteral("</h4>\r\n                                    <h4 class=\"head-list-titleup\">");
            EndContext();
            BeginContext(9841, 29, false);
#line 190 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                             Write(Model.data.Detail.NoBillboard);

#line default
#line hidden
            EndContext();
            BeginContext(9870, 355, true);
            WriteLiteral(@"</h4>
                                </div>
                            </div>
                        </div>

                        <div class=""side-booking-body"">
                            <!-- Single Row Booking -->
                            <div class=""single-row-booking"">
                                <input type=""hidden"" id=""imgH""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 10225, "\"", 10263, 1);
#line 198 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 10233, Model.data.Detail.ImageHeader, 10233, 30, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(10264, 5, true);
            WriteLiteral(" />\r\n");
            EndContext();
#line 199 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                 if (Model.data.Item.FirstOrDefault().Price.Count > 0)
                                {

#line default
#line hidden
            BeginContext(10392, 68, true);
            WriteLiteral("                                    <input type=\"hidden\" id=\"priceH\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 10460, "\"", 10552, 1);
#line 201 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
WriteAttributeValue("", 10468, Model.data.Item.FirstOrDefault().Price.FirstOrDefault().HargaAwal.ToString("#,##0"), 10468, 84, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(10553, 131, true);
            WriteLiteral(" />\r\n                                    <span class=\"onsale-section\"><span class=\"onsale\" style=\"max-width:185px;width:100%;\">Rp. ");
            EndContext();
            BeginContext(10685, 86, false);
#line 202 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                                         Write(Model.data.Item.FirstOrDefault().Price.FirstOrDefault().HargaAwal.ToString("#,##0.00"));

#line default
#line hidden
            EndContext();
            BeginContext(10771, 11, true);
            WriteLiteral("<small>per ");
            EndContext();
            BeginContext(10783, 66, false);
#line 202 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                                                                                                                                           Write(Model.data.Item.FirstOrDefault().Price.FirstOrDefault().MinimDasar);

#line default
#line hidden
            EndContext();
            BeginContext(10849, 24, true);
            WriteLiteral("</small></span></span>\r\n");
            EndContext();
#line 203 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"

                                }
                                else
                                {

#line default
#line hidden
            BeginContext(10983, 143, true);
            WriteLiteral("                                    <span class=\"onsale-section\"><span class=\"onsale\" style=\"max-width:185px;width:100%;\">Rp. 0</span></span>\r\n");
            EndContext();
            BeginContext(11128, 83, true);
            WriteLiteral("                                    <input type=\"hidden\" id=\"priceH\" value=\"0\" />\r\n");
            EndContext();
#line 210 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                }

#line default
#line hidden
            BeginContext(11246, 144, true);
            WriteLiteral("                                <div class=\"row\">\r\n                                    <div class=\"col-lg-12 col-md-12 col-sm-12 small-spilx\">\r\n");
            EndContext();
#line 213 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                         if (Model.data.Item.FirstOrDefault().Price.Count > 0)
                                        {

#line default
#line hidden
            BeginContext(11529, 78, true);
            WriteLiteral("                                            <h4 class=\"booking-title\">Minimal ");
            EndContext();
            BeginContext(11608, 66, false);
#line 215 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                         Write(Model.data.Item.FirstOrDefault().Price.FirstOrDefault().MinimOrder);

#line default
#line hidden
            EndContext();
            BeginContext(11674, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(11676, 66, false);
#line 215 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                                                             Write(Model.data.Item.FirstOrDefault().Price.FirstOrDefault().MinimDasar);

#line default
#line hidden
            EndContext();
            BeginContext(11742, 7, true);
            WriteLiteral("</h4>\r\n");
            EndContext();
#line 216 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"

                                        }

#line default
#line hidden
            BeginContext(11794, 3202, true);
            WriteLiteral(@"
                                    </div>
                                    <div class=""col-lg-12 col-md-12 col-sm-12 col-12 small-spilx"">
                                        <div class=""form-group"">
                                            <div class=""guests"">
                                                <div class=""advance-bboking"">

                                                    <div class=""guest-type"">
                                                        <h5>Start Date</h5>
                                                    </div>
                                                    <div class=""form-group"">
                                                        <div class=""cld-box"">
                                                            <i class=""ti-calendar""></i>
                                                            <input type=""text"" name=""startdate"" id=""startdate"" class=""form-control"" value="""" />
                                                        </d");
            WriteLiteral(@"iv>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=""col-lg-12 col-md-12 col-sm-12 small-spilx brl"">
                                        <div class=""form-group"">
                                            <div class=""guests"">
                                                <div class=""advance-bboking"">

                                                    <div class=""guest-type"">
                                                        <h5>End Date</h5>
                                                    </div>
                                                    <div class=""form-group"">
                                                        <div class=""cld-box"">
                                                            <i class=""ti-cale");
            WriteLiteral(@"ndar""></i>
                                                            <input type=""text"" name=""enddate"" id=""enddate"" class=""form-control"" value="""" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- Single Row Booking -->

                        </div>

                        <div class=""side-booking-footer light"">
                            <div class=""stbooking-footer-top"">
                                <div class=""stbooking-left"">
                                    <h5 class=""st-subtitle"">Duration</h5>
                                </div>
                                <h4 class=""stbooking-title""><span id=""");
            WriteLiteral("totDuration\" style=\"font-size: 17px;font-weight: 700;\">0 </span><span id=\"totDuration\" style=\"font-size: 17px;font-weight: 700;\"> ");
            EndContext();
            BeginContext(14997, 66, false);
#line 269 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                                                                                                                                                                                   Write(Model.data.Item.FirstOrDefault().Price.FirstOrDefault().MinimDasar);

#line default
#line hidden
            EndContext();
            BeginContext(15063, 536, true);
            WriteLiteral(@"</span></h4>
                            </div>
                            <div class=""stbooking-footer-top"">
                                <div class=""stbooking-left"">
                                    <h5 class=""st-subtitle"">Total Amount</h5>
                                </div>
                                <h4 class=""stbooking-title""><span id=""totAmt"" style=""font-size: 17px;font-weight: 700;"">Rp 0</span></h4>
                            </div>
                            <div class=""stbooking-footer-bottom"">
");
            EndContext();
#line 278 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                 if (Model.isLogin == true)
                                {

#line default
#line hidden
            BeginContext(15695, 220, true);
            WriteLiteral("                                    <a onclick=\"addToCartClicked()\" class=\"books-btn btn-theme\">Add Cart</a>\r\n                                    <a onclick=\"bookingNowClicked()\" class=\"books-btn black\">Booking Now</a>\r\n");
            EndContext();
#line 282 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                }
                                else
                                {

#line default
#line hidden
            BeginContext(16023, 261, true);
            WriteLiteral(@"                                    <a href=""#"" data-toggle=""modal"" data-target=""#login"" class=""books-btn btn-theme"">Add Cart</a>
                                    <a href=""#"" data-toggle=""modal"" data-target=""#login"" class=""books-btn black"">Booking Now</a>
");
            EndContext();
#line 287 "D:\Me\BillboardNewAdmin\Iklan\Iklan\Views\MediaBuyer\ViewDetail.cshtml"
                                }

#line default
#line hidden
            BeginContext(16319, 198, true);
            WriteLiteral("                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n<!-- Map -->\r\n");
            EndContext();
            BeginContext(16746, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Iklan.Models.SiteDetailResponseModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
