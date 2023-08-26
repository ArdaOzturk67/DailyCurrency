using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace RealTimeCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCurrencyController : ControllerBase
    {
        [HttpPost]
        public ResponseData Run(RequestData request)
        {
            ResponseData result = new();

            try
            {
                string tcmblink = string.Format("http://www.tcmb.gov.tr/kurlar/{0}.xml",
                    (request.IsToday) ? "today" : string.Format("{2}{1}/{0}{1}{2}"),
                    request.Day.ToString().PadLeft(2, '0'), request.Month.ToString().PadLeft(2, '0'), request.Year
                );

                result.Data = new List<ResponseDataCurrency>();

                XmlDocument doc = new();
                doc.Load(tcmblink);

                if(doc.SelectNodes("Tarih_Date").Count < 1)
                {
                    result.Error = "Kur bilgisi bulunamadı";
                    return result;
                }

                foreach(XmlNode node in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    ResponseDataCurrency currency = new();

                    currency.Code = node.Attributes["Kod"].Value;
                    currency.Name = node["Isim"].InnerText;
                    currency.Unit = int.Parse(node["Unit"].InnerText);
                    currency.PurchaseRate = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".",","));
                    currency.SalesRate = Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".",","));
                    currency.EfficientPurchaseRate = Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".",","));
                    currency.EfficientSalesRate = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".",","));

                    result.Data.Add(currency);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}