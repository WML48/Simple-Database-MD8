using System.Net;
using System.Text;

namespace SimpleMBD;

public class App
{
    private HttpListener server;

    public App()
    {
        string host = "http://127.0.0.1:8080/";
        server = new HttpListener();
        server.Prefixes.Add(host);
        Console.WriteLine("server is listening on " + host);
    }

    public async Task start()
    {
        server.Start();
        while (server.IsListening)
        {
            var ctx = server.GetContext(); // Blocks the thread until a request is received
            await HandleContextAsync(ctx); // Fixed typo here
        }
    }

    public void stop()
    {
        server.Stop();
        server.Close();
    }

    private async Task HandleContextAsync(HttpListenerContext ctx) // Fixed typo in method name
    {
        var request = ctx.Request;
        var response = ctx.Response;

        if (request.HttpMethod == "GET")
        {
            string html = "Hello!";
            byte[] content = Encoding.UTF8.GetBytes(html);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentEncoding = Encoding.UTF8; // Encoding to be sent in the response
            response.ContentType = "text/html";
            response.ContentLength64 = content.Length; // Length of content to be sent
            await response.OutputStream.WriteAsync(content);
            response.Close();
        }
    }
}