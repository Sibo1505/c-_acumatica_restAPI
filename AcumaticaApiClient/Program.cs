using System;
using System.Threading.Tasks;
using AcumaticaApiClient.Models;
using AcumaticaApiClient.UI;

class Program
{
    static async Task Main (string[] args)
    {
        Console.WriteLine("Acumatica REST API - SOORDER Erstellung");
        Console.WriteLine("-------------------------------------------");

        ConsoleUI ui = new ConsoleUI();
        
        string baseUrl = "http://localhost/AcumaticaERP";

        // Benutzer erstellen
        var credentials = ui.GetUserCredentials();
        User user = new User(credentials.username, credentials.password);

        
        bool loginResult = await user.LoginAsync(baseUrl);
        ui.DisplayLoginResult(loginResult);

        if (loginResult)
        {
            try
            {
                // Sales Order erstellen
                SalesOrderInput soInput = new SalesOrderInput();
                var salesOrder = soInput.GetSalesOrderInput();

                var orderDetail = soInput.GetOrderLineInput();
                salesOrder.Details.Add(orderDetail);

                Console.WriteLine("\nErstelle Sales Order...");
                bool createResult = await user.CreateSalesOrderAsync(salesOrder);

                if (createResult)
                {
                    Console.WriteLine("Sales Order erfolgreich erstellt!");
                }
                else
                {
                    Console.WriteLine("Fehler beim Erstellen der Sales Order!");
                }
            }
            finally
            {
                Console.WriteLine("\nLogging out...");
                await user.LogoutAsync();
            }
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}