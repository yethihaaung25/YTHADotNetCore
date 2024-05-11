using YTHADotNetCore.ConsoleAppHTTPClientExample;

Console.WriteLine("Hello, World!");
Console.WriteLine("--------------------------------------------------"); 

HTTPClientExample httpClientExample = new HTTPClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();


