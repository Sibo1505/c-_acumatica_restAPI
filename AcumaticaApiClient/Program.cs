using System;
using System.Threading.Tasks;
using AcumaticaApiClient.UI;

class Program
{
    static async Task Main (string[] args)
    {
        Console.WriteLine("Acumatica REST API Login Test");

        ConsoleUI ui = new ConsoleUI();
        
        string baseUrl = "http://localhost/AcumaticaERP";

        // Benutzer erstellen
        var credentials = ui.GetUserCredentials();
        User user = new User(credentials.username, credentials.password);

        Console.WriteLine("Logging in...");
        bool loginResult = await user.LoginAsync(baseUrl);

        if (loginResult)
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}