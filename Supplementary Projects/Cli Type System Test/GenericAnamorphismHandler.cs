using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    internal class GenericAnamorphismHandler :
        Dictionary<IType, GenericAnamorphism>
    {
        private IList<IAssembly> exceptionList;
        bool exceptionsAreExclusions;
        private IType CreateSinglePhaseAnamorphism(IType target, IIntermediateAssembly targetAssembly)
        {
            GenericAnamorphism anamorphism;
            if (!this.TryGetValue(target, out anamorphism))
                this.Add(target, anamorphism = new GenericAnamorphism((IGenericType)target, targetAssembly, this, true));
            IType result;
            anamorphism.TryGetAnamorphicStage((IGenericType)target, out result);
            return result;
        }

        public GenericAnamorphismHandler()
        {
            exceptionsAreExclusions = true;
            exceptionList = new List<IAssembly>();
        }

        public GenericAnamorphismHandler(bool exceptionsAreExclusions, IList<IAssembly> exceptionList)
        {
            this.exceptionsAreExclusions = exceptionsAreExclusions;
            this.exceptionList = new List<IAssembly>(exceptionList);
        }

        public IType CreateAnamorphicStage(IType target, IIntermediateAssembly targetAssembly)
        {
            if (!NeedsAnamorphism(target))
                return target;
            if (!target.IsGenericConstruct)
            {
                switch (target.ElementClassification)
                {
                    case TypeElementClassification.Array:
                        IArrayType iat = (IArrayType)(target);
                        if (iat.Lengths == null)
                            if (iat.LowerBounds == null)
                                return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeArray(iat.ArrayRank);
                            else
                                return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeArray(iat.LowerBounds.ToArray());
                        else if (iat.LowerBounds == null)
                            return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeArray(new int[iat.Lengths.Count], iat.Lengths.ToArray());
                        else
                            return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeArray(iat.LowerBounds.ToArray(), iat.Lengths.ToArray());
                    case TypeElementClassification.Nullable:
                        return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeNullable();
                    case TypeElementClassification.Pointer:
                        return CreateAnamorphicStage(target.ElementType, targetAssembly).MakePointer();
                    case TypeElementClassification.Reference:
                        return CreateAnamorphicStage(target.ElementType, targetAssembly).MakeByReference();
                    case TypeElementClassification.GenericTypeDefinition:
                        break;
                    case TypeElementClassification.ModifiedType:
                        IModifiedType imt = (IModifiedType)target;
                        return CreateAnamorphicStage(imt.ElementType, targetAssembly).MakeModified(imt.Modifiers.ToArray());
                    default:
                        return CreateSinglePhaseAnamorphism(target, targetAssembly);
                }
            }
            IGenericType genericTarget = (IGenericType)target;
            if (genericTarget.IsGenericDefinition)
                return target;
            var genericDefinition = (IGenericType)genericTarget.ElementType;
            GenericAnamorphism anamorphism;
            if (!this.TryGetValue(genericDefinition, out anamorphism))
                this.Add(genericDefinition, anamorphism = new GenericAnamorphism(genericDefinition, targetAssembly, this));
            IType result;
            anamorphism.TryGetAnamorphicStage(genericTarget, out result);
            return result;
        }

        internal void CreateAnamorphismFor(IType atype, ICoreVisualBasicAssembly targetAssembly)
        {
            CreateAnamorphicStage(atype, targetAssembly);
        }

        internal bool NeedsAnamorphism(IType target)
        {
            if (exceptionsAreExclusions && this.exceptionList.Contains(target.Assembly))
                return false;
            else if ((!exceptionsAreExclusions) &&
                !this.exceptionList.Contains(target.Assembly))
                return false;
            return NeedsAnamorphism(target, new List<IType>());
        }

        internal static bool NeedsAnamorphism(IType target, List<IType> entities)
        {
            if (entities.Contains(target))
                return false;
            entities.Add(target);
            switch (target.ElementClassification)
            {
                case TypeElementClassification.Array:
                case TypeElementClassification.Nullable:
                case TypeElementClassification.Pointer:
                case TypeElementClassification.Reference:
                    return NeedsAnamorphism(target.ElementType, entities);
                case TypeElementClassification.ModifiedType:
                    {
                        var modifiedType = (IModifiedType)target;
                        if (modifiedType.Modifiers.Any(p => NeedsAnamorphism(p.ModifierType, entities)))
                            return true;
                        return NeedsAnamorphism(target.ElementType, entities);
                    }
                case TypeElementClassification.GenericTypeDefinition:
                    return true;
                default:
                    if (target.ImplementedInterfaces.Any(t => NeedsAnamorphism(t, entities)))
                        return true;
                    switch (target.Type)
                    {
                        case TypeKind.Class:
                            return ClassNeedsAnamorphism((IClassType)target, entities);
                        case TypeKind.Delegate:
                            return DelegateNeedsAnamorphism((IDelegateType)target, entities);
                        case TypeKind.Enumeration:
                            return EnumerationNeedsAnamorphism((IEnumType)target, entities);
                        case TypeKind.Interface:
                            return InterfaceNeedsAnamorphism((IInterfaceType)target, entities);
                        case TypeKind.Struct:
                            return StructNeedsAnamorphism((IStructType)target, entities);
                    }
                    return false;
            }
        }

        private static bool ClassNeedsAnamorphism(IClassType target, List<IType> entities)
        {
            foreach (var binaryOperator in target.BinaryOperatorCoercions.Values)
                if (NeedsAnamorphism(binaryOperator.ReturnType, entities))
                    return true;
                else if (binaryOperator.ContainingSide != Abstract.Members.BinaryOpCoercionContainingSide.Both &&
                         NeedsAnamorphism(binaryOperator.OtherSide, entities))
                    return true;
            foreach (var ctor in target.Constructors.Values)
            {
                foreach (var parameter in ctor.Parameters.Values)
                    if (NeedsAnamorphism(parameter.ParameterType, entities))
                        return true;
            }
            foreach (var evt in target.Events.Values)
            {
                if (NeedsAnamorphism(evt.ReturnType, entities))
                    return true;
                else if (evt.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var indexer in target.Indexers.Values)
            {
                if (NeedsAnamorphism(indexer.PropertyType, entities))
                    return true;
                else if (indexer.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var method in target.Methods.Values)
                if (NeedsAnamorphism(method.ReturnType, entities))
                    return true;
                else if (method.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            foreach (var field in target.Fields.Values)
                if (field.AccessLevel != AccessLevelModifiers.Private &&
                    field.AccessLevel != AccessLevelModifiers.PrivateScope &&
                    NeedsAnamorphism(field.FieldType, entities))
                    return true;
            foreach (var property in target.Properties.Values)
                if (NeedsAnamorphism(property.PropertyType, entities))
                    return true;
            foreach (var typeCoercion in target.TypeCoercions.Values)
                if (NeedsAnamorphism(typeCoercion.CoercionType, entities))
                    return true;
            foreach (var unaryOperator in target.UnaryOperatorCoercions.Values)
                if (NeedsAnamorphism(unaryOperator.ResultedType, entities))
                    return true;
            return false;
        }

        private static bool DelegateNeedsAnamorphism(IDelegateType target, List<IType> entities)
        {
            return NeedsAnamorphism(target.ReturnType, entities) ||
                   target.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities));
        }

        private static bool EnumerationNeedsAnamorphism(IEnumType target, List<IType> entities)
        {
            return false;
        }

        private static bool InterfaceNeedsAnamorphism(IInterfaceType target, List<IType> entities)
        {
            foreach (var evt in target.Events.Values)
            {
                if (NeedsAnamorphism(evt.ReturnType, entities))
                    return true;
                else if (evt.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var indexer in target.Indexers.Values)
            {
                if (NeedsAnamorphism(indexer.PropertyType, entities))
                    return true;
                else if (indexer.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var method in target.Methods.Values)
                if (NeedsAnamorphism(method.ReturnType, entities))
                    return true;
                else if (method.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            foreach (var property in target.Properties.Values)
                if (NeedsAnamorphism(property.PropertyType, entities))
                    return true;
            return false;
        }

        private static bool StructNeedsAnamorphism(IStructType target, List<IType> entities)
        {
            foreach (var binaryOperator in target.BinaryOperatorCoercions.Values)
                if (NeedsAnamorphism(binaryOperator.ReturnType, entities))
                    return true;
                else if (binaryOperator.ContainingSide != Abstract.Members.BinaryOpCoercionContainingSide.Both &&
                         NeedsAnamorphism(binaryOperator.OtherSide, entities))
                    return true;
            foreach (var ctor in target.Constructors.Values)
            {
                foreach (var parameter in ctor.Parameters.Values)
                    if (NeedsAnamorphism(parameter.ParameterType, entities))
                        return true;
            }
            foreach (var evt in target.Events.Values)
            {
                if (NeedsAnamorphism(evt.ReturnType, entities))
                    return true;
                else if (evt.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var indexer in target.Indexers.Values)
            {
                if (NeedsAnamorphism(indexer.PropertyType, entities))
                    return true;
                else if (indexer.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            }
            foreach (var method in target.Methods.Values)
                if (NeedsAnamorphism(method.ReturnType, entities))
                    return true;
                else if (method.Parameters.Values.Any(param => NeedsAnamorphism(param.ParameterType, entities)))
                    return true;
            foreach (var field in target.Fields.Values)
                if (field.AccessLevel != AccessLevelModifiers.Private &&
                    field.AccessLevel != AccessLevelModifiers.PrivateScope &&
                    NeedsAnamorphism(field.FieldType, entities))
                    return true;
            foreach (var property in target.Properties.Values)
                if (NeedsAnamorphism(property.PropertyType, entities))
                    return true;
            foreach (var typeCoercion in target.TypeCoercions.Values)
                if (NeedsAnamorphism(typeCoercion.CoercionType, entities))
                    return true;
            foreach (var unaryOperator in target.UnaryOperatorCoercions.Values)
                if (NeedsAnamorphism(unaryOperator.ResultedType, entities))
                    return true;
            return false;
        }

    }
}
