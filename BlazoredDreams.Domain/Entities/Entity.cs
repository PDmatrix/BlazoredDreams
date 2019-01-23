using System;

namespace BlazoredDreams.Domain.Entities
{
	public abstract class Entity<T> where T : struct
	{
		public T Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime DeletedAt { get; set; }
	}
}