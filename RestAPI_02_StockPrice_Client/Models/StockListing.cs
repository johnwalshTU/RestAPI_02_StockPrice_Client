

namespace RestAPI_02_StockPrice_Client.Models
{
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        public string TickerSymbol { get; set; } = "";

        // price last trade in $
        public double Price { get; set; }


    }
}
