using System;
using System.ComponentModel;

namespace StringSearch.Contracts
{
  public interface IPluginMetadata
  {
    string Name { get; }

    [DefaultValue("1, 0, 0, 0")]
    string Version { get; }
    string Description { get; }

    [DefaultValue("D.Fedchenko")]
    string AuthorName { get; }

  }
}