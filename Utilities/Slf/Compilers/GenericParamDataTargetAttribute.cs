using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
                    throw new ArgumentException("Provided type must be child of a generic.","genericLocalType");
                if (tDecl.GetGenericArguments().Length != t.GetGenericArguments().Length)
                    throw new ArgumentException("Provided type must contain only the generic arguments of declaring generic type.");
                this.genericLocalType = genericLocalType;
            }
            else
                throw new ArgumentException("genericLocalType");
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
