using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
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
        private static MultikeyedDictionary<RuntimeCoreType, RuntimeCoreType, RuntimeCoreType> IntrinsicOperationResults = ConstructIntrinsicOperationResultTypes();


        internal static RuntimeCoreType GetIntrinsicBinaryOperationResult(IType left, IType right)
        {
            
            RuntimeCoreType result;
            var tcL = left.GetCoreType();
            var tcR = right.GetCoreType();
            if (IntrinsicOperationResults.TryGetValue(tcL, tcR, out result))
                return result;
            return RuntimeCoreType.None;
        }

        private static MultikeyedDictionary<RuntimeCoreType, RuntimeCoreType, RuntimeCoreType> ConstructIntrinsicOperationResultTypes()
        {
            var unorderedIntrinsicResults = new MultikeyedDictionary<RuntimeCoreType, RuntimeCoreType, RuntimeCoreType>();
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Byte, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.UInt16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.UInt32, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.UInt64, RuntimeCoreType.UInt64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Single, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Char, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.SByte, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Int16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Byte, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.UInt16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.UInt32, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.UInt64, RuntimeCoreType.UInt64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Single, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Char, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.SByte, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Int16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt16, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.UInt32, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.UInt64, RuntimeCoreType.UInt64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Single, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Char, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.SByte, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Int16, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Int32, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt32, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.UInt32, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.UInt64, RuntimeCoreType.UInt64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.Single, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.Char, RuntimeCoreType.UInt32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.UInt64, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Single, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Char, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.SByte, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Int16, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Int32, RuntimeCoreType.Single);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Single, RuntimeCoreType.Int64, RuntimeCoreType.Single);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.Double, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.Char, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.SByte, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.Int16, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.Int32, RuntimeCoreType.Double);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Double, RuntimeCoreType.Int64, RuntimeCoreType.Double);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.Char, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.SByte, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.Int16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Char, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.SByte, RuntimeCoreType.SByte, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.SByte, RuntimeCoreType.Int16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.SByte, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.SByte, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.SByte, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Int16, RuntimeCoreType.Int16, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int16, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int16, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int16, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Int32, RuntimeCoreType.Int32, RuntimeCoreType.Int32);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int32, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int32, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Int64, RuntimeCoreType.Int64, RuntimeCoreType.Int64);
            unorderedIntrinsicResults.Add(RuntimeCoreType.Int64, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            unorderedIntrinsicResults.Add(RuntimeCoreType.Decimal, RuntimeCoreType.Decimal, RuntimeCoreType.Decimal);

            foreach (var keysValue in unorderedIntrinsicResults.ToArray())
            {
                RuntimeCoreType dummyType;
                if (!unorderedIntrinsicResults.TryGetValue(keysValue.Keys.Key2, keysValue.Keys.Key1, out dummyType))
                    unorderedIntrinsicResults.Add(keysValue.Keys.Key2, keysValue.Keys.Key1, unorderedIntrinsicResults[keysValue.Keys.Key1, keysValue.Keys.Key2]);
            }
            var ksvsSet =
                from ksv in unorderedIntrinsicResults
                orderby ksv.Keys.Key1, ksv.Keys.Key2
                select ksv;
            var orderedIntrinsicResults = new MultikeyedDictionary<RuntimeCoreType, RuntimeCoreType, RuntimeCoreType>();
            foreach (var element in ksvsSet)
                orderedIntrinsicResults.Add(element.Keys.Key1, element.Keys.Key2, element.Value);
            return orderedIntrinsicResults;
        }



    }
}
