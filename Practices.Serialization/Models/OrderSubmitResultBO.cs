using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Practices.Serialization.Models
{

    [XmlRoot("response")]
    public class OrderSubmitResultBO
    {
        [XmlElement("error_code")]
        public string ErrorCode { get; set; }

        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public OrderSubmitResultItem[] Orders { get; set; }
        
    }

    [XmlRoot("order")]
    public class OrderSubmitResultItem
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("error_code")]
        public string ErrorCode { get; set; }
    }
}
