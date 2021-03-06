using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bzway.Writer.App
{
    public class RunCommand : ICommand
    {
        public string Name => "run";
        public string Usage => @"
Command: run
    -t, --template = VALUE       The templating engine to use
    -p, --port = VALUE           The port to test the site locally
        --cleantarget            Delete the target directory(_site by default)
        --vDir = VALUE           Rewrite url's to work inside the specified virtual directory
";
        public void Execute(IEnumerable<string> args)
        {
            var root = Directory.GetCurrentDirectory();
            var server = new Site();
            bool canRun = !File.Exists(server.ProcessFile);
            if (!canRun)
            {
                using (var stream = File.OpenText(server.ProcessFile))
                {
                    int pid = 0;
                    if (int.TryParse(stream.ReadLine(), out pid))
                    {
                        var process = Process.GetProcesses().FirstOrDefault(m => m.Id == pid);
                        canRun = (process == null);
                    }
                }
            }
            if (canRun)
            {
                using (var stream = File.CreateText(server.ProcessFile))
                {
                    stream.Write(Process.GetCurrentProcess().Id);
                };
                var site = new Site();
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls(server.HostUrl)
                    .UseContentRoot(site.PublicDirectory)
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton<Site>(site);
                    })
                    .Build();
                host.Run();
                File.Delete(server.ProcessFile);
            }
        }
    }
}