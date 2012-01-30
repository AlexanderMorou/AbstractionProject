using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{

    public class IntermediateCompilerBase 
    {
        private static MultikeyedDictionary<IType, IType, IType> IntrinsicOperationResults = ConstructIntrinsicOperationResultTypes();


        internal static IType GetIntrinsicBinaryOperationResult(IType left, IType right)
        {
            IType result;
            if (IntrinsicOperationResults.TryGetValue(left, right, out result))
                return result;
            return null;
        }

        private static MultikeyedDictionary<IType, IType, IType> ConstructIntrinsicOperationResultTypes()
        {
            var unorderedIntrinsicResults = new MultikeyedDictionary<IType, IType, IType>();
            IType intrinsicUInt8 = typeof(byte).GetTypeReference();
            IType intrinsicUInt16 = typeof(ushort).GetTypeReference();
            IType intrinsicUInt32 = typeof(uint).GetTypeReference();
            IType intrinsicUInt64 = typeof(ulong).GetTypeReference();
            IType intrinsicSingle = typeof(float).GetTypeReference();
            IType intrinsicDouble = typeof(double).GetTypeReference();
            IType intrinsicChar = typeof(char).GetTypeReference();
            IType intrinsicInt8 = typeof(sbyte).GetTypeReference();
            IType intrinsicInt16 = typeof(short).GetTypeReference();
            IType intrinsicInt32 = typeof(int).GetTypeReference();
            IType intrinsicInt64 = typeof(long).GetTypeReference();
            IType intrinsicDecimal = typeof(decimal).GetTypeReference();
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicUInt8, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicUInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicUInt32, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicUInt64, intrinsicUInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicSingle, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicChar, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicInt8, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt8, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicUInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicUInt32, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicUInt64, intrinsicUInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicSingle, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicChar, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicInt8, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt16, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicUInt32, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicUInt64, intrinsicUInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicSingle, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicChar, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicInt8, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicInt16, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicInt32, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt32, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicUInt32, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicUInt64, intrinsicUInt64);
            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicSingle, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicChar, intrinsicUInt32);
            unorderedIntrinsicResults.Add(intrinsicUInt64, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicSingle, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicChar, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicInt8, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicInt16, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicInt32, intrinsicSingle);
            unorderedIntrinsicResults.Add(intrinsicSingle, intrinsicInt64, intrinsicSingle);

            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicDouble, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicChar, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicInt8, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicInt16, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicInt32, intrinsicDouble);
            unorderedIntrinsicResults.Add(intrinsicDouble, intrinsicInt64, intrinsicDouble);

            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicChar, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicInt8, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicChar, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicInt8, intrinsicInt8, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt8, intrinsicInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt8, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt8, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicInt8, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicInt16, intrinsicInt16, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt16, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt16, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicInt16, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicInt32, intrinsicInt32, intrinsicInt32);
            unorderedIntrinsicResults.Add(intrinsicInt32, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicInt32, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicInt64, intrinsicInt64, intrinsicInt64);
            unorderedIntrinsicResults.Add(intrinsicInt64, intrinsicDecimal, intrinsicDecimal);

            unorderedIntrinsicResults.Add(intrinsicDecimal, intrinsicDecimal, intrinsicDecimal);

            foreach (var keysValue in unorderedIntrinsicResults.ToArray())
            {
                IType dummyType;
                if (!unorderedIntrinsicResults.TryGetValue(keysValue.Keys.Key2, keysValue.Keys.Key1, out dummyType))
                    unorderedIntrinsicResults.Add(keysValue.Keys.Key2, keysValue.Keys.Key1, unorderedIntrinsicResults[keysValue.Keys.Key1, keysValue.Keys.Key2]);
            }
            var ksvsSet =
                from ksv in unorderedIntrinsicResults
                orderby ksv.Keys.Key1.UniqueIdentifier.ToString(), ksv.Keys.Key2.UniqueIdentifier.ToString()
                select ksv;
            var orderedIntrinsicResults = new MultikeyedDictionary<IType, IType, IType>();
            foreach (var element in ksvsSet)
                orderedIntrinsicResults.Add(element.Keys.Key1, element.Keys.Key2, element.Value);
            return orderedIntrinsicResults;
        }


    }
}
