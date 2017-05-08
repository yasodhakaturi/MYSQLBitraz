using Analytics.Helpers.Utility;
using Analytics.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
//using System.Data.Objects;
using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Data.Sqlclient;
using System.Linq;
using System.Web;

namespace Analytics.Helpers.BO
{
    public class DataInsertionBO
    {
        shortenurlEntities dc = new shortenurlEntities();
        MySqlConnection lSQLConn = null;
        MySqlCommand lSQLCmd = new MySqlCommand();
        string connStr = "";
        public void Insertuiddata(int fk_rid, int? FK_ClientID, string referencenumber, string longurl, string mobilenumber)
        {
            connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            try
            {
                //// create and open a connection object
                //lSQLConn = new MySqlConnection(connStr);
                //lSQLConn.Open();
                //lSQLCmd.CommandType = CommandType.StoredProcedure;
                //lSQLCmd.CommandText = "Insertuiddata";
                //lSQLCmd.Parameters.Add(new MySqlParameter("@fk_rid", fk_rid));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@FK_ClientID", FK_ClientID));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@referencenumber", referencenumber));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@longurl", longurl));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@mobilenumber", mobilenumber));

                //lSQLCmd.Connection = lSQLConn;
                //lSQLCmd.ExecuteNonQuery();
                string utcdatetime = Helper.GetUTCTime();
                DateTime? utcdt = Convert.ToDateTime(utcdatetime);
                uiddata obju = new uiddata();
                obju.FK_RID = fk_rid;
                obju.FK_ClientID = FK_ClientID;
                obju.ReferenceNumber = referencenumber;
                obju.LongurlorMessage = longurl;
                obju.MobileNumber = mobilenumber;
                obju.CreatedDate = utcdt;
                obju.CreatedBy = FK_ClientID;
                dc.uiddatas.Add(obju);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {

                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
            }
            //finally
            //{
            //    lSQLCmd.Dispose();
            //    lSQLConn.Close();
            //}
        }
        public void Insertriddata(string CampaignName, string referencenumber, string pwd,int clientid)
        {
            //MySqlConnection lSQLConn = null;
            //MySqlCommand lSQLCmd = new MySqlCommand();
            //string connStr = "";
            //connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            try
            {
                //// create and open a connection object
                //lSQLConn = new MySqlConnection(connStr);
                //lSQLConn.Open();
                //lSQLCmd.CommandType = CommandType.StoredProcedure;
                //lSQLCmd.CommandText = "Insertriddata";
                //lSQLCmd.Parameters.Add(new MySqlParameter("@CampaignName", CampaignName));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@referencenumber", referencenumber));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@pwd", pwd));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@clientid", clientid));

                //lSQLCmd.Connection = lSQLConn;
                //lSQLCmd.ExecuteNonQuery();
                string utcdatetime = Helper.GetUTCTime();
                DateTime? utcdt = Convert.ToDateTime(utcdatetime);
                riddata objr = new riddata();
                objr.CampaignName = CampaignName;
                objr.ReferenceNumber = referencenumber;
                objr.Pwd = pwd;
                objr.FK_ClientId = clientid;
                objr.IsActive = true;
                objr.CreatedDate = utcdt;
                objr.CreatedBy = clientid;
                dc.riddatas.Add(objr);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);

            }
            //finally
            //{
            //    lSQLCmd.Dispose();
            //    lSQLConn.Close();
            //}
        }
        public void InsertShortUrldata(string ipv4, string ipv6, string browser, string browser_version, string city, string Region, string country, string countrycode, string req_url, string useragent, string hostname, string devicetype, string ismobiledevice,int? fk_uid,int? fk_rid,int? FK_ClientID)
        {
            MySqlConnection lSQLConn = null;
            MySqlCommand lSQLCmd = new MySqlCommand();
            string connStr = "";
            connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            try
            {
                // create and open a connection object
                lSQLConn = new MySqlConnection(connStr);
                lSQLConn.Open();
                lSQLCmd.CommandType = CommandType.StoredProcedure;
                lSQLCmd.CommandText = "InsertSHORTURLData";
                lSQLCmd.Parameters.Add(new MySqlParameter("@ipv4", ipv4));
                lSQLCmd.Parameters.Add(new MySqlParameter("@ipv6", ipv6));
                lSQLCmd.Parameters.Add(new MySqlParameter("@browser", browser));
                lSQLCmd.Parameters.Add(new MySqlParameter("@browser_version", browser_version));
                lSQLCmd.Parameters.Add(new MySqlParameter("@city", city));
                lSQLCmd.Parameters.Add(new MySqlParameter("@Region", Region));
                lSQLCmd.Parameters.Add(new MySqlParameter("@country", country));
                lSQLCmd.Parameters.Add(new MySqlParameter("@countrycode", countrycode));
                lSQLCmd.Parameters.Add(new MySqlParameter("@req_url", req_url));
                lSQLCmd.Parameters.Add(new MySqlParameter("@useragent", useragent));
                lSQLCmd.Parameters.Add(new MySqlParameter("@hostname", hostname));
                lSQLCmd.Parameters.Add(new MySqlParameter("@DeviceType", devicetype));
                lSQLCmd.Parameters.Add(new MySqlParameter("@IsMobiledevice", ismobiledevice));
                lSQLCmd.Parameters.Add(new MySqlParameter("@fk_uid", fk_uid));
                lSQLCmd.Parameters.Add(new MySqlParameter("@fk_rid", fk_rid));
                lSQLCmd.Parameters.Add(new MySqlParameter("@FK_ClientID", FK_ClientID));
                //lSQLCmd.Parameters.Add(new MySqlParameter("@uniqueid", uniqueid));
                lSQLCmd.Connection = lSQLConn;
                lSQLCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);

            }
            finally
            {
                lSQLCmd.Dispose();
                lSQLConn.Close();
            }
        }

