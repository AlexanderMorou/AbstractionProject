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
    internal class GenericAnamorphism
    {
        internal class Stage
        {
            private IGenericType original;
            private IIntermediateType stageType;
            private bool expanded;
            private GenericAnamorphismHandler handler;

            public Stage(IIntermediateType stageType, IGenericType target, GenericAnamorphismHandler anamorphisms, int index)
            {
                this.handler = anamorphisms;
                this.stageType = stageType;
                this.Index = index;
                this.original = target;
            }
            public IIntermediateType StageType { get { return this.stageType; } }
            public bool Expanded { get { return this.expanded; } }

            public IGenericType OriginalType { get { return this.original; } }

            internal void Expand()
            {
                if (this.expanded)
                    return;
                this.original.AggregateIdentifiers.ToString();
                switch (this.original.Type)
                {
                    case TypeKind.Class:
                        this.ExpandClass();
                        break;
                    case TypeKind.Delegate:
                        this.ExpandDelegate();
                        break;
                    case TypeKind.Enumeration:
                        this.ExpandEnumeration();
                        break;
                    case TypeKind.Interface:
                        this.ExpandInterface();
                        break;
                    case TypeKind.Struct:
                        this.ExpandStruct();
                        break;
                }
                this.expanded = true;
            }

            private void ExpandClass()
            {
                IClassType original = (IClassType)this.OriginalType;
                IIntermediateClassType stageType = (IIntermediateClassType)this.StageType;
                var stageAssembly = stageType.Assembly;
                stageType.SuspendDualLayout();
                //foreach (var implementedInterface in original.GetDirectlyImplementedInterfaces())
                //    stageType.ImplementedInterfaces.Add(handler.CreateAnamorphicStage(implementedInterface, stageAssembly));
                foreach (var ctor in original.Constructors.Values)
                {
                    if (ctor == original.TypeInitializer)
                        continue;
                    TypedNameSeries tns = new TypedNameSeries();
                    foreach (var parameter in ctor.Parameters.Values)
                        tns.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageAssembly), parameter.Direction));
                    var currentCtor = stageType.Constructors.Add(tns);
                    var ctors = (IConstructorMemberDictionary)((ICreatableParent)stageType).Constructors;
                }
                foreach (var method in original.Methods.Values)
                {
                    if (method.IsGenericConstruct && method.TypeParameters.Count > 0)
                        continue;
                    var currentMethod = stageType.Methods.Add(new TypedName(method.Name, handler.CreateAnamorphicStage(method.ReturnType, stageAssembly)));
                    foreach (var parameter in method.Parameters.Values)
                        currentMethod.Parameters.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageAssembly), parameter.Direction));
                }
                foreach (var property in original.Properties.Values)
                    stageType.Properties.Add(new TypedName(property.Name, handler.CreateAnamorphicStage(property.PropertyType, stageAssembly)), property.CanRead, property.CanWrite);
                stageType.ResumeDualLayout();
            }

            private void ExpandStruct()
            {
                IStructType original = (IStructType)this.OriginalType;
                IIntermediateStructType stageType = (IIntermediateStructType)this.StageType;
                var stageAssembly = stageType.Assembly;
                stageType.SuspendDualLayout();
                //foreach (var implementedInterface in original.GetDirectlyImplementedInterfaces())
                //    stageType.ImplementedInterfaces.Add(handler.CreateAnamorphicStage(implementedInterface, stageAssembly));
                foreach (var ctor in original.Constructors.Values)
                {
                    if (ctor == original.TypeInitializer)
                        continue;
                    TypedNameSeries tns = new TypedNameSeries();
                    foreach (var parameter in ctor.Parameters.Values)
                        tns.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageAssembly), parameter.Direction));
                    var currentCtor = stageType.Constructors.Add(tns);

                }
                foreach (var method in original.Methods.Values)
                {
                    if (method.IsGenericConstruct && method.TypeParameters.Count > 0)
                        continue;
                    var currentMethod = stageType.Methods.Add(new TypedName(method.Name, handler.CreateAnamorphicStage(method.ReturnType, stageAssembly)));
                    foreach (var parameter in method.Parameters.Values)
                        currentMethod.Parameters.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageAssembly), parameter.Direction));
                }
                foreach (var property in original.Properties.Values)
                    stageType.Properties.Add(new TypedName(property.Name, handler.CreateAnamorphicStage(property.PropertyType, stageAssembly)), property.CanRead, property.CanWrite);
                stageType.ResumeDualLayout();
            }

            private void ExpandInterface()
            {
                IInterfaceType original = (IInterfaceType)this.original;
                IIntermediateInterfaceType stageType = (IIntermediateInterfaceType)this.StageType;
                var stageAssembly = stageType.Assembly;
                foreach (var implementedInterface in original.GetDirectlyImplementedInterfaces())
                    stageType.ImplementedInterfaces.Add(handler.CreateAnamorphicStage(implementedInterface, stageAssembly));
                stageType.SuspendDualLayout();
                foreach (var method in original.Methods.Values)
                {
                    if (method.IsGenericConstruct && method.TypeParameters.Count > 0)
                        continue;
                    var currentMethod = stageType.Methods.Add(new TypedName(method.Name, handler.CreateAnamorphicStage(method.ReturnType, stageAssembly)));
                    foreach (var parameter in method.Parameters.Values)
                        currentMethod.Parameters.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageAssembly), parameter.Direction));
                }
                foreach (var property in original.Properties.Values)
                    stageType.Properties.Add(new TypedName(property.Name, handler.CreateAnamorphicStage(property.PropertyType, stageAssembly)), property.CanRead, property.CanWrite);
                stageType.ResumeDualLayout();
            }

            private void ExpandEnumeration()
            {

            }

            private void ExpandDelegate()
            {
                IDelegateType original = (IDelegateType)this.original;
                IIntermediateDelegateType stageType = (IIntermediateDelegateType)this.StageType;
                foreach (var parameter in original.Parameters.Values)
                    stageType.Parameters.Add(new TypedName(parameter.Name, handler.CreateAnamorphicStage(parameter.ParameterType, stageType.Assembly)));
            }

            public int Index { get; set; }
        }

        private GenericAnamorphismHandler anamorphisms;
        private IGenericType type;
        private IDictionary<IType, Stage> anamorphicStages;

        private IIntermediateAssembly targetAssembly;
        private object syncObject = new object();
        private int index;
        bool singlePhase;
        public GenericAnamorphism(IGenericType targetType, IIntermediateAssembly targetAssembly, GenericAnamorphismHandler anamorphisms, bool singlePhase = false)
        {
            this.type = targetType;
            this.targetAssembly = targetAssembly;
            this.anamorphicStages = new Dictionary<IType, Stage>();
            this.anamorphisms = anamorphisms;
            this.singlePhase = singlePhase;
        }

        internal IEnumerable<Stage> Stages { get { return anamorphicStages.Values; } }

        public IGenericType OriginalType { get { return this.type; } }
        public GenericAnamorphismHandler Handler { get { return this.anamorphisms; } }

        public bool SinglePhase { get { return this.singlePhase; } }

        public bool TryGetAnamorphicStage(IGenericType target, out IType anamorphicStage)
        {
            Stage stage;
            if (this.anamorphicStages.TryGetValue(target, out stage))
            {
                anamorphicStage = stage.StageType;
                return true;
            }
            var namespaceName = type.NamespaceName;
            string[] splitspace = namespaceName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            IIntermediateNamespaceParent current = targetAssembly;
            foreach (var namespacePart in splitspace)
            {
                var currentId = AstIdentifier.GetDeclarationIdentifier(namespacePart);
                if (current.Namespaces.ContainsKey(currentId))
                    current = current.Namespaces[currentId];
                else
                    current = current.Namespaces.Add(namespacePart);

            }
            IIntermediateType stageType = null;
            StringBuilder typeName = new StringBuilder();
            if (type.Parent is IType)
                typeName.AppendFormat("{0}.", ((IType)type.Parent).Name);
            typeName.Append(type.Name);
            lock (this.syncObject)
            {
                if (this.singlePhase)
                    typeName.Append("Anamorphism");
                else
                    typeName.AppendFormat("[", target.GenericParameters.Count, ++index);
            }
            bool first = true;
            if (!singlePhase)
            {
                foreach (var genericParameter in target.GenericParameters)
                {
                    if (first)
                        first = false;
                    else
                        typeName.Append(",");
                    var currentParameter = genericParameter;
                    if (currentParameter.IsGenericConstruct)
                        currentParameter = anamorphisms.CreateAnamorphicStage(currentParameter, targetAssembly);
                    if (genericParameter.IsGenericConstruct)
                        typeName.Append(currentParameter.FullName);
                    else
                        typeName.Append(currentParameter.Name);
                }
                typeName.Append("]");
            }

            switch (type.Type)
            {
                case TypeKind.Class:
                    stageType = current.Classes.Add(typeName.ToString());
                    break;
                case TypeKind.Delegate:
                    stageType = current.Delegates.Add(typeName.ToString());
                    break;
                case TypeKind.Enumeration:
                    stageType = current.Enums.Add(typeName.ToString());
                    break;
                case TypeKind.Interface:
                    stageType = current.Interfaces.Add(typeName.ToString());
                    break;
                case TypeKind.Struct:
                    stageType = current.Structs.Add(typeName.ToString());
                    break;
            }

            stage = new Stage(stageType, target, anamorphisms, index);
            this.anamorphicStages.Add(target, stage);
            anamorphicStage = stageType;
            return true;
        }

        public void ExpandAnamorphicConstruct()
        {
            if (!this.NeedsExpansion)
                return;
            foreach (var stage in from stage in this.anamorphicStages.Values.ToArray()
                                  where !stage.Expanded
                                  select stage)
                stage.Expand();
        }

        public bool NeedsExpansion
        {
            get
            {
                return this.anamorphicStages.Values.ToArray().Any(stage => !stage.Expanded);
            }
        }

    }
}
