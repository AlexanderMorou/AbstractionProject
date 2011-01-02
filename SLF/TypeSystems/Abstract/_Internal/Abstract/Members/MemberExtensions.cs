using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal static class MemberExtensions
    {
        internal static string GetUniqueIdentifier(this PropertyInfo property)
        {
            return property.Name;
        }

        internal static string GetUniqueIdentifier(this FieldInfo field)
        {
            return field.Name;
        }

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

        internal static string GetUniqueIdentifier(this MethodBase method)
        {
            return string.Format("{{0}}({0})", string.Join(", ", method.GetParameters().OnAll(p => string.IsNullOrEmpty(p.ParameterType.FullName) ? p.ParameterType.Name : p.ParameterType.FullName).ToArray()));
        }

        internal static string GetUniqueIdentifier<TParent, TParameter>(TParent signature)
            where TParent :
                IParameterParent<TParent, TParameter>
            where TParameter :
                IParameterMember<TParent>
        {
            return string.Format("{{0}}({0})", string.Join(", ", signature.Parameters.Values.OnAll(p => p.ParameterType.IsGenericTypeParameter ? p.ParameterType.Name : p.ParameterType.FullName).ToArray()));
        }

        internal static string GetUniqueIdentifier(this MethodInfo method)
        {
            return string.Format(((MethodBase)(method)).GetUniqueIdentifier(), string.Format(method.GetGenericArguments().Length > 0 ? "{2} {0}[{1}]" : "{2} {0}", method.Name, method.GetUniqueIdentifierTParams(), string.IsNullOrEmpty(method.ReturnType.FullName) ? method.ReturnType.Name : method.ReturnType.FullName));
        }

        internal static string GetUniqueIdentifierTParams(this MethodInfo mi)
        {
            if (mi.IsGenericMethod && mi.IsGenericMethodDefinition)
                return string.Join(", ", mi.GetGenericArguments().OnAll(f => f.Name).ToArray());
            else
                return string.Empty;
        }

        internal static string GetUniqueIdentifier(this ConstructorInfo item)
        {
            StringBuilder uid = new StringBuilder();
            bool first = true;
            foreach (ParameterInfo p in item.GetParameters())
            {
                if (first)
                    first = false;
                else
                    uid.Append(", ");
                if (p.ParameterType.FullName == null)
                    uid.Append(p.ParameterType.Name);
                else
                    uid.Append(p.ParameterType.FullName);
            }
            return string.Format("{0}({1})", item.Name, uid.ToString());
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
                return AccessLevelModifiers.ProtectedInternal;
            else if (field.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.InternalProtected;
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
                return AccessLevelModifiers.ProtectedInternal;
            else if (type.IsNestedFamANDAssem)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.InternalProtected;
            return AccessLevelModifiers.Private;
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
                return AccessLevelModifiers.ProtectedInternal;
            else if (firstMethod.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.InternalProtected;
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
                return AccessLevelModifiers.ProtectedInternal;
            else if (method.IsFamilyAndAssembly)
                //Special case, not available in C# or VB.
                return AccessLevelModifiers.InternalProtected;
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
                ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
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
