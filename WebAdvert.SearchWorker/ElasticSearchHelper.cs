using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdvert.SearchWorker
{
    internal class ElasticSearchHelper
    {
        private static IElasticClient? elasticClient;

        internal static IElasticClient GetInstance(IConfiguration configuration)
        {
            if (elasticClient == null)
            {
                var url = configuration.GetSection("ES").GetValue<string>("url");
                var settings = new ConnectionSettings(new Uri(url)).DefaultIndex("adverts").DefaultMappingFor<AdvertType>(m => m.IdProperty(x => x.Id));
                elasticClient = new ElasticClient(settings);
            }

            return elasticClient;
        }
    }
}
