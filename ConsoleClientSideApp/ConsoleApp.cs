using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using BusinessLogicLayer.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleClientSideApp
{
    internal class ConsoleApp
    {
        //Main Application 
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Online Retail Shop!");
            Console.WriteLine();

            while (true)
            {
                String tableId = ConsoleMethods.chooseTable();
                int operationId = ConsoleMethods.chooseOperation();
                
                Services.executeOperation(tableId, operationId);
     
                Console.WriteLine();
                Console.WriteLine("Press ANY key to continue or press ESC to exit application...");
                Console.WriteLine();

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }     
    }
}
