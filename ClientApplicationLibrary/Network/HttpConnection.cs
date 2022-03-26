using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;

namespace ClientApplicationLibrary.Network
{
    /// <summary>
    /// http通信の管理クラス
    /// babylon.jsや各種C#用に共通利用での呼び出しとして利用
    /// </summary>
    public class HttpConnectionManager
    {

        private string _host;

        private NameValueCollection _querys;

        private HttpClient _client;

        public void setParameter(string host, NameValueCollection querys)
        {
            this._host = host;
            this._querys = querys;
        }

        /// <summary>
        /// get通信を実行
        /// </summary>
        /// <param name="path"></param>
        public async Task<string> GetRequest(string path)
        {

            var url = $"{_host}{path}?";
            var count = 0;
            foreach (var key in _querys.AllKeys)
            {
                url += $"{key}={_querys[key]}";
                count++;
                if (count < _querys.Count - 1) {
                    url += "&";
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            _client = new HttpClient();
            var result = await _client.SendAsync(request);
            var resultString = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultString);

            return resultString;
        }

        /// <summary>
        /// get通信を実行
        /// </summary>
        /// <param name="path"></param>
        public async Task<string> PostRequest(string path, Dictionary<string, object> param)
        {

            var url = $"{_host}{path}";

            _client = new HttpClient();

            var jsonString = System.Text.Json.JsonSerializer.Serialize(param);
            var content = new StringContent(jsonString, Encoding.UTF8, @"application/json");
            content.Headers.Add("DNT", "1");
            var result = await _client.PostAsync(url, content);
            var resultString = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultString);

            return resultString;
        }

    }
}
