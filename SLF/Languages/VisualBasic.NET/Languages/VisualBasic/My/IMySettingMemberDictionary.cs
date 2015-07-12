using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    /// <summary>
    /// Defines properties and methods for working with a series of settings
    /// within a <see cref="IMyVisualBasicAssembly"/>.
    /// </summary>
    public interface IMySettingMemberDictionary :
        IDeclarationDictionary<IGeneralMemberUniqueIdentifier, IMySettingMember>
    {
        /// <summary>
        /// Returns the <see cref="IMySettingsClass"/> which contains the
        /// <see cref="IMySettingMemberDictionary"/>.
        /// </summary>
        IMySettingsClass Parent { get; }
    }
}
