using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ExprTree2
{
     class Program
    {
         static void Main(string[] args)
        { 
            //Для работы приложения нужно запустить Server, находится в этом же репозитории 
            Console.WriteLine("Введите выражение");
            string expression = "(2+3)/12*7+8*9";//Console.ReadLine();
            var mas = expression.Replace(" ", "").ToCharArray();
            Expression Tree = ExpTreeParsing.ParsingExpression(mas);
            Console.WriteLine("Получившееся дерево");
            Console.WriteLine(Tree.ToString());
            var ans = ExpTreeParsing.Calculate(Tree).Result;
            Console.WriteLine("Конечный ответ: "+ans.ToString()); 
        }
    }
}
