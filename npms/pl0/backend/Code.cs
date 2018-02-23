using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public enum Operation
    {
        Null,

        [Description("LIT")]
        LoadLiteral,

        [Description("LOD")]
        LoadVariable,

        [Description("INT")]
        Allocate,

        [Description("STO")]
        Store,

        [Description("JPC")]
        ConditionalJump,

        [Description("JMP")]
        Jump,

        [Description("CAL")]
        Call,

        [Description("OPR")]
        Divide,

        [Description("OPR")]
        Multiply,

        [Description("OPR")]
        Add,

        [Description("OPR")]
        Subtract,

        [Description("OPR")]
        Less,

        [Description("OPR")]
        Return,

        [Description("OPR")]
        Read,

        [Description("OPR")]
        Write,

        [Description("OPR")]
        LessEquals,

        [Description("OPR")]
        Greater,

        [Description("OPR")]
        GreaterEquals,

        [Description("OPR")]
        Equals,

        [Description("OPR")]
        NotEquals,

        [Description("OPR")]
        Negate,

        [Description("OPR")]
        Odd
    }
    public static class CodeUtility
    {
        // reference: https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
