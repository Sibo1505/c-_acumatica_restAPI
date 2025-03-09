using System;
using System.Net.Http;
using System.Threading.Tasks;
using AcumaticaApiClient.Models;
using AcumaticaApiClient.UI;

class Program
{
    // Main-Methode der Konsolenanwendung
    static async Task Main(string[] args)
    {
        Console.WriteLine("Acumatica REST API - SOORDER Erstellung");
        Console.WriteLine("-------------------------------------------");
        
        ConsoleUI ui = new ConsoleUI(); // Erstellt eine Benutzeroberfläche

        string baseUrl = "http://localhost/AcumaticaERP";

        // Erstellt eine User Instanz und führt den Login-Prozess durch
        using HttpClient httpClient = new HttpClient();
        var credentials = ui.GetUserCredentials();
        User user = new User(credentials.username, credentials.password, httpClient);

        // Wenn der Login fehlschlägt, wird die Anwendung beendet
        if (!await user.LoginAsync(baseUrl))
        {
            ui.DisplayLoginResult(false);
            return;
        }
        
        // Wenn der Login erfolgreich ist, startet der Prozess der SOOrder Erstellung
        ui.DisplayLoginResult(true);
        await ProcessSalesOrder(user);
        await user.LogoutAsync();
    }

    // Diese Funktion verarbeitet den Prozess der SOOrder Erstellung
    private static async Task ProcessSalesOrder(User user)
    {
        try
        {
            SalesOrderInput soInput = new SalesOrderInput(); // Ruft die Benutzeroberfläche für die Sales Order auf
            var salesOrder = soInput.GetSalesOrderInput(); // Ruft die Eingabe für die Sales Order ab
            salesOrder.Details.Add(soInput.GetOrderLineInput()); // Ruft die Eingabe für die Sales Order Position ab

            // Versucht die Sales Order zu erstellen
            Console.WriteLine("\nErstelle Sales Order...");
            if (await user.CreateSalesOrderAsync(salesOrder))
            {
                Console.WriteLine("Sales Order erfolgreich erstellt!");
            }
            else
            {
                Console.WriteLine("Fehler beim Erstellen der Sales Order!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}");
        }
    }
}