using System.Collections.Generic;
public class Weather {
    public string city_name;
    public List<Status> status_ar = null;
    public List<Temperature> temperature_ar = null;

    public class Status {
        public string starttime;
        public string endtime;
        public string text;
    }
    public class Temperature
    {
        public string starttime;
        public string endtime;
        public string text;
    }
}