using System;

namespace MockLibrariesComparison
{
    public class MyClassAuto
    {
        private readonly IGreetingService _greetingService;
        private readonly INameProvider _nameProvider;

        public MyClassAuto(IGreetingService greetingService, INameProvider nameProvider)
        {
            _greetingService = greetingService;
            _nameProvider = nameProvider;
        }

        public void DoGreeting()
        {
            string name = _nameProvider.GetName();
            _greetingService.Greet(name);
        }

        public DateTime GetGreetingDate(string dateString)
        {
            _greetingService.TryParseDate(dateString, out var date);

            return date;
        }
    }
}
