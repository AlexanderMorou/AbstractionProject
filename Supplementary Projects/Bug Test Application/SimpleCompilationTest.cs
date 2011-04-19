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
            var uo = new int[2][, ,] 
            { new int[,,] 
                {
                   {
                       { 0x01, 0x02, 0x03, 0x1C },
                       { 0x04, 0x05, 0x06, 0x1D },
                       { 0x07, 0x08, 0x09, 0x1E }
                   },
                   {
                       { 0x0A, 0x0B, 0x0C, 0x1F },
                       { 0x0D, 0x0E, 0x0F, 0x20 },
                       { 0x10, 0x11, 0x12, 0x21 }
                   },
                   {
                       { 0x13, 0x14, 0x15, 0x22 },
                       { 0x16, 0x17, 0x18, 0x23 },
                       { 0x19, 0x1A, 0x1B, 0x24 }
                   }
                }, 
                new int[,,] {
                   {
                       { 0x25, 0x26, 0x27, 0x40 },
                       { 0x28, 0x29, 0x2a, 0x41 },
                       { 0x2b, 0x2c, 0x2d, 0x42 }
                   },
                   {
                       { 0x2e, 0x2f, 0x30, 0x43 },
                       { 0x31, 0x32, 0x33, 0x44 },
                       { 0x34, 0x35, 0x36, 0x45 }
                   },
                   {
                       { 0x37, 0x38, 0x39, 0x46 },
                       { 0x3a, 0x3b, 0x3c, 0x47 },
                       { 0x3d, 0x3e, 0x3f, 0x48 }
                   }
                }
            };
            var uoe = uo.ToExpression<int[, ,]>(
                u => u.ToExpression());
            var m = typeof(int).MakeArrayType(3).MakeArrayType().GetTypeReference();
            int testCount = 9;
            for (int i = 0; i < testCount; i++)
            {
                if (i == 0)
                    Console.WriteLine("Test ran with full JIT.");
                else if (i == 1)
                {
                    CLIGateway.ClearCache();
                    Console.WriteLine("Test ran with JIT finished, with clean cache.");
                }
                else
                    Console.WriteLine("Test #{0} ran with JIT finished, with cache intact.", i - 1);

                DoTest();
                Console.WriteLine();
            }
        }
        private static void DoTest()
        {
            const string targetNamespace = "System.Collections.Generic";
            Console.WriteLine("Time elapsed for iteration test: {0}", PrintAllTimedAction(targetNamespace));
            Console.WriteLine("Time elapsed for object model test: {0}", WindowsFormsTestTimedAction());
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
            //Define the main method of the program class.
            var mainMethod = testAssembly.DefaultNamespace.Methods.Add("Main", new TypedNameSeries() { { "args", CommonTypeRefs.String.MakeArray() } });
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
