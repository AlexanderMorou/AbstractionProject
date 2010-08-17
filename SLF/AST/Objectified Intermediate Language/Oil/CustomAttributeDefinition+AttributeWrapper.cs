using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class CustomAttributeDefinition
    {
        private partial class AttributeWrapper :
            Attribute,
            ICustomTypeDescriptor
        {
            public AttributeWrapper()
            {

            }

            #region ICustomTypeDescriptor Members

            public AttributeCollection GetAttributes()
            {
                throw new NotImplementedException();
            }

            public string GetClassName()
            {
                throw new NotImplementedException();
            }

            public string GetComponentName()
            {
                throw new NotImplementedException();
            }

            public TypeConverter GetConverter()
            {
                throw new NotImplementedException();
            }

            public EventDescriptor GetDefaultEvent()
            {
                throw new NotImplementedException();
            }

            public PropertyDescriptor GetDefaultProperty()
            {
                throw new NotImplementedException();
            }

            public object GetEditor(Type editorBaseType)
            {
                throw new NotImplementedException();
            }

            public EventDescriptorCollection GetEvents(Attribute[] attributes)
            {
                throw new NotImplementedException();
            }

            public EventDescriptorCollection GetEvents()
            {
                throw new NotImplementedException();
            }

            public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                throw new NotImplementedException();
            }

            public PropertyDescriptorCollection GetProperties()
            {
                throw new NotImplementedException();
            }

            public object GetPropertyOwner(PropertyDescriptor pd)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
