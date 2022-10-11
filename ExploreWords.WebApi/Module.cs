namespace ExploreWords.WebApi
{
	public class Module
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public ICollection<Word> Words { get; set; }
	}
}
