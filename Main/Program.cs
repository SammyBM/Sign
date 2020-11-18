using System;
using static System.Console;
using Lib;
using System.Security.Cryptography;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Please enter your text: ");
            string message = ReadLine();

            //Creates a real signature and a fake one, therefore the parameters are different.
            string signature = Protector.generateSign(message);
            string xmlText = Protector.xmlToString();
            string fakeSignature = signature.Replace(signature[0], signature[1]);

            //Prints results.
            WriteLine($"\n XML String: {xmlText} \n");

            WriteLine($"\n Signature: {signature} \n");
            validate(message, signature);

            WriteLine($"\n Signature: {fakeSignature} \n");
            validate(message, fakeSignature);
        }

        static public void validate(string text, string sign) //Takes a bool to return a readable output.
        {
            if (Protector.validateSign(text, sign))  //If the signature is correct, therefore "if true".
            {
                WriteLine("Correct signature"); 
            }
            else                                      //If the sirgnature is not correct.
            {
                WriteLine("Wrong signature");
            }
        }
    }
}
