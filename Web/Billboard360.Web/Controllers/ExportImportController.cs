using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Billboard360.Web.Models;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Billboard360.Web.Controllers
{
    public class ExportImportController : Controller
    {
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyDomain = "_Domain";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public ExportImportController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		public IActionResult Index()
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return View();
			}
			else
			{
				TempData["CustomError"] = "Please login before using the web.";
				if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				{
					return RedirectToAction("AdminLogon", "Login");
				}
				else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				{
					return RedirectToAction("OwnerLogon", "Login");
				}
				else
				{
					return RedirectToAction("BuyerLogon", "Login");
				}
				//return RedirectToAction("Logon", "Login");
			}
		}

		[HttpPost]
        public IActionResult OnPostImport()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string folderroot = "wwwroot";
			string webRootPath = "";
			string errInputFormat = "Mohon check file input anda salah!";
			int rowNum = 1;
			if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			}
			else
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
			}
			string newPath = Path.Combine(webRootPath, folderroot, folderName);
            StringBuilder sb = new StringBuilder();
			ImportSiteModel model = null;
			List<ImportSiteModel> listmodel = new List<ImportSiteModel>();
			ImportSiteInputModel ImportModel = new ImportSiteInputModel();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
						model = new ImportSiteModel();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
							#region AddtoModel
							if (j == 0)
							{
								model.No = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 1)
							{
								model.CP = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 2)
							{
								model.KodeFile = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 3)
							{
								model.KodeLokasi = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 4)
							{
								model.ArahLokasi = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 5)
							{
								model.Pulau = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 6)
							{
								model.KodePulau = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 7)
							{
								model.Provinsi = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 8)
							{
								model.KodeProvinsi = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 9)
							{
								model.Kota = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 10)
							{
								model.NamaCabang = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 11)
							{
								model.Alamat = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 12)
							{
								//type Location
								model.KelasJalan = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 13)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.Panjang = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Panjang Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 14)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.Lebar = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Lebar Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 15)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.Luas = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Luas Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 16)
							{
								model.HorV = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 17)
							{
								model.Type = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 18)
							{
								model.Lampu = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 19)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.Qty = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Qty Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 20)
							{
								model.Satuan = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 21)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.HargaAwal = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Harga Awal Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 22)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.HargaAkhir = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Harga Akhir Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 23)
							{
								var minorder = int.Parse(row.GetCell(j).ToString().Split(" ")[0]);
								var mindasar = row.GetCell(j).ToString().Split(" ")[1];
								if (minorder > 0 && mindasar.Length > 0)
								{
									if (mindasar.ToLower() == "hari")
									{
										model.MinimOrder = minorder;
										model.MinimDasar = mindasar;
									}
									else
									{
										return this.Content("Satuan hari => row nomor " + rowNum + " " + errInputFormat);
									}
								}
								else
								{
									return this.Content("Minimum order harus diisi => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 24)
							{
								model.Cabang = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 25)
							{
								model.WW = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 26)
							{
								model.BID = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 27)
							{
								model.NonKop = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 28)
							{
								model.Note = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 29)
							{
								model.PIC = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 30)
							{
								model.Bendera = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 31)
							{
								model.ContactAgen = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 32)
							{
								model.Telepon = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 33)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									model.Scrore = double.Parse(row.GetCell(j).ToString());
								}
								else
								{
									return this.Content("Score Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 34)
							{
								model.Level = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 35)
							{
								model.A = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 36)
							{
								model.B = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 37)
							{
								model.C = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 38)
							{
								model.Keterangan = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 39)
							{
								model.LinkFolder = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
							}
							else if (j == 40)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									if (Double.Parse("-90") <= Double.Parse(row.GetCell(j).ToString()) && Double.Parse("90") >= Double.Parse(row.GetCell(j).ToString()))
									{
										model.Latitude = row.GetCell(j).ToString().Replace(",", ".");
									}
									else
									{
										return this.Content("Latitude Range -90 s/d 90 => row nomor " + rowNum + " " + errInputFormat);
									}
								}
								else
								{
									return this.Content("Latitude Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							else if (j == 41)
							{
								double retNum;
								bool isNum = Double.TryParse(Convert.ToString(row.GetCell(j)), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

								if (row.GetCell(j).ToString() != "" && isNum)
								{
									if (Double.Parse("-180") <= Double.Parse(row.GetCell(j).ToString()) && Double.Parse("180") >= Double.Parse(row.GetCell(j).ToString()))
									{ 
										model.Longitude = row.GetCell(j).ToString().Replace(",", ".");
									}
									else
									{
										return this.Content("Longitude Range -180 s/d 180 => row nomor " + rowNum + " " + errInputFormat);
									}
								}
								else
								{
									return this.Content("Longitude Number Format => row nomor " + rowNum + " " + errInputFormat);
								}
							}
							#endregion
							if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
						listmodel.Add(model);
						rowNum++;
					}
					ImportModel.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					ImportModel.Input = listmodel;
					sb.Append("</table>");
                }
				//return this.Content(sb.ToString());
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					client.Timeout = Timeout.InfiniteTimeSpan;
					//HTTP POST
					var postTask = client.PostAsJsonAsync<ImportSiteInputModel>("ImportSite", ImportModel);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						return this.Content(sb.ToString());
					}
					else
					{
						return RedirectToAction("Index", "ExportImport");
					}
				}
			}
			return this.Content(sb.ToString());
        }

		[HttpGet]
		public async Task<IActionResult> Download()
		{
			string filename = "BillboardIndo_Site.xlsx";
			string folderName = "Upload\\Template\\";
			string folderroot = "wwwroot";
			string webRootPath = "";
			if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			}
			else
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
			}
			var path = Path.Combine(webRootPath, folderroot, folderName + filename);
			if (!System.IO.File.Exists(path))
				return NotFound();

			var memory = new MemoryStream();
			using (var stream = new FileStream(path, FileMode.Open))
			{
				await stream.CopyToAsync(memory);
			}
			memory.Position = 0;
			return File(memory, GetContentType(path), Path.GetFileName(path));
		}

		private string GetContentType(string path)
		{
			var types = GetMimeTypes();
			var ext = Path.GetExtension(path).ToLowerInvariant();
			return types[ext];
		}

		private Dictionary<string, string> GetMimeTypes()
		{
			return new Dictionary<string, string>
			{
				{".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
				{".csv", "text/csv"}
			};
		}

		[HttpPost]
        public async Task<IActionResult> OnPostExport()
        {
            string folderroot = "wwwroot";
            string sWebRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, folderroot, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Name");
                row.CreateCell(2).SetCellValue("Age");

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Kane Williamson");
                row.CreateCell(2).SetCellValue(29);

                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Martin Guptil");
                row.CreateCell(2).SetCellValue(33);

                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Colin Munro");
                row.CreateCell(2).SetCellValue(23);

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

		public IActionResult Image()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return View();
			}
			else
			{
				TempData["CustomError"] = "Please login before using the web.";
				if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				{
					return RedirectToAction("AdminLogon", "Login");
				}
				else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				{
					return RedirectToAction("OwnerLogon", "Login");
				}
				else
				{
					return RedirectToAction("BuyerLogon", "Login");
				}
				//return RedirectToAction("Logon", "Login");
			}
		}

		public ActionResult DropzoneImages()
		{
			bool isSavedSuccessfully = false;
			IFormFile file = Request.Form.Files[0];
			string folderName = "Upload\\Image";
			string folderroot = "wwwroot";
			string webRootPath = "";
			if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			}
			else
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
			}
			string newPath = Path.Combine(webRootPath, folderroot, folderName);
			if (!Directory.Exists(newPath))
			{
				Directory.CreateDirectory(newPath);
			}
			string fullPath = Path.Combine(newPath, file.FileName);
			//using (var stream = new FileStream(fullPath, FileMode.Create))
			//{
			//	file.CopyTo(stream);
			//}

			#region getSite
			var ID = Guid.Parse("8531496F-9009-48D6-9A97-7B25877C1D30");
			EditSite site = new EditSite();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				SiteDetailInputModel filter = new SiteDetailInputModel();
				filter.SiteID = ID;// Guid.Parse(ID);
				var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
					site.val = resutl.data;

					if (site.val != null)
					{
						#region UpdateSite
						string userID = HttpContext.Session.GetString(SessionKeyID);
						EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
						dataSite.UserID = Guid.Parse(userID);
						#region DataDetail
						EditSiteDetailInputModel Detail = new EditSiteDetailInputModel();
						Detail.SiteID = Guid.Parse(site.val.Detail.SiteID.ToString());
						Detail.NoBillboard = site.val.Detail.NoBillboard;
						Detail.Pulau = site.val.Detail.Pulau;
						Detail.Kota = site.val.Detail.Kota;
						Detail.Provinsi = site.val.Detail.Provinsi;
						Detail.Cabang = site.val.Detail.Cabang;
						Detail.HorV = site.val.Detail.HorV;
						Detail.Tipe = site.val.Detail.Tipe;
						Detail.Longitude = site.val.Detail.Longitude;
						Detail.Latitude = site.val.Detail.Latitude;
						#endregion
						dataSite.Detail = Detail;
						if (site.val.Item != null && site.val.Item.Count > 0)
						{
							foreach (SiteDetailItemModel Item in site.val.Item)
							{
								#region DataPrice
								if (Item.Price != null)
								{
									List<EditSitePriceInputModel> listdata = new List<EditSitePriceInputModel>();
									foreach (var p in Item.Price)
									{
										EditSitePriceInputModel data = new EditSitePriceInputModel();
										data.Dasar = p.Dasar;
										data.HargaAwal = p.HargaAwal;
										data.HargaAkhir = p.HargaAkhir;
										data.SitePriceID = Guid.Parse(p.SitePriceID.ToString());
										data.Kelipatan = p.Kelipatan;
										listdata.Add(data);
									}
									dataSite.Price = listdata;
								}
								#endregion
								#region DataImage
								if (Item.Image != null)
								{
									List<EditSiteImageInputModel> listdatas = new List<EditSiteImageInputModel>();
									foreach (var i in Item.Image)
									{
										EditSiteImageInputModel datas = new EditSiteImageInputModel();
										datas.SiteImageID = Guid.Parse(i.SiteImageID.ToString());
										datas.Image = i.Image;
										listdatas.Add(datas);
									}
									EditSiteImageInputModel data = new EditSiteImageInputModel();
									data.SiteImageID = Guid.NewGuid();
									data.Image = fullPath;
									listdatas.Add(data);
									dataSite.Image = listdatas;
								}
								else
								{
									List<EditSiteImageInputModel> listdata = new List<EditSiteImageInputModel>();
									EditSiteImageInputModel data = new EditSiteImageInputModel();
									data.SiteImageID = Guid.NewGuid();
									data.Image = fullPath;
									listdata.Add(data);
									dataSite.Image = listdata;
								}
								#endregion
							}
						}
						JsonConvert.SerializeObject(dataSite);
						using (var clients = new HttpClient())
						{
							clients.BaseAddress = new Uri(BaseAPI + "Site/");
							//HTTP POST
							var postTasks = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
							postTasks.Wait();

							var results = postTasks.Result;
							if (results.IsSuccessStatusCode)
							{
								using (var stream = new FileStream(fullPath, FileMode.Create))
								{
									file.CopyTo(stream);
								}
								//return RedirectToAction("Index", "Site");
							}
						}
						#endregion
					}

				}
				else //web api sent error response 
				{
					//log response status here..
					site.val = null;

					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			#endregion

			//foreach (string fileName in Request.Form.Files[0].ToString())
			//{
			//	HttpPostedFileBase file = Request.Files[fileName];

			//	//You can Save the file content here

			//	isSavedSuccessfully = true;
			//}
			return Json(new { Message = string.Empty });
		}

	}
}
