using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public enum TypeOfKind
    {
        /// <summary>
        /// Uses the standardized type system.
        /// </summary>
        /// <remarks>Uses <see cref="System.Type"/> for all results.</remarks>
        General,
        /// <summary>
        /// Uses the specific type system.
        /// </summary>
        /// <remarks><para>The type that results from the typeof expression
        /// will change based off of the kind of type passed in.</para>
        /// <list type="bullet">
        /// <item><description>Classes will use <see cref="IClassType"/> via <see cref="AllenCopeland.Abstraction.Slf.Cli.CliGateway.GetTypeReference&lt;IGeneralGenericTypeUniqueIdentifier, IDelegateType&gt;(Type)"/>.</description></item>
        /// <item><description>Delegates will use <see cref="IDelegateType"/> via <see cref="CliGateway.GetTypeReference{TTypeIdentifier, TType}(Type)"/>.</description></item>
        /// <item><description>Enums will use <see cref="IEnumType"/> via <see cref="CliGateway.GetTypeReference{TTypeIdentifier, TType}(Type)"/>.</description></item>
        /// <item><description>Interfaces will use <see cref="IInterfaceType"/> via <see cref="CliGateway.GetTypeReference{TTypeIdentifier, TType}(Type)"/>.</description></item>
        /// <item><description>Structs will use <see cref="IStructType"/> via <see cref="CliGateway.GetTypeReference{TTypeIdentifier, TType}(Type)"/>.</description></item>
        /// </list></remarks>
        Specific
    }
    /// <summary>
    /// Defines properties and methods for working with a type of expression 
    /// which loads the meta-data token of the type in question onto the
    /// stack.
    /// </summary>
    public interface ITypeOfExpression :
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> which is represented
        /// under the typeof expression.
        /// </summary>
        IType ReferenceType { get; }
    }
}
