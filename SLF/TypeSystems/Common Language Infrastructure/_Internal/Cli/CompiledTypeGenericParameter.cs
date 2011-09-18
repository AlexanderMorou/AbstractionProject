using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class CompiledGenericTypeParameter<TType> :
        CompiledGenericParameterMemberBase<IGenericTypeParameter<TType>, TType>,
        ICompiledGenericTypeParameter<TType>
        where TType :
            class,
            IGenericType<TType>
    {
        protected CompiledGenericTypeParameter(TType parent, Type type)
            : base(parent, type)
        {
        }

        #region IGenericParameter Members

        IGenericParamParent IGenericParameter.Parent
        {
            get { return this.Parent; }
        }

        IGenericParameterConstructorMemberDictionary IGenericParameter.Constructors
        {
            get { return (IGenericParameterConstructorMemberDictionary)base.Constructors; }
        }

        #endregion

        #region IGenericTypeParameter Members

        IGenericType IGenericTypeParameter.Parent
        {
            get { return base.Parent; }
        }

        #endregion
    }
}