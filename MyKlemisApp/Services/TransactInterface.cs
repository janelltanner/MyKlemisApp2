using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace MyKlemisApp.Services
{
    class TransactInterface
    {
        private static HttpClient client = null;
        private String authenticateURL  = "https://qpc.transactcampus.com/QPWebOffice-Web-AuthenticationService.svc/JSON/Authenticate";
        private String loggedInURL      = "https://qpc.transactcampus.com/QPWebOffice-Web-AuthenticationService.svc/JSON/LoggedIn";
        private String itemsMainURL     = "https://qpc.transactcampus.com/QPWebOffice-Web-QuadPointDomain.svc/JSON/GetItemsWithInventoryMain";
        private String authToken = null;

        public TransactInterface()
        {
            if (client == null)
            {
                var handler = new HttpClientHandler
                {
                    CookieContainer = new CookieContainer(),
                    UseCookies = true,
                    UseDefaultCredentials = false
                };

                client = new HttpClient(handler);
            }
        }

        public async Task authenticate()
        {
            Task t = new Task(() => { authenticateTask(); });
        }

        public async void authenticateTask()
        {
            try
            {
                var logInReq = new HttpRequestMessage()
                {
                    RequestUri = new Uri(loggedInURL),
                    Method = HttpMethod.Post,

                };
                logInReq.Headers.Add("Referer",  "https://qpc.transactcampus.com/?tenant=gatech");
                logInReq.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
                logInReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                logInReq.Headers.Add("Accept", "*/*");
/*                HttpContent logInContent = new StringContent("");
                logInContent.Headers.ContentType = new MediaTypeHeaderValue("application/json"); 
                logInReq.Content = logInContent;*/

                

                var logResp = await client.SendAsync(logInReq).ConfigureAwait(false);
                if(logResp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var logRespString = await logResp.Content.ReadAsStringAsync();
                }

                
                /*
                 {"isPersistent":true,"customData":"","clientVersion":"6.0.12.35","dotNetLogicVer":1,"userName":"klemisrpt","password":"Yell0w!","reset":"***","id":"***"}
                 */
                ////////////////////

                var authReq = new HttpRequestMessage()
                {
                    RequestUri = new Uri(authenticateURL),
                    Method = HttpMethod.Post,
                };
                authReq.Headers.Add("Referer", "https://qpc.transactcampus.com/?tenant=gatech");
                authReq.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
                authReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                authReq.Headers.Add("Accept", "*/*");
                authReq.Headers.Add("Accept-Language", "en-US, en; q = 0.9,fr; q = 0.8");
                authReq.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                /*HttpContent authContent = new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("isPersistent", "true"),
                        new KeyValuePair<string, string>("customData", ""),
                        new KeyValuePair<string, string>("clientVersion", "6.0.12.35"),
                        new KeyValuePair<string, string>("dotNetLogicVer", "1"),
                        new KeyValuePair<string, string>("userName", "klemisrpt"),
                        new KeyValuePair<string, string>("password","Yell0w!"),
                        new KeyValuePair<string, string>("reset","***"),
                        new KeyValuePair<string, string>("id","***"),
                    });*/

                StringContent authContent = new StringContent("{\"isPersistent\":true,\"customData\":\"\",\"clientVersion\":\"6.0.12.35\",\"dotNetLogicVer\":1,\"userName\":\"klemisrpt\",\"password\":\"Yell0w!\",\"reset\":\" * **\",\"id\":\" * **\"}");
                authContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                authReq.Content = authContent;

                var authResp = await client.SendAsync(authReq);
                if(authResp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var authRespString = await authResp.Content.ReadAsStringAsync();
                    IEnumerator<string> headers = authResp.Headers.GetValues("Authorization").GetEnumerator();
                    headers.MoveNext();
                    authToken = headers.Current;
                }
            } catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Models.Item>> GetInventoryItems()
        {
            try
            {
                var itemsReq = new HttpRequestMessage()
                {
                    RequestUri = new Uri(itemsMainURL),
                    Method = HttpMethod.Get,
                };
                itemsReq.Headers.Add("Authorization", authToken);
                itemsReq.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
                itemsReq.Headers.Add("X-Requested-With", "XMLHttpRequest");
                itemsReq.Headers.Add("Accept", "*/*");

                Models.InventoryRootObj res = null;
                var itemsResp = await client.SendAsync(itemsReq);
                if (itemsResp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    String itemsRespString = await itemsResp.Content.ReadAsStringAsync();
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    res = json.Deserialize<Models.InventoryRootObj>(new JsonTextReader(new StringReader(itemsRespString)));
                    //res = JsonConvert.DeserializeObject<Models.InventoryRootObj>(itemsRespString);
                    return res.GetItemsWithInventoryMainResult.RootResults;

                }

                return null;
            } catch(Exception e)
            {
                throw e;
            }
        }

        public bool isAuthorized()
        {
            return authToken != null;
        }
    }
}
