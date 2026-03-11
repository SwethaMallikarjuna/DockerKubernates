using System;
using System.Threading.Tasks;

Console.WriteLine("AssignmentApp (new code) - .NET 7 console app");

var userName = args.Length > 0 ? args[0] : Environment.GetEnvironmentVariable("ASSIGNMENT_APP_NAME") ?? "Developer";
Console.WriteLine($"Hello, {userName}!");

await RunInteractiveLoopAsync();

static async Task RunInteractiveLoopAsync()
{
    Console.WriteLine("Type a simple math expression like: 5 + 3, or 'exit' to quit.");

    while (true)
    {
        Console.Write("> ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            continue;

        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Bye!");
            break;
        }

        try
        {
            var result = EvaluateExpression(input);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid expression: {ex.Message}");
        }

        await Task.Delay(10);
    }
}

static double EvaluateExpression(string expression)
{
    var tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (tokens.Length != 3)
        throw new InvalidOperationException("Expression must be in the form: [number] [operator] [number].");

    if (!double.TryParse(tokens[0], out var left))
        throw new InvalidOperationException("Left operand is not a number.");

    if (!double.TryParse(tokens[2], out var right))
        throw new InvalidOperationException("Right operand is not a number.");

    return tokens[1] switch
    {
        "+" => left + right,
        "-" => left - right,
        "*" or "x" or "X" => left * right,
        "/" => right == 0 ? throw new DivideByZeroException() : left / right,
        _ => throw new InvalidOperationException("Supported operators: + - * /"),
    };
}
