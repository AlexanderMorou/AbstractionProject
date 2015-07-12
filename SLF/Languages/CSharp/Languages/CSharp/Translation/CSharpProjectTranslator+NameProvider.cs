using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    partial class CSharpProjectTranslator
    {
        private class NameProvider :
            IIntermediateCodeNameProvider
        {
            private CSharpProjectTranslator owner;
            private Dictionary<IIntermediateAssembly, string> fileNameLookup;
            public NameProvider(CSharpProjectTranslator owner, Dictionary<IIntermediateAssembly, string> fileNameLookup)
            {
                this.owner = owner;
                this.fileNameLookup = fileNameLookup;
            }
            public string Visit(IIntermediateAssembly assembly, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(assembly, out name);
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = assembly.Name;
                        break;
                }
                return name;
            }

            public string Visit(IIntermediateNamespaceDeclaration @namespace, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(@namespace.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#ns{1:X8}", name, @namespace.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = @namespace.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = @namespace.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("ns{0:X8}", @namespace.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit(IIntermediateClassType @class, IntermediateNameRequestDetails context)
            {
                return VisitIntermediateType(@class, context);
            }

            public string Visit(IIntermediateDelegateType @delegate, IntermediateNameRequestDetails context)
            {
                return VisitIntermediateType(@delegate, context);
            }

            public string Visit(IIntermediateEnumType @enum, IntermediateNameRequestDetails context)
            {
                return VisitIntermediateType(@enum, context);
            }

            public string Visit(IIntermediateInterfaceType @interface, IntermediateNameRequestDetails context)
            {
                return VisitIntermediateType(@interface, context);
            }

            public string Visit(IIntermediateStructType @struct, IntermediateNameRequestDetails context)
            {
                return VisitIntermediateType(@struct, context);
            }

            private string VisitIntermediateType(IIntermediateType intermediateType, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(intermediateType.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#type{1:X8}", name, intermediateType.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = intermediateType.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = intermediateType.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("type{0:X8}", intermediateType.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, IntermediateNameRequestDetails context)
                where TGenericParameter : Abstract.IGenericParameter<TGenericParameter, TParent>
                where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
                where TParent : Abstract.IGenericParamParent<TGenericParameter, TParent>
                where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            {
                return VisitIntermediateType(@parameter, context);
            }

            public string Visit(ILocalMember local, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(local.Parent.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#loc{1:X8}", name, local.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = local.Parent.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = local.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("loc{0:X8}", local.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, IntermediateNameRequestDetails context)
                where TCtor : Abstract.Members.IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : Abstract.ICreatableParent<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(ctor.Parent.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#ctor{1:X8}", name, ctor.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = ctor.Parent.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = ctor.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("ctor{0:X8}", ctor.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor, IntermediateNameRequestDetails context)
                where TCtor : Abstract.Members.IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : Abstract.ICreatableParent<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(ctor.Parent.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#ctor{1:X8}", name, ctor.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = ctor.Parent.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = ctor.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("ctor{0:X8}", ctor.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, IntermediateNameRequestDetails context)
                where TEvent : Abstract.Members.IEventMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : Abstract.IEventParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(@event.Parent.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#evt{1:X8}", name, @event.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = @event.Parent.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = @event.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("evt{0:X8}", @event.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event, IntermediateNameRequestDetails context)
                where TEvent : Abstract.Members.IEventSignatureMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : Abstract.IEventSignatureParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        this.fileNameLookup.TryGetValue(@event.Parent.Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#evt{1:X8}", name, @event.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        name = @event.Parent.Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = @event.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("evt{0:X8}", @event.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TCoercionParent>(Abstract.Members.IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion, IntermediateNameRequestDetails context) where TCoercionParent : Abstract.ICoercibleType<Abstract.Members.IBinaryOperatorUniqueIdentifier, Abstract.Members.IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                string name = null;

                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(binaryCoercion.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)binaryCoercion.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#bnop{1:X8}", name, binaryCoercion.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(binaryCoercion.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)binaryCoercion.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = binaryCoercion.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("bnop{0:X8}", binaryCoercion.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TCoercionParent>(Abstract.Members.ITypeCoercionMember<TCoercionParent> typeCoercion, IntermediateNameRequestDetails context) where TCoercionParent : Abstract.ICoercibleType<Abstract.Members.ITypeCoercionUniqueIdentifier, Abstract.Members.ITypeCoercionMember<TCoercionParent>, TCoercionParent>
            {
                string name = null;

                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(typeCoercion.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)typeCoercion.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#typc{1:X8}", name, typeCoercion.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(typeCoercion.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)typeCoercion.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = typeCoercion.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("typc{0:X8}", typeCoercion.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TCoercionParent>(Abstract.Members.IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion, IntermediateNameRequestDetails context) where TCoercionParent : Abstract.ICoercibleType<Abstract.Members.IUnaryOperatorUniqueIdentifier, Abstract.Members.IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                string name = null;

                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(unaryCoercion.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)unaryCoercion.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#unop{1:X8}", name, unaryCoercion.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(unaryCoercion.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)unaryCoercion.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = unaryCoercion.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("unop{0:X8}", unaryCoercion.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field, IntermediateNameRequestDetails context)
                where TField : Abstract.Members.IFieldMember<TField, TFieldParent>
                where TIntermediateField : TField, IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
                where TFieldParent : Abstract.IFieldParent<TField, TFieldParent>
                where TIntermediateFieldParent : TFieldParent, IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(field.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)field.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#fld{1:X8}", name, field.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(field.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)field.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = field.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("fld{0:X8}", field.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit(IIntermediateEnumFieldMember field, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(field.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)field.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#fld{1:X8}", name, field.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(field.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)field.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = field.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("fld{0:X8}", field.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer, IntermediateNameRequestDetails context)
                where TIndexer : Abstract.Members.IIndexerMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : Abstract.IIndexerParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(indexer.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)indexer.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#idx{1:X8}", name, indexer.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(indexer.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)indexer.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = indexer.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("idx{0:X8}", indexer.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature, IntermediateNameRequestDetails context)
                where TIndexer : Abstract.Members.IIndexerSignatureMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : Abstract.IIndexerSignatureParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(indexerSignature.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)indexerSignature.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#idx{1:X8}", name, indexerSignature.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(indexerSignature.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)indexerSignature.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = indexerSignature.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("idx{0:X8}", indexerSignature.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method, IntermediateNameRequestDetails context)
                where TMethod : Abstract.Members.IMethodMember<TMethod, TMethodParent>
                where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
                where TMethodParent : Abstract.IMethodParent<TMethod, TMethodParent>
                where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(method.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)method.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#fnc{1:X8}", name, method.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(method.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)method.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = method.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("fnc{0:X8}", method.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature, IntermediateNameRequestDetails context)
                where TSignature : Abstract.Members.IMethodSignatureMember<TSignature, TParent>
                where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
                where TParent : Abstract.IMethodSignatureParent<TSignature, TParent>
                where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(methodSignature.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)methodSignature.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#fnc{1:X8}", name, methodSignature.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(methodSignature.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)methodSignature.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = methodSignature.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("fnc{0:X8}", methodSignature.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature, IntermediateNameRequestDetails context)
                where TProperty : Abstract.Members.IPropertySignatureMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : Abstract.IPropertySignatureParent<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(propertySignature.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)propertySignature.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#prop{1:X8}", name, propertySignature.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(propertySignature.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)propertySignature.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = propertySignature.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("prop{0:X8}", propertySignature.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property, IntermediateNameRequestDetails context)
                where TProperty : Abstract.Members.IPropertyMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : Abstract.IPropertyParent<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (!(property.Parent is IIntermediateType))
                            return null;
                        this.fileNameLookup.TryGetValue(((IIntermediateType)property.Parent).Assembly, out name);
                        if (this.HtmlContext)
                            name = string.Format("{0}#prop{1:X8}", name, property.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.SourceFileName:
                        if (!(property.Parent is IIntermediateType))
                            return null;
                        name = ((IIntermediateType)property.Parent).Assembly.FileName;
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = property.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("prop{0:X8}", property.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter, IntermediateNameRequestDetails context)
                where TParent : Abstract.Members.IParameterParent
                where TIntermediateParent : TParent, IIntermediateParameterParent
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (this.HtmlContext)
                            name = string.Format("#arg{0:X8}", parameter.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = parameter.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("arg{0:X8}", parameter.GetHashCode());
                        break;
                }
                return name;
            }

            public string Visit(ILinqRangeVariable rangeVariable, IntermediateNameRequestDetails context)
            {
                string name = null;
                switch (context)
                {
                    case IntermediateNameRequestDetails.TargetFileName:
                        if (this.HtmlContext)
                            name = string.Format("#lnq{0:X8}", rangeVariable.GetHashCode());
                        break;
                    case IntermediateNameRequestDetails.DisplayName:
                        name = rangeVariable.Name;
                        break;
                    case IntermediateNameRequestDetails.ReferenceName:
                        if (this.HtmlContext)
                            name = string.Format("lnq{0:X8}", rangeVariable.GetHashCode());
                        break;
                }
                return name;
            }
            private bool HtmlContext
            {
                get
                {
                    if (this.owner == null || this.owner.options == null || this.owner.options.FormatProvider == null)
                        return false;
                    return this.owner.options.FormatProvider is HtmlCodeFormatterProvider;
                }
            }
        }
    }
}
