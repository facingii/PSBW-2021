using System;
using System.Collections.Generic;

#nullable disable

namespace EFFundamentals.Models
{
    public partial class Salary
    {
        public int EmpNo { get; set; }
        public int Salary1 { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; }
    }
}
