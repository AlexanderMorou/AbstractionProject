﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    public interface IParserResults<T> 
        where T :
            IConcreteNode
    {
        /// <summary>
        /// Returns whether the operation was successful.
        /// </summary>
        bool Successful { get; }
        /// <summary>
        /// Returns the <see cref="IParserSyntaxMessageCollection"/> 
        /// which denotes points within the source file(s) that 
        /// represent halting errors.
        /// </summary>
        IParserSyntaxMessageCollection SyntaxErrors { get; }
        /// <summary>
        /// Returns the resulted <typeparamref name="T"/> from the parse operation.
        /// </summary>
        T Result { get; }
    }
}
