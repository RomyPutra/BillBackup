using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Base
    {
        public virtual Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateByUserID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateByUserID { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedByUserID { get; set; }
        
    }
}
