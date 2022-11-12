using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Utilities
{
    public class InputValidator
    {
        public static int validateInputNum(int max = int.MaxValue)
        {
            int n;
            String text = Console.ReadLine();
            while (!Int32.TryParse(text, out n) || n > max || n <= 0)
            {
                Console.Write($"Please enter a valid number (not exceeding {max}): ");
                text = Console.ReadLine();
            }
            return n;
        }

        public static String validateInputText(int maxLength)
        {
            String text = Console.ReadLine();
            while (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text) || text.Length > maxLength)
            {
                Console.Write($"Text can not be empty, whitespace or longer than {maxLength} symbols: ");
                text = Console.ReadLine();
            }
            return text;
        }

        public static int validateUpdatedNum(double value, int max = int.MaxValue)
        {
            int n = 0;
            String text = Console.ReadLine();
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
            {
                n = (int)value;             
            }
            else
            {
                while (!Int32.TryParse(text, out n) || n > max || n <= 0)
                {
                    Console.Write($"Please enter a valid number (not exceeding {max}): ");
                    text = Console.ReadLine();
                }
            }
            return n;
        }

        public static String validateUpdatedText(string value, int maxLength)
        {
            String text = Console.ReadLine();
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
            {
                text = value;
            }
            else
            {
                while (text.Length > maxLength)
                {
                    Console.Write($"Text can not be longer than {maxLength} symbols: ");
                    text = Console.ReadLine();
                }
            }
            return text;
        }

        public static void validateReadEntry(String data, int entryId, string tableId)
        {
            if (data.Equals("null")){
                Console.WriteLine($"\nNo such entry found with Id '{entryId}' in Table '{tableId}'");
            }
            else
            {
                Console.WriteLine($"\nEntry with Id '{entryId}' has been selected from table '{tableId}': ");
                Console.WriteLine(data);
            }
        }
    }
}
