using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    /// <summary>
    /// Defines properties and methods for working with a class dedicated
    /// to common aspects of the computer during run-time.
    /// </summary>
    public interface IMyComputerClass :
        IIntermediateClassType
    {
        /// <summary>
        /// Returns the <see cref="IMyVisualBasicAssembly"/> in which
        /// the <see cref="IMyComputerClass"/> is defined.
        /// </summary>
        new IMyVisualBasicAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IMyNamespaceDeclaration"/> in which the
        /// <see cref="IMyComputerClass"/> is defined.
        /// </summary>
        new IMyNamespaceDeclaration Parent { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the audio output
        /// of the active computer.
        /// </summary>
        IClassPropertyMember Audio { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the clipboard
        /// of the active computer.
        /// </summary>
        IClassPropertyMember Clipboard { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the keyboard input
        /// of the active computer and common states of the input.
        /// </summary>
        IClassPropertyMember Keyboard { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the mouse input
        /// of the active computer and common configuration information
        /// of the active mouse.
        /// </summary>
        IClassPropertyMember Mouse { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the ports 
        /// of the active computer.
        /// </summary>
        IClassPropertyMember Ports { get; }
        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/>
        /// which denotes an object which can access the primary screen
        /// of the active computer.
        /// </summary>
        IClassPropertyMember Screen { get; }
    }
}
