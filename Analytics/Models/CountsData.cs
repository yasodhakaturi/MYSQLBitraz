using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Analytics.Models
{
    public class CountsData
    {
        //public List<YearData> YearsData { get; set; }
        //public List<MonthData> MonthsData { get; set; }
        //public List<CurrentMonthData> CurrentMonthData { get; set; }
        //public List<TodayData> TodaysData { get; set; }
        //public List<YesterDayData> YesterdaysData { get; set; }
        //public List<Last7DaysData> Last7DaysData { get; set; }
        //public List<BrowsersData> BrowsersData { get; set; }
        //public List<CountryData> CountriesData { get; set; }
        //public List<CityData> CitiesData { get; set; }
        //public List<RegionData> RegionsData { get; set; }
        //public List<DeviceTypeData> DevicesData { get; set; }

        public List<DayWiseData> activity { get; set; }
        public List<CountryWiseData> locations { get; set; }
        public List<DeviceWiseData> devices { get; set; }
        public List<BrowserWiseData> platforms { get; set; }


    }
}