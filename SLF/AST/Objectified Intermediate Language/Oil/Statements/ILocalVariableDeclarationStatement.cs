using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// The kind of typing used on a series of locals in
    /// a <see cref="ILocalVariableDeclarationStatement"/>.
    /// </summary>
    public enum LocalTypingKind
    {
        /// <summary>
        /// The type of the locals declared is dynamic,
        /// the type can change throughout the runtime of the
        /// program, all calls are late-bound.
        /// </summary>
        Dynamic,
        /// <summary>
        /// The type of the locals declared is impliict,
        /// the <see cref="IType"/> associated is inferred
        /// by the initialization expression.
        /// </summary>
        Implicit,
        /// <summary>
        /// The type of the locals declared is explicit, 
        /// an <see cref="IType"/> is provided.
        /// </summary>
        Explicit,
    }
    /// <summary>
    /// Defines properties and methods for working with a statement which
    /// declares a series of named variables and their initialization 
    /// expressions.
    /// </summary>
    public interface ILocalVariableDeclarationStatement :
        IControlledStateDictionary<string, IExpression>,
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="LocalTypingKind"/>
        /// which determines how the type of the locals declared
        /// by the <see cref="ILocalVariableDeclarationStatement"/>
        /// is determined.
        /// </summary>
        LocalTypingKind TypeKind { get; }
        /// <summary>
        /// Adds a local variable to the <see cref="ILocalVariableDeclarationStatement"/>
        /// under the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique 
        /// name of the value in the current scope.</param>
        void Add(string name);
        /// <summary>
        /// Adds a local variable to the <see cref="ILocalVariableDeclarationStatement"/>
        /// under the <paramref name="name"/> and <paramref name="initializationExpression"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the unique 
        /// name of the value in the current scope.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/>
        /// which represents the value to initialize the new local to.</param>
        void Add(string name, IExpression initializationExpression);
        /// <summary>
        /// Returns/sets the initialization expression for a local
        /// defined within the <see cref="ILocalVariableDeclarationStatement"/> by its
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> which represents the name of the 
        /// local to set/get the initialization <see cref="IExpression"/> of.
        /// </param>
        /// <returns>A <see cref="IExpression"/> relative to the <paramref name="name"/>
        /// of the local.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// thrown when a variable under the <paramref name="name"/> provided
        /// does not exist within the <see cref="ILocalVariableDeclarationStatement"/>.</exception>
        new IExpression this[string name] { get; set; }
    }
}
