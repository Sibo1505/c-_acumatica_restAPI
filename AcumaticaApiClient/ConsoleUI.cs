using System;
using AcumaticaApiClient.Utils;

namespace AcumaticaApiClient.UI
{
    // Diese Klasse stellt die Benutzeroberfläche für die Konsolenanwendung bereit
    public class ConsoleUI
    {
        // Fragt den Benutzer nach den Zugangsdaten
        public (string username, string password) GetUserCredentials()
        {
            Console.WriteLine("\nLogging in...");

            // Zugangsdaten
            string username = InputHelper.CheckInput("Benutzername: ");
            string password = InputHelper.CheckInput("Passwort: ");

            return (username, password);
        }

        // Diese Funktion zeigt an, ob der Login erfolgreich war oder nicht
        public void DisplayLoginResult(bool loginStatus)
        {
            if (loginStatus)
            {
                Console.WriteLine("Login erfolgreich.");
            }
            else
            {
                Console.WriteLine("Login fehlgeschlagen.");
            }
        }
    }
}