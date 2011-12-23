using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples
{
    partial class ExampleHandler
    {
        partial class WindowsFormsApplication
        {

            const string clickMeButtonName = "ClickMeButton";
            const string clickMeButtonClickName = clickMeButtonName + "_Click";
            public static Tuple<TAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateStructure<TAssembly>(TAssembly assembly)
                where TAssembly :
                    IIntermediateAssembly
            {

                var @namespace = assembly.Namespaces.Add("WindowsFormsApplication1");
                //Define the main dialog.
                var mainDialog = @namespace.Classes.Add("MainDialog");
                mainDialog.BaseType = typeof(Form).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                mainDialog.AccessLevel = AccessLevelModifiers.Internal;
                var mainMethod = CreateMainMethod(assembly, @namespace, mainDialog);

                //Add the designer partial file to the main dialog.
                var mainDialogDesigner = mainDialog.Parts.Add();

                //Defines the components of the main dialog.
                var mdComponents = mainDialogDesigner.Fields.Add(new TypedName("components", typeof(IContainer).GetTypeReference()));
                //private System.Windows.Forms.Button ClickMeButton;
                var mdClickMeButton = mainDialogDesigner.Fields.Add(new TypedName(clickMeButtonName, typeof(Button).GetTypeReference()));

                //Defines the dispose method.
                //protected override void Dispose(bool disposing) {
                var mdDispose = mainDialogDesigner.Methods.Add("Dispose", new TypedNameSeries() { { "disposing", CommonTypeRefs.Boolean } });
                var mdDisposing = mdDispose.Parameters["disposing"];
                mdDispose.AccessLevel = AccessLevelModifiers.Protected;
                mdDispose.IsOverride = true;
                //if (disposing  && this.components != null)
                var disposeCondition = mdDispose.If(mdDisposing.LogicalAnd(mdComponents.InequalTo(IntermediateGateway.NullValue)));
                //    this.components.Dispose();
                disposeCondition.Call(mdComponents.GetReference(), "Dispose");
                //    base.Dispose();
                mdDispose.Call(new SpecialReferenceExpression(SpecialReferenceKind.Base), "Dispose", mdDisposing.GetReference());
                //}

                var mdClickMeClick = mainDialog.Methods.Add(clickMeButtonClickName, typeof(EventHandler).GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>());
                mdClickMeClick.Call("Close");

                var mdInitializeComponent = CreateInitializeComponentMethod(mainDialogDesigner, mdClickMeButton, mdClickMeClick);
                //private MainDialog() {
                var mdCtor = mainDialog.DefaultConstructor;
                //    this.InitializeComponent();
                mdCtor.Call(mdInitializeComponent.GetReference());
                //}
                return new Tuple<TAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember>(assembly, mainMethod, mainDialog, mdDispose, mdInitializeComponent, mdCtor);
            }

            private static IIntermediateTopLevelMethodMember CreateMainMethod(IIntermediateAssembly testAssembly, IIntermediateNamespaceDeclaration @namespace, IIntermediateClassType mainDialog)
            {
                //Define the main method of the program class.
                var mainMethod = @namespace.Methods.Add("Main", new TypedNameSeries() { { "args", CommonTypeRefs.StringArray } });
                mainMethod.AccessLevel = AccessLevelModifiers.Private;

                //Obtain a reference to the application class.
                var applicationRef = typeof(Application).GetTypeExpression();

                //Call the boiler plate code seen in most Windows Forms applications.
                mainMethod.Call("Application".Fuse("EnableVisualStyles").Fuse(ExpressionCollection.EmptyExpressionArray));
                mainMethod.Call("Application".Fuse("SetCompatibleTextRenderingDefault").Fuse(IntermediateGateway.TrueValue));
                mainMethod.Call("Application".Fuse("Run").Fuse(mainDialog.GetNewExpression()));
                return mainMethod;
            }

            private static IIntermediateClassMethodMember CreateInitializeComponentMethod(IIntermediateClassType mainDialogDesigner, IIntermediateClassFieldMember mdClickMeButton, IIntermediateClassMethodMember mdClickMeClick)
            {
                //using System;
                mainDialogDesigner.ScopeCoercions.Add(typeof(bool).Namespace);
                //using System.Windows.Forms;
                mainDialogDesigner.ScopeCoercions.Add(typeof(AutoScaleMode).Namespace);
                //using System.Drawing;
                mainDialogDesigner.ScopeCoercions.Add(typeof(Size).Namespace);

                var thisReference = mainDialogDesigner.GetThis();
                var clickMeReference = thisReference.Fuse(clickMeButtonName);
                //private void InitializeComponent() {
                var mdInitializeComponent = mainDialogDesigner.Methods.Add("InitializeComponent");
                mdInitializeComponent.AccessLevel = AccessLevelModifiers.Private;

                mdInitializeComponent.Comment("Control/Component initialization.");
                //this.ClickMeButton = new System.Windows.Forms.Button();
                mdInitializeComponent.Assign(clickMeReference, mdClickMeButton.FieldType.GetNewExpression());
                
                //SuspendLayout();
                mdInitializeComponent.Call("SuspendLayout");

                mdInitializeComponent.Comment(clickMeButtonName + " setup");
                //this.ClickMeButton.Location = new System.Drawing.Point(12, 12);
                mdInitializeComponent.Assign(clickMeReference.Fuse("Location"), "Point".GetSymbolType().GetNewExpression(12.ToPrimitive(), 12.ToPrimitive()));
                //this.ClickMeButton.Name = "ClickMeButton";
                mdInitializeComponent.Assign(clickMeReference.Fuse("Name"), "ClickMeButton".ToPrimitive());
                //this.ClickMeButton.Size = new System.Drawing.Size(185, 32);
                mdInitializeComponent.Assign(clickMeReference.Fuse("Size"), "Size".GetSymbolType().GetNewExpression(185.ToPrimitive(), 32.ToPrimitive()));
                //this.ClickMeButton.TabIndex = 0;
                mdInitializeComponent.Assign(clickMeReference.Fuse("TabIndex"), 0.ToPrimitive());
                //this.ClickMeButton.Text = "Click Me";
                mdInitializeComponent.Assign(clickMeReference.Fuse("Text"), "Click Me".ToPrimitive());
                //this.ClickMeButton.UseVisualStyleBackColor = true;
                mdInitializeComponent.Assign(clickMeReference.Fuse("UseVisualStyleBackColor"), IntermediateGateway.TrueValue);
                //this.ClickMeButton.Click += ClickMeButton_Click;
                var clickReference = ((IClassType)(mdClickMeButton.FieldType)).Events.FindInFamily("Click", typeof(EventHandler).GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>());
                mdInitializeComponent.AddHandler(clickReference.GetReference(mdClickMeButton.GetReference()), mdClickMeClick.GetReference());
                mdInitializeComponent.Comment("MainDialog setup");
                //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                mdInitializeComponent.Assign(thisReference.Fuse("AutoScaleDimensions"), "SizeF".GetSymbolType().GetNewExpression(6F.ToPrimitive(), 13F.ToPrimitive()));
                //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                mdInitializeComponent.Assign(thisReference.Fuse("AutoScaleMode"), "AutoScaleMode".Fuse("Font"));
                //this.ClientSize = new Size(209, 56);
                mdInitializeComponent.Assign(thisReference.Fuse("ClientSize"), "Size".GetSymbolType().GetNewExpression(209.ToPrimitive(), 56.ToPrimitive()));
                //this.Controls.Add(ClickMeButton);
                mdInitializeComponent.Call(thisReference.Fuse("Controls"), "Add", clickMeReference);
                //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                mdInitializeComponent.Assign(thisReference.Fuse("FormBorderStyle"), "FormBorderStyle".Fuse("FixedToolWindow"));
                //this.MinimizeBox = false;
                mdInitializeComponent.Assign(thisReference.Fuse("MinimizeBox"), IntermediateGateway.FalseValue);
                //this.MaximizeBox = false;
                mdInitializeComponent.Assign(thisReference.Fuse("MaximizeBox"), IntermediateGateway.FalseValue);
                //this.Name = "MainDialog";
                mdInitializeComponent.Assign(thisReference.Fuse("Name"), "MainDialog".ToPrimitive());
                //this.Text = "Windows Forms Test";
                mdInitializeComponent.Assign(thisReference.Fuse("Text"), "Windows Forms Test".ToPrimitive());

                //ResumeLayout();
                mdInitializeComponent.Call("ResumeLayout");
                return mdInitializeComponent;
            }
        }
    }
}
