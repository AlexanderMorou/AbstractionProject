using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal class CliMetadataMethodBody :
        ICliMetadataMethodBody
    {
        private interface IStackInstruction { CilOpcodeInstruction Instruction { get; } }

        private class StandardInstruction : IStackInstruction
        {
            public StandardInstruction(CilOpcodeInstruction instruction) { this.Instruction = instruction; }
            public CilOpcodeInstruction Instruction { get; private set; }
        }

        private class MethodInstruction : StandardInstruction { public MethodInstruction(CilOpcodeInstruction instruction, ICliMetadataMethodDefOrRefRow method) : base(instruction) { } }
        private class MethodInstInstruction : StandardInstruction { public MethodInstInstruction(CilOpcodeInstruction instruction, ICliMetadataMethodSpecificationTableRow method) : base(instruction) { } }

        ICliMetadataRoot metadataRoot;
        private uint rva;
        private CliMetadataMethodHeader header;
        private bool initialized;
        private object syncObject = new object();
        public CliMetadataMethodBody(ICliMetadataRoot metadataRoot, uint rva)
        {

            this.metadataRoot = metadataRoot;
            this.rva = rva;
        }

        private void Initialize()
        {
            lock (syncObject)
            {
                if (this.initialized)
                    return;
                this.header = new CliMetadataMethodHeader(this.metadataRoot, this.rva);
                Stack<IStackInstruction> instructions = new Stack<IStackInstruction>();
                for (int i = 0, c = (int)this.header.BodyVector.Length; i < c; i++)
                {

                }
                this.initialized = true;
            }
        }

        public ICliMetadataMethodHeader Header
        {
            get
            {
                lock (syncObject)
                    if (!this.initialized)
                        this.Initialize();
                return this.header;
            }
        }
    }
}
