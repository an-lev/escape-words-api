namespace ExploreWords.WebApi
{
	public class Word
	{
		public Guid Id { get; set; }
		public Guid ModuleId { get; set; }
		public Module Module { get; set; }
		public string Text { get; set; }
		public string Translate { get; set; }
	}
}
