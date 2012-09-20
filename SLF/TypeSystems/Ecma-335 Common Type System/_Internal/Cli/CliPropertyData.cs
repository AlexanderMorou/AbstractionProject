using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliPropertyData :
        ICliPropertyData
    {
        private ICliMetadataPropertyTableRow property;
        private ICliMetadataMethodDefinitionTableRow getMethod;
        private ICliMetadataMethodDefinitionTableRow setMethod;
        private IArrayReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> otherMethods;

        public CliPropertyData(ICliMetadataPropertyTableRow property, ICliMetadataMethodDefinitionTableRow getMethod, ICliMetadataMethodDefinitionTableRow setMethod, ICliMetadataMethodDefinitionTableRow[] otherMethods)
        {
            this.property = property;
            this.getMethod = getMethod;
            this.setMethod = setMethod;
            this.otherMethods = new ArrayReadOnlyCollection<ICliMetadataMethodDefinitionTableRow>(otherMethods);
        }

        public ICliMetadataPropertyTableRow Property { get { return this.property; } }

        public ICliMetadataMethodDefinitionTableRow GetMethod { get { return this.getMethod; } }
        public ICliMetadataMethodDefinitionTableRow SetMethod { get { return this.setMethod; } }

        public IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> OtherMethods { get { return this.otherMethods; } }

        public CliMemberType MemberType
        {
            get { return CliMemberType.Property; }
        }
        public override string ToString()
        {
            return string.Format("{0} {{ {1} {2} }}", this.property.Name, this.getMethod == null ? string.Empty : "get;", this.setMethod == null ? string.Empty : "set;");
        }

        public uint TokenIndex
        {
            get
            {
                if (this.getMethod != null && this.setMethod != null)
                    return Math.Min(this.getMethod.Index, this.setMethod.Index);
                else if (this.getMethod != null)
                    return this.getMethod.Index;
                else if (this.setMethod != null)
                    return this.setMethod.Index;
                else if (this.otherMethods.Count > 0)
                    return this.otherMethods[0].Index;
                return 0;
            }
        }
    }

    internal class CliEventData :
        ICliEventData
    {
        private ICliMetadataEventTableRow @event;
        private ICliMetadataMethodDefinitionTableRow onAddMethod;
        private ICliMetadataMethodDefinitionTableRow onFireMethod;
        private ICliMetadataMethodDefinitionTableRow onRemoveMethod;
        private IArrayReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> otherMethods;

        public CliEventData(ICliMetadataEventTableRow @event, ICliMetadataMethodDefinitionTableRow onAddMethod, ICliMetadataMethodDefinitionTableRow onFireMethod, ICliMetadataMethodDefinitionTableRow onRemoveMethod, ICliMetadataMethodDefinitionTableRow[] otherMethods)
        {
            this.@event = @event;
            this.onAddMethod = onAddMethod;
            this.onFireMethod = onFireMethod;
            this.onRemoveMethod = onRemoveMethod;
            this.otherMethods = new ArrayReadOnlyCollection<ICliMetadataMethodDefinitionTableRow>(otherMethods);
        }

        public ICliMetadataEventTableRow Event { get { return this.@event; } }

        public ICliMetadataMethodDefinitionTableRow OnAddMethod { get { return this.onAddMethod; } }
        public ICliMetadataMethodDefinitionTableRow OnFireMethod { get { return this.onFireMethod; } }
        public ICliMetadataMethodDefinitionTableRow OnRemoveMethod { get { return this.onRemoveMethod; } }

        public IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> OtherMethods { get { return this.otherMethods; } }

        public CliMemberType MemberType
        {
            get { return CliMemberType.Event; }
        }

        public uint TokenIndex
        {
            get
            {
                if (this.onAddMethod != null && this.onRemoveMethod != null && this.onFireMethod != null)
                    return Math.Min(Math.Min(this.onAddMethod.Index, this.onRemoveMethod.Index), this.onFireMethod.Index);
                else if (this.onAddMethod != null && this.onRemoveMethod != null)
                    return Math.Min(this.onAddMethod.Index, this.onRemoveMethod.Index);
                else if (this.onAddMethod != null && this.onFireMethod != null)
                    return Math.Min(this.onAddMethod.Index, this.onFireMethod.Index);
                else if (this.onRemoveMethod != null && this.onFireMethod != null)
                    return Math.Min(this.onRemoveMethod.Index, this.onFireMethod.Index);
                else if (this.onAddMethod != null)
                    return this.onAddMethod.Index;
                else if (this.onRemoveMethod != null)
                    return this.onRemoveMethod.Index;
                else if (this.onFireMethod != null)
                    return this.onFireMethod.Index;
                else if (this.otherMethods.Count > 0)
                    return this.otherMethods[0].Index;
                return 0;
            }
        }
    }

    internal class CliMethodTypeData :
        ICliMethodData
    {
        private CliMemberType type;
        private ICliMetadataMethodDefinitionTableRow method;
        internal CliMethodTypeData(CliMemberType type, ICliMetadataMethodDefinitionTableRow method)
        {
            this.type = type;
            this.method = method;
        }

        public ICliMetadataMethodDefinitionTableRow Method
        {
            get { return this.method; }
        }

        public CliMemberType MemberType
        {
            get { return this.type; }
        }

        public uint TokenIndex
        {
            get { return this.method.Index; }
        }
    }


    internal interface ICliPropertyData :
        ICliMemberData
    {
        ICliMetadataPropertyTableRow Property { get; }
        ICliMetadataMethodDefinitionTableRow GetMethod { get; }
        ICliMetadataMethodDefinitionTableRow SetMethod { get; }
        IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> OtherMethods { get; }
    }

    internal interface ICliEventData :
        ICliMemberData
    {
        ICliMetadataEventTableRow Event { get; }
        ICliMetadataMethodDefinitionTableRow OnAddMethod { get; }
        ICliMetadataMethodDefinitionTableRow OnRemoveMethod { get; }
        ICliMetadataMethodDefinitionTableRow OnFireMethod { get; }
        IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> OtherMethods { get; }
    }

    internal interface ICliMethodData :
        ICliMemberData
    {
        ICliMetadataMethodDefinitionTableRow Method { get; }
    }

    internal interface ICliMemberData
    {
        CliMemberType MemberType { get; }
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the 
        /// index of the member within the table that defines it.
        /// </summary>
        /// <remarks>For properties and events, the index returns the lowest index
        /// of the methods relative to that member.</remarks>
        uint TokenIndex { get; }
    }

    internal enum CliMemberType
    {
        BinaryOperator,
        Constructor,
        Event,
        Method,
        Property,
        TypeCoercionOperator,
        UnaryOperator,
    }
}
