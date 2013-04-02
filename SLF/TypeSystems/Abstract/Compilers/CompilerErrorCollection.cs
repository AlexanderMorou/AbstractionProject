using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerErrorCollection : 
        ControlledCollection<ICompilerMessage>,
        ICompilerErrorCollection
    {
        #region ICompilerErrorCollection Members

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceWarning"/>
        /// with the <paramref name="message"/>, <paramref name="column"/>, <paramref name="line"/>, 
        /// and <paramref name="fileName"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> instance from
        /// which the <see cref="ICompilerSourceWarning"/> obtains its message.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceError"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceError"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found associated to the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceWarning"/> instance
        /// which denotes the details of the compiler warning.</returns>
        public ICompilerSourceWarning SourceWarning(ICompilerReferenceWarning message, LineColumnPair start, LineColumnPair end, string fileName, params string[] replacements)
        {
            CompilerSourceWarning warning = new CompilerSourceWarning(message, fileName, start, end, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceError"/> 
        /// with the <paramref name="message"/>, <paramref name="column"/>, <paramref name="line"/>, 
        /// and <paramref name="fileName"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceError"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceError"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceError"/> instance
        /// which denotes the details of the compiler error.</returns>
        public ICompilerSourceError SourceError(ICompilerReferenceError message, LineColumnPair start, LineColumnPair end, string fileName, params string[] replacements)
        {
            CompilerSourceError error = new CompilerSourceError(message, fileName, start, end, replacements);
            base.AddImpl(error);
            return error;
        }


        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelError{T1}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelError{T1}"/> which represents the error.</returns>
        public ICompilerModelError<T1> ModelError<T1>(ICompilerReferenceError message, T1 item1, params string[] replacements)
        {
            var error = new CompilerModelError<T1>(message, item1, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelError{T1, T2}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/> , <paramref name="item2"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelError{T1, T2}"/> which represents the error.</returns>
        public ICompilerModelError<T1, T2> ModelError<T1, T2>(ICompilerReferenceError message, T1 item1, T2 item2, params string[] replacements)
        {
            var error = new CompilerModelError<T1, T2>(message, item1, item2, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelError{T1, T2, T3}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="item3">The tertiary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelError{T1, T2, T3}"/> which represents the error.</returns>
        public ICompilerModelError<T1, T2, T3> ModelError<T1, T2, T3>(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, params string[] replacements)
        {
            var error = new CompilerModelError<T1, T2, T3>(message, item1, item2, item3, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelError{T1, T2, T3, T4}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/>, <paramref name="item2"/>,
        /// <paramref name="item3"/>, <paramref name="item4"/> and 
        /// <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the error.</typeparam>
        /// <typeparam name="T4">The kind of <paramref name="item4"/> used to 
        /// reference to the quaternary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="item3">The tertiary target of the error.</param>
        /// <param name="item4">The quaternary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelError{T1, T2, T3, T4}"/> which represents the error.</returns>
        public ICompilerModelError<T1, T2, T3, T4> ModelError<T1, T2, T3, T4>(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, T4 item4, params string[] replacements)
        {
            var error = new CompilerModelError<T1, T2, T3, T4>(message, item1, item2, item3, item4, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelWarning{T1}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelWarning{T1}"/> which represents the warning.</returns>
        public ICompilerModelWarning<T1> ModelWarning<T1>(ICompilerReferenceWarning message, T1 item1, params string[] replacements)
        {
            var warning = new CompilerModelWarning<T1>(message, item1, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelWarning{T1, T2}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/> , <paramref name="item2"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelWarning{T1, T2}"/> which represents the warning.</returns>
        public ICompilerModelWarning<T1, T2> ModelWarning<T1, T2>(ICompilerReferenceWarning message, T1 item1, T2 item2, params string[] replacements)
        {
            var warning = new CompilerModelWarning<T1, T2>(message, item1, item2, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelWarning{T1, T2, T3}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="item3">The tertiary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelWarning{T1, T2, T3}"/> 
        /// which represents the warning.</returns>
        public ICompilerModelWarning<T1, T2, T3> ModelWarning<T1, T2, T3>(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, params string[] replacements)
        {
            var warning = new CompilerModelWarning<T1, T2, T3>(message, item1, item2, item3, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerModelWarning{T1, T2, T3, T4}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/>, <paramref name="item2"/>,
        /// <paramref name="item3"/>, <paramref name="item4"/> and 
        /// <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the warning.</typeparam>
        /// <typeparam name="T4">The kind of <paramref name="item4"/> used to 
        /// reference to the quaternary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="item3">The tertiary target of the warning.</param>
        /// <param name="item4">The quaternary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerModelWarning{T1, T2, T3, T4}"/>
        /// which represents the warning.</returns>
        public ICompilerModelWarning<T1, T2, T3, T4> ModelWarning<T1, T2, T3, T4>(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, T4 item4, params string[] replacements)
        {
            var warning = new CompilerModelWarning<T1, T2, T3, T4>(message, item1, item2, item3, item4, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelError{T1}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelError{T1}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelError{T1}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelError{T1}"/> which represents the error.</returns>
        public ICompilerSourceModelError<T1> SourceModelError<T1>(ICompilerReferenceError message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, params string[] replacements)
        {
            var error = new CompilerSourceModelError<T1>(message, item1, fileName, start, end, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelError{T1, T2}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/> , <paramref name="item2"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelError{T1, T2}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelError{T1, T2}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelError{T1, T2}"/> which represents the error.</returns>
        public ICompilerSourceModelError<T1, T2> SourceModelError<T1, T2>(ICompilerReferenceError message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, params string[] replacements)
        {
            var error = new CompilerSourceModelError<T1, T2>(message, item1, item2, fileName, start, end, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelError{T1, T2, T3}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelError{T1, T2, T3}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelError{T1, T2, T3}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="item3">The tertiary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelError{T1, T2, T3}"/> which represents the error.</returns>
        public ICompilerSourceModelError<T1, T2, T3> SourceModelError<T1, T2, T3>(ICompilerReferenceError message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, T3 item3, params string[] replacements)
        {
            var error = new CompilerSourceModelError<T1, T2, T3>(message, item1, item2, item3, fileName, start, end, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelError{T1, T2, T3, T4}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/>, <paramref name="item2"/>,
        /// <paramref name="item3"/>, <paramref name="item4"/> and 
        /// <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the error.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the error.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the error.</typeparam>
        /// <typeparam name="T4">The kind of <paramref name="item4"/> used to 
        /// reference to the quaternary model element which caused the error.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceError"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelError{T1, T2, T3, T4}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelError{T1, T2, T3, T4}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the error.</param>
        /// <param name="item2">The secondary target of the error.</param>
        /// <param name="item3">The tertiary target of the error.</param>
        /// <param name="item4">The quaternary target of the error.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelError{T1, T2, T3, T4}"/> which represents the error.</returns>
        public ICompilerSourceModelError<T1, T2, T3, T4> SourceModelError<T1, T2, T3, T4>(ICompilerReferenceError message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, T3 item3, T4 item4, params string[] replacements)
        {
            var error = new CompilerSourceModelError<T1, T2, T3, T4>(message, item1, item2, item3, item4, fileName, start, end, replacements);
            base.AddImpl(error);
            return error;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelWarning{T1}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelWarning{T1}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelWarning{T1}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelWarning{T1}"/> which represents the warning.</returns>
        public ICompilerSourceModelWarning<T1> SourceModelWarning<T1>(ICompilerReferenceWarning message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, params string[] replacements)
        {
            var warning = new CompilerSourceModelWarning<T1>(message, item1, fileName, start, end, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelWarning{T1, T2}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/> , <paramref name="item2"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelWarning{T1, T2}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelWarning{T1, T2}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelWarning{T1, T2}"/> which represents the warning.</returns>
        public ICompilerSourceModelWarning<T1, T2> SourceModelWarning<T1, T2>(ICompilerReferenceWarning message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, params string[] replacements)
        {
            var warning = new CompilerSourceModelWarning<T1, T2>(message, item1, item2, fileName, start, end, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/>, <paramref name="item2"/>, <paramref name="item3"/> 
        /// and <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="item3">The tertiary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/> 
        /// which represents the warning.</returns>
        public ICompilerSourceModelWarning<T1, T2, T3> SourceModelWarning<T1, T2, T3>(ICompilerReferenceWarning message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, T3 item3, params string[] replacements)
        {
            var warning = new CompilerSourceModelWarning<T1, T2, T3>(message, item1, item2, item3, fileName, start, end, replacements);
            base.AddImpl(warning);
            return warning;
        }

        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
        /// with the <paramref name="message"/>, <paramref name="start"/>, <paramref name="end"/>, 
        /// <paramref name="fileName"/>, <paramref name="item1"/>, <paramref name="item2"/>,
        /// <paramref name="item3"/>, <paramref name="item4"/> and 
        /// <paramref name="replacements"/> provided.
        /// </summary>
        /// <typeparam name="T1">The kind of <paramref name="item1"/> used to 
        /// reference to the primary model element which caused the warning.</typeparam>
        /// <typeparam name="T2">The kind of <paramref name="item2"/> used to 
        /// reference to the secondary model element which caused the warning.</typeparam>
        /// <typeparam name="T3">The kind of <paramref name="item3"/> used to 
        /// reference to the tertiary model element which caused the warning.</typeparam>
        /// <typeparam name="T4">The kind of <paramref name="item4"/> used to 
        /// reference to the quaternary model element which caused the warning.</typeparam>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> value associated which
        /// defines the base message text.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> value which denotes
        /// the start of the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/> which results.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> value which denotes
        /// the end of the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/> which results.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="item1">The primary target of the warning.</param>
        /// <param name="item2">The secondary target of the warning.</param>
        /// <param name="item3">The tertiary target of the warning.</param>
        /// <param name="item4">The quaternary target of the warning.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
        /// which represents the warning.</returns>
        public ICompilerSourceModelWarning<T1, T2, T3, T4> SourceModelWarning<T1, T2, T3, T4>(ICompilerReferenceWarning message, LineColumnPair start, LineColumnPair end, string fileName, T1 item1, T2 item2, T3 item3, T4 item4, params string[] replacements)
        {
            var warning = new CompilerSourceModelWarning<T1, T2, T3, T4>(message, item1, item2, item3, item4, fileName, start, end, replacements);
            base.AddImpl(warning);
            return warning;
        }

        #endregion

        /// <summary>
        /// Returns whether the <see cref="ICompilerErrorCollection"/> 
        /// has an <see cref="ICompilerSourceError"/>.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Any(p => p is ICompilerError); }
        }
    }
}
