using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickBondBlazor.Data
{
    public class BondApi
    {
        HttpClient _httpclient;
        string _ip;
        public BondApi()
        {
            _httpclient = new HttpClient();
           
        }

        public void RegisterBondIp(string Ipaddress)
        {
            _ip = Ipaddress;
            string basetemplate = "http://{ipaddress}/v2/";
            string newadd = basetemplate.Replace("{ipaddress}", _ip);
            _httpclient.BaseAddress = new Uri(newadd);

        }
        public async Task<string> CheckBondVersion()
        {

            var response = await _httpclient.GetAsync("sys/version").ConfigureAwait(false);
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        public async Task<string> GetToken()
        {

            var response = await _httpclient.GetAsync("token").ConfigureAwait(false);
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        public async Task<string> GetDevices()
        {

            var response = await _httpclient.GetAsync("devices").ConfigureAwait(false);
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        public async Task<string> GetDevice(string deviceid)
        {

            var response = await _httpclient.GetAsync("devices/" + deviceid).ConfigureAwait(false);
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        public async Task<JObject> StartScan(int freq, string modulation)
        {
            var signalscan = new SignalScan { freq = freq, modulation = modulation };
            var signalstring = JsonConvert.SerializeObject(signalscan);
            HttpContent content = new StringContent(signalstring);
            var response = await _httpclient.PutAsync("signal/scan", content);
            string contentresult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            JObject o = JObject.Parse(contentresult);

            return o;
        }
    }
}
