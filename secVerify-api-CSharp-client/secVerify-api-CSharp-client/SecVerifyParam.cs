using System;
using System.Collections.Generic;
using System.Text;

namespace secVerify_api_CSharp_client
{
    public class SecVerifyParam
    {

        private string appkey;
        private string appSecret;
        private string token;
        private string opToken;
        private string operators;
        private string md5 = string.Empty;

        /// <summary>
        /// app标识	
        /// </summary>
        public string Appkey { get => appkey; set => appkey = value; }
        /// <summary>
        /// app秘钥
        /// </summary>
        public string AppSecret { get => appSecret; set => appSecret = value; }
        /// <summary>
        /// 客户端的token
        /// </summary>
        public string Token { get => token; set => token = value; }
        /// <summary>
        /// 客户端返回的运营商token
        /// </summary>
        public string OpToken { get => opToken; set => opToken = value; }
        /// <summary>
        /// 客户端返回的运营商，CMCC:中国移动通信, CUCC:中国联通通讯, CTCC:中国电信
        /// </summary>
        public string Operator { get => operators; set => operators = value; }
        /// <summary>
        /// android必须要填写，例：e4caa1a08ba0570b5c1290b1a0bc9252
        /// </summary>
        public string Md5 { get => md5; set => md5 = value; }
     
    }
}
