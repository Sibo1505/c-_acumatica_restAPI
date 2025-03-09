using System;

namespace AcumaticaApiClient.Utils
{
    // Diese Klasse stellt Hilfsfunktionen für die Benutzereingabe bereit
    public static class InputHelper
    {
        // Funktion sorgt dafür dass die Eingabe nicht leer ist
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
