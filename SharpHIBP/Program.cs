using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = null;
        string email = null;
        string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36";
        string filename = null;
        bool verbose = false; // Verbose flag for more breach data output

        for (int i = 0; i < args.Length; i += 2)
        {
            if (args[i] == "--api")
            {
                apiKey = args[i + 1];
            }
            else if (args[i] == "--email")
            {
                email = args[i + 1];
            }
            else if (args[i] == "--file")
            {
                filename = args[i + 1];
            }
            else if (args[i] == "--verbose")
            {
                verbose = true;
            }
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("API key is required. Usage: SharpHIBP.exe --api [YourAPIKey] [--email email] [--file file containing email list] [--verbose Will output more info about the breach]");
            return;
        }

        if (!string.IsNullOrEmpty(email))
        {
            if (verbose)
            {
                await CheckEmailVerbose(apiKey, email, userAgent);
            }
            else
            {
                await CheckEmail(apiKey, email, userAgent);
            }
        }
        else if (!string.IsNullOrEmpty(filename))
        {
            if (File.Exists(filename))
            {
                string[] emails = File.ReadAllLines(filename);
                foreach (var singleEmail in emails)
                {
                    if (verbose)
                    {
                        await CheckEmailVerbose(apiKey, singleEmail, userAgent);
                    }
                    else
                    {
                        await CheckEmail(apiKey, singleEmail, userAgent);
                    }
                    await Task.Delay(7500);
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        else
        {
            Console.WriteLine("No email or file specified. Usage: SharpHIBP.exe --api [YourAPIKey] [--email email] [--file file containing email list] [--verbose Will output more info about the breach]");
        }
    }

    static async Task CheckEmail(string apiKey, string email, string userAgent)
    {
        string apiUrl = $"https://haveibeenpwned.com/api/v3/breachedaccount/{Uri.EscapeDataString(email)}/";

        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.DefaultRequestHeaders.Add("hibp-api-key", apiKey);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine(FormatJson(content));
                }
                else
                {
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Email: {email}");
                Console.WriteLine($"Request Exception: {ex.Message}");
            }
        }
    }

    static async Task CheckEmailVerbose(string apiKey, string email, string userAgent)
    {
        string apiUrl = $"https://haveibeenpwned.com/api/v3/breachedaccount/{Uri.EscapeDataString(email)}/?truncateResponse=false";

        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.DefaultRequestHeaders.Add("hibp-api-key", apiKey);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine(FormatJson(content));
                }
                else
                {
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Email: {email}");
                Console.WriteLine($"Request Exception: {ex.Message}");
            }
        }
    }

    static string FormatJson(string json)
    {
        int level = 0;
        var result = string.Empty;
        var indent = "    "; // 4 spaces for indentation

        foreach (var character in json)
        {
            if (character == '{' || character == '[')
            {
                result += character + Environment.NewLine + new string(' ', level++ * 4);
            }
            else if (character == '}' || character == ']')
            {
                result += Environment.NewLine + new string(' ', --level * 4) + character;
            }
            else if (character == ',')
            {
                result += character + Environment.NewLine + new string(' ', level * 4);
            }
            else
            {
                result += character;
            }
        }

        return result;
    }
}
