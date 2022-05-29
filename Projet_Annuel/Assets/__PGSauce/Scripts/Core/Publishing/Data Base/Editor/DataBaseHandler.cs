using System.Collections.Generic;

#if UNITY_EDITOR
using System.Net.Http;
using Newtonsoft.Json;
#endif

using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.Publishing.Data_Base
{
    public static class DataBaseHandler
    {
        private static string _endPoint = "https://hidden-brushlands-15865.herokuapp.com/";

#if UNITY_EDITOR
        private struct GameData
        {
            public string game_name { get; set; }
            public string bundle_id { get; set; }
            public string version { get; set; }
            public int android_bundle_code { get; set; }
        }
        
        public static void SetAndroidGameData(PGSettings pgSettings, AndroidPublishSettings settings)
        {
            var data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("bundle_id", settings.BundleId),
                new KeyValuePair<string, string>("game_name", pgSettings.GameName),
                new KeyValuePair<string, string>("version", settings.Version.CurrentVersion)
            };

            var client = new HttpClient();
            var content = new FormUrlEncodedContent(data);
            var response = client.PostAsync($"{_endPoint}games/android/gamedata/set", content);
            var result = response.Result;
            var responseString = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                PGDebug.Message($"ANDROID Set Game Data {responseString}").Log();
            }
            else
            {
                PGDebug.Message($"ANDROID Error updating the game {responseString}").LogError();
            }
        }
        
        public static int GetAndroidLastBundleCode(AndroidPublishSettings settings)
        {
            var data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("bundle_id", settings.BundleId)
            };
            
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(data);
            var response = client.PostAsync($"{_endPoint}games/android/lastbundlecode/get", content);
            var result = response.Result;
            var responseString = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                PGDebug.Message($"ANDROID Bundle Code {responseString}").Log();
            }
            else
            {
                PGDebug.Message($"ANDROID Error getting the bundle code {responseString}").LogError();
            }

            var json = JsonConvert.DeserializeObject<GameData>(responseString);

            return json.android_bundle_code;
        }
        
        public static void SetAndroidBundleCode(AndroidPublishSettings androidPublishSettings, int bundleCode)
        {
            var data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("bundle_id", androidPublishSettings.BundleId),
                new KeyValuePair<string, string>("bundle_code", bundleCode.ToString())
            };
            
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(data);
            var response = client.PostAsync($"{_endPoint}games/android/lastbundlecode/set", content);
            var result = response.Result;
            var responseString = result.Content.ReadAsStringAsync().Result;
            
            if (result.IsSuccessStatusCode)
            {
                PGDebug.Message($"ANDROID Set Bundle Code {responseString}").Log();
            }
            else
            {
                PGDebug.Message($"ANDROID Error setting the bundle code {responseString}").LogError();
            }
        }
#endif

        
    }
}