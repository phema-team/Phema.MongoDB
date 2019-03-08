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
	
	internal sealed class MongoDBColletionBuilder : IMongoDBCollectionBuilder
	{
		private readonly IServiceCollection services;
		private readonly string database;

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
			
			services.TryAddScoped(sp =>
			{
				var factory = sp.GetRequiredService<IOptions<MongoDBOptions>>()
					.Value
					.Databases[database];

				return factory(sp).GetCollection<TEntity>(collection, settings);
			});
			
			return this;
		}
	}
}