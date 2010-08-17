using System;
using System.Linq;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Diagnostics;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    static class nTPProgram
    {
        static void Main()
        {

            Stopwatch timer = new Stopwatch();
            timer.Start();
            RunTest();
            timer.Stop();
            TimeSpan span = timer.Elapsed;
            timer.Reset();
            Console.ReadKey(true);
            Console.Clear();
            CLIGateway.ClearCache();
            timer.Start();
            RunTest();
            timer.Stop();
            Console.WriteLine("Test took {0}", timer.Elapsed);
            Console.WriteLine("Test took {0} with JIT", span);
            timer.Reset();
            Console.ReadKey(true);

        }
        private static void RunTest()
        {
            //Define the assembly.
            var codeVisitorType = typeof(IIntermediateCodeVisitor).GetTypeReference<IInterfaceType>();
            CopyType<IInterfaceIndexerMember, IInterfaceMethodMember, IInterfacePropertyMember, IInterfaceEventMember, IInterfaceType>(codeVisitorType);
        }

        private static void CopyType<TIndexer, TMethod, TProperty, TEvent, TType>(TType codeVisitorType)
            where TMethod :
                IMethodSignatureMember<TMethod, TType>
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TType>
            where TProperty :
                IPropertySignatureMember<TProperty, TType>
            where TEvent :
                IEventSignatureMember<TEvent, TType>
            where TType :
                IMethodSignatureParent<TMethod, TType>,
                IPropertySignatureParentType<TProperty, TType>,
                IIndexerSignatureParent<TIndexer, TType>,
                IEventSignatureParent<TEvent, TType>,
                IType<TType>
        {
            var assembly = IntermediateGateway.CreateAssembly(codeVisitorType.Assembly.Name);
            //Make the intermediate copy have the same namespace.
            assembly.DefaultNamespace = assembly.Namespaces.Add(codeVisitorType.Namespace.FullName);
            //Define the interface within the namespace.
            var iiit = (IIntermediateInterfaceType)assembly.DefaultNamespace.Types.Add(codeVisitorType.Name, codeVisitorType.Type);


            foreach (var method in from m in codeVisitorType.Methods.Values
                                   orderby m.UniqueIdentifier
                                   select m)
            {
                //Define a value to hold the types and names of the parameters.
                var parameters = new TypedNameSeries();
                //Iterate the parameters and insert a new typed-name for it.
                foreach (var param in method.Parameters.Values)
                    parameters.Add(param.Name, param.ParameterType, param.Direction);
                var genericParameters = new List<GenericParameterData>();
                if (method.IsGenericMethod)
                    //If it's a generic method, copy over the generic parameter information.
                    foreach (var tParam in method.TypeParameters.Values)
                        genericParameters.Add(new GenericParameterData(tParam.Name));
                var methodCopy = iiit.Methods.Add(new TypedName(method.Name, method.ReturnType), parameters, genericParameters.ToArray());
                if (methodCopy.IsGenericMethod)
                {
                    /* *
                     * If it's a generic method, replace the original method's 
                     * type-parameter references with the copy method's 
                     * type-parameters.
                     * */
                    foreach (var param in methodCopy.Parameters.Values)
                        param.ParameterType = param.ParameterType.Disambiguify(LockedTypeCollection.Empty, methodCopy.GenericParameters, TypeParameterSources.Method);

                    foreach (var gParam in method.TypeParameters.Values)
                    {
                        /* *
                         * Now copy over the constraints from the original type-parameter.
                         * Constraints are the requirements on the types that are used
                         * on what interfaces must be implemented in order to 
                         * function properly.
                         * */
                        var gParamCopy = methodCopy.TypeParameters[gParam.Name];
                        foreach (var constraint in gParam.Constraints)
                            gParamCopy.Constraints.Add(constraint);
                    }
                }
            }
            Console.WriteLine(iiit.Methods.Count);
            foreach (var method in iiit.Methods.Values.Shuffle())
                Console.WriteLine(method);
        }

    }
}
