using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working on a dictionary of enumerator fields.
    /// </summary>
    [CLSCompliant(false)]
    public interface IIntermediateEnumFieldMemberDictionary :
        IIntermediateGroupedMemberDictionary<IEnumType, IIntermediateEnumType, IEnumFieldMember, IIntermediateEnumFieldMember>,
        IFieldMemberDictionary<IEnumFieldMember, IEnumType>
    {
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="SByte"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, sbyte value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="Byte"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, byte value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="Int16"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, short value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="UInt16"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, ushort value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="Int32"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, int value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="UInt32"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, uint value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="Int64"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, long value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">A <see cref="UInt64"/> that the <see cref="IIntermediateEnumFieldMember"/>
        /// will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name, ulong value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the 
        /// <see cref="IIntermediateEnumFieldMemberDictionary"/> with the 
        /// <paramref name="name"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <param name="value">The <see cref="IExpression"/> that the 
        /// <see cref="IIntermediateEnumFieldMember"/> will be.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with the <paramref name="value"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="name"/> or <paramref name="value"/> is null.</exception>
        IIntermediateEnumFieldMember Add(string name, IExpression value);
        /// <summary>
        /// Adds a <see cref="IIntermediateEnumFieldMember"/> to the <see cref="IIntermediateEnumFieldMemberDictionary"/>
        /// with the <paramref name="name"/> specified.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the <see cref="IIntermediateEnumFieldMember"/>
        /// which is used to identify it later.</param>
        /// <returns>A <see cref="IIntermediateEnumFieldMember"/> instance
        /// with an automatically generated value.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateEnumFieldMember Add(string name);
    }
}
