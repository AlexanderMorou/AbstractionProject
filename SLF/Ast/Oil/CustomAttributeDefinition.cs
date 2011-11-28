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
    /// <see cref="ICustomAttributedDeclaration"/> instances.
    /// </summary>
    public sealed partial class CustomAttributeDefinition :
        ICustomAttributeDefinition
    {
        /* *
         * Created upon request.
         * */
        private Attribute wrappedAttribute;
        private IIntermediateCustomAttributedDeclaration declarationPoint;
        private CustomAttributeDefinitionParameterCollection parameters;
        private IType attributeType;

        /// <summary>
        /// Creates a new <see cref="CustomAttributeDefinition"/>
        /// with the <paramref name="declarationPoint"/> provided.
        /// </summary>
        /// <param name="declarationPoint">The <see cref="IIntermediateCustomAttributedDeclaration"/>
        /// on which the current <see cref="CustomAttributeDefinition"/>
        /// is declared.</param>
        /// <param name="data">The <see cref="CustomAttributeDefinition.ParameterValueCollection"/>
        /// which contains the information about the <see cref="CustomAttributeDefinition"/>.</param>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="data"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.-or-
        /// <paramref name="data"/>'s <see cref="CustomAttributeDefinition.ParameterValueCollection.AttributeType"/> contains a type
        /// which does not have properties, or is not an attribute.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="declarationPoint"/> is null; -or-
        /// <paramref name="data"/>'s <see cref="CustomAttributeDefinition.ParameterValueCollection.AttributeType"/> is null.</exception>
        public CustomAttributeDefinition(IIntermediateCustomAttributedDeclaration declarationPoint, CustomAttributeDefinition.ParameterValueCollection data)
        {
            if (data.AttributeType == null)
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ArgumentNull_CustomAttribute_ctor_data, "data"), "data");
            if (!typeof(Attribute).GetTypeReference().IsAssignableFrom(data.AttributeType))
                throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
            this.declarationPoint = declarationPoint;
            this.attributeType = data.AttributeType;
            this.AddSeries(data);
            if (!this.VerifyAttributeType())
                throw new ArgumentException("data");
        }

        private bool VerifyAttributeType()
        {
            if (this.attributeType is ICompiledType)
            {
                if (this.attributeType is ICreatableParent)
                {
                    if (this.parameters != null)
                    {
                        if (this.Parameters.Any(q => q is ICustomAttributeDefinitionNamedParameter))
                            if (!(this.attributeType is IPropertyParent))
                                return false;
                            else
                            {
                                IEnumerable<ICustomAttributeDefinitionNamedParameter> namedParameters =
                                    from p in this.Parameters
                                    where p is ICustomAttributeDefinitionNamedParameter
                                    select (ICustomAttributeDefinitionNamedParameter)p;
                                IPropertyParent propertyParent = ((IPropertyParent)(this.attributeType));
                                foreach (var propertyValue in namedParameters)
                                    if (!propertyParent.Properties.ContainsKey(propertyValue.Name))
                                        return false;

                            }
                    }
                    IEnumerable<IType> ctorSignature = null;
                    if (parameters != null)
                        ctorSignature =
                            from r in this.Parameters
                            where !(r is ICustomAttributeDefinitionNamedParameter)
                            select r.Value.GetType().GetTypeReference();
                    else
                        ctorSignature = TypeCollection.Empty;
                    ICreatableParent creatableParent = (ICreatableParent)this.attributeType;
                    if (creatableParent.Constructors.Find(false, ctorSignature.ToCollection()).Count == 0)
                        return false;
                }
                else
                    return false;
                return true;
            }
            else if (this.attributeType is IIntermediateClassType)
            {
                return true;
            }
            else
                return false;
        }

        #region ICustomAttributeDefinition Members

        public IType Type
        {
            get
            {
                return this.attributeType;
            }
            set
            {
                if (value == this.attributeType)
                    return;
                if (value == null)
                    throw new ArgumentNullException("value");
                if (this.wrappedAttribute != null)
                    this.wrappedAttribute = null;
                if (!typeof(Attribute).GetTypeReference().IsAssignableFrom(value))
                    throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
                this.attributeType = value;
                if (!this.VerifyAttributeType())
                    throw new ArgumentException("value");
            }
        }

        public IIntermediateCustomAttributedDeclaration DeclarationPoint
        {
            get { return this.declarationPoint; }
        }

        public ICustomAttributeDefinitionParameterCollection Parameters
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
                this.parameters = new CustomAttributeDefinitionParameterCollection(this);
                this.parameters.NamedParameterChangedName += new EventHandler<AllenCopeland.Abstraction.Utilities.Events.EventArgsR1<ICustomAttributeDefinitionNamedParameter>>(parameters_NamedParameterChangedName);
                this.parameters.NamedParameterChangedValue += new EventHandler<AllenCopeland.Abstraction.Utilities.Events.EventArgsR1<ICustomAttributeDefinitionNamedParameter>>(parameters_NamedParameterChangedValue);
                this.parameters.NamelessParametersChanged += new EventHandler(parameters_NamelessParametersChanged);
            }
        }

        void parameters_NamelessParametersChanged(object sender, EventArgs e)
        {
            if (this.wrappedAttribute != null)
                this.wrappedAttribute = null;
        }

        void parameters_NamedParameterChangedValue(object sender, EventArgsR1<ICustomAttributeDefinitionNamedParameter> e)
        {
            //Needless if the wrapped attribute isn't instantiated.
            if (this.wrappedAttribute == null)
                return;
            if (this.attributeType is ICompiledType)
            {
                if (!(this.attributeType is IPropertyParent))
                    throw new InvalidOperationException();
                IPropertyParent parent = ((IPropertyParent)(this.attributeType));
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

        void parameters_NamedParameterChangedName(object sender, EventArgsR1<ICustomAttributeDefinitionNamedParameter> e)
        {
            if (this.wrappedAttribute != null)
                this.wrappedAttribute = null;
        }

        #endregion

        #region ICustomAttributeInstance Members


        public Attribute WrappedAttribute
        {
            get {
                if (this.Type is ICompiledType)
                {
                    List<IType> ctorParamTypes = new List<IType>();
                    List<object> ctorParamValues = new List<object>();
                    Dictionary<string, object> ctorParamNamedValues = new Dictionary<string, object>();
                    foreach (var item in this.Parameters)
                        if (!(item is ICustomAttributeDefinitionNamedParameter))
                        {
                            ctorParamTypes.Add(item.Value.GetType().GetTypeReference());
                            ctorParamValues.Add(item.Value);
                        }
                        else
                            ctorParamNamedValues.Add(((ICustomAttributeDefinitionNamedParameter)(item)).Name, item.Value);
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

        ICustomAttributedDeclaration ICustomAttributeInstance.DeclarationPoint
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
            this.attributeType = null;
            this.wrappedAttribute = null;
        }

        #endregion

        internal void AddSeries(CustomAttributeDefinition.ParameterValueCollection values)
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
