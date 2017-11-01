using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Practices.Serialization.Models;

namespace Practices.Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(new Models.LoginInfoVO { CompId = "220579605500023" });
            Print($"序列化结果：{Environment.NewLine}{s}");
            s = s.Replace("CompId", "compiD"); // 大小写都可以反序列化
            Print($"新的字符串，个别首字母小写：{Environment.NewLine}{s}");
            Models.LoginInfoVO loginInfoVO = null;
            loginInfoVO = JsonConvert.DeserializeObject<Models.LoginInfoVO>(s);
            Print($"反序列化验证：{Environment.NewLine}{loginInfoVO.CompId}"); // 大小写都可以反序列化


            Models.OrderSubmitBO orders = new Models.OrderSubmitBO();
            orders.RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Models.OrderSubmitItem[] orderArray = new Models.OrderSubmitItem[2];
            orderArray[0] = new Models.OrderSubmitItem { Id = "232", ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            orderArray[1] = new Models.OrderSubmitItem { Id = "233", ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };

            orders.GunId = "11111111111111";
            orders.Orders = orderArray;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;  // 不生成声明头  
            
            

            XmlSerializer xs = new XmlSerializer(typeof(Models.OrderSubmitBO));
            MemoryStream ms = new MemoryStream();
            // 强制指定命名空间，覆盖默认的命名空间  
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            string output;
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                xs.Serialize(writer, orders, namespaces);
                writer.Close();
                output = sb.ToString();
                //output = Encoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine("Look...");
                Console.WriteLine(output);
                Console.WriteLine(output);
            }

            Models.OrderSubmitBO ordersNew = (Models.OrderSubmitBO)xs.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(output)));

            Console.WriteLine(ordersNew.GunId);

            xs = new XmlSerializer(typeof(Models.OrderSubmitBO));

            Models.OrderSubmitBO orderSubmitBo = new Models.OrderSubmitBO();
            orderSubmitBo.Orders = null;
            OrderSubmitItem[] items = new OrderSubmitItem[2];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new OrderSubmitItem
                {
                    ScanTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Id = $"111110{i}"
                };
            }
            orderSubmitBo.Orders = items;
            orderSubmitBo.RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ms = new MemoryStream();
            xs.Serialize(ms, orderSubmitBo, namespaces);
            Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));

            Models.OrderSubmitResultBO orderSubmitResult = new OrderSubmitResultBO();
            orderSubmitResult.ErrorCode = "0";
            OrderSubmitResultItem[] orderSubmitResultItems = new OrderSubmitResultItem[2];
            for (int i = 0; i < orderSubmitResultItems.Length; i++)
            {
                orderSubmitResultItems[i] = new OrderSubmitResultItem
                {
                    ErrorCode = "2", Id = $"3233424{i}"
                };
            }
            orderSubmitResult.Orders = orderSubmitResultItems;
            ms = new MemoryStream();
            xs = new XmlSerializer(typeof(OrderSubmitResultBO));
            xs.Serialize(ms, orderSubmitResult, namespaces);
            Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
            Console.ReadKey();

            
        }

        private static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
