using System;

namespace AcumaticaApiClient.Utils
{
    public static class InputHelper
    {
        public static string CheckInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(input));
            return input;
        }
    }
}
