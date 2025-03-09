using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using AcumaticaApiClient.Models;

// Diese Klasse definiert die Benutzerklasse, die die Anfragen an den Acumatica-Server sendet
public class User
{
    private readonly string _username;
    private readonly string _password;
    private string _authToken;
    private HttpClient _httpClient;
    private string _baseUrl;

    // Konstruktor der Benutzerklasse
    public User(string username, string password, HttpClient httpClient)
    {
        _username = username;
        _password = password;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    // Diese Funktion sendet eine Anfrage an den Server, um sich anzumelden
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

        // Konvertiert die Zugangsdaten in JSON
        var content = new StringContent(
            JsonSerializer.Serialize(loginData),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        // Sendet die JSON-Daten an den Server (Login API-URL)
        using var response = await _httpClient.PostAsync(loginEndpoint, content);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        // Wenn der Server erfolgreich antwortet, wird der Auth-Token gespeichert    
        if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
        {
            _authToken = cookies.FirstOrDefault()?.Split(';').FirstOrDefault();
            if (!string.IsNullOrEmpty(_authToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Cookie", _authToken);
                return true;
            }
        }
        return false;
    }

    // Diese Funktion sendet eine Anfrage an den Server, um eine Verkaufsbestellung zu erstellen
    public async Task<bool> CreateSalesOrderAsync(SOOrder order)
    {
        if (string.IsNullOrEmpty(_authToken))
        {
            Console.WriteLine("User is not logged in.");
            return false;
        }

        var salesorder_endpoint = $"{_baseUrl}/entity/Default/24.200.001/SalesOrder";
        var json = JsonSerializer.Serialize(order); // Konvertiert das SOOrder-Objekt in JSON
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Sendet die Anfrage an den Server, um die Verkaufsbestellung zu erstellen
        using var response = await _httpClient.PutAsync(salesorder_endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            //Console.WriteLine("Sales Order erfolgreich erstellt!");
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            return true;
        }
        else
        {
            Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung! Fehler: {response.StatusCode}");
            Console.WriteLine($"Fehler beim Erstellen der Verkaufsbestellung!: {await response.Content.ReadAsStringAsync()}" );
            return false;
        }
    }

    // Diese Funktion sendet eine Anfrage an den Server, um sich abzumelden
    public async Task LogoutAsync()
    {
        if (string.IsNullOrEmpty(_authToken))
        {
            Console.WriteLine("Kein aktiver Login gefunden.");
            return;
        }

        // Sende die Anfrage zum Abmelden
        var logoutEndpoint = $"{_baseUrl}/entity/auth/logout";
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
            
        // Löscht den Auth-Token und den Cookie aus dem HttpClient
        _authToken = null;
        _httpClient.DefaultRequestHeaders.Remove("Cookie");
    }
}