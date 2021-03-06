﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// The functional kind of a statement.
    /// </summary>
    [Flags]
    public enum StatementKinds :
        ulong
    {
        
        None                                = 0x0000000000000000,
        /// <summary>
        /// The statement is a block statement which
        /// simply acts as a scoping region with statements
        /// of its own.
        /// </summary>
        BlockStatement                      = 0x0000000000000001,
        /// <summary>
        /// The statement serves to control code flow
        /// via means of a predicate condition.
        /// </summary>
        ConditionStatement                  = 0x0000000000000002,
        /// <summary>
        /// The statement serves to control code flow 
        /// via means of repetition in the statement
        /// block provided
        /// </summary>
        DoStatement                         = 0x0000000000000004,
        /// <summary>
        /// The statement serves to provide functionality to
        /// step through the members of an iterator which
        /// exposes GetEnumerator() functionality.
        /// </summary>
        /// <remarks>Does not necessarily have to implement
        /// <see cref="IEnumerable"/> or <see cref="IEnumerable{T}"/>;
        /// however, it must contain a method called GetEnumerator
        /// which retrieves an object which has MoveNext,
        /// and Current.</remarks>
        IteratorStatement                   = 0x0000000000000008,
        /// <summary>
        /// The statement serves to iterate through the
        /// code block based upon the predicate condition
        /// that discerns whether to continue or exit.
        /// </summary>
        IterationStatement                  = 0x0000000000000010,
        /// <summary>
        /// The statement serves to iterate through the code
        /// block predicated by a simplistic numeric range 
        /// with a possible span between iterations greater 
        /// than one.
        /// </summary>
        SimpleIteratorStatement             = 0x0000000000000020,
        /// <summary>
        /// The statement serves to provide access to a jump
        /// table.
        /// </summary>
        /// <remarks><para>
        /// Operates by analysis upon the cases provided;
        /// should the span between two values be too great
        /// the jump table is segmented as needed.
        /// </para><para>
        /// The case expression is pushed to the stack for each
        /// table group and adjusted to zero relative to the lowest
        /// valued index for the current grouping.  Afterwords
        /// a jmp instruction is supplied which denotes the individual
        /// labels for the cases of the current grouping.
        /// </para></remarks>
        SwitchStatement                     = 0x0000000000000040,
        /// <summary>
        /// The statement provides functionality to attempt
        /// an action and catch potential errors based upon
        /// an exception.
        /// </summary>
        TryCatchStatement                   = 0x0000000000000080,
        /// <summary>
        /// The statement provides functionality to provide functional
        /// code with a finally block that is always executed regardless
        /// of errors, returns, et al. 
        /// </summary>
        TryFinallyStatement                 = 0x0000000000000100,
        /// <summary>
        /// The statement provides functionality to attempt an
        /// action with the ability to catch errors based upon
        /// an exception, with a finally block that is always 
        /// executed regardless of errors, returns, et al. 
        /// </summary>
        TryCatchFinallyStatement            = 0x0000000000000200,
        /// <summary>
        /// Provides functionality to use a disposable component
        /// inside a statement block.
        /// </summary>
        /// <remarks>Upon completion of the statement block
        /// the used component is disposed.</remarks>
        UsingBlockStatement                 = 0x0000000000000400,
        /// <summary>
        /// The statement repeats the block until the condition
        /// for the iteration is false.
        /// </summary>
        WhileStatement                      = 0x0000000000000800,
        /// <summary>
        /// The statement declares a local variable for use
        /// within the block, where member accessors can start
        /// statements which, in fact, refer to the witheld 
        /// local variable declared for the block.
        /// </summary>
        WithStatement                       = 0x0000000000001000,
        /// <summary>
        /// The statement forwards to an assignment expression
        /// which is responsible for assignment to a property,
        /// field, or local.
        /// </summary>
        AssignmentStatement                 = 0x0000000000002000,
        /// <summary>
        /// The statement breaks out of the flow from a breakable
        /// block statement.
        /// </summary>
        BreakStatement                      = 0x0000000000004000,
        /// <summary>
        /// The statement invokes a method.
        /// </summary>
        CallStatement                       = 0x0000000000008000,
        /// <summary>
        /// The statement is a fusion of an expression to a series
        /// of comma delimited expressions which looks like a
        /// call, but the specifics aren't clear until further
        /// investigation is done.
        /// </summary>
        CallFusionStatement                 = 0x0000000000010000,
        /// <summary>
        /// The statement invokes a method in a series
        /// of calls under the same name but with different
        /// parameters.
        /// </summary>
        CallSetSeriesStatement              = 0x0000000000020000,
        /// <summary>
        /// The statement moves to the current iterator/iteration
        /// statement's initial execution point, repeating any
        /// predicated logic to further discern flow.
        /// </summary>
        ContinueStatement                   = 0x0000000000040000,
        /// <summary>
        /// The statement increments or decrements an assignment
        /// target.
        /// </summary>
        CrementStatement                    = 0x0000000000080000,
        /// <summary>
        /// The statement declares a local with the type of the
        /// local explicitly declared.
        /// </summary>
        DeclarationExplicitStatement        = 0x0000000000100000,
        /// <summary>
        /// The statement implicitly declares a local with the
        /// type of the local inferred by assignment.
        /// </summary>
        DeclarationImplicitStatement        = 0x0000000000200000,
        /// <summary>
        /// The statement dynamically declares a local with
        /// the type of the local, and dispatch resolution, 
        /// deferred until runtime.
        /// </summary>
        DeclarationDynamicStatement         = 0x0000000000400000,
        /// <summary>
        /// The statement unconditionally transfers flow control
        /// to the label provided by the goto statement.
        /// </summary>
        GoToStatement                       = 0x0000000000800000,
        /// <summary>
        /// The statement declares a label for reference by a 
        /// goto statement, or other flow-control aspect.
        /// </summary>
        LabelStatement                      = 0x0000000001000000,
        /// <summary>
        /// The statement ends the execution of the current method
        /// and returns execution control back to the call site.
        /// </summary>
        /// <remarks>On non-<see cref="Void"/>
        /// return values of methods, properties, or otherwise
        /// this yields the value on the stack, or in stackless
        /// languages, requires a return expression to be stated.
        /// </remarks>
        ReturnStatement                     = 0x0000000002000000,
        /// <summary>
        /// The statement represents a means to throw an 
        /// exception.
        /// </summary>
        ThrowStatement                      = 0x0000000004000000,
        /// <summary>
        /// The statement represents an iterator return point.
        /// </summary>
        YieldReturnStatement                = 0x0000000008000000,
        /// <summary>
        /// The statement represents an iterator breaking point
        /// (exit state).
        /// </summary>
        YieldBreakStatement                 = 0x0000000010000000,
        /// <summary>
        /// The statement represents an asynchronous method's
        /// breakpoint to mark what's after as a point of 
        /// continuation.
        /// </summary>
        AwaitStatement                      = 0x0000000020000000,
        /// <summary>
        /// The statement represents a monitor enter/exit block.
        /// </summary>
        LockStatement                       = 0x0000000040000000,
        /// <summary>
        /// The statement represents a using statement where the 
        /// expression targeted as a resource acquisition is disposed upon exit.
        /// </summary>
        UsingExpressionStatement            = 0x0000000080000000,
        /// <summary>
        /// All statements.
        /// </summary>
        All                                 = 0x00000000FFFFFFFF,
    }
}
