using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Utilities.Misc;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace MultikeyedTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var keys1 = new[] { "Test", "Test2", "test3", "test4", "test5", "test6" };
            var keys2 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var keys3 = new[] { 0.0, 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9 };
            var keys4 = new[] { CultureIdentifiers.English, CultureIdentifiers.Dutch, CultureIdentifiers.English_Australia, CultureIdentifiers.English_Canada, CultureIdentifiers.Japanese_Japan };
            var keys5 = new[] { DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddMonths(1), DateTime.Now.AddYears(1), DateTime.Now.AddSeconds(1) };
#if MKD_FIVE
            Console.WriteLine("\nFive");
            Console.WriteLine("New: {0}", MiscHelperMethods.TimeAction(() => FiveTest.TryNewMethod(keys1, keys2, keys3, keys4, keys5)));
            Console.WriteLine("Old: {0}", MiscHelperMethods.TimeAction(() => FiveTest.TryStandardMethod(keys1, keys2, keys3, keys4, keys5)));
#endif
            Console.WriteLine("\nFour");
            Console.WriteLine("New: {0}", MiscHelperMethods.TimeAction(() => FourTest.TryNewMethod(keys1, keys2, keys3, keys4, keys5)));
            Console.WriteLine("Old: {0}", MiscHelperMethods.TimeAction(() => FourTest.TryStandardMethod(keys1, keys2, keys3, keys4, keys5)));
        }

    }
}
