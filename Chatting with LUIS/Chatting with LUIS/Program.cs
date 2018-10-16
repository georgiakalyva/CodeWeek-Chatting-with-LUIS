using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chatting_with_LUIS
{
    
        class Program
        {
            static void Main(string[] args)
            {
            Console.WriteLine("Hello this is an automating system. How can I help you?");

                MakeRequest(Console.ReadLine());
                Console.WriteLine("Calculating...");
                Console.ReadLine();
            }

            static async void MakeRequest(string Input)
            {
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                // This app ID is for a public sample app that recognizes requests to turn on and turn off lights
                var luisAppId = "AddyourID";
                var endpointKey = "AddyourID";

                // The request header contains your subscription key
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointKey);

                // The "q" parameter contains the utterance to send to LUIS
                queryString["q"] = Input;

                // These optional request parameters are set to their default values
                queryString["timezoneOffset"] = "0";
                queryString["verbose"] = "false";
                queryString["spellCheck"] = "false";
                queryString["staging"] = "false";

                var endpointUri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
                var response = await client.GetAsync(endpointUri);

                var strResponseContent = await response.Content.ReadAsStringAsync();

                Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(strResponseContent);

                switch (result.topScoringIntent.intent)
                {
                    case "ShowHotelsReviews":
                        Console.WriteLine("I see you are looking for hotel reviews? Visit hotelreviews.com or call at +00123457989 for more");
                        break;
                    case "SearchHotels":
                        Console.WriteLine("For bookings a call center employee will contact you. Leave your details here.");
                        break;
                    case "Help":
                        Console.WriteLine("Type what you need for example \"I am looking for hotels near Athens\"");
                        break;
                    default:
                        Console.WriteLine("Oups! Something went wrong. Cannt find what you are looking for? Type what you need for example \"I am looking for hotels near Athens\"");
                        break;
            }
            
            }
        }
    }

