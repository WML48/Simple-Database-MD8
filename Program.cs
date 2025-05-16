namespace SimpleMBD;

public class Program
{
    public static async Task Main()
    { 
        App app = new App();
        await app.start();
    }
}