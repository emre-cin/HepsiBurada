using HepsiBurada.Core.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HepsiBurada.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.ConfigureServices();

            var hostedService = Startup.ServiceProvider.GetService<IHostedService>();
            hostedService.StartAsync(new System.Threading.CancellationToken());

            string command = string.Empty;

            do
            {
                string input = Console.ReadLine();

                var commArray = input.Split(null).Where(x => x != "").ToArray();

                bool hasCommand = false;

                if (commArray.Any())
                {
                    command = commArray[0];

                    hasCommand = Dispatcher.CommandSet.TryGetValue(command, out Delegate @delegate);

                    if (hasCommand)
                    {
                        var parameters = new List<object>();

                        for (int i = 1; i < commArray.Length; i++)
                            parameters.Add(commArray[i]);

                        try
                        {
                            dynamic result = Dispatcher.CallMethod(@delegate.Method.Name, @delegate.Method, parameters) as IReturn;

                            if (result != null)
                                Console.WriteLine(result.Message);
                        }
                        catch
                        {
                            Console.WriteLine("Incorrect command usage!");
                        }
                    }
                    else
                        Console.WriteLine("Command not found!");
                }

            } while (command != "exit" || command != "close");
        }
    }
}
