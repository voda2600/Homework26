using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace ExprTree2
{
    public class Responcing:IResponcing
    {
        public static int chert = 0;

        public double GetPesponsing(double a1, double b1, string oper)
        {
            chert++;
            for (int i = 0; i < chert; i++)
                Console.Write("-");
            string oper1 = "";
            switch (oper)
            {
                case "+": oper1 = "%2B"; break;
                case "/": oper1 = "%2F"; break;
                case "*": oper1 = "*"; break;
                case "-": oper1 = "-"; break;
                default: throw new ArgumentException();
            }
            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            Console.WriteLine("Считаю на сервере (" + a1.ToString() + oper + b1.ToString() + " = " + ans + ") ");
            return double.Parse(ans);
        }

    }
}
