using System;
using System.Text.RegularExpressions;
using System.ComponentModel.Composition;
using StringSearch.Contracts;

namespace StringSearch.SimpleStringSearcher
{

  [Export(typeof(ISearchable)), 
   ExportMetadata("Name", "SimpleStringSearcher"),
   ExportMetadata("Description", "Usual text search exclusively for the string that the user entered")]
  public class StringSearcher : ISearchable
  {
    public string SearchText(string requiredString, string text)
    {
      string result ="";

      Regex regex = new Regex($"{requiredString}");
      MatchCollection matches = regex.Matches(text);

      if (matches.Count > 0)
      {
        foreach (Match match in matches)
        {
          result = result + "\n" + match;
        }
      }

      return result;
    }
  }
}