# Crontastic

**Crontastic** is a DLL written in .NET 4.8 that includes a user-friendly interface to simplify the process of setting cron expressions. It's made for users who may not be familiar with cron syntax.

This is not a scheduling service, it only builds CRON expressions you can then integrate into your application.

![image](https://github.com/user-attachments/assets/50e4d580-1c69-4231-aba4-b85ff392f28b)


## Features

- **User Interface**: Intuitive UI to help users create cron expressions without needing in-depth knowledge.
- **Comprehensive Cron Parsing**: Supports standard cron syntax with minute, hour, day of the month, month, and day of the week fields.
- **Advanced Syntax Support**: Handles ranges (`-`), steps (`/`) and lists (`,`)..
- **Error Handling**: Validates cron expressions and provides error messages for invalid syntax.
- **Trigger Calculation**: Calculates the next trigger times based on the cron expression.

## Getting Started

1. **Installation**: Add Crontastic to your project by adding a reference to the DLL on your project.
2. **Usage**: Open the UI by running the static method `Crontastic.Crontastic.Run()`. You may pass a CRON as string as a parameter to be loaded into the UI. The method returns a string of the CRON expression built in the UI.

```csharp
using Crontastic;

namespace CrontasticRunnable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Crontastic.Crontastic.Run()); // Prints CRON-Expression made in the UI.
            Console.ReadKey();
        }
    }
}

```

## License
This project is licensed under the MIT License.
