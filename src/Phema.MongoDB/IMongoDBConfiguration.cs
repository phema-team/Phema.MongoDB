using System;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

namespace Phema.MongoDB
{
	public interface IMongoDBDatabaseConfiguration
	{
		IMongoDBDatabaseConfiguration AddDatabase(
			string database,
			Action<IMongoDBCollectionConfiguration> configuration,
			Action<MongoDatabaseSettings> options = null);
	}
	
	internal sealed class MongoDBDatabaseConfiguration : IMongoDBDatabaseConfiguration
	{
		private readonly IServiceCollection services;

		public MongoDBDatabaseConfiguration(IServiceCollection services)
		{
			this.services = services;
		}

		public IMongoDBDatabaseConfiguration AddDatabase(
			string database,
			Action<IMongoDBCollectionConfiguration> configuration,
			Action<MongoDatabaseSettings> options = null)
		{
			var settings = new MongoDatabaseSettings();
			
			options?.Invoke(settings);
			
			services.Configure<MongoDBOptions>(o =>
				o.Databases.Add(database, sp => sp.GetRequiredService<IMongoClient>()
					.GetDatabase(database, settings)));

			configuration.Invoke(new MongoDBColletionConfiguration(services, database));
			
			return this;
		}
	}
}