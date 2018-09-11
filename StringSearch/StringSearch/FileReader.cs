using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using log4net;
using log4net.Core;
using StringSearch.Contracts;

namespace StringSearchApp
{
  public class FileReader : IFileReader
  {
    private ILog Log;

    [ImportMany]
    public IEnumerable<Lazy<ISearchable, IPluginMetadata>> TextHunters { get; set; }

    public byte[] OpenFile(string path)
    {

      if (!File.Exists(path))
      {
        Log.Warn("File was not found. Incorrect the file path");
        return null;
      }

      using (FileStream fileStream = File.OpenRead(path))
      {
        byte[] array = new byte[fileStream.Length];
        fileStream.Read(array, 0, array.Length);
        return array;
      }
    }

    public string ConvertBytesToText(byte[] fileData)
    {
      if (fileData == null)
      {
        Log.Warn("Input array fileData is null.");
        return "";
      }

      try
      {
        var encodingResult = Encoding.Default.GetString(fileData);
        return encodingResult;
      }
      catch (Exception e)
      {
        Log.Error(e.Message);
      }

      return "";
    }

    public string FindStringInFile(string filePath, string requiredString, ISearchable textHunter)
    {
      if (textHunter == null || string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(requiredString))
      {
        Log.Warn($"Is textHunter null: {textHunter == null}, is filePath null: {string.IsNullOrEmpty(filePath)}, is requiredString null: {string.IsNullOrEmpty(requiredString)}");
        return "";
      }

      var fileData = OpenFile(filePath);
      var text = ConvertBytesToText(fileData);

      Log.Info($"Try to find string '{requiredString}' in the file with path '{filePath}'.");
      return textHunter.SearchText(requiredString, text);
    }

    public string GetPluginsInfo()
    {
      if (TextHunters != null)
      {
        foreach (var plugin in TextHunters)
        {
          return $"\n Search procceded by plugin: \n " +
                                   $"Plugin name: {plugin.Metadata.Name}; \n" +
                                   $"Plugin version: {plugin.Metadata.Version}; \n" +
                                   $"Plugin Description: {plugin.Metadata.Description}; \n" +
                                   $"Plugin author: {plugin.Metadata.AuthorName}; \n" + new string('*', 50);
        }
      }
      Log.Warn($"Plugins were not found.");
      return "";
    }

    public FileReader(ILog logger)
    {
      Log = logger;
    }

  }
}