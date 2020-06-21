using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetronicTop.Models
{
    public class JsonProvince
    {
        public Guid ProvinceID { get; set; }
        public string kode { get; set; }
        public string provinsi { get; set; }
    }

    public class JsonCity
    {
        public Guid CityID { get; set; }
        public string kode { get; set; }
        public string city { get; set; }
    }

    public class JsonUser
    {
        public Guid UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string CompanyName { get; set; }
		public string RoleName { get; set; }
		public Guid RoleID { get; set; }
		public bool IsActive { get; set; }
		public string Address { get; set; }
		public string NoTelp { get; set; }
		public string Website { get; set; }
		public string EmailPerusahaan { get; set; }
		public string Kategory { get; set; }
		public string NPWP { get; set; }
		public string Note { get; set; }
    }

    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}
