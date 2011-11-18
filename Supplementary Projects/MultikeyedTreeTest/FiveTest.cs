using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Miscellaneous;
using AllenCopeland.Abstraction.Globalization;
#if MKD_FIVE
namespace MultikeyedTreeTest
{
    class FiveTest
    {

        public static void TryStandardMethod(string[] keys1, int[] keys2, double[] keys3, ICultureIdentifier[] keys4, DateTime[] keys5)
        {
            for (int i = 0; i < 1000; i++)
                StandardMethod(keys1, keys2, keys3, keys4, keys5);
        }

        public static void TryNewMethod(string[] keys1, int[] keys2, double[] keys3, ICultureIdentifier[] keys4, DateTime[] keys5)
        {
            for (int i = 0; i < 1000; i++)
                NewMethod(keys1, keys2, keys3, keys4, keys5);
        }

        private static Word[] NewMethod(string[] keys1, int[] keys2, double[] keys3, ICultureIdentifier[] keys4, DateTime[] keys5)
        {
            MultikeyedTree2<string, int, double, ICultureIdentifier, DateTime, Word> newMethod = new MultikeyedTree2<string, int, double, ICultureIdentifier, DateTime, Word>();
            Word[] results = new Word[keys1.Length * keys2.Length * keys3.Length * keys4.Length * keys5.Length];
            for (int i = 0, n = 0; i < keys1.Length; i++)
                for (int j = 0; j < keys2.Length; j++)
                    for (int k = 0; k < keys3.Length; k++)
                        for (int l = 0; l < keys4.Length; l++)
                            for (int m = 0; m < keys5.Length; m++)
                            {
                                var key1 = keys1[i];
                                var key2 = keys2[j];
                                var key3 = keys3[k];
                                var key4 = keys4[l];
                                var key5 = keys5[m];
                                Word current;
                                if (!newMethod.TryGetValue(key1, key2, key3, key4, key5, out current))
                                    newMethod.Add(key1, key2, key3, key4, key5, current = new Word((ushort)(i + j + k + l + m)));
                                results[n++] = current;
                            }
            return results;
        }

        private static Word[] StandardMethod(string[] keys1, int[] keys2, double[] keys3, ICultureIdentifier[] keys4, DateTime[] keys5)
        {
            Dictionary<string, Dictionary<int, Dictionary<Double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>>>> standardMethod = new Dictionary<string, Dictionary<int, Dictionary<double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>>>>();
            Word[] results = new Word[keys1.Length * keys2.Length * keys3.Length * keys4.Length * keys5.Length];
            for (int i = 0, n = 0; i < keys1.Length; i++)
                for (int j = 0; j < keys2.Length; j++)
                    for (int k = 0; k < keys3.Length; k++)
                        for (int l = 0; l < keys4.Length; l++)
                            for (int m = 0; m < keys5.Length; m++)
                            {
                                var key1 = keys1[i];
                                var key2 = keys2[j];
                                var key3 = keys3[k];
                                var key4 = keys4[l];
                                var key5 = keys5[m];
                                Dictionary<int, Dictionary<Double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>>> level2;
                                if (!standardMethod.TryGetValue(key1, out level2))
                                    standardMethod.Add(key1, level2 = new Dictionary<int, Dictionary<double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>>>());
                                Dictionary<Double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>> level3;
                                if (!level2.TryGetValue(key2, out level3))
                                    level2.Add(key2, level3 = new Dictionary<double, Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>>());
                                Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>> level4;
                                if (!level3.TryGetValue(key3, out level4))
                                    level3.Add(key3, level4 = new Dictionary<ICultureIdentifier, Dictionary<DateTime, Word>>());
                                Dictionary<DateTime, Word> level5;
                                if (!level4.TryGetValue(key4, out level5))
                                    level4.Add(key4, level5 = new Dictionary<DateTime, Word>());
                                Word current;
                                if (!level5.TryGetValue(key5, out current))
                                    level5.Add(key5, current = new Word((ushort)(i + j + k + l + m)));
                                results[n++] = current;
                            }
            return results;
        }
    }
}
#endif