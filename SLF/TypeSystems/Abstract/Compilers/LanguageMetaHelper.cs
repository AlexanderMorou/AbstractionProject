using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static partial class LanguageMetaHelper
    {
        #region Type Conversion Info

        private static Dictionary<TypeCode, Dictionary<TypeCode, bool>> conversionInfo = GetConversionInfo();
        private static Dictionary<TypeCode, Dictionary<TypeCode, bool>> GetConversionInfo()
        {
            Dictionary<TypeCode, Dictionary<TypeCode, bool>> conversionInfo = new Dictionary<TypeCode, Dictionary<TypeCode, bool>>();
            TypeCode[] supportedTypeCodes = new TypeCode[] { TypeCode.Byte, TypeCode.SByte, TypeCode.Single, TypeCode.Double, TypeCode.Char, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            foreach (TypeCode tc in supportedTypeCodes)
            {
                Dictionary<TypeCode, bool> current = new Dictionary<TypeCode, bool>();
                switch (tc)
                {
                    case TypeCode.Char:
                        current[TypeCode.UInt16] = true;
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.Byte:
                        current[TypeCode.Char] = true;
                        current[TypeCode.UInt16] = true;
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.SByte;
                    case TypeCode.SByte:
                        current[TypeCode.Int16] = true;
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt16:
                        current[TypeCode.UInt32] = true;
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.Int16;
                    case TypeCode.Int16:
                        current[TypeCode.Int32] = true;
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt32:
                        current[TypeCode.UInt64] = true;
                        goto case TypeCode.Int32;
                    case TypeCode.Int32:
                        current[TypeCode.Int64] = true;
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.UInt64:
                    case TypeCode.Int64:
                        current[TypeCode.Single] = true;
                        current[TypeCode.Double] = true;
                        break;
                    case TypeCode.Single:
                        current[TypeCode.Double] = true;
                        break;
                }
                conversionInfo[tc] = current;
            }
            return conversionInfo;
        }
        /// <summary>
        /// Checks to see if you can go <paramref name="from"/> one type <paramref name="to"/> another.
        /// </summary>
        /// <param name="from">The type to check conversion of.</param>
        /// <param name="to">The type to see if <paramref name="from"/> can go to.</param>
        /// <returns>True if <paramref name="from"/> can be cast/converted <paramref name="to"/>; otherwise false.</returns>
        public static bool CanConvertFrom(this Type from, Type to)
        {
            TypeCode fromTC = Type.GetTypeCode(from);
            TypeCode toTC = Type.GetTypeCode(to);
            try
            {
                if (fromTC != toTC)
                    return conversionInfo[fromTC][toTC];
                else if (fromTC == TypeCode.Object)
                    return (from.IsAssignableFrom(to));
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region CtorBinding
        public static ConstructorInfo FindConstructor(this ConstructorInfo[] list, params Type[] binding)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (binding == null)
                throw new ArgumentNullException("binding");
            var match = new bool[list.Length];
            var deviations = new Dictionary<ConstructorInfo, int>();
            for (int i = 0; i < list.Length; i++)
            {
                var current = list[i];
                var paramsInfo = current.GetParameters();
                if (paramsInfo.Length != binding.Length)
                    continue;
                match[i] = true;
                for (int j = 0; j < paramsInfo.Length; j++)
                {
                    if (paramsInfo[j].ParameterType != binding[j])
                        if (binding[j].CanConvertFrom(paramsInfo[j].ParameterType))
                            if (deviations.ContainsKey(list[i]))
                                deviations[list[i]]++;
                            else
                                deviations.Add(list[i], 1);
                        else
                        {
                            match[i] = false;
                            break;
                        }
                }
            }
            int index1 = 0;
            return (from constructor in list
                    where match[index1++]
                    orderby deviations.ContainsKey(constructor) ? deviations[constructor] : 0
                    select constructor).FirstOrDefault();
        }
        #endregion

        #region BuildOptimizedConstructorDelegate
        #region Helpers
        private delegate void FuncV<T>(T arg);

        private static IEnumerable<TCallResult> OnAll<TItem, TCallResult>(this IEnumerable<TItem> e, Func<TItem, TCallResult> f)
        {
            foreach (TItem t in e)
                yield return f(t);
            yield break;
        }

        private static void OnAll<TItem>(this IEnumerable<TItem> e, FuncV<TItem> f)
        {
            foreach (TItem t in e)
                f(t);
        }

        private static string GetStringFormSignature(Type[] parameters)
        {
            bool firstMember = true;
            StringBuilder sb = new StringBuilder();
            foreach (Type paramInfo in parameters)
            {
                if (firstMember)
                    firstMember = false;
                else
                    sb.Append(", ");
                sb.Append(paramInfo.FullName == null ? paramInfo.Name : paramInfo.FullName);
            }
            return sb.ToString();
        }
        #endregion
        /// <summary>
        /// Builds an optimized constructor delegate using a dynamic method
        /// bound to the <typeparamref name="TDelegate"/> provided for the
        /// <paramref name="ctor"/>.
        /// </summary>
        /// <typeparam name="TDelegate">The type of delegate used to bind to <paramref name="ctor"/>.</typeparam>
        /// <param name="ctor">The constructor to build a dynamic method for that matches
        /// the signature of <typeparamref name="TDelegate"/>.</param>
        /// <param name="caller">The <see cref="Type"/> from which the
        /// method was called.</param>
        /// <returns>A <typeparamref name="TDelegate"/> instance
        /// provided there's no issues between the call between <typeparamref name="TDelegate"/>
        /// and the <paramref name="ctor"/>.</returns>
        /// <exception cref="System.ArgumentException"><typeparamref name="TDelegate"/>
        /// is not a type of a delegate, or <paramref name="ctor"/> does not match the parameter
        /// list of the delegate provided in <typeparamref name="TDelegate"/>.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="ctor"/> is null.</exception>
        /// <remarks><para>Delegate will be bound by the scoping rules of the <paramref name="caller"/>.</para>
        /// <para><paramref name="caller"/> is explicitly defined due to trace not returning 
        /// type-parameters of caller's declaring type.</para></remarks>
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.PreserveSig)]
        public static TDelegate BuildOptimizedConstructorDelegateEx<TDelegate>(this ConstructorInfo ctor)
        {
            if (ctor == null)
                throw new ArgumentNullException("ctor");
            var delegateType = typeof(TDelegate);
            if (!delegateType.IsSubclassOf(typeof(Delegate)))
                throw new ArgumentException("TDelegate");
            //Obtain the invoke method on the delegate and obtain its signature.
            var delegateInvoke = delegateType.GetMethod("Invoke");
            var delegateTypes = (from parameter in delegateInvoke.GetParameters()
                                 select parameter.ParameterType).ToArray();

            //Obtain the method call parameters.
            var ctorParameters = ctor.GetParameters();
            if (delegateTypes.Length != ctorParameters.Length)
                throw new ArgumentException("ctor");

            var optimizedCtor = new DynamicMethod(string.Format(".ctor@{0}({1})", ctor.DeclaringType.Name, GetStringFormSignature(delegateTypes)), delegateInvoke.ReturnType, delegateTypes, ctor.DeclaringType, true);
            var ctorTypes = ctorParameters.OnAll(param => param.ParameterType).ToArray();
            //Instantiate the generator.
            var interLangGenerator = optimizedCtor.GetILGenerator();
            optimizedCtor.InitLocals = true;
            int argIndex = 0;
            bool[] compareCasts = new bool[ctorTypes.Length];
            for (int i = 0; i < compareCasts.Length; i++)
                compareCasts[i] = ctorTypes[i] != delegateTypes[i];
            /* *
             * Iterate through all the parameters, emit a load for each
             * parameter relative to their index.
             * *
             * Additionally include checks for by-ref parameters,
             * which are loaded onto the stack by address.
             * */
            ctorParameters.OnAll(parameter =>
            {
                if (parameter.IsOut || parameter.ParameterType.IsByRef)
                {
                    if (argIndex < 128)
                    {
                        interLangGenerator.Emit(OpCodes.Ldarga_S, argIndex);
                    }
                    else
                        interLangGenerator.Emit(OpCodes.Ldarga, argIndex);
                }
                else
                {
                    switch (argIndex)
                    {
                        case 0:
                            interLangGenerator.Emit(OpCodes.Ldarg_0, argIndex);
                            break;
                        case 1:
                            interLangGenerator.Emit(OpCodes.Ldarg_1, argIndex);
                            break;
                        case 2:
                            interLangGenerator.Emit(OpCodes.Ldarg_2, argIndex);
                            break;
                        case 3:
                            interLangGenerator.Emit(OpCodes.Ldarg_3, argIndex);
                            break;
                        default:
                            if (argIndex < 128)
                                interLangGenerator.Emit(OpCodes.Ldarg_S, argIndex);
                            else
                                interLangGenerator.Emit(OpCodes.Ldarg, argIndex);
                            break;
                    }
                }
                if (compareCasts[argIndex])
                {
                    /* *
                     * The types weren't equal; therefore there's some
                     * transitional work that needs done.
                     * */
                    if ( ctorTypes[argIndex].IsValueType &&
                        !delegateTypes[argIndex].IsValueType)
                        interLangGenerator.Emit(OpCodes.Unbox_Any, ctorTypes[argIndex]);
                    else if (!ctorTypes[argIndex].IsValueType &&
                              delegateTypes[argIndex].IsValueType)
                        interLangGenerator.Emit(OpCodes.Box, delegateTypes[argIndex]);
                    else
                        interLangGenerator.Emit(OpCodes.Castclass, ctorTypes[argIndex]);
                }
                argIndex++;
            });

            interLangGenerator.Emit(OpCodes.Newobj, ctor);
            if (ctor.DeclaringType.IsValueType)
                interLangGenerator.Emit(OpCodes.Box, ctor.DeclaringType);

            interLangGenerator.Emit(OpCodes.Ret);
            return (TDelegate)(object)optimizedCtor.CreateDelegate(typeof(TDelegate));
        }
        #endregion

        #region Type Parameter Constructor Procurement

        public static ConstructorInfo[] ObtainTypeParameterConstrutors(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().First();
                    var sigSource = sigs.GenericLocalType;
                    var ctors     = sigSource.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                    return ctors;
                }
                catch (InvalidOperationException)
                {
                    return new ConstructorInfo[0];
                }
            }
            else
                throw new ArgumentException("parameter");
        }

        public static MethodInfo[] ObtainTypeParameterMethods(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().First();
                    var sigSource = sigs.GenericLocalType;
                    var methods   = sigSource.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    return methods;
                }
                catch (InvalidOperationException)
                {
                    //First fails
                    return new MethodInfo[0];
                }
            }
            else
                throw new ArgumentException("parameter");
        }

        public static PropertyInfo[] ObtainTypeParameterProperties(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().First();
                    var sigSource                         = sigs.GenericLocalType;
                    var properties                        = sigSource.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    List<PropertyInfo> filteredProperties = new List<PropertyInfo>();
                    foreach (var prop in properties)
                    {
                        /* *
                         * Check to make sure the properties have the proper
                         * quantity of parameters on the get or set
                         * aspect, respectively.
                         * */
                        if (prop.CanRead)
                        {
                            if (prop.GetGetMethod(true).GetParameters().Length > 0)
                                continue;
                        }
                        else if (prop.CanWrite)
                            if (prop.GetSetMethod(true).GetParameters().Length > 1)
                                continue;
                        filteredProperties.Add(prop);
                    }
                    return filteredProperties.ToArray();
                }
                catch (InvalidOperationException)
                {
                    //First fails
                    return new PropertyInfo[0];
                }
            }
            else
                throw new ArgumentException("parameter");
        }

        public static PropertyInfo[] ObtainTypeParameterIndexers(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                GenericParamDataTargetAttribute sigs;
                try
                {
                    sigs = parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().First();
                }
                catch (InvalidOperationException)
                {
                    //First fails
                    return new PropertyInfo[0];
                }
                var sigSource                       = sigs.GenericLocalType;
                var indexers                        = sigSource.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                List<PropertyInfo> filteredIndexers = new List<PropertyInfo>();
                foreach (var prop in indexers)
                {
                    /* *
                     * Check to make sure the properties have the proper
                     * quantity of parameters on the get or set
                     * aspect, respectively, for an indexer.
                     * */
                    if (prop.CanRead && prop.GetGetMethod(true).GetParameters().Length == 0)
                        continue;
                    else if (prop.CanWrite && prop.GetSetMethod(true).GetParameters().Length <= 1)
                        continue;
                    filteredIndexers.Add(prop);
                }
                return filteredIndexers.ToArray();
            }
            else
                throw new ArgumentException("parameter");
        }

        public static EventInfo[] ObtainTypeParameterEvents(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().First();
                    var sigSource = sigs.GenericLocalType;
                    var events    = sigSource.GetEvents(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    return events;
                }
                catch (InvalidOperationException)
                {
                    //First fails
                    return new EventInfo[0];
                }
            }
            else
                throw new ArgumentException("parameter");
        }

        #endregion
    }
}
