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
    private HttpClient _client;
    private string _baseUrl;

    public User(string username, string password)
    {
        _username = username;
        _password = password;
        _client = new HttpClient();
    }

    public async Task<bool> LoginAsync(string baseUrl)
    {
        try
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
            var response = await _client.PostAsync(loginEndpoint, content);

            // Wenn die Anfrage erfolgreich war, wird true zurückgegeben
            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
                {
                    _authToken = cookies.FirstOrDefault();
                    _client.DefaultRequestHeaders.Add("Cookie", _authToken);
                }

                Console.WriteLine("Login successful!");
                Console.WriteLine($"Auth token: {_authToken}");
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed! Error: {response.StatusCode}");
                Console.WriteLine($"Login failed!: {errorContent}" );
                return false;
            }
            
        }
        catch (Exception ex) // Wenn ein Fehler auftritt, wird false zurückgegeben
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> CreateSalesOrderAsync(SOOrder order)
    {
        if (string.IsNullOrEmpty(_authToken))
        {
            Console.WriteLine("User is not logged in.");
            return false;
        }

        try
        {
            var salesorder_endpoint = $"{_baseUrl}/entity/Default/24.200.001/SalesOrder";

            var json = JsonSerializer.Serialize(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(salesorder_endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Sales Order erfolgreich erstellt!");
                Console.WriteLine(responseContent);
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung! Fehler: {response.StatusCode}");
                Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung!: {errorContent}" );
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        try
    {
        if (!string.IsNullOrEmpty(_authToken))
        {
            // Logout API-URL
            var logoutEndpoint = $"{_baseUrl}/entity/auth/logout";
            
            // Sende die Anfrage zum Abmelden
            var response = await _client.PostAsync(logoutEndpoint, null);
            
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
            _client.DefaultRequestHeaders.Remove("Cookie");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fehler beim Logout: {ex.Message}");
    }
    }
}