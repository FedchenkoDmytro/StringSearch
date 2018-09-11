namespace StringSearch.Contracts
{
  public interface ISearchable
  {
    string SearchText(string requiredString, string text);
  }
}