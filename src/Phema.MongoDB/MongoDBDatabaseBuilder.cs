using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Phema.MongoDB
{
	public interface IMongoDBDatabaseBuilder
	{
		IMongoDBDatabaseBuilder AddDatabase(
			string database,
			Action<IMongoDBCollectionBuilder> builder,
			Action<MongoDatabaseSettings> options = null);
	}
}

namespace Phema.MongoDB.Internal
{
	internal sealed class MongoDBDatabaseBuilder : IMongoDBDatabaseBuilder
	{
		private readonly IServiceCollection services;

		public MongoDBDatabaseBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public IMongoDBDatabaseBuilder AddDatabase(
			string database,
			Action<IMongoDBCollectionBuilder> builder,
			Action<MongoDatabaseSettings> options = null)
		{
			var settings = new MongoDatabaseSettings();

			options?.Invoke(settings);

			services.Configure<MongoDBOptions>(
				o => o.Databases.Add(database, 
					sp => sp.GetRequiredService<IMongoClient>().GetDatabase(database, settings)));

			builder.Invoke(new MongoDBColletionBuilder(services, database));

			return this;
		}
	}
}