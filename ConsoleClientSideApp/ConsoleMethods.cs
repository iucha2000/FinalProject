using BusinessLogicLayer;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientSideApp
{
    public class ConsoleMethods
    {
        //UI methods for console
        public static String chooseTable()
        {
            Console.WriteLine("Available tables: ");
            var enumTableValues = Enum.GetValues(typeof(Tables));
            foreach (var t in enumTableValues)
            {
                Console.WriteLine($"{(int)t}. {t}");
            }
            Console.WriteLine();
            Console.Write($"Choose which table to access (enter number from 1 to {enumTableValues.Length}): ");
            int c = InputValidator.validateInputNum(enumTableValues.Length);

            return enumTableValues.GetValue(c - 1).ToString();

        }

        public static int chooseOperation()
        {
            Console.WriteLine("Available operations: ");
            var enumOpValues = Enum.GetValues(typeof(Operations));
            foreach (var op in enumOpValues)
            {
                Console.WriteLine($"{(int)op}. {op}");
            }
            Console.WriteLine();
            Console.Write($"Choose which CRUD operation to execute (enter number from 1 to {enumOpValues.Length}): ");
            int c = InputValidator.validateInputNum(enumOpValues.Length);

            return c;
        }
        
        public static void DisplayUserInput(String tableId, int operationId)
        {
            Console.WriteLine("Table Name: " + tableId);
            Console.WriteLine("Operation: " + Enum.GetValues(typeof(Operations)).GetValue(operationId - 1).ToString());
        }
           
    }
}
