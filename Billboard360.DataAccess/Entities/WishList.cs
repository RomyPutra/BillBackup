using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class WishList: Base
    {
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public Guid UserID { get; set; }

        public virtual TitikLokasiDetail SiteItem { get; set; }
        public virtual User User { get; set; }

    }
}
