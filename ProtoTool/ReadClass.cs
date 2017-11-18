using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using Google.Protobuf;
using Google.Protobuf.Reflection;
namespace ProtoTool
{
    public  class ReadClass : IMessage<ReadClass>
    {
        public MessageDescriptor Descriptor { get; }

        public int CalculateSize()
        {
            return 0;
        }
        public void MergeFrom(ReadClass message)
        {

        }
        public void MergeFrom(CodedInputStream input)
        {

        }
        public void WriteTo(CodedOutputStream output)
        {

        }
        public ReadClass Clone()
        {
            return null;
        }

        public bool Equals(ReadClass other)
        {
            return false;
        }
    }
}
