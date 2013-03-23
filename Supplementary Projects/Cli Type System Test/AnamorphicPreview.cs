using AllenCopeland.Abstraction.OwnerDrawnControls;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    public partial class AnamorphicPreview : Form
    {
        private GenericAnamorphismHandler genericAnamorphisms;

        internal AnamorphicPreview(GenericAnamorphismHandler genericAnamorphisms)
        {
            InitializeComponent();
            this.genericAnamorphisms = genericAnamorphisms;
            this.imgCboAnamorphisms.Items.Clear();
            foreach (var entry in from e in this.genericAnamorphisms.Values
                                  where !e.SinglePhase
                                  orderby e.OriginalType.Name, 
                                          e.OriginalType.GenericParameters.Count
                                  select e)
            {
                string image = "";
                switch (entry.OriginalType.Type)
                {
                    case TypeKind.Class:
                        image = "Class";
                        break;
                    case TypeKind.Delegate:
                        image = "Delegate";
                        break;
                    case TypeKind.Enumeration:
                        image = "Enum";
                        break;
                    case TypeKind.Interface:
                        image = "Interface";
                        break;
                    case TypeKind.Struct:
                        image = "Struct";
                        break;
                }
                string name = entry.OriginalType.Name;
                if (entry.OriginalType.Parent is IType)
                    name = string.Format("{0}.{1}", ((IType)entry.OriginalType.Parent).Name, name);

                imgCboAnamorphisms.Items.Add(new ImageComboBox.ImageObjectItem(string.Format("{0}`{2} ({1} variants)", name, entry.Stages.Count(), entry.OriginalType.GenericParameters.Count), entry, image));
            }
            imgCboAnamorphisms.SelectedItem = imgCboAnamorphisms.Items.First();
        }

        private void imgCboAnamorphisms_SelectedValueChanged(object sender, EventArgs e)
        {
            var currentSelection = (ImageComboBox.ImageObjectItem)imgCboAnamorphisms.SelectedItem;
            var anamorphism = ((GenericAnamorphism)currentSelection.Item);
            imgLstVariations.Items.Clear();
            foreach (var stage in anamorphism.Stages)
            {
                var item = new ImageListBox.ImageObjectItem(string.Format("{0}", stage.StageType.Name), Tuple.Create(anamorphism, stage), -1);
                imgLstVariations.Items.Add(item);
            }
            imgLstVariations.SelectedItem = imgLstVariations.Items.First();
            lblGenericTypeName.Text = string.Format("{0}`{1}", anamorphism.OriginalType.Name, anamorphism.OriginalType.GenericParameters.Count);
            imgCboGenericParameters.Items.Clear();
            foreach (IGenericTypeParameter gp in anamorphism.OriginalType.TypeParameters.Values)
            {
                var current = new ImageComboBox.ImageObjectItem(gp.Name, gp, -1);
                imgCboGenericParameters.Items.Add(current);
            }
            if (anamorphism.OriginalType.IsGenericConstruct)
                imgCboGenericParameters.SelectedItem = imgCboGenericParameters.Items.First();
        }

        private void imgLstVariations_SelectedValueChanged(object sender, EventArgs e)
        {
            var currentSelection = (ImageListBox.ImageObjectItem)(imgLstVariations.SelectedItem);
            if (currentSelection == null)
            {

                if (lblGenericVariationName.Text != "No selection")
                    lblGenericVariationName.Text = "No selection";
                if (lblGenericVariationName.Enabled)
                    lblGenericVariationName.Enabled = false;
                imgLstGenericReplacements.Items.Clear();
            }
            else
            {
                var anamorphismStagePair = (Tuple<GenericAnamorphism, GenericAnamorphism.Stage>)currentSelection.Item;
                if (!lblGenericVariationName.Enabled)
                    lblGenericVariationName.Enabled = true;
                var anamorphism = anamorphismStagePair.Item1;
                var stage = anamorphismStagePair.Item2;
                lblGenericVariationName.Text = stage.StageType.Name;
                imgLstGenericReplacements.Items.Clear();
                foreach (var gpRepPair in (from index in 0.RangeTo(anamorphism.OriginalType.TypeParameters.Count)
                                           let replacement = stage.OriginalType.GenericParameters[index]
                                           let original = (IGenericTypeParameter)anamorphism.OriginalType.TypeParameters.Values[index]
                                           select Tuple.Create(original, replacement)))
                {
                    var genericParameter = gpRepPair.Item1;
                    var replacement = gpRepPair.Item2;

                    var currentItem = new ImageListBox.ImageObjectItem(string.Format("{0} -> {1}", genericParameter.Name, anamorphism.Handler.CreateAnamorphicStage(replacement, stage.StageType.Assembly).Name), gpRepPair, -1);
                    imgLstGenericReplacements.Items.Add(currentItem);
                }
            }
        }

    }
}
