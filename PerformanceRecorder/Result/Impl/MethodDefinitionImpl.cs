using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Impl
{
    class MethodDefinitionImpl : IMethodDefinition
    {
        public string Namespace { get; }

        public string ClassName { get; }

        public string MethodName { get; }

        public MethodDefinitionImpl(string @namespace, string className, string methodName)
        {
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
            MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", Namespace, ClassName, MethodName);
        }
    }
}
