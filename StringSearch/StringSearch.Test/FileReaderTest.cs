using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringSearchApp.Properties;

namespace StringSearch.Test
{
  [TestClass]
  public class FileReaderTest : FileReaderTestInitializer
  {
    [TestMethod]
    public void SearchTextTest()
    {
      string searchResult = "";

      if (InitializeComponents(FileReader))
      {
        foreach (var textHunter in FileReader.TextHunters)
        {
          searchResult += FileReader.FindStringInFile(@"D:\Repositories\StringSearch\StringSearch\TestText2.fsdfa", "1c 2b    3A", textHunter.Value);
        }
      }

      Assert.IsFalse(string.IsNullOrEmpty(searchResult));
    }


  }
}
