using System.Collections.Generic;

namespace MockLibrariesComparison
{
    public class PersonHasher
    {
        public static int GetHashCode(IPerson person)
        {
            int hashCode = 383198026;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(person.FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(person.LastName);
            hashCode = hashCode * -1521134295 + person.Birthday.GetHashCode();
            hashCode = hashCode * -1521134295 + person.Heigth.GetHashCode();
            return hashCode;
        }

        public static int GetIncompleteHashCode(IPerson person)
        {
            int hashCode = 383198026;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(person.LastName);
            hashCode = hashCode * -1521134295 + person.Birthday.GetHashCode();
            hashCode = hashCode * -1521134295 + person.Heigth.GetHashCode();
            return hashCode;
        }

        public static int GetToMuchHashCode(IPerson person)
        {
            int hashCode = 383198026;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(person.FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(person.LastName);
            hashCode = hashCode * -1521134295 + person.Birthday.GetHashCode();
            hashCode = hashCode * -1521134295 + person.Heigth.GetHashCode();
            bool b = person.IsRelevant;
            return hashCode;
        }
    }
}
