using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BlazoredDreams.Data
{
	public class TagRepository : IRepository<Tag>
	{
		private readonly IConfiguration _configuration;

		private IDbConnection Connection => 
			new NpgsqlConnection(_configuration.GetConnectionString("Source"));

		public TagRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		
		public IEnumerable<Tag> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public Tag Get(long id)
		{
			using (var conn = Connection)
			{
				return conn.Query<Tag>("SELECT * From tag WHERE id = @id", new {id}).FirstOrDefault();
			}
		}

		public void Insert(Tag entity)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Tag entity)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(Tag entity)
		{
			throw new System.NotImplementedException();
		}
	}
}