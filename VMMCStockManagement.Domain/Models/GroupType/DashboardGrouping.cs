using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.GroupType
{
    public class DashboardGrouping
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Total { get; set; }

    }
}
