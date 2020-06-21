using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{

	public class ForMaps
	{
		public List<City> Cities { get; set; }
		public List<BillboardDetail> BillBoard { get; set; }
	}

	public class City
	{
		public string Title { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public string Kota { get; set; }
		public string Prov { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public double Rate { get; set; }
		public string Price { get; set; }
		public string Image { get; set; }
	}

	public class BillboardDetail
	{
		public string SiteID { get; set; }
		public string SiteItemID { get; set; }
		public string SitePriceID { get; set; }
		public string NoBillBoard { get; set; }
		public double RateScoreAvg { get; set; }
		public string Kota { get; set; }
		public string Cab { get; set; }
		public string HorV { get; set; }
		public string Tipe { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public double Pjg { get; set; }
		public double Lbr { get; set; }
		public double Hawl { get; set; }
		public double Hahr { get; set; }
		public string Img { get; set; }
		public string Alamat { get; set; }
	}
}
