using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;

namespace SimpleBraintreeGraphQLClient
{ 
    class Program
    {
        static async Task Main(string[] args)
        {
            AppSetting settings = new AppSetting();
            try
            {
                string publicKey = settings.PublickKey;
                string privateKey = settings.PrivateKey;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{publicKey}:{privateKey}");

                var key = System.Convert.ToBase64String(plainTextBytes);

                Console.Write("Enter QUERY :");
                var input = Console.ReadLine();

                BraintreeQuery pingQuery = new BraintreeQuery { query = input };
                var pingQueryString = JsonSerializer.Serialize(pingQuery);
                Console.WriteLine($"QUERY -> {pingQueryString}");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(settings.Url);
                request.Method = "POST";
                request.Headers.Add("Authorization", $"bearer {key}");
                request.Headers.Add("Braintree-Version", "2020-03-25");

                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(pingQueryString.Trim());

                request.ContentLength = byteArray.Length;
                request.ContentType = @"application/json";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                long length = 0;
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        length = response.ContentLength;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            var json = reader.ReadToEnd();

                            Console.WriteLine($"RESPONSE -> {json}");
                        }
                    }
                }
                catch (WebException ex)
                {
                    WebResponse errorResponse = ex.Response;
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        Console.WriteLine($"ERROR -> {errorText}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR -> Unexpected error : {ex}");
            }
        
            Console.ReadKey();
        }
    }
}
