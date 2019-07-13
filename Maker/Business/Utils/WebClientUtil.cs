using Maker.Business.Currency;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Utils
{
    public class WebClientUtil
    {
        public static void WebToModel<T>( String url,ref T obj) where T : class
        {
            StringBuilder sb = new StringBuilder();
            WebClient wclient = new WebClient();//实例化WebClient类对象 
            wclient.BaseAddress = url;//设置WebClient的基URI 
            wclient.Encoding = Encoding.UTF8;//指定下载字符串的编码方式 
                                             //为WebClient类对象添加标头 
            wclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //不加会导致请求被中止: 未能创建 SSL/TLS 安全通道
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //使用OpenRead方法获取指定网站的数据，并保存到Stream流中 
            Stream stream = wclient.OpenRead(url);
            //使用流Stream声明一个流读取变量sreader 
            StreamReader sreader = new StreamReader(stream);
            string str = string.Empty;//声明一个变量，用来保存一行从WebCliecnt下载的数据 
            while ((str = sreader.ReadLine()) != null)
            {
                sb.Append(str + Environment.NewLine);
            }
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sb.ToString())))
            {
                XmlSerializerBusiness.Load(ref obj, ms);
            }
        }
    }
}
