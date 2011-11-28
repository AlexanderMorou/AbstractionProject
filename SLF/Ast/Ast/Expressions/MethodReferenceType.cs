using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// The kind of reference a method reference is.
    /// </summary>
    public enum MethodReferenceType
    {
        /// <summary>
        /// Denotes that the method reference is a reference to a 
        /// virtual method.
        /// </summary>
        /// <remarks>
        /// Used to denote that the reference is virtual in nature;
        /// that is, if the method is overridden, then the overridden
        /// variant will be invoked.
        /// </remarks>
        VirtualMethodReference,
        /// <summary>
        /// Denotes that the method reference is a reference to a 
        /// method (irregardless of virtual status).
        /// </summary>
        /// <remarks>
        /// <para>
        /// Used to denote that the reference is explicit and ignores
        /// the virtual status of a method.
        /// </para>
        /// <para>
        /// Used to implement 'base' method calls in C&#9839; and 
        /// 'MyBase' and 'MyClass' calls in VB.
        /// </para>
        /// </remarks>
        StandardMethodReference
    }
}
