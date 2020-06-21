using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class TitikLokasiImage: Base
    {
        public Guid TitikLokasiID { get; set; }
        public Guid TitikLokasiDetailID { get; set; }
        public string PathImage { get; set; }
        public string IsThumbnail { get; set; }

        public virtual TitikLokasiDetail TitikLokasiDetail { get; set; }
    }
}
