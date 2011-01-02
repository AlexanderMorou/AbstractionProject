using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    internal class IntermediateDynamicMethod :
        IntermediateMethodMemberBase<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler>,
        IIntermediateDynamicMethod
    {
        private Delegate compiledDelegate;
        public IntermediateDynamicMethod(string name, IntermediateDynamicHandler parent)
            : base(name, parent)
        {
        }

        public override IIntermediateAssembly Assembly
        {
            get { return null; }
        }

        protected override IIntermediateDynamicMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
        {
            throw new NotImplementedException();
        }

        private T _Compile<T>()
        {
            throw new NotImplementedException();
        }

        #region IIntermediateDynamicMethod Members

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 16)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 16)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 15)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 15)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 14)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 14)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 13)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 13)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 12)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 12)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 11)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 11)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 10)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 10)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Compile<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 9)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 9)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Action<T1, T2, T3, T4, T5, T6, T7, T8> Compile<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 8)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7, T8>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8>))
                return (Action<T1, T2, T3, T4, T5, T6, T7, T8>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        public Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, T8, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 8)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and six parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/> and <typeparamref name="T7"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2, T3, T4, T5, T6, T7> Compile<T1, T2, T3, T4, T5, T6, T7>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 7)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6, T7>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6, T7>))
                return (Action<T1, T2, T3, T4, T5, T6, T7>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and seven parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/>, <typeparamref name="T6"/> and <typeparamref name="T7"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T7">The type of the seventh parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, T3, T4, T5, T6, T7, TResult> CompileAs<T1, T2, T3, T4, T5, T6, T7, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 7)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, T7, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, T7, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, T7, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and six parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/> and <typeparamref name="T6"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5, T6}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2, T3, T4, T5, T6> Compile<T1, T2, T3, T4, T5, T6>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 6)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5, T6>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5, T6>))
                return (Action<T1, T2, T3, T4, T5, T6>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and six parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/>,
        /// <typeparamref name="T5"/> and <typeparamref name="T6"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T6">The type of the sixth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, T3, T4, T5, T6, TResult> CompileAs<T1, T2, T3, T4, T5, T6, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 6)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, T6, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, T6, TResult>))
                return (Func<T1, T2, T3, T4, T5, T6, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and five parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/> and <typeparamref name="T5"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4, T5}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2, T3, T4, T5> Compile<T1, T2, T3, T4, T5>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 5)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4, T5>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4, T5>))
                return (Action<T1, T2, T3, T4, T5>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and five parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/> and <typeparamref name="T5"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T5">The type of the fifth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, T3, T4, T5, TResult> CompileAs<T1, T2, T3, T4, T5, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 5)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, T5, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, T5, TResult>))
                return (Func<T1, T2, T3, T4, T5, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and four parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3, T4}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2, T3, T4> Compile<T1, T2, T3, T4>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 4)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3, T4>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3, T4>))
                return (Action<T1, T2, T3, T4>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and four parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, T4, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, T3, T4, TResult> CompileAs<T1, T2, T3, T4, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 4)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, T4, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, T4, TResult>))
                return (Func<T1, T2, T3, T4, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and three parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/> and <typeparamref name="T3"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2, T3}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2, T3> Compile<T1, T2, T3>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 3)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2, T3>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2, T3>))
                return (Action<T1, T2, T3>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and three parameters with types <typeparamref name="T1"/>,
        /// <typeparamref name="T2"/> and <typeparamref name="T3"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, T3, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, T3, TResult> CompileAs<T1, T2, T3, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 3)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, T3, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, T3, TResult>))
                return (Func<T1, T2, T3, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined 
        /// and two parameters with types <typeparamref name="T1"/>
        /// and <typeparamref name="T2"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1, T2}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1, T2> Compile<T1, T2>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 2)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1, T2>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1, T2>))
                return (Action<T1, T2>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined 
        /// as <typeparamref name="TResult"/> and two parameters with types <typeparamref name="T1"/>
        /// and <typeparamref name="T2"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, T2, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, T2, TResult> CompileAs<T1, T2, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 2)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, T2, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, T2, TResult>))
                return (Func<T1, T2, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return defined and one parameter
        /// of type <typeparamref name="T1"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <returns>A <see cref="Action{T1}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action<T1> Compile<T1>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 1)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action<T1>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action<T1>))
                return (Action<T1>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with a return defined as <typeparamref name="TResult"/>
        /// and one parameter of type <paramref name="T1"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter of the method that the <see cref="IntermediateDynamicMethod"/> represents.</typeparam>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{T1, TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<T1, TResult> CompileAs<T1, TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 1)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<T1, TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<T1, TResult>))
                return (Func<T1, TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method into a delegate with no return or parameters defined.
        /// </summary>
        /// <returns>A <see cref="Action"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Action Compile()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 0)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(IntermediateGateway.CommonlyUsedTypeReferences.Void))
                    return _Compile<Action>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Action))
                return (Action)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        /// <summary>
        /// Compiles the dynamic method to a delegate with a return defined
        /// as <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type used to represent the result value of the function to compile.</typeparam>
        /// <returns>A <see cref="Func{TResult}"/> which represents the <see cref="IntermediateDynamicMethod"/>.</returns>
        public Func<TResult> CompileAs<TResult>()
        {
            if (this.compiledDelegate == null)
            {
                if (this.Parameters.Count != 0)
                    throw new InvalidOperationException(Resources.Exception_ParameterCount_Mismatch);
                if (this.ReturnType.Equals(typeof(TResult).GetTypeReference()))
                    return _Compile<Func<TResult>>();
                else
                    throw new InvalidOperationException(Resources.Exception_ReturnType_Mismatch);
            }
            else if (compiledDelegate.GetType() == typeof(Func<TResult>))
                return (Func<TResult>)compiledDelegate;
            else
                throw new InvalidOperationException(Resources.Exception_Precompiled_Mismatch);
        }

        #endregion
    }
}
