using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace secVerify_api_CSharp_client
{
    public class SecVerify
    {
        /// <summary>
        /// 一键登录身份验证
        /// </summary>
        /// <param name="requestParam">请求参数</param>
        /// <returns>身份验证响应返回结果对象</returns>
        public static ResAuthModel AuthVerify(SecVerifyParam secVerifyParam)
        {
            string url = "http://identify.verify.mob.com/auth/auth/sdkClientFreeLogin";

            Dictionary<string, object> pairs = new Dictionary<string, object>();

            //注意:参数添加顺序不可变动【因为官方排序方式和.net的Dictionary自带排序函数排序不一致，字母T和e大小写排序存在差异】
            pairs.Add("appkey", secVerifyParam.Appkey);
            if (!string.IsNullOrEmpty(secVerifyParam.Md5))
            {
                pairs.Add("md5", secVerifyParam.Md5);
            }
            pairs.Add("opToken", secVerifyParam.OpToken);
            pairs.Add("operator", secVerifyParam.Operator);
            pairs.Add("timestamp", ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString());
            pairs.Add("token", secVerifyParam.Token);
            pairs.Add("sign", GetSign(pairs, secVerifyParam.AppSecret));

            string resOrigin = HttpPost(url, JsonConvert.SerializeObject(pairs));

            #region  定制化处理,可根据需求自定义修改
            ///响应结果对象化
            ResAuthModel resAuthModel = new ResAuthModel();
            resAuthModel = (ResAuthModel)JsonConvert.DeserializeObject(resOrigin, resAuthModel.GetType());

            if (resAuthModel.Status.Equals(200))
            {
                //解密信息对象化
                ResDESModel resDESModel = new ResDESModel();
                resDESModel = (ResDESModel)JsonConvert.DeserializeObject(DESDecrypt(resAuthModel.Res, secVerifyParam.AppSecret), resDESModel.GetType());
                resAuthModel.Res = resDESModel.Phone;
            }

            return resAuthModel;
            #endregion
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="pairs">参数信息</param>
        /// <param name="secret">秘钥</param>
        /// <returns></returns>
        private static string GetSign(Dictionary<string, object> pairs, String secret)
        {
            StringBuilder plainText = new StringBuilder();

            foreach (var item in pairs)
            {
                if (!item.Key.Equals("sign") && !string.IsNullOrEmpty(item.Value.ToString()))
                {
                    plainText.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
            }

            String substring = plainText.ToString().TrimEnd('&');
            return MD5Encrypt(substring + secret);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="info">待加密信息</param>
        /// <returns>加密后的信息</returns>
        private static string MD5Encrypt(string info)
        {
            byte[] sor = Encoding.UTF8.GetBytes(info);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));
            }
            return strbul.ToString();
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>响应结果</returns>
        private static string HttpPost(string url, string postData)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptInfo">待解密信息</param>
        /// <returns>解密后的信息</returns>
        private static string DESDecrypt(string encryptInfo, string appSecret)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(encryptInfo);
            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] iv = { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 };  //偏移量:00000000
                byte[] key = UTF8Encoding.UTF8.GetBytes(appSecret.Substring(0, 8));
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
            }
        }

    }
}
