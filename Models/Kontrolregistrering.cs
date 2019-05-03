﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Kontrolregistrering
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime Klokkeslæt { get; set; }
        public DateTime Holdbarhedsdato { get; set; }
        public DateTime Produktionsdato { get; set; }
        public int FærdigvareNr { get; set; }
        public string Kommentar { get; set; }
        public bool Spritkontrol { get; set; }
        public int HætteNr { get; set; }
        public int EtiketNr { get; set; }
        public string Fustage { get; set; }
        public string Signatur { get; set; }

        public const string URI = "https://restapi20190501124159.azurewebsites.net/api/Kontrolregistrerings";

        public List<Kontrolregistrering> GetAll()
        {
            List<Kontrolregistrering> returnList = new List<Kontrolregistrering>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<Kontrolregistrering>>(jsonStr);
            }

            return returnList;
        }

        public Kontrolregistrering GetOne(int id)
        {
            Kontrolregistrering returnOne = new Kontrolregistrering();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<Kontrolregistrering>(jsonStr);
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

        public void Post(Kontrolregistrering obj)
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

        public void Put(int id, Kontrolregistrering obj)
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