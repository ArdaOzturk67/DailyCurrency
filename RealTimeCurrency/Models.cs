namespace RealTimeCurrency
{
    public class RequestData
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsToday { get; set; }
    }

    public class ResponseDataCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SalesRate { get; set; }
        public decimal EfficientPurchaseRate { get; set; }
        public decimal EfficientSalesRate { get; set; }
    }

    public class ResponseData
    {
        public List<ResponseDataCurrency> Data { get; set; }
        public string Error { get; set; }
    }
}
