using AdvertApi.Models.Messages;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Amazon.Lambda.SNSEvents;
using Nest;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    
    public class SearchWorker
    {
        private readonly IElasticClient elasticClient;

        public SearchWorker(): this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {

        }

        public SearchWorker(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async void Function(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach(var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);

                var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDocument = MappingHelper.Map(message);
                await elasticClient.IndexDocumentAsync(advertDocument);
            }
        }
    }
}