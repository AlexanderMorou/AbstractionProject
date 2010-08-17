using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.TypeSystems.Cli;

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public partial class ObjectBrowserDialog : Form
    {
        private List<ICompiledAssembly> assemblies = new List<ICompiledAssembly>();
        public ObjectBrowserDialog()
        {
            InitializeComponent();
        }

        private void AssembliesAddMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
