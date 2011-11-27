using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.CompilerServices
{
    /* *
     * Dummy constructor information to represent
     * default constructor constraint.
     * */
    internal class GenericParameterDefaultConstructorConstraint :
        ConstructorInfo
    {
        private Type genericParameter;
        private static readonly ParameterInfo[] emptyParameterSet = new ParameterInfo[0];
        private static readonly Attribute[] emptyAttributeSet = new Attribute[0];
        public GenericParameterDefaultConstructorConstraint(Type genericParameter)
        {
            this.genericParameter = genericParameter;
        }

        public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        
        public override MethodAttributes Attributes
        {
            get { return MethodAttributes.Public | MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.SpecialName; }
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            return MethodImplAttributes.Managed | MethodImplAttributes.IL;
        }

        public override ParameterInfo[] GetParameters()
        {
            return emptyParameterSet;
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override RuntimeMethodHandle MethodHandle
        {
            get { return default(RuntimeMethodHandle);}
        }

        public override Type DeclaringType
        {
            get { return this.genericParameter; }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return emptyAttributeSet;
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return emptyAttributeSet;
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return false;
        }

        public override string Name
        {
            get { return ".ctor"; }
        }

        public override Type ReflectedType
        {
            get { return this.genericParameter; }
        }
    }
}
