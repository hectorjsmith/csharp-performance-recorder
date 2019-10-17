using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result
{
    interface IMethodDefinition
    {
        string Namespace { get; }

        string ClassName { get; }

        string MethodName { get; }
    }
}
