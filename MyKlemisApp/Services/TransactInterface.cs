using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace MyKlemisApp.Services
{
    class TransactInterface
    {
        private static HttpClient client = null;
        private static String portalURL        = "https://qpc.transactcampus.com/?tenant=gatech";
        private static String authenticateURL  = "https://qpc.transactcampus.com/QPWebOffice-Web-AuthenticationService.svc/JSON/Authenticate";
        private static String loggedInURL      = "https://qpc.transactcampus.com/QPWebOffice-Web-AuthenticationService.svc/JSON/LoggedIn";
        private static String itemsMainURL     = "https://qpc.transactcampus.com/QPWebOffice-Web-QuadPointDomain.svc/JSON/GetItemsWithInventoryMain";
        private static String authToken = null;
        private static String clientVersion = null;
        private static bool isInit = false;

        private static async Task authenticateTask()
        {
            try
            {
                //get portal site for client version
                var portalReq = new HttpRequestMessage()
                {
                    RequestUri = new Uri(portalURL),
                    Method = HttpMethod.Get,
                };

                var portalResp = await client.SendAsync(portalReq).ConfigureAwait(false);
                if (portalResp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var portalRespString = await portalResp.Content.ReadAsStringAsync();
                    Regex vRx = new Regex("version=\\d+\\.\\d+\\.\\d+\\.\\d+");
                    MatchCollection matches = vRx.Matches(portalRespString);
                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        clientVersion = groups[0].Value.Substring(8, groups[0].Value.Length - 8);
                        if(clientVersion != null)
                        {
                            break;
                        }
                    }
                }

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

                StringContent authContent = new StringContent("{\"isPersistent\":true,\"customData\":\"\",\"clientVersion\":\"" + clientVersion + "\",\"dotNetLogicVer\":1,\"userName\":\"klemisrpt\",\"password\":\"Yell0w!\",\"reset\":\" * **\",\"id\":\" * **\"}");
                authContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                authReq.Content = authContent;

                var authResp = await client.SendAsync(authReq).ConfigureAwait(false);
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

        private static async Task getInventoryItems()
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
                var itemsResp = await client.SendAsync(itemsReq).ConfigureAwait(false);
                if (itemsResp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    String itemsRespString = await itemsResp.Content.ReadAsStringAsync();
                    JsonSerializer json = new JsonSerializer();
                    json.NullValueHandling = NullValueHandling.Ignore;
                    res = json.Deserialize<Models.InventoryRootObj>(new JsonTextReader(new StringReader(itemsRespString)));
                    //res = JsonConvert.DeserializeObject<Models.InventoryRootObj>(itemsRespString);
                    
                    Models.InventoryCache.setItems(res.GetItemsWithInventoryMainResult.RootResults);
                }
                int i = 0;

            } catch(Exception e)
            {
                throw e;
            }
        }

        public static async void initialize()
        {
            if (isInit == false)
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
                await authenticateTask();
                await getInventoryItems();
                isInit = true;
            }
        }


        public static bool isInitialized()
        {
            return isInit;
        }

        public static bool isAuthorized()
        {
            return authToken != null;
        }
    }
}
