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
        private static MultikeyedDictionary<TypeCode, TypeCode, TypeCode> IntrinsicOperationResults = ConstructIntrinsicOperationResultTypes();


        internal static TypeCode GetIntrinsicBinaryOperationResult(IType left, IType right)
        {
            TypeCode result;
            var tcL = left.GetTypeCode();
            var tcR = right.GetTypeCode();
            if (IntrinsicOperationResults.TryGetValue(tcL, tcR, out result))
                return result;
            return TypeCode.Empty;
        }

        private static MultikeyedDictionary<TypeCode, TypeCode, TypeCode> ConstructIntrinsicOperationResultTypes()
        {
            var unorderedIntrinsicResults = new MultikeyedDictionary<TypeCode, TypeCode, TypeCode>();
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Byte, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.UInt16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.UInt32, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.UInt64, TypeCode.UInt64);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Single, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Char, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.SByte, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Int16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.Byte, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.UInt16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.UInt64, TypeCode.UInt64);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Single, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Char, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.SByte, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Int16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.UInt16, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.UInt32, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.UInt64, TypeCode.UInt64);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Single, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Char, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.SByte, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Int16, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Int32, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.UInt32, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.UInt32, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.UInt64, TypeCode.UInt64);
            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.Single, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.Char, TypeCode.UInt32);
            unorderedIntrinsicResults.Add(TypeCode.UInt64, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Single, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Char, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.SByte, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Int16, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Int32, TypeCode.Single);
            unorderedIntrinsicResults.Add(TypeCode.Single, TypeCode.Int64, TypeCode.Single);

            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.Double, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.Char, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.SByte, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.Int16, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.Int32, TypeCode.Double);
            unorderedIntrinsicResults.Add(TypeCode.Double, TypeCode.Int64, TypeCode.Double);

            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.Char, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.SByte, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.Int16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.Char, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.SByte, TypeCode.SByte, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.SByte, TypeCode.Int16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.SByte, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.SByte, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.SByte, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.Int16, TypeCode.Int16, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Int16, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Int16, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.Int16, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.Int32, TypeCode.Int32, TypeCode.Int32);
            unorderedIntrinsicResults.Add(TypeCode.Int32, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.Int32, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.Int64, TypeCode.Int64, TypeCode.Int64);
            unorderedIntrinsicResults.Add(TypeCode.Int64, TypeCode.Decimal, TypeCode.Decimal);

            unorderedIntrinsicResults.Add(TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal);

            foreach (var keysValue in unorderedIntrinsicResults.ToArray())
            {
                TypeCode dummyType;
                if (!unorderedIntrinsicResults.TryGetValue(keysValue.Keys.Key2, keysValue.Keys.Key1, out dummyType))
                    unorderedIntrinsicResults.Add(keysValue.Keys.Key2, keysValue.Keys.Key1, unorderedIntrinsicResults[keysValue.Keys.Key1, keysValue.Keys.Key2]);
            }
            var ksvsSet =
                from ksv in unorderedIntrinsicResults
                orderby ksv.Keys.Key1, ksv.Keys.Key2
                select ksv;
            var orderedIntrinsicResults = new MultikeyedDictionary<TypeCode, TypeCode, TypeCode>();
            foreach (var element in ksvsSet)
                orderedIntrinsicResults.Add(element.Keys.Key1, element.Keys.Key2, element.Value);
            return orderedIntrinsicResults;
        }



    }
}
