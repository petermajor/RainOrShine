namespace RainOrShine
{
	public class ApiKeyProvider : IApiKeyProvider
	{
		const string Key = "YOUR_KEY_HERE";

		public string Get()
		{
			return Key;
		}
	}

	public interface IApiKeyProvider
	{
		string Get();
	}
}
