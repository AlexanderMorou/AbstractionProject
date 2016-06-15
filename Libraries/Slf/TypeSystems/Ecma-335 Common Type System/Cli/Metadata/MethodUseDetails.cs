using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public enum MethodMemberAccessibility
    {
        CompilerControlled,
        Private,
        FamilyAndAssembly,
        Assembly,
        Family,
        FamilyOrAssembly,
        Public,
        Mask,
    }
    [Flags]
    public enum MethodUseFlags
    {
        Static           = 0x0010,
        Final            = 0x0020,
        Virtual          = 0x0040,
        HideBySig        = 0x0080,
        Strict           = 0x0200,
        Abstract         = 0x0400,
        SpecialName      = 0x0800,
        UnmanagedExport  = 0x0008,
        RTSpecialName    = 0x1000,
        PInvokeImpl      = 0x2000,
        HasSecurity      = 0x4000,
        RequireSecObject = 0x8000,
        Mask             = 0xFEF8, 
    }
    [Flags]
    public enum MethodVTableLayoutFlags
    {
        ReuseSlot        = 0x0000,
        NewSlot          = 0x0100,
    }
    public struct MethodUseDetails
    {
        private ushort value;
        public MethodUseDetails(ushort value)
        {
            this.value = value;
        }

        public MethodMemberAccessibility Accessibility
        {
            get
            {
                return ((MethodMemberAccessibility)this.value & MethodMemberAccessibility.Mask);
            }
        }

        public MethodUseFlags UsageFlags
        {
            get
            {
                return ((MethodUseFlags)this.value & MethodUseFlags.Mask);
            }
        }

        public MethodVTableLayoutFlags VTableFlags
        {
            get
            {
                return ((MethodVTableLayoutFlags)this.value & MethodVTableLayoutFlags.NewSlot);
            }
        }

        public static implicit operator MethodUseDetails(ushort value)
        {
            return new MethodUseDetails(value);
        }

        public override string ToString()
        {
            return string.Format("Accessibility = {0}, UsageFlags = {1}, VTableFlags = {2}", this.Accessibility, this.UsageFlags, this.VTableFlags);
        }
    }
}
