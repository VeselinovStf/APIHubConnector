using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Core.Abstraction
{
    public interface ISerializationFormatter<T>
    {
        T FormatSettings { get; }
    }
}
