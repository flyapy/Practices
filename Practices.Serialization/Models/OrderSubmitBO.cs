using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Practices.Serialization.Models
{
    [XmlRoot(ElementName = "orders")]
    public class OrderSubmitBO
    {
        private DateTime requestTime;

        [XmlElement(ElementName = "request_time")]
        public string RequestTime
        {
            get
            {
                return requestTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                requestTime = DateTime.Parse(value);
            }
        }


        [XmlElement(ElementName = "gun_id")]
        public string GunId { get; set; }

        [XmlElement(ElementName = "order")]
        public OrderSubmitItem[] Orders { get; set; }

    }

    public class OrderSubmitItem
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "doc_id")]
        public string DocId { get; set; }

        [XmlElement(ElementName = "scan_site")]
        public string ScanSite { get; set; }


        private DateTime scanTime;

        [XmlElement(ElementName = "scan_time")]
        public string ScanTime
        {
            get
            {
                return scanTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                scanTime = DateTime.Parse(value);
            }
        }

        [XmlElement(ElementName = "scan_man")]
        public string ScanMan { get; set; }

        [XmlElement(ElementName = "obj_wei")]
        public string ObjWei { get; set; }

        
    }
}
