using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using tgtgBot.Models;

namespace tgtgBot.Api
{
    public class Auth
    {
        public static async Task<String> GetAccessToken(User tgtgUser)
        {
            string loginUrl = "https://apptoogoodtogo.com/api/auth/v1/loginByEmail";
            string access_token = string.Empty;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "tgtgBot/0.01");
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", "nl");
                    tgtgUser.device_type = "IOS";

                    string userJson = JsonConvert.SerializeObject(tgtgUser);
                    StringContent postData = new StringContent(userJson, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage result = await httpClient.PostAsync(loginUrl, postData))
                    {
                        using (HttpContent content = result.Content)
                        {
                            string responseData = await content.ReadAsStringAsync();

                            if (responseData != null){
                                // Get oAuth access_token from response JSON
                                access_token = JObject.Parse(responseData)["access_token"].ToString();
                                tgtgUser.access_token = access_token;
                                
                                // Get user_id and oAuth refresh_token 
                                tgtgUser.refresh_token = JObject.Parse(responseData)["refresh_token"].ToString();
                                tgtgUser.user_id = JObject.Parse(responseData)["startup_data"]["user"]["user_id"].ToString();
                            }
                            else
                            {
                                // No data received
                                access_token = string.Empty;
                            }
                        }
                    }
                }
                return access_token;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static async Task<String> GetItems(User tgtgUser, String latitude, String longitude, String radius)
        {
            string itemsUrl = "https://apptoogoodtogo.com/api/item/v4/";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "tgtgBot/0.01");
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", "nl");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tgtgUser.access_token);

                    GetItems getItems = new GetItems()
                    {
                        origin = new Origin(){latitude = latitude, longitude = longitude},
                        radius = radius,
                        user_id = tgtgUser.user_id
                    };

                    string getItemsJson = JsonConvert.SerializeObject(getItems);
                    StringContent postData = new StringContent(getItemsJson, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage result = await httpClient.PostAsync(itemsUrl, postData))
                    {
                        using (HttpContent content = result.Content)
                        {
                            string responseData = await content.ReadAsStringAsync();

                            if (responseData != null){
                                // Return response JSON
                                return responseData;
                            }
                            else
                            {
                                // No data received
                                return string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}