using System.Collections.Generic;

namespace MockLibrariesComparison
{
    public interface INameProvider
    {
        string GetName();
        IEnumerable<string> Genders { get; }
    }
}
