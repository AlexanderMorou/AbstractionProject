using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public interface IIntermediateDynamicMethod :
        IIntermediateMethodMember<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler>
    {
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>();
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and nine parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/> and <typeparamref name="T8"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and nine parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/>,
        /// <typeparamref name="T8"/> and <typeparamref name="T9"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T9">The type of the ninth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and eight parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/> and <typeparamref name="T8"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4, T5, T6, T7, T8> Compile<T1, T2, T3, T4, T5, T6, T7, T8>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and eight parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/>, <typeparamref name="T7"/> and <typeparamref name="T8"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T8">The type of the eighth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and seven parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/> and <typeparamref name="T7"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4, T5, T6, T7> Compile<T1, T2, T3, T4, T5, T6, T7>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and seven parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/> and <typeparamref name="T7"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and six parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/> and <typeparamref name="T6"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4, T5, T6> Compile<T1, T2, T3, T4, T5, T6>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and six parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/> and <typeparamref name="T6"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, T5, T6, TResult> CompileAs<T1, T2, T3, T4, T5, T6, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and five parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/> and <typeparamref name="T5"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4, T5> Compile<T1, T2, T3, T4, T5>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and five parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/> and <typeparamref name="T5"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, T5, TResult> CompileAs<T1, T2, T3, T4, T5, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and four parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3, T4> Compile<T1, T2, T3, T4>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and four parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, T4, TResult> CompileAs<T1, T2, T3, T4, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and three parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/> and <typeparamref name="T3"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2, T3> Compile<T1, T2, T3>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and three parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/> and <typeparamref name="T3"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, T3, TResult> CompileAs<T1, T2, T3, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and two parameters with types <typeparamref name="T1"/>
        /// and <typeparamref name="T2"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1, T2> Compile<T1, T2>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and two parameters with types <typeparamref name="T1"/>
        /// and <typeparamref name="T2"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, T2, TResult> CompileAs<T1, T2, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined and one parameter
        /// of type <typeparamref name="T1"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action<T1> Compile<T1>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined as <typeparamref name="TResult"/>
        /// and one parameter of type <typeparamref name="T1"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IIntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<T1, TResult> CompileAs<T1, TResult>();
        /// <summary>
        /// Compiles the dynamic method into a delegate with no return or parameters defined.
        /// </summary>
        /// <returns>A <see cref="Action"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Action Compile();
        /// <summary>
        /// Compiles the dynamic method to a delegate with a return defined
        /// as <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{TResult}"/> which represents the <see cref="IIntermediateDynamicMethod"/>.</returns>
        Func<TResult> CompileAs<TResult>();
    }
}
