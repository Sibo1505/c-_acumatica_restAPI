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

        Console.WriteLine("\nLogging in...");
        bool loginResult = await user.LoginAsync(baseUrl);
        ui.DisplayLoginResult(loginResult);

        if (loginResult)
        {
            try
            {
                // Sales Order erstellen
                Console.WriteLine("\n=== SOORDER erstellen ===");

                // Kundedaten eingeben
                Console.Write("Kunden-ID eingeben: ");
                string customerID = Console.ReadLine();

                Console.Write("Beschreibung für den Auftrag: ");
                string description = Console.ReadLine();

                var salesOrder = new SOOrder
                {
                    OrderType = new ValueField { Value = "SO"},
                    CustomerID = new ValueField { Value = customerID},
                    Description = new ValueField { Value = description}
                };

                // Eine Position (SOLine) hinzufügen
                Console.WriteLine("\n=== SOLINE hinzufügen ===");
                Console.Write("Artikelnummer eingeben: ");
                string inventoryID = Console.ReadLine();

                Console.Write("Menge eingeben: ");
                string quantity = Console.ReadLine();

                Console.Write("Preis pro Stück: ");
                string unitPrice = Console.ReadLine();

                salesOrder.Details.Add(new SOOrderDetail
                {
                    InventoryID = new ValueField { Value = inventoryID},
                    Quantity = new ValueField { Value = quantity},
                    UnitPrice = new ValueField { Value = unitPrice}
                });

                // Sales Order anlegen
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
            //Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}