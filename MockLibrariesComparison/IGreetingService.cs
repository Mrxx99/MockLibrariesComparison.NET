using System;

namespace MockLibrariesComparison
{
    public interface IGreetingService
    {
        void Greet(string name);
        bool TryParseDate(string dateString, out DateTime date);
    }
}
