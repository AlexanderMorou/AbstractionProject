using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.CSharp;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples
{
    partial class ExampleHandler
    {
        partial class WindowsFormsApplication
        {

            public static Tuple<TAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateStructure<TAssembly>(TAssembly assembly)
                where TAssembly :
                    IIntermediateAssembly
            {
                //Create the assembly and define its output type.
                var testAssembly = assembly; //activator("WindowsFormsTest");
                testAssembly.References.Add(typeof(int).Assembly.GetAssemblyReference());
                testAssembly.References.Add(typeof(Form).Assembly.GetAssemblyReference(), "forms", AssemblyReferenceCollection.DefaultAlias);
                testAssembly.References.Add(typeof(Queryable).Assembly.GetAssemblyReference());
                testAssembly.CompilationContext.OutputType = AssemblyOutputType.WinFormsApplication;

                var @namespace = testAssembly.Namespaces.Add("WindowsFormsApplication1");
                //Define the assembly's default namespace.
                //testAssembly.DefaultNamespace = testAssembly.Namespaces.Add("WindowsFormsApplication1");

                //Define the main dialog.
                var mainDialog = @namespace.Classes.Add("MainDialog");
                mainDialog.BaseType = typeof(Form).GetTypeReference<IClassType>();
                mainDialog.AccessLevel = AccessLevelModifiers.Internal;
                var mainMethod = CreateMainMethod(testAssembly, @namespace, mainDialog);

                //Add the designer partial file to the main dialog.
                var mainDialogDesigner = mainDialog.Parts.Add();

                //Defines the components of the main dialog.
                var mdComponents = mainDialogDesigner.Fields.Add(new TypedName("components", typeof(IContainer).GetTypeReference()));

                //Defines the dispose method.
                //protected override void Dispose(bool disposing) {
                var mdDispose = mainDialogDesigner.Methods.Add("Dispose", new TypedNameSeries() { { "disposing", CommonTypeRefs.Boolean } });
                var mdDisposing = mdDispose.Parameters["disposing"];
                mdDispose.AccessLevel = AccessLevelModifiers.Protected;
                mdDispose.IsOverride = true;

                //if (disposing  && this.components != null)
                var disposeCondition = mdDispose.If(mdDisposing.LogicalAnd(mdComponents.InequalTo(IntermediateGateway.NullValue)));
                //   this.components.Dispose();
                disposeCondition.Call(mdComponents.GetReference(), "Dispose");
                //base.Dispose();
                mdDispose.Call(new SpecialReferenceExpression(SpecialReferenceKind.Base), "Dispose", mdDisposing.GetReference());
                //}

                //private System.Windows.Forms.Button ClickMeButton;
                var mdClickMeButton = mainDialogDesigner.Fields.Add(new TypedName("ClickMeButton", typeof(Button).GetTypeReference()));

                var mdClickMeClick = mainDialog.Methods.Add("ClickMeButton_Click", typeof(EventHandler).GetTypeReference<IDelegateType>());
                mdClickMeClick.Call("Close");

                var mdInitializeComponent = CreateInitializeComponentMethod(mainDialogDesigner, mdClickMeButton, mdClickMeClick);
                //private MainDialog() {
                var mdCtor = mainDialog.Constructors.Add();
                //this.InitializeComponent();
                mdCtor.Call(mdInitializeComponent.GetReference());
                //}
                return new Tuple<TAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember>(testAssembly, mainMethod, mainDialog, mdDispose, mdInitializeComponent, mdCtor);
            }

            private static IIntermediateTopLevelMethodMember CreateMainMethod(IIntermediateAssembly testAssembly, IIntermediateNamespaceDeclaration @namespace, IIntermediateClassType mainDialog)
            {
                //Define the main method of the program class.
                var mainMethod = @namespace.Methods.Add("Main", new TypedNameSeries() { { "args", CommonTypeRefs.StringArray } });
                mainMethod.AccessLevel = AccessLevelModifiers.Private;

                //Obtain a reference to the application class.
                var applicationRef = typeof(Application).GetTypeExpression();

                //Call the boiler plate code seen in most Windows Forms applications.
                mainMethod.Call(applicationRef, "EnableVisualStyles");
                mainMethod.Call(applicationRef, "SetCompatibleTextRenderingDefault", IntermediateGateway.TrueValue);
                mainMethod.Call(applicationRef, "Run", mainDialog.GetNew());
                return mainMethod;
            }

            private static IIntermediateClassMethodMember CreateInitializeComponentMethod(IIntermediateClassType mainDialogDesigner, IIntermediateClassFieldMember mdClickMeButton, IIntermediateClassMethodMember mdClickMeClick)
            {
                var thisReference = mainDialogDesigner.GetThis();
                //private void InitializeComponent() {
                var mdInitializeComponent = mainDialogDesigner.Methods.Add("InitializeComponent");
                mdInitializeComponent.AccessLevel = AccessLevelModifiers.Private;

                mdInitializeComponent.Comment("Control/Component initialization.");
                //this.ClickMeButton = new System.Windows.Forms.Button();
                mdInitializeComponent.Assign(mdClickMeButton.GetReference(), mdClickMeButton.FieldType.GetNew());

                //SuspendLayout();
                mdInitializeComponent.Call("SuspendLayout");

                var mdClickMeReference = mdClickMeButton.GetReference();
                mdInitializeComponent.Comment("ClickMeButton setup");
                //this.ClickMeButton.Location = new System.Drawing.Point(12, 12);
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Location"), typeof(Point).GetNewExpression(12.ToPrimitive(), 12.ToPrimitive()));
                //this.ClickMeButton.Name = "ClickMeButton";
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Name"), "ClickMeButton".ToPrimitive());
                //this.ClickMeButton.Size = new System.Drawing.Size(185, 32);
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Size"), typeof(Size).GetNewExpression(185.ToPrimitive(), 32.ToPrimitive()));
                //this.ClickMeButton.TabIndex = 0;
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("TabIndex"), 0.ToPrimitive());
                //this.ClickMeButton.Text = "Click Me";
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Text"), "Click Me".ToPrimitive());
                //this.ClickMeButton.UseVisualStyleBackColor = true;
                mdInitializeComponent.Assign(mdClickMeReference.GetProperty("UseVisualStyleBackColor"), IntermediateGateway.TrueValue);
                //this.ClickMeButton.Click += ClickMeButton_Click;
                mdInitializeComponent.AddHandler(mdClickMeReference, "Click", mdClickMeClick.GetReference());
                
                mdInitializeComponent.Comment("MainDialog setup");
                //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                mdInitializeComponent.Assign(thisReference.GetProperty("AutoScaleDimensions"), typeof(SizeF).GetNewExpression(6F.ToPrimitive(), 13F.ToPrimitive()));
                //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                mdInitializeComponent.Assign(thisReference.GetProperty("AutoScaleMode"), typeof(AutoScaleMode).GetFieldExpression("Font"));
                //this.ClientSize = new Size(209, 56);
                mdInitializeComponent.Assign(thisReference.GetProperty("ClientSize"), typeof(Size).GetNewExpression(209.ToPrimitive(), 56.ToPrimitive()));
                //this.Controls.Add(ClickMeButton);
                mdInitializeComponent.Call(thisReference.GetProperty("Controls"), "Add", mdClickMeReference);
                //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                mdInitializeComponent.Assign(thisReference.GetProperty("FormBorderStyle"), typeof(FormBorderStyle).GetFieldExpression("FixedToolWindow"));
                //this.MinimizeBox = false;
                mdInitializeComponent.Assign(thisReference.GetProperty("MinimizeBox"), IntermediateGateway.FalseValue);
                //this.MaximizeBox = false;
                mdInitializeComponent.Assign(thisReference.GetProperty("MaximizeBox"), IntermediateGateway.FalseValue);
                //this.Name = "MainDialog";
                mdInitializeComponent.Assign(thisReference.GetProperty("Name"), "MainDialog".ToPrimitive());
                //this.Text = "Windows Forms Test";
                mdInitializeComponent.Assign(thisReference.GetProperty("Text"), "Windows Forms Test".ToPrimitive());

                //ResumeLayout();
                mdInitializeComponent.Call("ResumeLayout");
                return mdInitializeComponent;

            }
        }
    }
}
