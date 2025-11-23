using System;
using System.Diagnostics;
using BookAChristmasHam.Models;
using Spectre.Console;

namespace BookAChristmasHam.UI.Helper
{
    public static class LoadingUI
    {

        // Visar inloggningsstatus med färg beroende på användartyp
        public static void ShowLoginStatus(User user)
        {
            var color = user.Type switch
            {
                UserType.Business => "yellow",
                UserType.Private => "cyan",
                _ => "blue" // fallback för okända typer
            };

            AnsiConsole.MarkupLine($"[bold {color}]Logged in as {user.Name} ({user.Type})[/]");
        }


        public static void RunWithSpinner(string message, Action action)
        {
            
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("green"))
                .Start(message, ctx =>
                {
                    var stopwatch = Stopwatch.StartNew();
                    action();
                    stopwatch.Stop();
                    Thread.Sleep(1000); // ge spinnern tid att synas
                });


        }







    }
}
