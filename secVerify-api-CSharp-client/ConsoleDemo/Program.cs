
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using secVerify_api_CSharp_client;
using System;

namespace ConsoleDemo
{
    class Program
    {


        static void Main(string[] args)
        {

            SecVerifyParam secVerifyParam = new SecVerifyParam();
            secVerifyParam.Appkey = "m4600e5e51c50dd";
            secVerifyParam.AppSecret = "43fd853b3585139d6f0b46b849bac194";
            secVerifyParam.Token = "0:AAAAhAAAAIASIDwhTOvpfHBJIBNE5QRcFNWRnuYr45aJfKMiO7oqYZnfUwuJBAZQy0bgfkunCTDHvLG+GVgyDvp5yuz23Ag1lJ/d3Pxicuv7M1GdLUtzxmoowaA+9kP8+FJBpDufi9h+Bla9tYeibWNXxrNKjP3Dkn0TrVOG3B4z/zsqQbaDpwAAAMBOnylZjXZGKifp3Iwbrd05gUT+M5108HysF+8PHfPn9RsEfCNg/X5AL9dJJO283VqAZJUUwIrnCtlsKkGJeT69oi7TKpO6d1jW+Sj5dbrKXob/oqneH+K0yLAZb+WO0a5AgfRi2Yz/lASPxrCO3JIKLrZe5wXDU0GcroWUv7gjEhAp3Wthj66WLrBjFdViRWSHf5KZxGArNe3XgTdSOD08TT3Ie0brMN9RuJNhsZnTE/M5klYOlbAAVIhQ/h+rr5k=";
            secVerifyParam.OpToken = "STsid00000015960068735937BvV1XipV5wEQAw0aMNHgp1FRFSeJXJv";
            secVerifyParam.Operator = "CMCC";
            secVerifyParam.Md5 = "2e567f8304e701d74b7fc738984a70b7";

            ResAuthModel strResult = SecVerify.AuthVerify(secVerifyParam);

            Console.WriteLine(JsonConvert.SerializeObject(strResult));
            Console.ReadKey();

        }
    }
}

