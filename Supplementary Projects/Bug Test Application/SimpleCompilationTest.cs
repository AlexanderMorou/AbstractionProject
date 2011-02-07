using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using Microsoft.VisualBasic;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
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
        internal static void Main(string[] args)
        {
            Console.WriteLine("Time elapsed for test (1): {0}", RunTest());
            Console.WriteLine("Time elapsed for test (2): {0}", RunTest());
        }

        private static TimeSpan RunTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            WindowsFormsTest();
            sw.Stop();
            var elapsed = sw.Elapsed;
            return elapsed;
        }

        private static void WindowsFormsTest()
        {
            var testAssembly = IntermediateGateway.CreateAssembly("WindowsFormsTest");
            testAssembly.CompilationContext.OutputType = AssemblyOutputType.WindowsApplication;

            testAssembly.DefaultNamespace = testAssembly.Namespaces.Add("WindowsFormsApplication1");

            var program = testAssembly.DefaultNamespace.Classes.Add("Program");
            program.SpecialModifier = SpecialClassModifier.Static;

            var mainMethod = program.Methods.Add("Main", new TypedNameSeries() { { "args", CommonTypeRefs.String.MakeArray() } });
            mainMethod.IsStatic = true; //implicit, but explicit in some languages.
            mainMethod.AccessLevel = AccessLevelModifiers.Private;

            var mainDialog = testAssembly.DefaultNamespace.Classes.Add("MainDialog");
            mainDialog.BaseType = typeof(Form).GetTypeReference<IClassType>();
            mainDialog.AccessLevel = AccessLevelModifiers.Internal;
            
            var applicationRef = typeof(Application).GetTypeExpression();
            mainMethod.Call(applicationRef, "EnableVisualStyles");
            mainMethod.Call(applicationRef, "SetCompatibleTextRenderingDefault", IntermediateGateway.TrueValue);
            mainMethod.Call(applicationRef, "Run", mainDialog.GetNew());
            
            var mainDialogDesigner = mainDialog.Parts.Add();

            var mdComponents = mainDialogDesigner.Fields.Add(new TypedName("components", typeof(IContainer).GetTypeReference()));

            var mdDispose = mainDialogDesigner.Methods.Add("Dispose", new TypedNameSeries() { { "disposing", CommonTypeRefs.Boolean } });
            var mdDisposing = mdDispose.Parameters["disposing"];

            var disposeCondition = mdDispose.If(mdDisposing.GetReference().LogicalAnd(mdComponents.InequalTo(IntermediateGateway.NullValue)));
            disposeCondition.Call(mdComponents.GetReference(), "Dispose");

            mdDispose.Call(new SpecialReferenceExpression(SpecialReferenceKind.Base), "Dispose", mdDisposing.GetReference());

            var mdInitializeComponent = mainDialogDesigner.Methods.Add("InitializeComponent");
            mdInitializeComponent.AccessLevel = AccessLevelModifiers.Private;

            var mdClickMeButton = mainDialogDesigner.Fields.Add(new TypedName("ClickMeButton", typeof(Button).GetTypeReference()));

            mdInitializeComponent.Assign(mdClickMeButton.GetReference(), mdClickMeButton.FieldType.GetNew());

            mdInitializeComponent.Call("SuspendLayout");

            var mdClickMeReference = mdClickMeButton.GetReference();

            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Location"), typeof(Point).GetNewExpression(12.ToPrimitive(), 12.ToPrimitive()));
            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Name"), "ClickMeButton".ToPrimitive());
            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Size"), typeof(Size).GetNewExpression(185.ToPrimitive(), 32.ToPrimitive()));
            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("TabIndex"), 0.ToPrimitive());
            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("Text"), "Click Me".ToPrimitive());
            mdInitializeComponent.Assign(mdClickMeReference.GetProperty("UseVisualStyleBackColor"), IntermediateGateway.TrueValue);
            //ToDo: Add event add handler.

            var thisReference = new SpecialReferenceExpression(SpecialReferenceKind.This);
            mdInitializeComponent.Assign(thisReference.GetProperty("AutoScaleDimensions"), typeof(SizeF).GetNewExpression(6F.ToPrimitive(), 13F.ToPrimitive()));
            mdInitializeComponent.Assign(thisReference.GetProperty("AutoScaleMode"), typeof(AutoScaleMode).GetFieldExpression("Font"));
            mdInitializeComponent.Assign(thisReference.GetProperty("ClientSize"), typeof(Size).GetNewExpression(209.ToPrimitive(), 56.ToPrimitive()));
            mdInitializeComponent.Call(thisReference.GetProperty("Controls"), "Add", mdClickMeReference);
            mdInitializeComponent.Assign(thisReference.GetProperty("FormBorderStyle"), typeof(FormBorderStyle).GetFieldExpression("FixedToolWindow"));
            mdInitializeComponent.Assign(thisReference.GetProperty("MinimizeBox"), IntermediateGateway.FalseValue);
            mdInitializeComponent.Assign(thisReference.GetProperty("MaximizeBox"), IntermediateGateway.FalseValue);
            mdInitializeComponent.Assign(thisReference.GetProperty("Name"), "MainDialog".ToPrimitive());
            mdInitializeComponent.Assign(thisReference.GetProperty("Text"), "Windows Forms Test".ToPrimitive());

            mdInitializeComponent.Call("ResumeLayout");

            var mdCtor = mainDialog.Constructors.Add();
            mdCtor.Call(mdInitializeComponent.GetReference());

            
        }
    }
}
