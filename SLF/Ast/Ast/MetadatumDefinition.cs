using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides an implementation of a custom attribute definition for
    /// <see cref="IMetadataEntity"/> instances.
    /// </summary>
    public sealed partial class MetadatumDefinition :
        IMetadatumDefinition
    {
        /* *
         * Created upon request.
         * */
        private Attribute wrappedAttribute;
        private IIntermediateMetadataEntity declarationPoint;
        private MetadatumDefinitionParameterCollection parameters;
        private IType metadatumType;

        /// <summary>
        /// Creates a new <see cref="MetadatumDefinition"/>
        /// with the <paramref name="declarationPoint"/> provided.
        /// </summary>
        /// <param name="declarationPoint">The <see cref="IIntermediateMetadataEntity"/>
        /// on which the current <see cref="MetadatumDefinition"/>
        /// is declared.</param>
        /// <param name="data">The <see cref="MetadatumDefinitionParameterValueCollection"/>
        /// which contains the information about the <see cref="MetadatumDefinition"/>.</param>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="data"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.-or-
        /// <paramref name="data"/>'s <see cref="MetadatumDefinitionParameterValueCollection.AttributeType"/> contains a type
        /// which does not have properties, or is not an attribute.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="declarationPoint"/> is null; -or-
        /// <paramref name="data"/>'s <see cref="MetadatumDefinitionParameterValueCollection.AttributeType"/> is null.</exception>
        public MetadatumDefinition(IIntermediateMetadataEntity declarationPoint, MetadatumDefinitionParameterValueCollection data)
        {
            if (data.AttributeType == null)
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ArgumentNull_CustomAttribute_ctor_data, "data"), "data");
            if (!typeof(Attribute).GetTypeReference().IsAssignableFrom(data.AttributeType))
                throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
            this.declarationPoint = declarationPoint;
            this.metadatumType = data.AttributeType;
            this.AddSeries(data);
            if (!this.VerifyAttributeType())
                throw new ArgumentException("data");
        }

        private bool VerifyAttributeType()
        {
            if (this.metadatumType is ICompiledType)
            {
                if (this.metadatumType is ICreatableParent)
                {
                    if (this.parameters != null)
                    {
                        if (this.Parameters.Any(q => q is IMetadatumDefinitionNamedParameter))
                            if (!(this.metadatumType is IPropertyParent))
                                return false;
                            else
                            {
                                IEnumerable<IMetadatumDefinitionNamedParameter> namedParameters =
                                    from p in this.Parameters
                                    where p is IMetadatumDefinitionNamedParameter
                                    select (IMetadatumDefinitionNamedParameter)p;
                                IPropertyParent propertyParent = ((IPropertyParent)(this.metadatumType));
                                foreach (var propertyValue in namedParameters)
                                    if (!propertyParent.Properties.ContainsKey(propertyValue.Name))
                                        return false;

                            }
                    }
                    IEnumerable<IType> ctorSignature = null;
                    if (parameters != null)
                        ctorSignature =
                            from r in this.Parameters
                            where !(r is IMetadatumDefinitionNamedParameter)
                            select r.Value.GetType().GetTypeReference();
                    else
                        ctorSignature = TypeCollection.Empty;
                    ICreatableParent creatableParent = (ICreatableParent)this.metadatumType;
                    if (creatableParent.Constructors.Find(false, ctorSignature.ToCollection()).Count == 0)
                        return false;
                }
                else
                    return false;
                return true;
            }
            else if (this.metadatumType is IIntermediateClassType)
            {
                return true;
            }
            else
                return false;
        }

        #region IMetadatumDefinition Members

        public IType Type
        {
            get
            {
                return this.metadatumType;
            }
            set
            {
                if (value == this.metadatumType)
                    return;
                if (value == null)
                    throw new ArgumentNullException("value");
                if (this.wrappedAttribute != null)
                    this.wrappedAttribute = null;
                if (!typeof(Attribute).GetTypeReference().IsAssignableFrom(value))
                    throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
                this.metadatumType = value;
                if (!this.VerifyAttributeType())
                    throw new ArgumentException("value");
            }
        }

        public IIntermediateMetadataEntity DeclarationPoint
        {
            get { return this.declarationPoint; }
        }

        public IMetadataDefinitionParameterCollection Parameters
        {
            get
            {
                CheckParameters();
                return this.parameters;
            }
        }

        private void CheckParameters()
        {
            if (this.parameters == null)
            {
                this.parameters = new MetadatumDefinitionParameterCollection(this);
                this.parameters.NamedParameterChangedName += new EventHandler<AllenCopeland.Abstraction.Utilities.Events.EventArgsR1<IMetadatumDefinitionNamedParameter>>(parameters_NamedParameterChangedName);
                this.parameters.NamedParameterChangedValue += new EventHandler<AllenCopeland.Abstraction.Utilities.Events.EventArgsR1<IMetadatumDefinitionNamedParameter>>(parameters_NamedParameterChangedValue);
                this.parameters.NamelessParametersChanged += new EventHandler(parameters_NamelessParametersChanged);
            }
        }

        void parameters_NamelessParametersChanged(object sender, EventArgs e)
        {
            if (this.wrappedAttribute != null)
                this.wrappedAttribute = null;
        }

        void parameters_NamedParameterChangedValue(object sender, EventArgsR1<IMetadatumDefinitionNamedParameter> e)
        {
            //Needless if the wrapped attribute isn't instantiated.
            if (this.wrappedAttribute == null)
                return;
            if (this.metadatumType is ICompiledType)
            {
                if (!(this.metadatumType is IPropertyParent))
                    throw new InvalidOperationException();
                IPropertyParent parent = ((IPropertyParent)(this.metadatumType));
                if (parent.Properties.ContainsKey(e.Arg1.Name))
                {
                    ICompiledPropertyMember property = (ICompiledPropertyMember)parent.Properties[e.Arg1.Name];
                    ICompiledMethodMember setMethod = (ICompiledMethodMember)property.SetMethod;
                    if (setMethod == null)
                        return;
                    setMethod.MemberInfo.Invoke(wrappedAttribute, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, System.Type.DefaultBinder, new object[] { e.Arg1.Value }, CultureInfo.CurrentCulture);
                }
            }
        }

        void parameters_NamedParameterChangedName(object sender, EventArgsR1<IMetadatumDefinitionNamedParameter> e)
        {
            if (this.wrappedAttribute != null)
                this.wrappedAttribute = null;
        }

        #endregion

        #region IMetadatum Members


        public Attribute WrappedAttribute
        {
            get {
                if (this.Type is ICompiledType)
                {
                    List<IType> ctorParamTypes = new List<IType>();
                    List<object> ctorParamValues = new List<object>();
                    Dictionary<string, object> ctorParamNamedValues = new Dictionary<string, object>();
                    foreach (var item in this.Parameters)
                        if (!(item is IMetadatumDefinitionNamedParameter))
                        {
                            ctorParamTypes.Add(item.Value.GetType().GetTypeReference());
                            ctorParamValues.Add(item.Value);
                        }
                        else
                            ctorParamNamedValues.Add(((IMetadatumDefinitionNamedParameter)(item)).Name, item.Value);
                    if (this.Type is ICreatableParent)
                    {
                        ICreatableParent creatableType = (ICreatableParent)this.Type;
                        var matches = creatableType.Constructors.Find(false, ctorParamTypes.ToArray());
                        if (matches.Count == 0)
                            throw new InvalidOperationException("No constructor for the attribute could be found");
                        ICompiledCtorMember ctor = (ICompiledCtorMember)matches.Values[0];
                        var attribute = (Attribute)ctor.MemberInfo.Invoke(ctorParamValues.ToArray());
                        if (this.Type is IPropertyParent)
                        {
                            IPropertyParent parent = ((IPropertyParent)(this.Type));
                            foreach (var prop in ctorParamNamedValues)
                            {
                                ICompiledPropertyMember propMember = ((ICompiledPropertyMember)(parent.Properties[prop.Key]));
                                ICompiledMethodMember setMethod = ((ICompiledMethodMember)(propMember.SetMethod));
                                setMethod.MemberInfo.Invoke(attribute, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, System.Type.DefaultBinder, new object[] { prop.Value }, CultureInfo.CurrentCulture);
                            }
                        }
                        else if (ctorParamNamedValues.Count != 0)
                            throw new InvalidOperationException("Cannot set properties on propertyless parent");
                        this.wrappedAttribute = attribute;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
                return this.wrappedAttribute;
            }
        }

        IMetadataEntity IMetadatum.DeclarationPoint
        {
            get { return this.DeclarationPoint; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.parameters != null)
                this.Parameters.Dispose();
            this.declarationPoint = null;
            this.metadatumType = null;
            this.wrappedAttribute = null;
        }

        #endregion

        internal void AddSeries(MetadatumDefinitionParameterValueCollection values)
        {
            if (values.Count() > 0)
            {
                this.CheckParameters();
                this.parameters.AddSeries(values);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Type.BuildTypeName(true, false, TypeParameterDisplayMode.DebuggerStandard), string.Join(", ", this.Parameters));
        }
    }
}
