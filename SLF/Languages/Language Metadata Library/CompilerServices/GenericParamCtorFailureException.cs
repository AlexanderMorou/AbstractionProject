using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.CompilerServices
{
    /// <summary>
    /// The exception is thrown when type-loading fails due to 
    /// a type passed to a generic does not contain the proper
    /// signature publicly.
    /// </summary>
    public class GenericParamCtorFailureException :
        TypeLoadException
    {
        private Type[] correctSignature;
        private Type failedType;
        private string typeParameter;

        /// <summary>
        /// Creates a new <see cref="GenericParamCtorFailureException"/> with the 
        /// <paramref name="typeParameter"/> and <paramref name="failedType"/> provided.
        /// </summary>
        /// <param name="typeParameter">The <see cref="String"/> form of the 
        /// type-parameter that caused the failure.</param>
        /// <param name="failedType">The <see cref="System.Type"/> passed
        /// that failed the constraint check.</param>
        public GenericParamCtorFailureException(string typeParameter, Type failedType)
            : base(string.Format("Constraints on generic parameter {0} failed on the type provided ({1}).", typeParameter, failedType.FullName == null ? failedType.Name : failedType.FullName))
        {
            this.typeParameter = typeParameter;
            this.failedType = failedType;
        }
        /// <summary>
        /// Creates a new <see cref="GenericParamCtorFailureException"/> with the 
        /// <paramref name="typeParameter"/>, <paramref name="failedType"/> 
        /// and <paramref name="correctSignature"/> provided.
        /// </summary>
        /// <param name="typeParameter">The <see cref="String"/> form of the 
        /// type-parameter that caused the failure.</param>
        /// <param name="failedType">The <see cref="System.Type"/> passed
        /// that failed the constraint check.</param>
        /// <param name="correctSignature">The <see cref="System.Type"/> array
        /// of the proper signature that <paramref name="failedType"/> did not
        /// have a public version of.</param>
        public GenericParamCtorFailureException(string typeParameter, Type failedType, params Type[] correctSignature)
        {
            this.correctSignature = correctSignature;
            this.typeParameter = typeParameter;
            this.failedType = failedType;
        }

        /// <summary>
        /// Returns the <see cref="String"/> form of the 
        /// type parameter that failed.
        /// </summary>
        public string TypeParameter
        {
            get
            {
                return this.typeParameter;
            }
        }
        /// <summary>
        /// Returns the <see cref="Type"/> that was passed
        /// that didn't have a constructor with the 
        /// <see cref="CorrectSignature"/>.
        /// </summary>
        public Type FailedType
        {
            get
            {
                return this.failedType;
            }
        }

        /// <summary>
        /// Returns the <see cref="Type"/> array that defines the
        /// proper signature that needed to be present, and public.
        /// </summary>
        public Type[] CorrectSignature
        {
            get
            {
                return this.correctSignature;
            }
        }
    }
}
