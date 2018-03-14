using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BullclipTestAutomation.Controller
{
    public class RestExecutor
    {
        public static string Url;

        public static string RunPOSTRequest(string jSon, out string StatusCode)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jSon);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StatusCode = httpResponse.StatusCode.ToString();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    return result.ToString();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    StatusCode = httpResponse.StatusCode.ToString();
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string result = reader.ReadToEnd();
                        return result;
                    }

                }
            }
        }

        public static string RunPOSTRequest(string jSon, string authKey, out string StatusCode)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Ocp-Apim-Subscription-Key", authKey);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jSon);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StatusCode = httpResponse.StatusCode.ToString();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    return result.ToString();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    StatusCode = httpResponse.StatusCode.ToString();
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string result = reader.ReadToEnd();
                        return result;
                    }

                }

            }
        }

            public static string RunPOSTRequest(string jSon, string authKey,string authHeader, out string StatusCode)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Ocp-Apim-Subscription-Key", authKey);
                httpWebRequest.Headers.Add("Authorization", authHeader);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jSon);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    StatusCode = httpResponse.StatusCode.ToString();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        return result.ToString();
                    }
                }
                catch (WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        StatusCode = httpResponse.StatusCode.ToString();
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string result = reader.ReadToEnd();
                            return result;
                        }

                    }
                }

            }
    }
}
