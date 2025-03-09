using System;
using AcumaticaApiClient.Models;
using AcumaticaApiClient.Utils;

namespace AcumaticaApiClient.UI
{
    // Diese Klasse stellt die Benutzeroberfläche für die erstellung einer Sales Order bereit
    public class SalesOrderInput
    {
        // Diese Funktion erfasst die Daten für eine Sales Order
        public SOOrder GetSalesOrderInput()
        {
            Console.WriteLine("\n=== SOORDER erstellen ===");

                string customerID = InputHelper.CheckInput("Kunden-ID eingeben: ");
                string description = InputHelper.CheckInput("Beschreibung für den Auftrag: ");

                return new SOOrder
                {
                    OrderType = new ValueField { Value = "SO"},
                    CustomerID = new ValueField { Value = customerID},
                    Description = new ValueField { Value = description}
                };
        }

        // Diese Funktion erfasst die Daten für eine Sales Order Position
        public SOOrderDetail GetOrderLineInput()
        {
            Console.WriteLine("\n=== SOLINE hinzufügen ===");

                string inventoryID = InputHelper.CheckInput("Artikelnummer eingeben: ");
                string quantity = InputHelper.CheckInput("Menge eingeben: ");
                string unitPrice = InputHelper.CheckInput("Preis pro Stück: ");

                return new SOOrderDetail
                {
                    InventoryID = new ValueField { Value = inventoryID},
                    OrderQty = new ValueField { Value = quantity},
                    UnitPrice = new ValueField { Value = unitPrice}
                };
        }
    }
}