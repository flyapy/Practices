using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.MvvmLight.Models
{
    public class ProductInfo
    {
        public bool IsChecked { get; set; }
        public string ProductName { get; set; }
        public string ProductIcon { get; set; }
        public string ProductUrl { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }
    }
}
