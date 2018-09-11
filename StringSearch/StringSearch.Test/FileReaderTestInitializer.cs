using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using StringSearch.Test.Properties;
using StringSearchApp;

namespace StringSearch.Test
{
  public class FileReaderTestInitializer
  {
    static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public FileReader FileReader { get;}
    public bool InitializeComponents(FileReader fileReader)
    {
      if (fileReader == null)
        return false;

      var catalog = new DirectoryCatalog(Settings.Default.AddInDirectory);
      var container = new CompositionContainer(catalog);

      try
      {
        container.ComposeParts(fileReader);
      }
      catch (ChangeRejectedException ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }

      return true;
    }
    public FileReaderTestInitializer()
    {
      FileReader = new FileReader(Log);
    } 
  }
}