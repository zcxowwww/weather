using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using weather;
using weather.Controllers;

namespace weather.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetWeatherTest()
        {
            HomeController controller = new HomeController();
            string path = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-34D6D99B-512D-4EF9-94D4-1B18D0D49783";
            var result = controller.GetWeather(path);

            // Assert
            Assert.IsNotNull(result);
        }

     
    }
}
