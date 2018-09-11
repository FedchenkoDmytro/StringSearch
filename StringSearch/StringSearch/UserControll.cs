using System;
using StringSearch.Contracts;

namespace StringSearchApp
{
  public class UserControll
  {
    private IFileReader _fileReader;

    public void Run()
    {
      string filePath = "";
      string requiredText = "";

      bool isUserDataValid = false;

      while (!isUserDataValid)
      {
        Console.WriteLine("Please enter the file path:");
        filePath = Console.ReadLine();

        Console.WriteLine("Please enter required string to search in the file:");
        requiredText = Console.ReadLine();

        if (string.IsNullOrEmpty(filePath))
        {
          Console.WriteLine(new string('-', 40));
          Console.WriteLine("File path is empty.");
        }
        else if (string.IsNullOrEmpty(requiredText))
        {
          Console.WriteLine("Required text is empty.");
        }

        isUserDataValid = true;
      }

      FindTextByAllAvailableTextHunters(filePath, requiredText);
      Console.ReadLine();
    }

    private void FindTextByAllAvailableTextHunters(string filePath, string requiredText)
    {
      foreach (var stringHunter in _fileReader.TextHunters)
      {
        Console.WriteLine(new string('-', 40) + "\n");

        try
        {
          string hunterResult = _fileReader.FindStringInFile(filePath, requiredText, stringHunter.Value);

          if (!string.IsNullOrEmpty(hunterResult))
          {
            Console.WriteLine($"\n Search procceded by plugin: \n " +
                              $"Plugin name: {stringHunter.Metadata.Name}; \n" +
                              $"Plugin version: {stringHunter.Metadata.Version}; \n" +
                              $"Plugin Description: {stringHunter.Metadata.Description}; \n" +
                              $"Plugin author: {stringHunter.Metadata.AuthorName}; \n");
            Console.WriteLine(" Result: \n");
            Console.WriteLine(hunterResult);
            Console.WriteLine(new string('.', 40) + "\n");
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
        }
      }
    }

    public UserControll(IFileReader fileReader)
    {
      _fileReader = fileReader;
    }
  }
}