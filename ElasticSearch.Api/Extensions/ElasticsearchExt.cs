using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearch.Api.Extensions
{
    public static class ElasticsearchExt
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            /*NEST LIBRARY*/
            //var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            //var settings = new ConnectionSettings(pool);
            //var client = new ElasticClient(settings);

            /*ElasticsearchClient*/
            string userName = configuration.GetSection("Elastic")["UserName"];
            string password = configuration.GetSection("Elastic")["Password"];
            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!)).Authentication(new BasicAuthentication(userName, password));
            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client); //Elastic Search singleton olarak kullanmanızı tavsiye ediyor.
        }
    }
}
