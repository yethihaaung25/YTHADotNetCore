using YTHADotNetCore.ConsoleAppRestClientExample;

Console.WriteLine("Hello, World!");
Console.WriteLine("-----------------------------------------");

RestClientExamples restClientExamples = new RestClientExamples();
await restClientExamples.RunAsync();

Console.ReadLine();