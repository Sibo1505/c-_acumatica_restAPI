using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using AcumaticaApiClient.Models;
using System.Text.Json.Serialization;
using System.Net;

public class User
{
    private readonly string _username;
    private readonly string _password;
    private string _authToken;
    private HttpClient _httpClient;
    private string _baseUrl;

    public User(string username, string password, HttpClient httpClient)
    {
        _username = username;
        _password = password;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<bool> LoginAsync(string baseUrl)
    {
        _baseUrl = baseUrl;

        // Login API-URL
        var loginEndpoint = $"{baseUrl}/entity/auth/login";

        // Speichert die Zugangsdaten aus der Userklasse
        var loginData = new
        {
            name = _username,
            password = _password
        };

        // Packt die Zugangsdaten in einen JSON-String, um sie an den Server zu senden
        var content = new StringContent(
            JsonSerializer.Serialize(loginData),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        // Sendet die JSON-Daten an den Server (Login API-URL)
        using var response = await _httpClient.PostAsync(loginEndpoint, content);

        // Wenn die Anfrage erfolgreich war, wird true zurückgegeben
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Login fehlgeschlagen! Fehler: {response.StatusCode}");
            return false;
        }
            
        if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
        {
            _authToken = cookies.FirstOrDefault()?.Split(';').FirstOrDefault();
            if (!string.IsNullOrEmpty(_authToken))
            {
            _httpClient.DefaultRequestHeaders.Add("Cookie", _authToken);
                Console.WriteLine("Login erfolgreich.");
                return true;
            }
        }
        Console.WriteLine($"An error occurred");
        return false;
    }

    public async Task<bool> CreateSalesOrderAsync(SOOrder order)
    {
        if (string.IsNullOrEmpty(_authToken))
        {
            Console.WriteLine("User is not logged in.");
            return false;
        }

        var salesorder_endpoint = $"{_baseUrl}/entity/Default/24.200.001/SalesOrder";

        var json = JsonSerializer.Serialize(order);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        using var response = await _httpClient.PutAsync(salesorder_endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Sales Order erfolgreich erstellt!");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return true;
        }
        else
        {
            Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung! Fehler: {response.StatusCode}");
            Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung!: {await response.Content.ReadAsStringAsync()}" );
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        if (string.IsNullOrEmpty(_authToken))
        {
            Console.WriteLine("Kein aktiver Login gefunden.");
            return;
        }

            // Logout API-URL
            var logoutEndpoint = $"{_baseUrl}/entity/auth/logout";
            
            // Sende die Anfrage zum Abmelden
            using var response = await _httpClient.PostAsync(logoutEndpoint, null);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Logout erfolgreich.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Logout fehlgeschlagen: {response.StatusCode}");
                Console.WriteLine(errorContent);
            }
            
            // Bereinige die Ressourcen
            _authToken = null;
            _httpClient.DefaultRequestHeaders.Remove("Cookie");
    }
}