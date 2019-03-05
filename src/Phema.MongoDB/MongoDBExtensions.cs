using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MongoDB.Driver;

namespace Phema.MongoDB
{
	public static class MongoDBExtensions
	{
		public static IMongoDBDatabaseConfiguration AddPhemaMongoDB(this IServiceCollection services, MongoClientSettings settings)
		{
			services.TryAddSingleton<IMongoClient>(sp => new MongoClient(settings));

			return new MongoDBDatabaseConfiguration(services);
		}
		
		public static IMongoDBDatabaseConfiguration AddPhemaMongoDB(
			this IServiceCollection services,
			Action<MongoClientSettings> options)
		{
			var settings = new MongoClientSettings();

			options(settings);

			return services.AddPhemaMongoDB(settings);
		}
		
		public static IMongoDBDatabaseConfiguration AddPhemaMongoDB(
			this IServiceCollection services,
			string connectionString)
		{
			var settings = MongoClientSettings.FromConnectionString(connectionString);
			
			return services.AddPhemaMongoDB(settings);
		}
	}
}