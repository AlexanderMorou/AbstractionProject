using System;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal static class MemberExtensions
    {

        internal static bool LastParameterIsParams(this MethodBase mb)
        {
            return LastParameterIsParams(mb.GetParameters());
        }
        internal static bool LastParameterIsParams(this PropertyInfo property)
        {
            return LastParameterIsParams(property.GetIndexParameters());
        }

        private static bool LastParameterIsParams(ParameterInfo[] target)
        {
            if (target == null)
                throw new ArgumentNullException();
            var piA = target;
            if (piA.Length > 0)
            {
                var pi = piA[piA.Length - 1];
                object[] custAttrs = pi.GetCustomAttributes(typeof(ParamArrayAttribute), true);
                if (custAttrs.Length > 0)
                    return true;
            }
            return false;
        }

        private static MethodInfo GetFirstMethod(this PropertyInfo property)
        {
            if (property.CanRead)
                return property.GetGetMethod(true);
            else if (property.CanWrite)
                return property.GetSetMethod(true);
            return null;
        }

        public static AccessLevelModifiers GetAccessModifiers(this FieldInfo field)
        {
            if (field.IsPublic)
                return AccessLevelModifiers.Public;
            else if (field.IsAssembly)
                return AccessLevelModifiers.Internal;
            else if (field.IsPrivate)
                return AccessLevelModifiers.Private;
            else if (field.IsFamily)
                return AccessLevelModifiers.Protected;
            else if (field.IsFamilyOrAssembly)
                return AccessLevelModifiers.ProtectedOrInternal;
            else if (field.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.ProtectedAndInternal;
            return AccessLevelModifiers.Private;
        }

        public static AccessLevelModifiers GetAccessModifiers(this Type type)
        {
            if (type.IsPublic || type.IsNestedPublic)
                return AccessLevelModifiers.Public;
            else if (type.IsNestedAssembly || type.IsNotPublic)
                return AccessLevelModifiers.Internal;
            else if (type.IsNestedPrivate)
                return AccessLevelModifiers.Private;
            else if (type.IsNestedFamily)
                return AccessLevelModifiers.Protected;
            else if (type.IsNestedFamORAssem)
                return AccessLevelModifiers.ProtectedOrInternal;
            else if (type.IsNestedFamANDAssem)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.ProtectedAndInternal;
            return AccessLevelModifiers.Private;
        }


        internal static bool IsModifierAccessible(this AccessLevelModifiers modifiers)
        {
            switch (modifiers)
            {
                case AccessLevelModifiers.Private:
                case AccessLevelModifiers.PrivateScope:
                    return false;
                case AccessLevelModifiers.ProtectedAndInternal:
                case AccessLevelModifiers.Internal:
                case AccessLevelModifiers.Public:
                case AccessLevelModifiers.Protected:
                case AccessLevelModifiers.ProtectedOrInternal:
                default:
                    return true;
            }
        }

        public static AccessLevelModifiers GetAccessModifiers(this EventInfo @event)
        {
            MethodInfo firstMethod = @event.GetFirstEventMethod();
            if (firstMethod.IsPublic)
                return AccessLevelModifiers.Public;
            else if (firstMethod.IsAssembly)
                return AccessLevelModifiers.Internal;
            else if (firstMethod.IsPrivate)
                return AccessLevelModifiers.Private;
            else if (firstMethod.IsFamily)
                return AccessLevelModifiers.Protected;
            else if (firstMethod.IsFamilyOrAssembly)
                return AccessLevelModifiers.ProtectedOrInternal;
            else if (firstMethod.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.ProtectedAndInternal;
            return AccessLevelModifiers.Private;
        }

        internal static MethodInfo GetFirstEventMethod(this EventInfo @event)
        {
            MethodInfo result = @event.GetAddMethod(true);
            if (result == null)
                result = @event.GetRemoveMethod(true);
            if (result == null)
                result = @event.GetRaiseMethod(true);
            if (result == null)
            {
                var otherMethods = @event.GetOtherMethods(true);
                foreach (var oMeth in otherMethods)
                    if (result == null)
                    {
                        result = oMeth;
                        break;
                    }
            }
            return result;
        }

        public static AccessLevelModifiers GetAccessModifiers(this MethodBase method)
        {
            if (method.IsPublic)
                return AccessLevelModifiers.Public;
            else if (method.IsAssembly)
                return AccessLevelModifiers.Internal;
            else if (method.IsPrivate)
                return AccessLevelModifiers.Private;
            else if (method.IsFamily)
                return AccessLevelModifiers.Protected;
            else if (method.IsFamilyOrAssembly)
                return AccessLevelModifiers.ProtectedOrInternal;
            else if (method.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.ProtectedAndInternal;
            return AccessLevelModifiers.Private;
        }

        public static AccessLevelModifiers GetAccessModifiers(this PropertyInfo property)
        {
            if (property.CanRead && property.CanWrite)
            {
                AccessLevelModifiers read = property.GetGetMethod(true).GetAccessModifiers();
                AccessLevelModifiers write = property.GetGetMethod(true).GetAccessModifiers();
                if (read.CompareTo(write) < 0)
                    return write;
                else //If they're equal it doesn't matter; if write is lower than read, then use read's
                     //because the accessability of the property is always the highest of the two.
                    return read;
            }
            else if (property.CanRead)
            {
                return property.GetGetMethod(true).GetAccessModifiers();
            }
            else if (property.CanWrite)
            {
                return property.GetSetMethod(true).GetAccessModifiers();
            }
            else
                return AccessLevelModifiers.Private;
        }
        public static int CalculateStackRequirement<TSignatureParameter, TSignature, TSignatureParent>(this IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent> method)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            var extendedInstanceMember = method as IExtendedInstanceMember;
            if (extendedInstanceMember != null)
            {
                if (extendedInstanceMember.IsStatic)
                    return method.Parameters.Count;
                else
                    return method.Parameters.Count + 1;
            }
            return method.Parameters.Count;
        }


    }
}
