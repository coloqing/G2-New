using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection.PortableExecutable;
namespace KP.Util
{
    public class HttpClientExample
    {
        public static T? DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        // 发送GET请求并处理返回值的方法
        public static async Task<T> SendGetRequestAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // 发送GET请求
                    HttpResponseMessage response = await client.GetAsync(url);

                    // 确保请求成功
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return DeserializeJson<T>(responseBody);
                }
                catch (HttpRequestException e)
                {
    
                    return default;
                }
            }
        }

        /// <summary>
        /// // 发送POST请求并处理返回值的方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<T> SendPostRequestAsync<T>(string url, object content, Dictionary<string,string> headers = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // 如果提供了请求头，则添加到HttpClient的请求头中  
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    string jsonContent = JsonConvert.SerializeObject(content);
                    StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // 发送POST请求  
                    HttpResponseMessage response = await client.PostAsync(url, stringContent);

                    // 确保请求成功  
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容  
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return DeserializeJson<T>(responseBody);
                }
                catch (HttpRequestException e)
                {
                    return default; 
                }
            }
        }

    }
}
