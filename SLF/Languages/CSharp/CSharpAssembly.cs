using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    /// <summary>
    /// Provides a base implementation for an assembly
    /// defined within the C&#9839; language.
    /// </summary>
    public class CSharpAssembly :
        IntermediateAssembly<ICSharpLanguage, ICSharpProvider, CSharpAssembly, IIntermediateCliManager, Type, Assembly>,
        ICSharpAssembly
    {
        private ICSharpProvider provider;
        /// <summary>
        /// Creates a new <see cref="CSharpAssembly"/> which is linked to another
        /// <paramref name="root"/> <see cref="CSharpAssembly"/>.
        /// </summary>
        /// <param name="root">The <see cref="CSharpAssembly"/>
        /// from which the current is a part of.</param>
        internal protected CSharpAssembly(CSharpAssembly root)
            : base(root)
        {
        }


        internal protected CSharpAssembly(string name, ICSharpProvider provider)
            : base(name)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Returns a new <see cref="CSharpAssembly"/> for the 
        /// 
        /// </summary>
        /// <returns></returns>
        protected override CSharpAssembly GetNewPart()
        {
            return new CSharpAssembly(this);
        }


        //#region IIntermediateAssembly<ICSharpLanguage,ICSharpCompilationUnit,ICSharpProvider> Members

        public override ICSharpLanguage Language
        {
            get {
                if (this.IsRoot)
                    return this.provider.Language;
                else
                    return this.GetRoot().provider.Language;
            }
        }

        public override ICSharpProvider Provider
        {
            get {
                if (this.IsRoot)
                    return this.provider;
                else
                    return this.GetRoot().provider;
            }
        }

        //#endregion

    }
}
