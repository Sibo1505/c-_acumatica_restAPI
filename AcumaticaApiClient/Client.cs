using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class User
{
    private readonly string _username;
    private readonly string _password;

    public User(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public async Task<bool> LoginAsync(string baseUrl)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
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
                var response = await client.PostAsync(loginEndpoint, content);

                // Wenn die Anfrage erfolgreich war, wird true zurückgegeben
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Login failed!");
                    return false;
                }
            }
        }
        catch (Exception ex) // Wenn ein Fehler auftritt, wird false zurückgegeben
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}