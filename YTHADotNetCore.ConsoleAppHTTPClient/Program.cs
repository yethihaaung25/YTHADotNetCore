// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var json = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(json);

//Console.WriteLine(json);
foreach(var questions in model.questions)
{
    Console.WriteLine(questions.questionNo);
}

Console.ReadLine();


public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}

