using NUnit.Framework;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.Recorder.RecordingTree
{
    class RecordingTreeTest
    {
        [Test]
        public void TestGivenRecordingTreeWhenChildAddedThenChildCountIncreases()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            Assert.AreEqual(0, tree.ChildCount, "GIVEN: Child cound should be null on new tree");
            
            tree.AddChild(HelperMethodToGetStandardRecordingResult());
            Assert.AreEqual(1, tree.ChildCount, "Child count should be 1 after adding a child");
        }

        [Test]
        public void TestGivenRecordingTreeWhenGetParentThenParentIsNullOnTopLevel()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            Assert.IsNull(tree.Parent, "Parent should be null at top level");
        }

        [Test]
        public void TestGivenRecordingTreeWhenAddChildThenChildParentIsNotNull()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            tree.AddChild(HelperMethodToGetStandardRecordingResult());

            IRecordingTree result = tree.Find(r => r.MethodName == "m");
            Assert.IsNotNull(result.Parent, "Parent should not be null on child");
        }

        private IRecordingResult HelperMethodToGetStandardRecordingResult()
        {
            return new RecordingResultImpl(new MethodDefinitionImpl("n", "c", "m"));
        }
    }
}
