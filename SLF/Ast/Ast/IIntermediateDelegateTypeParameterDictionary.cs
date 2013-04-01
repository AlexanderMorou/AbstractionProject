using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast
{

    /// <summary>
    /// Defines properties and methods for working with the type-parameters
    /// of a delegate type.
    /// </summary>
    /// <remarks>Used to shorten the type-name of the delegate type's
    /// type-parameter dictionary.</remarks>
    public interface IIntermediateDelegateTypeParameterTypeDictionary :
        IIntermediateGenericParameterDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IDelegateType>, IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>, IDelegateType, IIntermediateDelegateType>
    {
        /// <summary>
        /// Adds a new type-parameter by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// type-parameter.</param>
        /// <returns>A new <see cref="IIntermediateDelegateTypeParameterType"/> instance.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> overlaps
        /// an existing type-parameter name.</exception>
        new IIntermediateDelegateTypeParameterType Add(string name);
        /// <summary>
        /// Adds a new type-parameter by specifying the generic full set of generic
        /// parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> 
        /// which specifies the name, constructors, methods, and interface
        /// constraints of the <see cref="IIntermediateDelegateTypeParameterType"/>
        /// which results.</param>
        /// <returns>A new <see cref="IIntermediateDelegateTypeParameterType"/> instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <see cref="GenericParameterData.Name"/>
        /// is <see cref="String.Empty"/> or null, or when the <see cref="GenericParameterData.Name"/>
        /// collides with another type-parameter name.</exception>
        new IIntermediateDelegateTypeParameterType Add(GenericParameterData genericParameterData);
        /// <summary>
        /// Adds a new series of type-parameters by specifying the full set of
        /// generic parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> array
        /// associated to the elements to add.</param>
        /// <returns>A series of <see cref="IIntermediateDelegateTypeParameterType"/>
        /// elements resulted from the operation.</returns>
        new IIntermediateDelegateTypeParameterType[] AddRange(params GenericParameterData[] genericParameterData);

        /// <summary>
        /// Obtains the <see cref="IIntermediateDelegateTypeParameterType"/>
        /// by the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value used to differentiate the 
        /// <see cref="IIntermediateDelegateTypeParameterType"/> elements.</param>
        /// <returns>The <see cref="IIntermediateDelegateTypeParameterType"/>
        /// with the <paramref name="name"/> provided.</returns>
        new IIntermediateDelegateTypeParameterType this[string name] { get; }

        /// <summary>
        /// Obtains the <see cref="IIntermediateDelegateTypeParameterType"/>
        /// by the <paramref name="uniqueIdentifier"/> provided.
        /// </summary>
        /// <param name="uniqueIdentifier">The <see cref="IGeneralGenericTypeUniqueIdentifier"/> 
        /// value used to differentiate the <see cref="IIntermediateDelegateTypeParameterType"/>
        /// elements.</param>
        /// <returns>The <see cref="IIntermediateDelegateTypeParameterType"/>
        /// with the <paramref name="uniqueIdentifier"/> provided.</returns>
        new IIntermediateDelegateTypeParameterType this[IGenericParameterUniqueIdentifier uniqueIdentifier] { get; }
    }
    namespace Members
    {
        public interface IIntermediateDelegateTypeParameterType :
            IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>
        {
        }
    }
}
