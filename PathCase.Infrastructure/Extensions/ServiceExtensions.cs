using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace PathCase.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, string connectionString,
            string databaseName)
        {
            services.AddScoped<IMongoClient, MongoClient>(provider =>
            {
                return new MongoClient(connectionString);
            });
            services.AddScoped(provider =>
            {
                return provider.GetRequiredService<IMongoClient>().GetDatabase(databaseName);
            });

            return services;
        }
    }
}