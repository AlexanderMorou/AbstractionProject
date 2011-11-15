using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Metadata;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides an attribute for associating a type-parameter to a 
    /// structure for inference of type parameter constructors, methods, properties
    /// indexers, and events.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.GenericParameter)]
    public class GenericParamDataTargetAttribute :
        Attribute
    {
        private Type genericLocalType;
        /// <summary>
        /// Creates a new <see cref="GenericParamDataTargetAttribute"/>
        /// instance with the <paramref name="genericLocalType"/>.
        /// </summary>
        /// <param name="genericLocalType">The <see cref="Type"/> relative
        /// to the local of the generic that defines the signatures of the
        /// constructors to use.</param>
        public GenericParamDataTargetAttribute(Type genericLocalType)
        {
            if (genericLocalType == null)
                throw new ArgumentNullException("genericLocalType");
            /* *
             * Verify that the type is properly structured...
             * */
            Type t = genericLocalType;
            if (t.IsGenericType)
            {
                if (!(t.IsGenericTypeDefinition))
                    t = t.GetGenericTypeDefinition();
                Type tDecl = t.DeclaringType;
                if (!tDecl.IsGenericTypeDefinition)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.genericLocalType, ExceptionMessageId.TypeMustBeGenericChild);
                if (tDecl.GetGenericArguments().Length != t.GetGenericArguments().Length)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.genericLocalType, ExceptionMessageId.TypeParameterInfoError);
                this.genericLocalType = genericLocalType;
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.genericLocalType, ExceptionMessageId.TypeNotGeneric);
        }

        /// <summary>
        /// Returns the <see cref="Type"/> of the local type defined in the 
        /// generic type that has new signature information
        /// defined on a type-parameter.
        /// </summary>
        public Type GenericLocalType
        {
            get
            {
                return this.genericLocalType;
            }
        }
    }
}