        public CountsData GetALLCOUNTS(string Fk_Uniqueid, string DateFrom, string DateTo)
        {
            CountsData countobj = new CountsData();

            connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            try
            {

                // create and open a connection object
                lSQLConn = new MySqlConnection(connStr);
                MySqlDataReader myReader ;
                lSQLConn.Open();
                lSQLCmd.CommandType = CommandType.StoredProcedure;
                lSQLCmd.CommandText = "spGetALLCOUNTS1";
                lSQLCmd.Parameters.Add(new MySqlParameter("@Fk_Uniqueid", Fk_Uniqueid));
                lSQLCmd.Parameters.Add(new MySqlParameter("@DateFrom", DateFrom));
                lSQLCmd.Parameters.Add(new MySqlParameter("@DateTo", DateTo));
                lSQLCmd.Connection = lSQLConn;
                myReader = lSQLCmd.ExecuteReader();

                List<DayWiseData> activity = ((IObjectContextAdapter)dc)
               .ObjectContext
               .Translate<DayWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                // Move to locations result 
                myReader.NextResult();
                List<CountryWiseData> locations = ((IObjectContextAdapter)dc)
               .ObjectContext
               .Translate<CountryWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                // Move to devices result 
                myReader.NextResult();
                List<DeviceWiseData> devices = ((IObjectContextAdapter)dc)
              .ObjectContext
              .Translate<DeviceWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                // Move to platforms result 
                myReader.NextResult();
                List<BrowserWiseData> platforms = ((IObjectContextAdapter)dc)
              .ObjectContext
              .Translate<BrowserWiseData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                countobj.activity = activity;
                countobj.locations = locations;
                countobj.devices = devices;
                countobj.platforms = platforms;
                return countobj;

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return countobj;
            }
            finally
            {
                lSQLCmd.Dispose();
                lSQLConn.Close();
            }
        }
            

         public CountsData GetALLCOUNTS1(string FilterBy,string DateFrom, string DateTo)
        {
            CountsData countobj = new CountsData();

            connStr = ConfigurationManager.ConnectionStrings["shortenURLConnectionString"].ConnectionString;
            try
            {

                // create and open a connection object
                lSQLConn = new MySqlConnection(connStr);
                MySqlDataReader myReader ;
                lSQLConn.Open();
                lSQLCmd.CommandType = CommandType.StoredProcedure;
                lSQLCmd.CommandText = "spGetALLCOUNTS";
                lSQLCmd.Parameters.Add(new MySqlParameter("@FilterBy", FilterBy));
                lSQLCmd.Parameters.Add(new MySqlParameter("@DateFrom", DateFrom));
                lSQLCmd.Parameters.Add(new MySqlParameter("@DateTo", DateTo));
                lSQLCmd.Connection = lSQLConn;
                myReader = lSQLCmd.ExecuteReader();

                //List<YearData> YearsDataObj = ((IObjectContextAdapter)dc)
                //.ObjectContext
                //.Translate<YearData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


                //// Move to Month result 
                //myReader.NextResult();
                //List<MonthData> MonthDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<MonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to CurrentMonth result 
                //myReader.NextResult();
                //List<CurrentMonthData> CurrentMonthDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<CurrentMonthData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to TodayData result 
                //myReader.NextResult();
                //List<TodayData> TodayDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<TodayData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to YesterDayData result 
                //myReader.NextResult();
                //List<YesterDayData> YesterDayDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<YesterDayData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to Last7DaysData result 
                //myReader.NextResult();
                //List<Last7DaysData> Last7DaysDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<Last7DaysData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to BrowsersData result 
                //myReader.NextResult();
                //List<BrowsersData> BrowsersDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<BrowsersData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to CountryData result 
                //myReader.NextResult();
                //List<CountryData> CountryDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<CountryData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to CityData result 
                //myReader.NextResult();
                //List<CityData> CityDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<CityData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to RegionData result 
                //myReader.NextResult();
                //List<RegionData> RegionDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<RegionData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();

                //// Move to DeviceTypeData result 
                //myReader.NextResult();
                //List<DeviceTypeData> DeviceTypeDataObj = ((IObjectContextAdapter)dc)
                //    .ObjectContext
                //    .Translate<DeviceTypeData>(myReader, "SHORTURLDATAs", MergeOption.AppendOnly).ToList();


                //countobj.YearsData = YearsDataObj;
                //countobj.MonthsData = MonthDataObj;
                //countobj.CurrentMonthData = CurrentMonthDataObj;
                //countobj.TodaysData = TodayDataObj;
                //countobj.YesterdaysData = YesterDayDataObj;
                //countobj.Last7DaysData = Last7DaysDataObj;
                //countobj.BrowsersData = BrowsersDataObj;
                //countobj.CountriesData = CountryDataObj;
                //countobj.CitiesData = CityDataObj;
                //countobj.RegionsData = RegionDataObj;
                //countobj.DevicesData = DeviceTypeDataObj;
                return countobj;

            }
            catch (Exception ex)
            {
                ErrorLogs.LogErrorData(ex.StackTrace, ex.Message);
                return countobj;
            }
            finally
            {
                lSQLCmd.Dispose();
                lSQLConn.Close();
            }
        }
    }
}