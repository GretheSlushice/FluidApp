﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Models
{
    public class KontrolSkema
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        private DateTime _klokkeslæt;
        private double? _ludkoncentration;
        public string Fustage { get; set; }
        public int? Kvittering { get; set; }
        private double? _mS;
        private bool? _ludKontrol;
        public string Signatur { get; set; }
        private bool? _mSKontrol;
        public double? Vægt { get; set; }
        public string FormattedTime { get; set; }
        public string FormattedLudkontrol { get; set; }
        public string FormattedmSkontrol { get; set; }
        public string LudColor { get; set; }
        public string MsColor { get; set; }

        public DateTime Klokkeslæt
        {
            get { return _klokkeslæt; }
            set
            {
                _klokkeslæt = value;
                FormattedTime = Klokkeslæt.TimeOfDay.ToString("hh\\:mm");
            }
        }

        public bool? LudKontrol
        {
            get { return _ludKontrol; }
            set
            {
                _ludKontrol = value;
                if (_ludKontrol == true) FormattedLudkontrol = "OK";
                else if (_ludKontrol == false) FormattedLudkontrol = "IKKE OK";
                else FormattedLudkontrol = "";
            }
        }

        public bool? MSKontrol
        {
            get { return _mSKontrol; }
            set
            {
                _mSKontrol = value;
                if (_mSKontrol == true) FormattedmSkontrol = "OK";
                else if (_mSKontrol == false) FormattedmSkontrol = "IKKE OK";
                else FormattedmSkontrol = "";
            }
        }

        public double? MS
        {
            get { return _mS; }
            set
            {
                _mS = value;
                if (_mS < 24 || _mS > 26.5) MsColor = "Red";
                else MsColor = "Black";
            }
        }

        public double? Ludkoncentration
        {
            get { return _ludkoncentration; }
            set
            {
                _ludkoncentration = value;
                if (_ludkoncentration < 1 || _ludkoncentration > 2) LudColor = "Red";
                else LudColor = "Black";
            }
        }

        public const string URI = "https://restapi20190611111326.azurewebsites.net/api/Kontrolskemas";

        public List<KontrolSkema> GetAll()
        {
            List<KontrolSkema> returnList = new List<KontrolSkema>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<KontrolSkema>>(jsonStr);
            }

            return returnList;
        }

        public KontrolSkema GetOne(int id)
        {
            KontrolSkema returnOne = new KontrolSkema();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<KontrolSkema>(jsonStr);
            }

            return returnOne;
        }

        public void Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public void Post(KontrolSkema obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public void Put(int id, KontrolSkema obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}