// See https://aka.ms/new-console-template for more information


/*
  ****NB *****
 You need to add nuget package Microsoft.AspNet.WebApi.Client
 This will give you the htttpContect ReadAsAsync fucntions below
  
*/

using RestAPI_02_StockPrice_Client.Models;
using System.Net.Http.Headers;



try
{
    // 1)  create an instance of HttpClient
    using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
    {
        // 2)  init the base address of the Webservice we are calling
        client.BaseAddress = new Uri("https://localhost:7117/");                             // base URL for API Controller i.e. RESTFul service

        // 3) Set the media types this client will accept (in this case, for a webservice,  JSON)
        client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            // or application/xml

        //4) Call the Webapi using a get Http request(we pass in the URL (without base address part as we already set that) 
        // GET ../api/stock
        // get all stock listings
        HttpResponseMessage response = await client.GetAsync("api/stock");                  // async call, await suspends until task finished            

        //5) we can then check the Http response we get back to see if it succeeded
        if (response.IsSuccessStatusCode)                                                   // 200.299
        {
            // read result 
            var listings = await response.Content.ReadAsAsync<IEnumerable<StockListing>>();
            foreach (var listing in listings)
            {
                Console.WriteLine(listing.TickerSymbol + " " + listing.Price);
            }
        }
        else
        {
            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
        }


        // GET ../api/stock/IBM
        // get stock price for IBM
        try
        {
            response = await client.GetAsync("api/stock/IBM");

            // or
            //Task <HttpResponseMessage> result = client.GetAsync("api/stock/IBM");
            // do indepedent work in the meantime...
            //response = await result;

            response.EnsureSuccessStatusCode();                         // throw exception if not success
            var price = await response.Content.ReadAsAsync<double>();         
            Console.WriteLine(price);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}