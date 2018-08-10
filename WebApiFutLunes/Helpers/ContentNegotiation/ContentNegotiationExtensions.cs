using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace WebApiFutLunes.Helpers.ContentNegotiation
{
    public static class ContentNegotiationExtensions
    {
        public static void RemoveXmlFormatter(this HttpConfiguration config)
        {
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes
                    .FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        // Set CamelCase Resolver for JsonFormatter
        public static void SetCamelCaseResolverForJson(this HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}