using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebAdvert.SearchWorker
{
    internal class ConfigurationHelper
    {
        private static readonly IConfiguration? configuration = null;

        internal static IConfiguration Instance => configuration ?? new ConfigurationBuilder()
                                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                                        .AddJsonFile("appsettings.json")
                                                                        .Build();
    }
}
