using System;
using System.Collections.Generic;
using System.Text;

namespace secVerify_api_CSharp_client
{
    /// <summary>
    /// 响应成功DES解密后的结果对象
    /// </summary>
    internal class ResDESModel
    {
        private int isValid;

        private string phone;

        private bool valid;

        /// <summary>
        /// 验证状态，1:成功, 2:失败
        /// </summary>
        public int IsValid { get => isValid; set => isValid = value; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get => phone; set => phone = value; }

        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool Valid { get => valid; set => valid = value; }

    }
}
