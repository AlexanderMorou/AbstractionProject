using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.ModelViewer
{
    public partial class CodeModelViewer : 
        Form
    {
        private TypeAssemblyAndNamespaceVisitor primaryVisitor;
        private class TypeAssemblyAndNamespaceVisitor :
            IIntermediateDeclarationVisitor,
            IIntermediateTypeVisitor
        {
            private CodeModelViewer viewer;
            #region IIntermediateDeclarationVisitor Members

            public void Visit(IIntermediateAssembly assembly)
            {
                viewer.SelectNamespaceAndTypeParent(assembly);
            }

            public void Visit(IIntermediateNamespaceDeclaration @namespace)
            {
                viewer.SelectNamespaceAndTypeParent(@namespace);
            }

            #endregion

            #region IIntermediateTypeVisitor Members

            public void Visit(IIntermediateClassType @class)
            {
                viewer.SelectMemberAndTypeParent(@class);
            }

            public void Visit(IIntermediateDelegateType @delegate)
            {
                viewer.SelectMemberParent(@delegate);
            }

            public void Visit(IIntermediateEnumType @enum)
            {
                viewer.SelectMemberParent(@enum);
            }

            public void Visit(IIntermediateInterfaceType @interface)
            {
                viewer.SelectMemberAndTypeParent(@interface);
            }

            public void Visit(IIntermediateStructType @struct)
            {
                viewer.SelectMemberAndTypeParent(@struct);
            }

            #endregion
        }
        //ImageComboBox ClassSelector;
        //ImageComboBox ClassMemberSelector;
        //TableLayoutPanel tableLayoutPanel1;
        //TreeView ClassAndNamespaceView;
        //TreeView ClassAndMemberStatementView;
        internal CodeModelViewer()
        {
            InitializeComponent();
            this.Shown += new EventHandler(CodeModelViewer_Shown);
        }

        void CodeModelViewer_Shown(object sender, EventArgs e)
        {
            if (this.Assembly != null)
            {
                this.ClassAndNamespaceView.Nodes.Clear();
                this.AddNode(this.Assembly);
            }
        }

        private void AddNode(IIntermediateAssembly assembly)
        {
            AddNode<IIntermediateAssembly>(assembly, "Assembly");
        }

        private void AddNode<T>(T target, string targetImage, TreeNode parent = null)
            where T :
                IIntermediateDeclaration,
                IIntermediateTypeParent,
                IIntermediateNamespaceParent
        {
            TreeNode node = new TreeNode(target.Name);
            node.ImageKey = targetImage;
            node.Tag = target;
            foreach (var childspace in target.Namespaces)
            {
                
            }
            if (parent == null)
                ClassAndNamespaceView.Nodes.Add(node);
            else
                parent.Nodes.Add(node);
        }

        private void AddNode(IIntermediateTypeParent target, string targetImage, TreeNode parent = null)
        {

        }

        public IIntermediateAssembly Assembly { get; set; }

        private void MakeNewSelection()
        {
            ClassAndMemberStatementView.Nodes.Clear();

        }

        private void SelectMemberAndTypeParent<T>(T target)
            where T :
                IIntermediateTypeParent,
                IIntermediateMemberParent
        {
            MakeNewSelection();
        }

        private void SelectTypeParent(IIntermediateTypeParent target)
        {
            MakeNewSelection();

        }

        internal void SelectMemberParent(IIntermediateMemberParent target)
        {
            MakeNewSelection();
            
        }

        internal void SelectNamespaceAndTypeParent<T>(T assembly)
            where T :
                IIntermediateTypeParent,
                IIntermediateNamespaceParent
        {
            MakeNewSelection();
        }
    }
}
