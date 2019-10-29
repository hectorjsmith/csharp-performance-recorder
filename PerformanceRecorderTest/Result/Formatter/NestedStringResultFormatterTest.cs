using NUnit.Framework;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.Result.Formatter
{
    class NestedStringResultFormatterTest
    {
        [Test]
        public void TestGivenLargeResultTreeWhenFormattedAsNestedStringThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults(2, 2, 2);
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToNestedString();
            Console.WriteLine(output);
            string expectedOutput =
                @"+- 
   +- ni0.ci0.mi0                    count:  5  sum:   0.00  avg:   0.00  max:   0.00  min:   0.00
   |  +- ni0j0.ci0j0.mi0j0           count:  5  sum:   0.00  avg:   0.00  max:   0.00  min:   0.00
   |  |  +- ni0j0k0.ci0j0k0.mi0j0k0  count:  5  sum:   0.00  avg:   0.00  max:   0.00  min:   0.00
   |  |  +- ni0j0k1.ci0j0k1.mi0j0k1  count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
   |  +- ni0j1.ci0j1.mi0j1           count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
   |     +- ni0j1k0.ci0j1k0.mi0j1k0  count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
   |     +- ni0j1k1.ci0j1k1.mi0j1k1  count: 25  sum: 141.42  avg:   5.66  max:   5.66  min:   5.66
   +- ni1.ci1.mi1                    count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
      +- ni1j0.ci1j0.mi1j0           count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
      |  +- ni1j0k0.ci1j0k0.mi1j0k0  count: 15  sum:  15.00  avg:   1.00  max:   1.00  min:   1.00
      |  +- ni1j0k1.ci1j0k1.mi1j0k1  count: 25  sum: 141.42  avg:   5.66  max:   5.66  min:   5.66
      +- ni1j1.ci1j1.mi1j1           count: 25  sum: 141.42  avg:   5.66  max:   5.66  min:   5.66
         +- ni1j1k0.ci1j1k0.mi1j1k0  count: 25  sum: 141.42  avg:   5.66  max:   5.66  min:   5.66
         +- ni1j1k1.ci1j1k1.mi1j1k1  count: 35  sum: 545.60  avg:  15.59  max:  15.59  min:  15.59
";

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        private IRecordingTree GenerateMockResults(int topLevelCount, int midLevelCount, int bottomLevelCount)
        {
            IRecordingTree tree = new RecordingTreeImpl();
            for (int i = 0; i < topLevelCount; i++)
            {
                string iId = "i" + i;
                IRecordingTree subTree = HelperMethodToGetRecordingResult("n" + iId, "c" + iId, "m" + iId, i, tree);
                for (int j = 0; j < midLevelCount; j++)
                {
                    string jId = iId + "j" + j;
                    IRecordingTree subSubTree = HelperMethodToGetRecordingResult("n" + jId, "c" + jId, "m" + jId, i + j, subTree);
                    for (int k = 0; k < bottomLevelCount; k++)
                    {
                        string kId = jId + "k" + k;
                        HelperMethodToGetRecordingResult("n" + kId, "c" + kId, "m" + kId, i + j + k, subSubTree);
                    }
                }
            }

            return tree;
        }

        private IRecordingTree HelperMethodToGetRecordingResult(string namespaceName, string className, string method, int value, IRecordingTree tree)
        {
            MethodDefinitionImpl methodDef = new MethodDefinitionImpl(namespaceName, className, method);
            RecordingResultImpl recording = new RecordingResultImpl(methodDef);
            for (int i = 0; i < 10 * (value + 0.5); i++)
            {
                recording.AddResult(Math.Pow(value, 2.5));
            }

            return tree.AddChild(recording);
        }
    }
}
