using System;
using System.Threading.Tasks;

namespace AcumaticaApiClient.UI
{
    // Diese Klasse stellt die Benutzeroberfläche für die Konsolenanwendung bereit
    public class ConsoleUI
    {
        public (string username, string password) GetUserCredentials()
        {
            // Zugangsdaten
            Console.Write("Benutzername: ");
            string username = Console.ReadLine();
            
            Console.Write("Passwort: ");
            string password = Console.ReadLine();

            return (username, password);
        }

        // Diese Funktion zeigt an, ob der Login erfolgreich war oder nicht
        public void DisplayLoginResult(bool loginStatus)
        {
            if (loginStatus)
            {
                Console.WriteLine("Login erfolgreich!");
            }
            else
            {
                Console.WriteLine("Login fehlgeschlagen!");
            }
        }
    }
}