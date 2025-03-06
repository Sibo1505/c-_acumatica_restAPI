using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AcumaticaApiClient.Models
{
    public class SOOrder
    {
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

    public class SOOrderDetail
    {
        [JsonProperty("InventoryID")]
        public ValueField InventoryID { get; set; } = new ValueField();

        [JsonProperty("Quantity")]
        public ValueField Quantity { get; set; } = new ValueField();

        [JsonProperty("UnitPrice")]
        public ValueField UnitPrice { get; set; } = new ValueField();
    }

    public class ValueField
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}