using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Phema.MongoDB.Internal;

namespace Phema.MongoDB
{
	public static class MongoDBExtensions
	{
		public static IMongoDBDatabaseBuilder AddMongoDB(
			this IServiceCollection services,
			MongoClientSettings settings)
		{
			services.TryAddSingleton<IMongoClient>(sp => new MongoClient(settings));

			return new MongoDBDatabaseBuilder(services);
		}

		public static IMongoDBDatabaseBuilder AddMongoDB(
			this IServiceCollection services,
			Action<MongoClientSettings> options)
		{
			var settings = new MongoClientSettings();

			options(settings);

			return services.AddMongoDB(settings);
		}

		public static IMongoDBDatabaseBuilder AddMongoDB(
			this IServiceCollection services,
			string connectionString)
		{
			var settings = MongoClientSettings.FromConnectionString(connectionString);

			return services.AddMongoDB(settings);
		}
	}
}