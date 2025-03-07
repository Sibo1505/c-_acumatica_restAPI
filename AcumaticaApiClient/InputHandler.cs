using System;
using AcumaticaApiClient.Models;

namespace AcumaticaApiClient.UI
{
    // Diese Klasse stellt die Benutzeroberfläche für die erstellung einer Sales Order bereit
    public class SalesOrderInput
    {
        public SOOrder GetSalesOrderInput()
        {
            // Kundendaten eingeben
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

                return salesOrder;
        }

        // Diese Funktion erfasst die Daten für eine Sales Order Position
        public SOOrderDetail GetOrderLineInput()
        {
            Console.WriteLine("\n=== SOLINE hinzufügen ===");
                Console.Write("Artikelnummer eingeben: ");
                string inventoryID = Console.ReadLine();

                Console.Write("Menge eingeben: ");
                string quantity = Console.ReadLine();

                Console.Write("Preis pro Stück: ");
                string unitPrice = Console.ReadLine();

                return new SOOrderDetail
                {
                    InventoryID = new ValueField { Value = inventoryID},
                    OrderQty = new ValueField { Value = quantity},
                    UnitPrice = new ValueField { Value = unitPrice}
                };
        }
    }
}