﻿using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    internal enum TypeParameterDisplayMode
    {
        SystemStandard,
        DebuggerStandard,
    }
    /// <summary>
    /// Defines the source of type-replacements for the
    /// generic.
    /// </summary>
    [Flags]
    public enum TypeParameterSources 
    {
        /// <summary>
        /// The source of the type-replacements is the 
        /// method generic parameters.
        /// </summary>
        Method = 1,
        /// <summary>
        /// The source of the type-replacments is the
        /// type's generic parameters.
        /// </summary>
        Type = 2,
        /// <summary>
        /// The source of the type-replacements is the 
        /// type and the method's generic parameters.
        /// </summary>
        Both = Method | Type,
    }
    /// <summary>
    /// Provides a series of extensions for <see cref="IType"/> instances.
    /// </summary>
    public static partial class TypeExtensions
    {
        /// <summary>
        /// Obtains a <see cref="TypedName"/> for a given <paramref name="type"/> with the 
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to create a <see cref="TypedName"/>
        /// for.</param>
        /// <param name="name">The <see cref="String"/> value of the typed name pair.</param>
        /// <returns>A new <see cref="TypedName"/> surrounding the <paramref name="type"/> and 
        /// <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="type"/> or
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// is <see cref="String.Empty"/></exception>
        public static TypedName GetTypedName(this IType type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            return new TypedName(name, type);
        }

        /// <summary>
        /// Obtains a <see cref="TypedName"/> for a given <paramref name="type"/> with the
        /// <paramref name="name"/> and <paramref name="typeParameter"/> set provided.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to create a <see cref="TypedName"/>
        /// for.</param>
        /// <param name="name">The <see cref="String"/> value of the typed name pair.</param>
        /// <param name="typeParameters">The type-parameters to make a closed generic of the
        /// original <paramref name="type"/> provided.</param>
        /// <returns>A new <see cref="TypedName"/> surrounding the <paramref name="type"/> and 
        /// <paramref name="name"/> furthered to a closed generic type by <paramref name="typeParameters"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when one of <paramref name="type"/>, 
        /// <paramref name="name"/> or <paramref name="typeParameters"/> are null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="typeParameters"/>
        /// contains no type-parameters, <paramref name="name"/> is <see cref="String.Empty"/></exception>
        public static TypedName GetTypedName(this IType type, string name, params IType[] typeParameters)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (name == string.Empty)
                throw new ArgumentException("name");
            if (type.IsGenericType && type is IGenericType)
            {
                if (type.IsGenericType && !((IGenericType)(type)).IsGenericTypeDefinition)
                    type = ((IGenericType)(type.ElementType)).MakeGenericType(typeParameters.ToCollection());
                else if (type.IsGenericType && ((IGenericType)(type)).IsGenericTypeDefinition)
                    type = ((IGenericType)(type)).MakeGenericType(typeParameters.ToCollection());
            }
            else if (typeParameters.Length == 0)
            {
                throw new ArgumentException("Type not generic.", "typeParameters");
            }
            return new TypedName(name, type);
        }


        /// <summary>
        /// Obtains a <see cref="ITypeCollection"/> for the <paramref name="array"/> of 
        /// <typeparamref name="T"/> provided.
        /// </summary>
        /// <param name="array">The array of <see cref="IType"/> the resulted
        /// <see cref="ITypeCollection"/> should contain.</param>
        /// <typeparam name="T">The type of <see cref="IType"/> to enumerate from <paramref name="array"/>.</typeparam>
        /// <returns>A new <see cref="ITypeCollection"/> instance containing the 
        /// <paramref name="array"/> entries.</returns>
        public static ITypeCollection ToCollection<T>(this IEnumerable<T> array)
            where T :
                IType
        {
            return new TypeCollection(array.Cast<IType>().ToArray());
        }

        /// <summary>
        /// Creates a locked type collection from the <see cref="IEnumerable{T}"/> of
        /// <see cref="IType"/> variants.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IType"/> used in the elements of the collection.</typeparam>
        /// <param name="array">The <see cref="IEnumerable{T}"/> of <typeparamref name="T"/> elements.</param>
        /// <returns>A new <see cref="ILockedTypeCollection"/> which contains the elements from the 
        /// <paramref name="array"/>.</returns>
        public static ILockedTypeCollection ToLockedCollection<T>(this IEnumerable<T> array)
            where T :
                IType
        {
            return new LockedTypeCollection(array.Cast<IType>());
        }

        internal static string BuildTypeName(this IType target, bool shortFormGeneric = false, bool numericTypeParams = false, TypeParameterDisplayMode typeParameterDisplayMode = TypeParameterDisplayMode.SystemStandard)
        {
            switch (target.ElementClassification)
            {
                case TypeElementClassification.None:
                    if (target is IGenericParameter)
                        if (numericTypeParams)
                        {
                            var genericParameter = (IGenericParameter)target;
                            if (genericParameter.Parent is IType)
                                return string.Format("!{0}", genericParameter.Position);
                            else
                                return string.Format("!!{0}", genericParameter.Position);
                        }
                        else
                            return ((IGenericParameter)(target)).Name;
                    string targetName = target.Name;
                    var genericTarget = target as IGenericType;
                    if (typeParameterDisplayMode == TypeParameterDisplayMode.SystemStandard && genericTarget != null && genericTarget.IsGenericType)
                    {
                        int count = 0;
                        if (genericTarget.ElementClassification == TypeElementClassification.None)
                        {
                            if ((count = genericTarget.TypeParameters.Count) > 0)
                                targetName += '`' + count.ToString();
                        }
                        else if (genericTarget.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                        {
                            var genericElementType = genericTarget.ElementType as IGenericType;
                            if (genericElementType != null)
                            {
                                if ((count = genericElementType.TypeParameters.Count) > 0)
                                    targetName += '`' + count.ToString();
                            }
                            else
                                targetName += "`?";
                        }
                    }
                    if (target.DeclaringType != null)
                        return string.Format("{0}+{1}", target.DeclaringType.BuildTypeName(shortFormGeneric, numericTypeParams), targetName);
                    else if (!string.IsNullOrEmpty(target.NamespaceName))
                        return string.Format("{0}.{1}", target.NamespaceName, targetName);
                    else
                        return targetName;
                case TypeElementClassification.Array:
                    if (target is IArrayType)
                        if (((IArrayType)(target)).IsZeroBased)
                            return string.Format("{0}[{1}]", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams), ','.Repeat(((IArrayType)(target)).ArrayRank - 1));
                        else
                            return string.Format("{0}[{1}]", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams), string.Join(",", ((IArrayType)(target)).LowerBounds.OnAll(q => q == 0 ? string.Empty : string.Format("{0}...", q)).ToArray()));
                    else
                        return string.Format("{0}[?,...]", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams));
                case TypeElementClassification.Nullable:
                    return string.Format("{0}?", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams));
                case TypeElementClassification.Pointer:
                    return string.Format("{0}*", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams));
                case TypeElementClassification.Reference:
                    return string.Format("{0}&", target.ElementType.BuildTypeName(shortFormGeneric, numericTypeParams));
                case TypeElementClassification.GenericTypeDefinition:
                    if (target is IGenericType)
                    {
                        /* *
                         * Construct the name in full by joining the 
                         * names of the parameters with a comma, obtain 
                         * the string-variant of each via a lambda operating on each.
                         * */
                        bool debuggerStandard = typeParameterDisplayMode == TypeParameterDisplayMode.DebuggerStandard;
                        return string.Format("{0}{2}{1}{3}", target.ElementType.BuildTypeName(typeParameterDisplayMode: typeParameterDisplayMode), string.Join(",",
                                ((IGenericType)(target)).GenericParameters.OnAll(genericReplacement =>
                                {
                                    if (shortFormGeneric)
                                        if (genericReplacement.IsGenericTypeParameter)
                                            /* *
                                             * In certain situations, the position of the
                                             * parameter is more important, especially when
                                             * building the unique identifier of a parameter
                                             * of a method.  This way two methods with similarly 
                                             * positioned generic-parameters can intentionally
                                             * collide.  
                                             * *
                                             * This is important because its the order of the parameters
                                             * that determines the actual final signature of the method,
                                             * if two generic methods are identical in their ordering,
                                             * inference of which to call can be an ambiguity.
                                             * */
                                            if (numericTypeParams)
                                            {
                                                var genericParameterReplacement = (IGenericParameter)genericReplacement;
                                                if (genericParameterReplacement.Parent is IType)
                                                    return string.Format("!{0}", genericParameterReplacement.Position);
                                                else
                                                    return string.Format("!!{0}", genericParameterReplacement.Position);
                                            }
                                            else
                                                return genericReplacement.Name;
                                        else
                                            return genericReplacement.BuildTypeName(true, shortFormGeneric);
                                    else if (genericReplacement.IsGenericTypeParameter)
                                        if (numericTypeParams)
                                        {
                                            var genericParameterReplacement = (IGenericParameter)genericReplacement;
                                            if (genericParameterReplacement.Parent is IType)
                                                return string.Format("[!{0}]", genericParameterReplacement.Position);
                                            else
                                                return string.Format("[!!{0}]", genericParameterReplacement.Position);
                                        }
                                        else
                                            return string.Format("[{0}]", genericReplacement.Name);
                                    else if (genericReplacement.Assembly == null)
                                        return string.Format("{0}", genericReplacement.FullName);
                                    else
                                    {
                                        try
                                        {
                                            IAssembly replacementAssembly = genericReplacement.Assembly;
                                            return string.Format("[{0}, {1}]", genericReplacement.FullName, replacementAssembly.ToString());
                                        }
                                        //For Symbol types.
                                        catch (NotSupportedException)
                                        {
                                            return string.Format("{0}", genericReplacement.FullName);
                                        }
                                    }
                                }).ToArray()), debuggerStandard ? '<' : '[', debuggerStandard ? '>' : ']');
                    }
                    else
                    {
                        bool debuggerStandard = typeParameterDisplayMode == TypeParameterDisplayMode.DebuggerStandard;
                        return string.Format("{0}{1}[?],...{2}", target.FullName, debuggerStandard ? '<' : '[', debuggerStandard ? '>' : ']');
                    }
            }
            return null;
        }

        internal static bool ContainsGenericParameters(this IType type)
        {
            if (type.IsGenericType && type is IGenericType)
            {
                if (((IGenericType)(type)).IsGenericTypeDefinition)
                    return true;
                else
                {
                    return ((IGenericType)(type)).GenericParameters.ContainsGenericParameters();
                }
            }
            else
            {
                switch (type.ElementClassification)
                {
                    case TypeElementClassification.Array:
                    case TypeElementClassification.Nullable:
                    case TypeElementClassification.Pointer:
                    case TypeElementClassification.Reference:
                        return type.ElementType.ContainsGenericParameters();
                }
                return type.IsGenericTypeParameter;
            }
        }

        internal static bool ContainsGenericParameters(this ITypeCollectionBase collection)
        {
            return collection.Any(current => current.ContainsGenericParameters());
        }

        /// <summary>
        /// Obtains the top-most element type of a jagged <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The jagged <see cref="IArrayType"/> to obtain
        /// the top-most <see cref="IType.ElementType"/> of.</param>
        /// <returns>A <see cref="IType"/> relative of the top-most element type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="array"/> is not an array type.</exception>
        public static IType GetTopmostArrayElement(this IArrayType array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.ElementClassification != TypeElementClassification.Array)
                throw new ArgumentException("array");
            IType lastType = array;
            while (lastType != null && lastType.ElementClassification == TypeElementClassification.Array)
                lastType = lastType.ElementType;
            return lastType;
        }

        /// <summary>
        /// Resolves generic type-parameters contained within the
        /// <paramref name="target"/> <see cref="IType"/> by 
        /// rebuilding the <paramref name="target"/> with
        /// the <paramref name="typeReplacements"/> used in place of
        /// the <see cref="IGenericTypeParameter"/> instances.
        /// </summary>
        /// <param name="target">The target <see cref="IType"/>
        /// to disambiguify.</param>
        /// <param name="methodReplacements">The <see cref="IType"/>
        /// series which contains a series of types which is index-relative
        /// to the <paramref name="target"/>'s type-parameters.</param>
        /// <param name="typeReplacements">The <see cref="IType"/>
        /// series which contains a series of types that is index-relative
        /// to the <paramref name="target"/>'s type-parameters.</param>
        /// <param name="parameterSource">The point to source
        /// the replacements from.</param>
        /// <returns>A <see cref="IType"/> instance which has been disambiguified 
        /// by replacing the type-parameters in the present context with the 
        /// type-replacements in the disambiguated context.</returns>
        /// <exception cref="System.ArgumentNullException">When <paramref name="typeReplacements"/> is
        /// null and <paramref name="parameterSource"/> contains the <see cref="TypeParameterSources.Type"/>
        /// flag, or when <paramref name="methodReplacements"/> is null and 
        /// <paramref name="parameterSource"/> contains the <see cref="TypeParameterSources.Method"/> 
        /// flag.</exception>
        public static IType Disambiguify(this IType target, ITypeCollectionBase typeReplacements, ITypeCollectionBase methodReplacements, TypeParameterSources parameterSource)
        {
            if (((parameterSource & TypeParameterSources.Type) == TypeParameterSources.Type) &&
                typeReplacements == null)
                throw new ArgumentNullException("typeReplacements");
            if (((parameterSource & TypeParameterSources.Method) == TypeParameterSources.Method) &&
                methodReplacements == null)
                throw new ArgumentNullException("methodReplacements");
            /* *
             * Assumes a great deal in that: Types provided are used in scope.
             * Beyond scope this does not work as intended since the positions
             * of out-of-scope type-parameters might not be relatively 
             * equivalent to the scope they are used in (typeReplacements).
             * */
            if (target.IsGenericTypeParameter)
            {
                if (target is IGenericParameter)
                {
                    /* *
                     * If the declaring type is null, this method assumes 
                     * it's a method.  Primarily to aid in possible other 
                     * uses.
                     * */
                    if ((parameterSource & TypeParameterSources.Method) == TypeParameterSources.Method && 
                        ((IGenericParameter)(target)).DeclaringType == null &&
                        ((IGenericParameter)(target)).Position < methodReplacements.Count)
                            return methodReplacements[((IGenericParameter)(target)).Position];
                    if ((parameterSource & TypeParameterSources.Type) == TypeParameterSources.Type && 
                        ((IGenericParameter)(target)).DeclaringType != null &&
                        ((IGenericParameter)(target)).Position < typeReplacements.Count)
                            return typeReplacements[((IGenericParameter)(target)).Position];
                }
            }
            else
            {
                switch (target.ElementClassification)
                {
                    case TypeElementClassification.Array:
                        if (target is IArrayType)
                            return target.ElementType.Disambiguify(typeReplacements, methodReplacements, parameterSource).MakeArray(((IArrayType)target).ArrayRank);
                        break;
                    case TypeElementClassification.Nullable:
                        return target.ElementType.Disambiguify(typeReplacements, methodReplacements, parameterSource).MakeNullable();
                    case TypeElementClassification.Pointer:
                        return target.ElementType.Disambiguify(typeReplacements, methodReplacements, parameterSource).MakePointer();
                    case TypeElementClassification.Reference:
                        return target.ElementType.Disambiguify(typeReplacements, methodReplacements, parameterSource).MakeByReference();
                    case TypeElementClassification.GenericTypeDefinition:
                        if (target.ElementType is IGenericType)
                            return ((IGenericType)target.ElementType).MakeVerifiedGenericType(((IGenericType)target).GenericParameters.OnAll(gP => gP.Disambiguify(typeReplacements, methodReplacements, parameterSource)).ToCollection());
                        break;
                    case TypeElementClassification.None:
                        if (target is IGenericType &&
                           (parameterSource == TypeParameterSources.Type && ((IGenericType)(target)).GenericParameters.Count == typeReplacements.Count))
                            return ((IGenericType)(target)).MakeGenericType(typeReplacements);
                        break;
                }
            }
            return target;
        }
    }
}