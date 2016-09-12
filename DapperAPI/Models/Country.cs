using System.Collections.Generic;

namespace DapperAPI.Models
{
	public class Country<T>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<T> Cities { get; set; }
	}
}