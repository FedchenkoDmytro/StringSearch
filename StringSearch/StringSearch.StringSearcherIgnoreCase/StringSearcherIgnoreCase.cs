using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using StringSearch.Contracts;

namespace StringSearch.StringSearcherIgnoreCaseExtension
{
  [Export(typeof(ISearchable)), 
   ExportMetadata("Name", "StringSearcherIgnoreCase"),
   ExportMetadata("Description", "Search for text without case and the number of spaces in the search string")]
  public class StringSearcherIgnoreCase : ISearchable
  {
    public string SearchText(string requiredString, string text)
    {
      string result = "";
      string pattern = Regex.Replace($"({requiredString})", "\\s+",  "(\\s*)");

      MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

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