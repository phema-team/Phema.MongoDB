using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Phema.MongoDB
{
	public interface IMongoDBCollectionBuilder
	{
		IMongoDBCollectionBuilder AddCollection<TEntity>(
			string collection,
			Action<MongoCollectionSettings> options = null);
	}
}

namespace Phema.MongoDB.Internal
{
	internal sealed class MongoDBColletionBuilder : IMongoDBCollectionBuilder
	{
		private readonly string database;
		private readonly IServiceCollection services;

		public MongoDBColletionBuilder(IServiceCollection services, string database)
		{
			this.services = services;
			this.database = database;
		}

		public IMongoDBCollectionBuilder AddCollection<TEntity>(
			string collection,
			Action<MongoCollectionSettings> options = null)
		{
			var settings = new MongoCollectionSettings();

			options?.Invoke(settings);

			services.TryAddScoped(
				sp => sp.GetRequiredService<IOptions<MongoDBOptions>>()
					.Value
					.Databases[database]
					.Invoke(sp)
					.GetCollection<TEntity>(collection, settings));

			return this;
		}
	}
}