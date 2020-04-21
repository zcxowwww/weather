using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace weather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-34D6D99B-512D-4EF9-94D4-1B18D0D49783";
            List<Weather> weather_list = GetWeather(path);
            return View(weather_list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static List<Weather> GetWeather(string path)
        {
            try
            {
                List<Weather> weather_list = new List<Weather>();
                HttpClient client = new HttpClient();
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
                requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(path).GetAwaiter().GetResult();

                if (response.StatusCode.ToString() == "OK")
                {
                    var json = response.Content.ReadAsStringAsync().Result.ToString();
                    var obj = JsonConvert.DeserializeObject<dynamic>(json);

                    foreach (var location in obj["records"]["location"])
                    {
                        Weather entity = new Weather();
                        entity.city_name = location["locationName"].ToString();         // city
                        foreach (var weather_element in location["weatherElement"])
                        {
                            if (weather_element["elementName"].ToString().ToLower() == "wx") {
                                entity.status_ar = new List<Weather.Status>();
                                // three times
                                foreach (var time in weather_element["time"])
                                {
                                    entity.status_ar.Add(new Weather.Status()
                                    {
                                        starttime = time["startTime"],
                                        endtime = time["endTime"],
                                        text = time["parameter"]["parameterName"]   // desc
                                    });
                                }
                            }
                            else if (weather_element["elementName"].ToString().ToLower() == "mint")
                            {
                                entity.temperature_ar = new List<Weather.Temperature>();
                                // three times
                                foreach (var time in weather_element["time"])
                                {
                                    entity.temperature_ar.Add(new Weather.Temperature()
                                    {
                                        starttime = time["startTime"],
                                        endtime = time["endTime"],
                                        text = time["parameter"]["parameterName"] + time["parameter"]["parameterUnit"]  // temperature
                                    });
                                }
                            }
                        }
                        weather_list.Add(entity);
                    }
                }
                else
                {
                    throw new Exception("error StatusCode:" + response.StatusCode.ToString());
                }

                return weather_list;
            }
            catch (Exception e)
            {
                return null;
            }


            //HttpClient client = new HttpClient();
            //dynamic product = null;
            //HttpResponseMessage response = client.GetAsync(path);
            //if (response.IsSuccessStatusCode)
            //{
            //    //product = await response.Content.ReadAsAsync();
            //    product = response.Content.ReadAsStringAsync();
            //}
            //return product;
        }
    }
}