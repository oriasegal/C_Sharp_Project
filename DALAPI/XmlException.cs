using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

    public class BadIndexException : Exception
    {
        public int indx;
        public BadIndexException(int index) : base() => indx = index;
        public BadIndexException(int index, string message) :
            base(message) => indx = index;
        public BadIndexException(int index, string message, Exception innerException) :
            base(message, innerException) => indx = index;

        public override string ToString() => base.ToString() + $", bad index: {indx}";
    }
}
