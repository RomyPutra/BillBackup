using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
    public class LandingPageModel
    {
        public List<BillboardTypeListOutputModel> BillBoardType { get; set; }
        public List<ProvinceOutputModel> Province { get; set; }

    }
}
