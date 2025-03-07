using System;
using System.Net.Http;
using System.Threading.Tasks;
using AcumaticaApiClient.Models;
using AcumaticaApiClient.UI;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Acumatica REST API - SOORDER Erstellung");
        Console.WriteLine("-------------------------------------------");

        ConsoleUI ui = new ConsoleUI();
        string baseUrl = "http://localhost/AcumaticaERP";

        using HttpClient httpClient = new HttpClient();
        var credentials = ui.GetUserCredentials();
        User user = new User(credentials.username, credentials.password, httpClient);

        if (!await user.LoginAsync(baseUrl))
        {
            ui.DisplayLoginResult(false);
            return;
        }
        
        ui.DisplayLoginResult(true);
        await ProcessSalesOrder(user);
        await user.LogoutAsync();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static async Task ProcessSalesOrder(User user)
    {
        try
        {
            SalesOrderInput soInput = new SalesOrderInput();
            var salesOrder = soInput.GetSalesOrderInput();
            salesOrder.Details.Add(soInput.GetOrderLineInput());

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