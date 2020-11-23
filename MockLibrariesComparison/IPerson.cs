using System;

namespace MockLibrariesComparison
{
    public interface IPerson
    {
        [ShouldBeCalled]
        DateTime Birthday { get; }

        [ShouldBeCalled]
        string FirstName { get; }

        [ShouldBeCalled]
        double Heigth { get; }

        [ShouldBeCalled]
        string LastName { get; }

        bool IsRelevant { get; }
    }

    public class ShouldBeCalledAttribute : Attribute
    {

    }
}