using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    /// <summary>
    /// Defines properties and methods for working with the MyProject class which
    /// exposes the <see cref="IMyApplicationClass"/>, <see cref="IMyComputerClass"/>,
    /// and the <see cref="IMyWebservicesClass"/>.
    /// </summary>
    public interface IMyProjectClass :
        IIntermediateClassType
    {
        /// <summary>
        /// Returns the <see cref="IMyVisualBasicAssembly"/> in which
        /// the <see cref="IMyProjectClass"/> is defined.
        /// </summary>
        new IMyVisualBasicAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IMyNamespaceDeclaration"/> in which the
        /// <see cref="IMyProjectClass"/> is defined.
        /// </summary>
        new IMyNamespaceDeclaration Parent { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMember"/>
        /// relative to the <see cref="IMyApplicationClass"/> for the
        /// <see cref="IMyVisualBasicAssembly"/>.
        /// </summary>
        IIntermediatePropertyMember Application { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMember"/>
        /// relative to the <see cref="IMyComputerClass"/> for the
        /// <see cref="IMyVisualBasicAssembly"/>.
        /// </summary>
        IIntermediatePropertyMember Computer { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMember"/>
        /// relative to the <see cref="IMyWebservicesClass"/> for the
        /// <see cref="IMyVisualBasicAssembly"/>.
        /// </summary>
        IIntermediatePropertyMember WebServices { get; }
        /// <summary>
        /// Returns the <see cref="IMyWebServicesClass"/> which denotes
        /// the object that marshals the <see cref="WebServices"/>.
        /// </summary>
        IMyWebservicesClass MyWebServices { get; }
    }
}
