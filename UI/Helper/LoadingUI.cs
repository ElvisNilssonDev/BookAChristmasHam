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




        // Runs an action with a spinner
        public static void RunWithSpinner(string message, Action action)
        {
            AnsiConsole.Status()
                .Start(message, ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);         // Choose spinner style
                    ctx.SpinnerStyle(Style.Parse("green"));  // Set spinner color


                    var stopwatch = Stopwatch.StartNew();
                    action();
                    stopwatch.Stop();

                    if (stopwatch.ElapsedMilliseconds < 1000)
                        Thread.Sleep(1000); // spinnern får tid att avslutas snyggt



                    ctx.Status("[green]Done![/]");           // Show final status
                });

        }







    }
}
