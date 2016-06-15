using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>Defines properties and methods for working with a service which maintains unique identity amongst members of a given type or
    /// assembly.</summary>
    public interface IIntermediateIdentifierLanguageQualifierService :
        ILanguageService
    {
        bool HandlesGenericParameterIdentifier { get; }
        bool HandlesEventMemberIdentifier { get; }
        bool HandlesEventSignatureMemberIdentifier { get; }
        bool HandlesIndexerMemberIdentifier { get; }
        bool HandlesIndexerSignatureMemberIdentifier { get; }
        bool HandlesConstructorMemberIdentifier { get; }
        bool HandlesConstructorSignatureMemberIdentifier { get; }
        bool HandlesMemberIdentifier { get; }
        bool HandlesMethodMemberIdentifier{ get; }
        bool HandlesMethodSignatureMemberIdentifier{ get; }
        bool HandlesTypeIdentifier { get; }
        bool HandlesAssemblyIdentifier { get; }
        bool HandlesTypeCoercionIdentifier { get; }
        bool HandlesBinaryOperatorCoercionIdentifier { get; }
        bool HandlesUnaryOperatorCoercionIdentifier { get; }

        IAssemblyUniqueIdentifier                       GetIdentifier(IIntermediateAssembly                     assembly);
        IGenericParameterUniqueIdentifier               GetIdentifier(IIntermediateGenericParameter             member  );
        ITypeCoercionUniqueIdentifier                   GetIdentifier(IIntermediateTypeCoercionMember           member  );
        IBinaryOperatorUniqueIdentifier                 GetIdentifier(IIntermediateBinaryOperatorCoercionMember member  );
        IUnaryOperatorUniqueIdentifier                  GetIdentifier(IIntermediateUnaryOperatorCoercionMember  member  );
        IGeneralMemberUniqueIdentifier                  GetIdentifier(IIntermediatePropertySignatureMember      member  );
        IGeneralMemberUniqueIdentifier                  GetIdentifier(IIntermediateFieldMember                  member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateIndexerMember                member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateIndexerSignatureMember       member  );
        IGeneralGenericSignatureMemberUniqueIdentifier  GetIdentifier(IIntermediateMethodMember                 member  );
        IGeneralGenericSignatureMemberUniqueIdentifier  GetIdentifier(IIntermediateMethodSignatureMember        member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateConstructorMember            member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateConstructorSignatureMember   member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateEventSignatureMember         member  );
        IGeneralSignatureMemberUniqueIdentifier         GetIdentifier(IIntermediateEventMember                  member  );
    }
}
