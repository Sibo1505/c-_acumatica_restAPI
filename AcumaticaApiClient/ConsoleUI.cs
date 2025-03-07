using System;
using System.Threading.Tasks;

namespace AcumaticaApiClient.UI
{
    // Diese Klasse stellt die Benutzeroberfläche für die Konsolenanwendung bereit
    public class ConsoleUI
    {
        public (string username, string password) GetUserCredentials()
        {
            Console.WriteLine("\nLogging in...");

            // Zugangsdaten
            string username = GetValidatedInput("Benutzername: ");
            string password = GetValidatedInput("Passwort: ");

            return (username, password);
        }

        // Diese Funktion zeigt an, ob der Login erfolgreich war oder nicht
        public void DisplayLoginResult(bool loginStatus)
        {
            Console.WriteLine(loginStatus ? "Login erfolgreich!" : "Login fehlgeschlagen!");
        }

        private string GetValidatedInput(string prompt)
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