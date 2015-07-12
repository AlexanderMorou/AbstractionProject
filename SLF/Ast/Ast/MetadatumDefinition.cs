using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
using AllenCopeland.Abstraction.Utilities.Events;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides an implementation of a metadatum definition for
    /// <see cref="IMetadataEntity"/> instances.
    /// </summary>
    public sealed partial class MetadatumDefinition :
        IMetadatumDefinition
    {
        /* *
         * Created upon request.
         * */
        private Attribute wrappedAttribute;
        private MetadataDefinition parent;
        private MetadatumDefinitionParameterCollection parameters;
        /// <summary>
        /// Data member for <see cref="Type"/>.
        /// </summary>
        private IType type;

        /// <summary>
        /// Creates a new <see cref="MetadatumDefinition"/>
        /// with the <paramref name="declarationPoint"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="MetadataDefinition"/>
        /// on which the current <see cref="MetadatumDefinition"/>
        /// is declared.</param>
        /// <param name="data">The <see cref="MetadatumDefinitionParameterValueCollection"/>
        /// which contains the information about the <see cref="MetadatumDefinition"/>.</param>
        /// <exception cref="System.ArgumentException">thrown when the <paramref name="data"/> points to a compiled attribute type
        /// which has no public constructor that matches the values given, or a property referenced in the named value series did not exist.-or-
        /// <paramref name="data"/>'s <see cref="MetadatumDefinitionParameterValueCollection.MetadatumType"/> contains a type
        /// which does not have properties, or is not an attribute.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="declarationPoint"/> is null; -or-
        /// <paramref name="data"/>'s <see cref="MetadatumDefinitionParameterValueCollection.MetadatumType"/> is null.</exception>
        public MetadatumDefinition(MetadataDefinition parent, MetadatumDefinitionParameterValueCollection data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.MetadatumType == null)
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_ArgumentNull_CustomAttribute_ctor_data, "data"), "data");
            this.parent = parent;
            if ((this.OwningAssembly.IdentityManager.MetadatumHandler.GetTypeMetadatumRepresentation(data.MetadatumType) & TypeMetadatumRepresentation.IsMetadata) != TypeMetadatumRepresentation.IsMetadata)
                throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
            this.type = data.MetadatumType;
            this.AddSeries(data);
        }
        //*
#if false//*/
        private bool VerifyAttributeType()
        {
            if (this.type is ICliType)
            {
                if (this.type is ICreatableParent)
                {
                    if (this.parameters != null)
                    {
                        if (this.Parameters.Any(q => q is IMetadatumDefinitionNamedParameter))
                            if (!(this.type is IPropertyParent))
                                return false;
                            else
                            {
                                IEnumerable<IMetadatumDefinitionNamedParameter> namedParameters =
                                    from p in this.Parameters
                                    where p is IMetadatumDefinitionNamedParameter
                                    select (IMetadatumDefinitionNamedParameter)p;
                                IPropertyParent propertyParent = ((IPropertyParent)(this.type));
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
                            select r.ParameterType;
                    else
                        ctorSignature = TypeCollection.Empty;
                    ICreatableParent creatableParent = (ICreatableParent)this.type;
                    if (creatableParent.Constructors.Find(false, ctorSignature.ToCollection()).Count == 0)
                        return false;
                }
                else
                    return false;
                return true;
            }
            else if (this.type is IIntermediateClassType)
            {
                return true;
            }
            else
                return false;
        }
        /*
#endif//*/
        
        #region IMetadatumDefinition Members

        /// <summary>
        /// Returns the <see cref="IType"/> which defines the kind of
        /// metadatum that is generated.
        /// </summary>
        /// <remarks>In ECMA-335 this is an <see cref="IClassType"/>; however
        /// in Java this is an interface with a </remarks>
        public IType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                if (value == this.type)
                    return;
                if (value == null)
                    throw new ArgumentNullException("value");
                if (this.wrappedAttribute != null)
                    this.wrappedAttribute = null;
                if ((OwningAssembly.IdentityManager.MetadatumHandler.GetTypeMetadatumRepresentation(value) & TypeMetadatumRepresentation.IsMetadata) != TypeMetadatumRepresentation.IsMetadata)
                    throw new ArgumentException(Resources.Exception_Argument_CustomAttribute_Type_MustBeAttribute, "value");
                this.type = value;
                //if (!this.VerifyAttributeType())
                //    throw new ArgumentException("value");
            }
        }

        /// <summary>
        /// Provides the <see cref="IIntermediateMetadataEntity"/> at which the
        /// <see cref="MetadatumDefinition"/> is contained.
        /// </summary>
        public IIntermediateMetadataEntity DeclarationPoint
        {
            get { return this.parent.Parent.Parent; }
        }

        /// <summary>
        /// Provides the parameter collection which denotes information about
        /// the definition of the metadatum.
        /// </summary>
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
                this.parameters = new MetadatumDefinitionParameterCollection(this);
        }

        #endregion

        #region IMetadatum Members

#if false
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
#endif
        IMetadataEntity IMetadatum.DeclarationPoint
        {
            get { return this.DeclarationPoint; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="MetadatumDefinition"/>.
        /// </summary>
        public void Dispose()
        {
            if (this.parameters != null)
            {
                this.parameters.Dispose();
                this.parameters = null;
            }
            this.parent = null;
            this.type = null;
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

        IEnumerable<Tuple<IType, object>> IMetadatum.Parameters
        {
            get { 
                foreach (var parameter in this.parameters)
                    if (!(parameter is IMetadatumDefinitionNamedParameter))
                    {
                        yield return new Tuple<IType, object>(parameter.ParameterType, parameter.Value);
                    }
            }
        }

        IEnumerable<Tuple<IType, string, object>> IMetadatum.NamedParameters
        {
            get {
                foreach (var parameter in this.parameters)
                    if (parameter is IMetadatumDefinitionNamedParameter)
                    {
                        var nParameter = (IMetadatumDefinitionNamedParameter)parameter;
                        yield return new Tuple<IType, string, object>(nParameter.ParameterType, nParameter.Name, nParameter.Value);
                    }
            }
        }

        public IIntermediateAssembly OwningAssembly { get { return this.parent.OwningAssembly; } }
    }
}
