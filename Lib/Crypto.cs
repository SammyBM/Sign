using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using static System.Convert;

namespace Lib
{
    public static class Protector
    {
        //Generate key tupple and export all information as parameters.
        private static RSAParameters Keys = new RSACryptoServiceProvider().ExportParameters(true);

        #region RSA

        public static string xmlToString() //Converts XML formatted text into a non formatted string.
        {
            using (RSA rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(Keys);
                return rsa.ToXmlString(false);
            }
        }
    
    
        public static void stringToXml(string xml) //Converts a non formatted string into XML format.
        {
            using (RSA rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(xml);
                Keys = rsa.ExportParameters(false);
            }
        }
        
        
        public static string generateSign(string text) //Generates a signature based on the key tupple generated before.
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) //Using disposes of the object in parenthesis after the brackets are closed.
            {
                rsa.ImportParameters(Keys);

                byte[] signatureBytes = rsa.SignData(Encoding.Unicode.GetBytes(text), SHA256.Create());

                //Pase to a Base64 formatted string and return it
                return ToBase64String(signatureBytes);
            }
        }
        
        
        public static bool validateSign(string text, string sign) //If the signature is valid returns true, otherwise returns false.
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                //Config the provider with the current key
                rsa.ImportParameters(Keys);

                byte[] signatureBytes = FromBase64String(sign);
                byte[] messageBytes = Encoding.Unicode.GetBytes(text);

                //Return result
                return rsa.VerifyData(messageBytes, SHA256.Create(), signatureBytes);
            }
        }
        #endregion
    }
}