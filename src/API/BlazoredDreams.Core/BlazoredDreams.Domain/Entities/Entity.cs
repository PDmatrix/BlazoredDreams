using System;

namespace BlazoredDreams.Domain.Entities
{
	public class Entity<T> where T : struct
	{
		public T Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}