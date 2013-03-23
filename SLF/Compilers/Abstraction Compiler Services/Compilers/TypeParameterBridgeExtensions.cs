using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.CompilerServices;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with the functionality associated
    /// to building type-parameter bridges.
    /// </summary>
    public static class TypeParameterBridgeExtensions
    {
        /* *
         * Added 7/12/2011 - Extracted into method via LINQ expression,
         * removed ctorParameters.OnAll(parameter => { ...
         * because it was unnecessary, foreach works just as well.
         * */
        private static Type[] GetParameterTypes(this IEnumerable<ParameterInfo> series)
        {
            return (from param in series
                    select param.ParameterType).ToArray();
        }

        /// <summary>
        /// Builds an optimized constructor delegate using a dynamic method
        /// bound to the <typeparamref name="TDelegate"/> provided for the
        /// <paramref name="constructor"/>.
        /// </summary>
        /// <typeparam name="TDelegate">The type of delegate used to bind to <paramref name="constructor"/>.</typeparam>
        /// <param name="constructor">The constructor to build a dynamic method for that matches
        /// the signature of <typeparamref name="TDelegate"/>.</param>
        /// <returns>A <typeparamref name="TDelegate"/> instance
        /// provided there's no issues between the call between <typeparamref name="TDelegate"/>
        /// and the <paramref name="constructor"/>.</returns>
        /// <exception cref="System.ArgumentException"><typeparamref name="TDelegate"/>
        /// is not a type of a delegate, or <paramref name="constructor"/> does not match the parameter
        /// list of the delegate provided in <typeparamref name="TDelegate"/>.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="constructor"/> is null.</exception>
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.PreserveSig)]
        public static TDelegate BuildOptimizedConstructorDelegate<TDelegate>(this ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("ctor");
            var delegateType = typeof(TDelegate);
            if (!delegateType.IsSubclassOf(typeof(Delegate)))
                throw new ArgumentException("TDelegate");
            //Obtain the invoke method on the delegate and obtain its signature.
            var delegateInvoke = delegateType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            var delegateTypes = delegateInvoke.GetParameters().GetParameterTypes();

            //Obtain the method call parameters.
            var ctorParameters = constructor.GetParameters();
            if (delegateTypes.Length != ctorParameters.Length)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.ctor, ExceptionMessageId.DelegateTypeParameterMismatch);

            var optimizedCtor = new DynamicMethod(string.Format(".ctor@{0}({1})", constructor.DeclaringType.Name, GetStringFormSignature(delegateTypes)), delegateInvoke.ReturnType, delegateTypes, constructor.DeclaringType, true);
            int argIndex = 0;
            var ctorTypes = ctorParameters.GetParameterTypes();
            //Instantiate the generator.
            var interLangGenerator = optimizedCtor.GetILGenerator();
            optimizedCtor.InitLocals = true;
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
            foreach (var parameter in ctorParameters)
            {
                if (parameter.IsOut || parameter.ParameterType.IsByRef)
                {
                    if (argIndex < 0x100)
                        interLangGenerator.Emit(OpCodes.Ldarga_S, argIndex);
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
                            if (argIndex < 0x100)
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
                    if (ctorTypes[argIndex].IsValueType &&
                        !delegateTypes[argIndex].IsValueType)
                        interLangGenerator.Emit(OpCodes.Unbox_Any, ctorTypes[argIndex]);
                    else if (!ctorTypes[argIndex].IsValueType &&
                              delegateTypes[argIndex].IsValueType)
                        interLangGenerator.Emit(OpCodes.Box, delegateTypes[argIndex]);
                    else
                        interLangGenerator.Emit(OpCodes.Castclass, ctorTypes[argIndex]);
                }
                argIndex++;
            }


            interLangGenerator.Emit(OpCodes.Newobj, constructor);
            if (constructor.DeclaringType.IsValueType && !delegateInvoke.ReturnType.IsValueType)
                interLangGenerator.Emit(OpCodes.Box, constructor.DeclaringType);

            interLangGenerator.Emit(OpCodes.Ret);
            return (TDelegate)(object)optimizedCtor.CreateDelegate(typeof(TDelegate));
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


        #region Type Parameter [Structural Typing] Member Procurement

        public static ConstructorInfo[] ObtainTypeParameterConstrutors(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().FirstOrDefault();
                    if (sigs == null)
                        goto yieldNone;
                    var sigSource = sigs.GenericLocalType;
                    var ctors = from c in sigSource.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                                /* *
                                 * Might otherwise imply new() constraint.
                                 * */
                                where c.GetParameters().Length > 0
                                select c;
                    if ((parameter.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) == GenericParameterAttributes.DefaultConstructorConstraint)
                        ctors = new ConstructorInfo[] { new GenericParameterDefaultConstructorConstraint(parameter) }.Concat(ctors);
                    return ctors.ToArray();
                }
                catch (InvalidOperationException)
                {
                    //Type obtained in an assembly loaded for reflection only.
                    goto yieldNone;
                }
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.parameter, ExceptionMessageId.TypeMustBeGenericParameter);
        yieldNone:
            if ((parameter.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) == GenericParameterAttributes.DefaultConstructorConstraint)
                return new ConstructorInfo [] { new GenericParameterDefaultConstructorConstraint(parameter) };
            return new ConstructorInfo[0];
        }

        public static MethodInfo[] ObtainTypeParameterMethods(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().FirstOrDefault();
                    if (sigs == null)
                        goto yieldNone;
                    var sigSource = sigs.GenericLocalType;
                    var methods = sigSource.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    return methods;
                }
                catch (InvalidOperationException)
                {
                    //Type obtained in an assembly loaded for reflection only.
                    goto yieldNone;
                }
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.parameter, ExceptionMessageId.TypeMustBeGenericParameter);
        yieldNone:
            return new MethodInfo[0];
        }

        public static PropertyInfo[] ObtainTypeParameterProperties(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().FirstOrDefault();
                    if (sigs == null)
                        goto yieldNone;
                    var sigSource = sigs.GenericLocalType;
                    var properties = (from property in sigSource.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                      where property.GetIndexParameters().Length == 0
                                      select property).ToArray();
                    return properties;
                }
                catch (InvalidOperationException)
                {
                    //Type obtained in an assembly loaded for reflection only.
                    goto yieldNone;
                }
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.parameter, ExceptionMessageId.TypeMustBeGenericParameter);
        yieldNone:
            return new PropertyInfo[0];
        }

        public static PropertyInfo[] ObtainTypeParameterIndexers(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                GenericParamDataTargetAttribute sigs;
                try
                {
                    sigs = parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().FirstOrDefault();
                    if (sigs == null)
                        goto yieldNone;
                }
                catch (InvalidOperationException)
                {
                    //Type obtained in an assembly loaded for reflection only.
                    goto yieldNone;
                }
                var sigSource = sigs.GenericLocalType;
                return (from indexer in sigSource.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        where indexer.GetIndexParameters().Length > 0
                        select indexer).ToArray();
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.parameter, ExceptionMessageId.TypeMustBeGenericParameter);
        yieldNone:
            return new PropertyInfo[0];
        }

        public static EventInfo[] ObtainTypeParameterEvents(this Type parameter)
        {
            if (parameter.IsGenericParameter)
            {
                try
                {
                    GenericParamDataTargetAttribute sigs =
                        parameter.GetCustomAttributes(typeof(GenericParamDataTargetAttribute), true).Cast<GenericParamDataTargetAttribute>().FirstOrDefault();
                    if (sigs == null)
                        goto yieldNone;
                    var sigSource = sigs.GenericLocalType;
                    var events = sigSource.GetEvents(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    return events;
                }
                catch (InvalidOperationException)
                {
                    //First fails
                    goto yieldNone;
                }
            }
            else
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.parameter, ExceptionMessageId.TypeMustBeGenericParameter);
        yieldNone:
            return new EventInfo[0];
        }

        #endregion

    }
}
