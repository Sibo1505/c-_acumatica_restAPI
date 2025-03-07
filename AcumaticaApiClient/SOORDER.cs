using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AcumaticaApiClient.Models
{
    // Diese Klasse definiert die Struktur der Sales Order
    public class SOOrder
    {
        // OrderType = SO
        [JsonProperty("OrderType")]
        public ValueField OrderType { get; set; } = new ValueField();

        [JsonProperty("OrderNbr")]
        public ValueField OrderNbr { get; set; } = new ValueField();

        [JsonProperty("CustomerID")]
        public ValueField CustomerID { get; set; } = new ValueField();

        [JsonProperty("Description")]
        public ValueField Description { get; set; } = new ValueField();

        [JsonProperty("Details")]
        public List<SOOrderDetail> Details { get; set; } = new List<SOOrderDetail>();
    }

    // Diese Klasse definiert die Struktur der Sales Order Position
    public class SOOrderDetail
    {
        [JsonProperty("InventoryID")]
        public ValueField InventoryID { get; set; } = new ValueField();

        [JsonProperty("OrderQty")]
        public ValueField OrderQty { get; set; } = new ValueField();

        [JsonProperty("UnitPrice")]
        public ValueField UnitPrice { get; set; } = new ValueField();
    }

    // Diese Klasse definiert die Struktur des ValueFields
    public class ValueField
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}