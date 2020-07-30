using System;
using System.Collections.Generic;
using System.Text;

namespace secVerify_api_CSharp_client
{
    /// <summary>
    /// 身份验证响应返回结果对象
    /// </summary>
    public class ResAuthModel
    {


        private int status;

        private string res = null;

        private string error = null;

        private string seqid;

        /// <summary>
        /// 错误码
        /// </summary>
        public int Status { get => status; set => status = value; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Res { get => res; set => res = value; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get => error; set => error = value; }
        /// <summary>
        /// 序列编号()
        /// </summary>
        public string Seqid { get => seqid; set => seqid = value; }
    }




}
