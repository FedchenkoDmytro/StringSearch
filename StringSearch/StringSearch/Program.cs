using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using log4net.Core;
using StringSearch.Contracts;
using StringSearchApp.Properties;

namespace StringSearchApp
{
  class Program
  {
    static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    static void Main(string[] args)
    {
      FileReader fileReader = new FileReader(Log);

      if (InitializeComponents(fileReader))
      {
        UserControll userControll = new UserControll(fileReader);
        userControll.Run();
      }
    }

    //Used the MEF framework for the extensibility architecture
    //More details by link: https://docs.microsoft.com/en-us/dotnet/framework/mef/
    static bool InitializeComponents(FileReader fileReader)
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


  }
}
