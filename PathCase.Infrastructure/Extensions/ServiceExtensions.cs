using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PathCase.Core.Entities;

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
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            var collection = db.GetCollection<ChatRoom>(nameof(ChatRoom));



            var isThereRoom = collection.Find(Builders<ChatRoom>.Filter.Empty).Any();
            if (!isThereRoom)
            {
                for (int i = 1; i <= 20; i++)
                {
                    ChatRoom ch = new ChatRoom()
                    {
                        Name = "Room " + (i < 10 ? "0" + i : i)
                    };
                    collection.InsertOne(ch);
                }
            }
            return services;
        }
    }
}