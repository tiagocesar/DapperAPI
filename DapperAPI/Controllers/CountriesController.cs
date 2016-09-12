using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Dapper;
using DapperAPI.Models;

namespace DapperAPI.Controllers
{
	public class CountriesController : ApiController
    {
		private readonly string connectionString = ConfigurationManager.ConnectionStrings["ReceptivoGlobal"].ConnectionString;

		public IEnumerable<Country<CityViewModel>> Get()
		{
			IEnumerable<Country<CityViewModel>> countries = null;

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				{
					var sql = @"
						SELECT cn.*, ct.Id AS Cities_Id, ct.Name AS Cities_Name
						  FROM Countries cn
						  LEFT JOIN Cities ct ON ct.CountryId = cn.Id";

					var tmp = connection.Query<dynamic>(sql);

					Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Country<CityViewModel>), new List<string> { "Id" });
					Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(CityViewModel), new List<string> { "Id" });

					countries = Slapper.AutoMapper.MapDynamic<Country<CityViewModel>>(tmp).AsList<Country<CityViewModel>>();
				}
			}

			return countries;
		}
    }
}