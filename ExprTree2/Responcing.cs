using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace ExprTree2
{
    public class Responcing
    {
        public static int chert = 0;
        public static double GetResponsiPlus(Expression a, Expression b)
        {
            string a1 = a.ToString();
            string b1 = b.ToString();
            string oper1 = "%2B";
            double ans = GetPesponsing(a1, b1, oper1);
            Console.WriteLine("Считаю на сервере (" + a1.ToString() + " + " + b1.ToString() + " = " + ans + ")");
            return ans;
        }
        public static double GetResponsiMin(Expression a, Expression b)
        {
            string a1 = a.ToString();
            string b1 = b.ToString();
            string oper1 = "-";
            double ans = GetPesponsing(a1, b1, oper1);
            Console.WriteLine("Считаю на сервере (" + a1.ToString() + " - " + b1.ToString() + " = " + ans + ") ");
            return ans;
        }
        public static double GetResponsiDel(Expression a, Expression b)
        {
            string a1 = a.ToString();
            string b1 = b.ToString();
            string oper1 = "%2F";
            double ans = GetPesponsing(a1, b1, oper1);
            Console.WriteLine("Считаю на сервере (" + a1.ToString() + " / " + b1.ToString() + " = " + ans + ") ");
            return ans;
        }
        public static double GetResponsiMult(Expression a, Expression b)
        {
            string a1 = a.ToString();
            string b1 = b.ToString();
            string oper1 = "*";
            double ans = GetPesponsing(a1, b1, oper1);
            Console.WriteLine("Считаю на сервере (" + a1.ToString() + " * " + b1.ToString() + " = " + ans + ") ");
            return ans;
        }
        public static double GetPesponsing(string a1, string b1, string oper1)
        {
            HttpWebRequest proxy = (HttpWebRequest)HttpWebRequest.Create("http://localhost:53881?a=" + a1 + "&b=" + b1 + "&oper=" + oper1 + "");
            var resp = proxy.GetResponse();
            var resp1 = resp.GetResponseStream();
            var read = new StreamReader(resp1);
            var ans = read.ReadToEnd();
            return double.Parse(ans);
        }

    }
}
