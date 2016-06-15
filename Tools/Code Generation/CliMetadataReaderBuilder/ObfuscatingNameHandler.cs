using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
namespace CliMetadataReader
{
    internal class ObfuscatingNameHandler :
        ICodeGeneratorNameHandler
    {
        private Dictionary<IDeclaration, string> obfuscatedTargets = new Dictionary<IDeclaration, string>();
        private Dictionary<string, string> obfuscations = new Dictionary<string, string>();
        private Random r = new Random();
        #region ICodeGeneratorNameHandler Members

        public bool HandlesName(IDeclaration declaredMember)
        {
            if (declaredMember is IFieldMember)
            {
                if (declaredMember.ParentTarget is IClassType)
                {
                    IClassType parentC = declaredMember.ParentTarget as IClassType;
                    if (parentC.Name == "CliMetadataTableStreamAndHeader")
                        return false;
                }
                return true;
            }
            else if (declaredMember is IMethodParameterMember)
            {
                return true;
            }
            else if (declaredMember is IConstructorParameterMember)
            {
                return true;
            }
            else if (declaredMember is IStatementBlockLocalMember)
            {
                return true;
            }
            return false;
        }

        public bool HandlesName(string name)
        {
            return false;
        }

        public string HandleName(IDeclaration declaredMember)
        {
            if (!obfuscatedTargets.ContainsKey(declaredMember))
                obfuscatedTargets.Add(declaredMember, Obfuscate(declaredMember.Name, declaredMember));
            return obfuscatedTargets[declaredMember];
        }

        private string Obfuscate(string p, IDeclarationTarget target)
        {
            string result;
            string oP = p;
            p += target.ToString();
            if (!obfuscations.TryGetValue(p, out result))
            {
                int l = (int)r.Next(1, oP.Length);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < l; i++)
                {
                    char current = p[r.Next(0, p.Length)];
                    switch (current)
                    {
                        case '[':
                        case ']':
                            current = 'Ø';
                            break;
                        case ':':
                        case '.':
                        case ',':
                        case ' ':
                        case '+':
                            current = '_';
                            break;
                        case '(':
                        case ')':
                            current = 'µ';
                            break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            if (i == 0)
                                current= (char)('ⅰ' + (current - '0'));
                            break;
                    }
                    sb.Append(current);
                }
                result = sb.ToString();
                obfuscations.Add(p, result);
            }
            return result;
        }

        public string HandleName(string name)
        {
            return name;
        }

        #endregion
    }
}
