using NUnit.Framework;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;

namespace PerformanceRecorderTest.Result.Formatter
{
    internal class NestedStringResultFormatterTest
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
   +- ni1.ci1.mi1                    count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
   |  +- ni1j1.ci1j1.mi1j1           count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
   |  |  +- ni1j1k1.ci1j1k1.mi1j1k1  count: 35  sum: 545.596  avg:  15.588  max:  15.588  min:  15.588
   |  |  +- ni1j1k0.ci1j1k0.mi1j1k0  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
   |  +- ni1j0.ci1j0.mi1j0           count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
   |     +- ni1j0k1.ci1j0k1.mi1j0k1  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
   |     +- ni1j0k0.ci1j0k0.mi1j0k0  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
   +- ni0.ci0.mi0                    count:  5  sum:   0.000  avg:   0.000  max:   0.000  min:   0.000
      +- ni0j1.ci0j1.mi0j1           count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
      |  +- ni0j1k1.ci0j1k1.mi0j1k1  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
      |  +- ni0j1k0.ci0j1k0.mi0j1k0  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
      +- ni0j0.ci0j0.mi0j0           count:  5  sum:   0.000  avg:   0.000  max:   0.000  min:   0.000
         +- ni0j0k1.ci0j0k1.mi0j0k1  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
         +- ni0j0k0.ci0j0k0.mi0j0k0  count:  5  sum:   0.000  avg:   0.000  max:   0.000  min:   0.000
".Replace("\r\n", Environment.NewLine);

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsNestedtringWithFilterThenFilterIsRespected()
        {
            IRecordingTree results = GenerateMockResults(2, 2, 2);
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string rawOutput = sessionResult.ToNestedString();
            string filteredOutput = sessionResult.ToNestedString(r => r.Sum > 10);
            string expectedOutput =
                @"+- 
   +- ni1.ci1.mi1                    count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
      +- ni1j1.ci1j1.mi1j1           count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
      |  +- ni1j1k1.ci1j1k1.mi1j1k1  count: 35  sum: 545.596  avg:  15.588  max:  15.588  min:  15.588
      |  +- ni1j1k0.ci1j1k0.mi1j1k0  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
      +- ni1j0.ci1j0.mi1j0           count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
         +- ni1j0k1.ci1j0k1.mi1j0k1  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
         +- ni1j0k0.ci1j0k0.mi1j0k0  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
".Replace("\r\n", Environment.NewLine);

            Console.WriteLine(filteredOutput);

            Assert.AreEqual(expectedOutput, filteredOutput, "Formatted output did not match expected format");
            Assert.AreNotEqual(rawOutput, filteredOutput, "Filtered output should not match raw output");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsNestedtringWithDepthFilterThenFilterIsRespected()
        {
            IRecordingTree results = GenerateMockResults(2, 2, 2);
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string rawOutput = sessionResult.ToNestedString();
            string filteredOutput = sessionResult.ToNestedString(r => r.Depth < 2);
            string expectedOutput =
                @"+- 
   +- ni1.ci1.mi1           count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
   |  +- ni1j1.ci1j1.mi1j1  count: 25  sum: 141.421  avg:   5.657  max:   5.657  min:   5.657
   |  +- ni1j0.ci1j0.mi1j0  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
   +- ni0.ci0.mi0           count:  5  sum:   0.000  avg:   0.000  max:   0.000  min:   0.000
      +- ni0j1.ci0j1.mi0j1  count: 15  sum:  15.000  avg:   1.000  max:   1.000  min:   1.000
      +- ni0j0.ci0j0.mi0j0  count:  5  sum:   0.000  avg:   0.000  max:   0.000  min:   0.000
".Replace("\r\n", Environment.NewLine);

            Console.WriteLine(filteredOutput);

            Assert.AreEqual(expectedOutput, filteredOutput, "Formatted output did not match expected format");
            Assert.AreNotEqual(rawOutput, filteredOutput, "Filtered output should not match raw output");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsNestedStringWithZeroDecimalPlacesThenResultFormattedCorrectly()
        {
            IRecordingTree results = GenerateMockResults(1, 1, 0);
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            sessionResult.DecimalPlacesInResults = 0;
            string output = sessionResult.ToNestedString();

            string expectedOutput =
                @"+- 
   +- ni0.ci0.mi0           count: 5  sum: 0  avg: 0  max: 0  min: 0
      +- ni0j0.ci0j0.mi0j0  count: 5  sum: 0  avg: 0  max: 0  min: 0
".Replace("\r\n", Environment.NewLine);

            Console.WriteLine(output);
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        private IRecordingTree GenerateMockResults(int topLevelCount, int midLevelCount, int bottomLevelCount)
        {
            var recorder = new ActivePerformanceRecorderImpl();
            for (int i = 0; i < topLevelCount; i++)
            {
                string iId = "i" + i;
                IRecordingTree subTree = recorder.RegisterMethd(new MethodDefinitionImpl("n" + iId, "c" + iId, "m" + iId));
                HelperMethodToGetRecordingResult(recorder, i, subTree);
                for (int j = 0; j < midLevelCount; j++)
                {
                    string jId = iId + "j" + j;
                    IRecordingTree subSubTree = recorder.RegisterMethd(new MethodDefinitionImpl("n" + jId, "c" + jId, "m" + jId), subTree);
                    HelperMethodToGetRecordingResult(recorder, j + i, subSubTree);
                    for (int k = 0; k < bottomLevelCount; k++)
                    {
                        string kId = jId + "k" + k;
                        IRecordingTree subSubSubTree = recorder.RegisterMethd(new MethodDefinitionImpl("n" + kId, "c" + kId, "m" + kId), subSubTree);
                        HelperMethodToGetRecordingResult(recorder, k + i + j, subSubSubTree);
                    }
                }
            }

            return recorder.GetResults();
        }

        private void HelperMethodToGetRecordingResult(ActivePerformanceRecorderImpl recorder, int value, IRecordingTree node)
        {
            for (int i = 0; i < 10 * (value + 0.5); i++)
            {
                recorder.RecordMethodDuration(node, Math.Pow(value, 2.5));
            }
        }
    }
}