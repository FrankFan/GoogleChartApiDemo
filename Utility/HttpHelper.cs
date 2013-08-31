using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace GoogleChartApiDemo
{
    public class HttpHelper
    {
        //全局变量：cookie容器
        CookieContainer cookie = new CookieContainer();


        /// <summary>
        /// 模拟浏览器发出HttpGet请求
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <param name="postedData">请求参数</param>
        /// <returns></returns>
        public string HttpGet(string url, string postedData)
        {
            //拼接url参数
            string urlParam = string.Empty;
            if (string.IsNullOrEmpty(postedData))
            {
                urlParam = "";
            }
            else
            {
                urlParam = "?" + postedData;
            }
            //创建一个http request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            //得到http response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //从HttpWebResponse对象中得到stream
            Stream myResponseStream = response.GetResponseStream();
            //通过StreamReader读取Stream流中的内容
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            //将流转化成字符串
            string retString = myStreamReader.ReadToEnd();

            //关闭资源
            myResponseStream.Close();
            myStreamReader.Close();

            return retString;
        }



        /// <summary>
        /// 模拟浏览器发出HttpPost请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="postedData">请求参数</param>
        /// <returns></returns>
        public string HttpPost(string url, string postedData)
        {
            //创建一个Http request
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //设置请求类型等
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "chrome 28.0";
            request.ContentLength = Encoding.UTF8.GetByteCount(postedData);
            //设置cookie
            request.CookieContainer = cookie;

            //获得request stream
            Stream myRequestStream = request.GetRequestStream();
            //用StreamWriter向流request stream中写入字符，格式是gb2312
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postedData);
            //关闭writer
            myStreamWriter.Close();



            //得到http response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //设置cookie给response对象的cookie属性
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            //得到response stream
            Stream myResponseStream = response.GetResponseStream();
            //用StreamReader解析response stream
            StreamReader mySreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            //将流转化成字符串
            string retString = mySreamReader.ReadToEnd();
            //关闭资源
            myResponseStream.Close();
            mySreamReader.Close();

            return retString;
        }

        public byte[] DownloadPng(string url)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);

            return bytes;
        }
    }
}
