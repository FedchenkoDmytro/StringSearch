using System;
using System.Collections.Generic;

using System.IO;
using System.Text;
using StringSearch.Contracts;
namespace StringSearch.Contracts
{
  public interface IFileReader
  {
    IEnumerable<Lazy<ISearchable, IPluginMetadata>> TextHunters { get; set; }
    byte[] OpenFile(string path);
    string ConvertBytesToText(byte[] fileData);
    string FindStringInFile(string filePath, string requiredString, ISearchable textHunter);
    string GetPluginsInfo();
  }
}