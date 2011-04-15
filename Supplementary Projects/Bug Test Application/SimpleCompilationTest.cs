using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.CSharp;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Common;
using System.Reflection;
using System.Linq;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication;
using System.Runtime.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public static class SimpleCompilerTest 
    {
        public static Func<TimeSpan> WindowsFormsTestTimedAction = ((Action)WindowsFormsTest).TimeActionFunc();
        public static Func<string, TimeSpan> PrintAllTimedAction = ((Action<string>)PrintAll).TimeActionFunc();
        public static void Main(string[] args)
        {
            const string targetNamespace = "System.Collections.Generic";
            Console.WriteLine("Time elapsed for iteration test (1): {0}", PrintAllTimedAction(targetNamespace));
            Console.WriteLine("Time elapsed for test (1): {0}", WindowsFormsTestTimedAction());
            CLIGateway.ClearCache();
            Console.WriteLine("Time elapsed for iteration test (2): {0}", PrintAllTimedAction(targetNamespace));
            Console.WriteLine("Time elapsed for test (2): {0}", WindowsFormsTestTimedAction());
        }

        private static void PrintAll(string @namespace)
        {
            typeof(int).Assembly.GetAssemblyReference().Namespaces[@namespace].AggregateIdentifiers.OnAll(PrintAllOnAll);
        }

        private static void PrintAllOnAll(string target)
        { 
            //Do nothing, simple iteration test.
        }

        private static void WindowsFormsTest()
        {
            //Create the assembly and define its output type.
            var testAssembly = IntermediateGateway.CreateAssembly("WindowsFormsTest");
            testAssembly.References.Add(typeof(int).Assembly.GetAssemblyReference());
            testAssembly.References.Add(typeof(Form).Assembly.GetAssemblyReference(), "forms", AssemblyReferenceCollection.DefaultAlias);
            testAssembly.References.Add(typeof(Queryable).Assembly.GetAssemblyReference());
            testAssembly.CompilationContext.OutputType = AssemblyOutputType.WinFormsApplication;

            //Define the assembly's default namespace.
            testAssembly.DefaultNamespace = testAssembly.Namespaces.Add("WindowsFormsApplication1");

            //Define the main dialog.
            var mainDialog = testAssembly.DefaultNamespace.Classes.Add("MainDialog");
            mainDialog.BaseType = typeof(Form).GetTypeReference<IClassType>();
            mainDialog.AccessLevel = AccessLevelModifiers.Internal;

            CreateProgramClass(testAssembly, mainDialog);
            
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

        }

        private static void CreateProgramClass(IIntermediateAssembly testAssembly, IIntermediateClassType mainDialog)
        {
            //Define the program class.
            var program = testAssembly.DefaultNamespace.Classes.Add("Program");
            program.SpecialModifier = SpecialClassModifier.Static;

            //Define the main method of the program class.
            var mainMethod = program.Methods.Add("Main", new TypedNameSeries() { { "args", CommonTypeRefs.String.MakeArray() } });
            mainMethod.IsStatic = true; //implicit, but explicit in some languages.
            mainMethod.AccessLevel = AccessLevelModifiers.Private;

            //Obtain a reference to the application class.
            var applicationRef = typeof(Application).GetTypeExpression();

            //Call the boiler plate code seen in most Windows Forms applications.
            mainMethod.Call(applicationRef, "EnableVisualStyles");
            mainMethod.Call(applicationRef, "SetCompatibleTextRenderingDefault", IntermediateGateway.TrueValue);
            mainMethod.Call(applicationRef, "Run", mainDialog.GetNew());
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
/*               Split view reference line.  139 Visible Characters.  Visual Studio doesn't persist this information. :(                */
/*                              Split view reference line.  165 Visible Characters.  When the solution explorer is hidden from view.                              */
/* *
 * Identity Resolution.
 * Rewrites
 * Type checking
 * Stack Generation
 * */
