﻿using NUnit.Framework;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorderTest.Recorder.RecordingTree
{
    internal class RecordingTreeTest
    {
        [Test]
        public void TestGivenRecordingTreeWhenChildAddedThenChildCountIncreases()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            Assert.AreEqual(0, tree.ChildCount, "GIVEN: Child count should be null on new tree");

            tree.AddChild(HelperMethodToGetStandardRecordingResult());
            Assert.AreEqual(1, tree.ChildCount, "Child count should be 1 after adding a child");
        }

        [Test]
        public void TestGivenRecordingTreeWhenChildAddedThenReturnedTreeIsChildOfOriginalTree()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            IRecordingTree subTree = tree.AddChild(HelperMethodToGetStandardRecordingResult());

            Assert.AreSame(tree, subTree.Parent, "Parent of subtree should be the original tree");
        }

        [Test]
        public void TestGivenRecordingTreeWhenChildAddedThenCorrectValueSet()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            IRecordingResultWithDepth recording = HelperMethodToGetStandardRecordingResult();

            IRecordingTree subtree = tree.AddChild(recording);
            Assert.AreSame(recording, subtree.Value, "Value in returned subtree does not match value added");
        }

        [Test]
        public void TestGivenRecordingTreeWhenFindingResultsThenCorrectResultFound()
        {
            IRecordingTree tree = new RecordingTreeImpl();

            IRecordingResultWithDepth recordingToMatch = HelperMethodToGetStandardRecordingResult();
            tree.AddChild(recordingToMatch);

            IRecordingTree result = tree.Find(r => r.MethodName == recordingToMatch.MethodName);
            Assert.NotNull(result, "Result should not be null, a result was expected when using a matching function");
            Assert.AreEqual(recordingToMatch.MethodName, result.Value.MethodName,
                "Method name in result should match name being searched for");

            result = tree.Find(recordingToMatch);
            Assert.NotNull(result, "Result should not be null, a result was expected when matching by class");
            Assert.AreEqual(recordingToMatch.Id, result.Value.Id,
                "Result ID should match recording data being searched for");
        }

        [Test]
        public void TestGivenRecordingTreeWhenFindingResultsThenResultIsFoundInSubtrees()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            IRecordingTree subtree = tree.AddChild(HelperMethodToGetStandardRecordingResult());

            string methodNameToSearch = "aaa";
            IRecordingResultWithDepth recordingToSearch = HelperMethodToGetRecordingResult("n1", "c1", methodNameToSearch);
            subtree.AddChild(HelperMethodToGetRecordingResult("n1", "c1", "m1"));
            subtree.AddChild(recordingToSearch);

            IRecordingTree result = tree.Find(r => r.MethodName == methodNameToSearch);
            Assert.NotNull(result, "Result should not be null, a result was expected when using a matching function");
            Assert.AreEqual(methodNameToSearch, result.Value.MethodName,
                "Method name in result should match name being searched for");

            result = tree.Find(recordingToSearch);
            Assert.NotNull(result, "Result should not be null, a result was expected when matching by class");
            Assert.AreEqual(recordingToSearch.Id, result.Value.Id,
                "Result ID should match recording data being searched for");
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

        [Test]
        public void TestGivenRecordingTreeWhenTreeFlattenedThenAllResultsIncludedAndNoDuplicates()
        {
            IRecordingTree tree = new RecordingTreeImpl();
            const int topLevelCount = 10;
            const int midLevelCount = 5;
            const int bottomLevelCount = 2;
            for (int i = 0; i < topLevelCount; i++)
            {
                string iId = "i" + i;
                IRecordingTree subTree = tree.AddChild(HelperMethodToGetRecordingResult("n" + iId, "c" + iId, "m" + iId));
                for (int j = 0; j < midLevelCount; j++)
                {
                    string jId = iId + "j" + j;
                    IRecordingTree subSubTree = subTree.AddChild(HelperMethodToGetRecordingResult("n" + jId, "c" + jId, "m" + jId));
                    for (int k = 0; k < bottomLevelCount; k++)
                    {
                        string kId = jId + "k" + k;
                        subSubTree.AddChild(HelperMethodToGetRecordingResult("n" + kId, "c" + kId, "m" + kId));
                    }
                }
            }

            Assert.AreEqual(topLevelCount, tree.ChildCount, "GIVEN: Child count of top tree should match expected");

            IEnumerable<IRecordingResult> flatResults = tree.Flatten();
            Assert.NotNull(flatResults, "Flat list should not be null");

            IList<IRecordingResult> flatResultList = flatResults.ToList();
            AssertNoDuplicatesInFlatList(flatResultList);

            int expectedNumber = topLevelCount + topLevelCount * midLevelCount + topLevelCount * midLevelCount * bottomLevelCount;
            Assert.AreEqual(expectedNumber, flatResultList.Count(),
                "Total number of entries in flat list should match expected");
        }

        [Test]
        public void TestGivenRecordingTreeWithTheSameMethodInMultiplePlacesWhenFlattenedThenDuplicatesFound()
        {
            IRecordingTree tree = HelperMethodToBuildMockTreeWithDuplicates();

            IEnumerable<IRecordingResult> flatRecordings = tree.Flatten();
            AssertDuplicatesInFlatList(flatRecordings);
        }

        [Test]
        public void TestGivenRecordingTreeWithTheSameMethodInMultiplePlacesWhenFlattenedAndCombinedThenNoDuplicatesFound()
        {
            IRecordingTree tree = HelperMethodToBuildMockTreeWithDuplicates();

            IEnumerable<IRecordingResult> flatRecordings = tree.FlattenAndCombine();
            AssertNoDuplicatesInFlatList(flatRecordings);
        }

        [Test]
        public void TestGivenRecordingTreeWithDataWhenResultDepthCheckedThenDepthInRecordingMatchesActual()
        {
            IRecordingTree tree = HelperMethodToGetCorrectlyBuiltMockTree();

            foreach (IRecordingTree child in tree.Children())
            {
                Assert.NotNull(child.Value, "Value should not be null in child nodes");
                Assert.AreEqual(0, child.Value.Depth, "Children of the root node should be at depth 0");
                foreach (IRecordingTree subChild in child.Children())
                {
                    Assert.NotNull(child.Value, "Value should not be null in sub-child nodes");
                    Assert.AreEqual(1, subChild.Value.Depth, "Children of the child node should be at depth 1");
                }
            }
        }

        private IRecordingTree HelperMethodToGetCorrectlyBuiltMockTree()
        {
            ActivePerformanceRecorderImpl recorder = new ActivePerformanceRecorderImpl();

            for (int i = 0; i < 1; i++)
            {
                MethodDefinitionImpl method = new MethodDefinitionImpl("n" + i, "c" + i, "m" + i);
                IRecordingTree tree = recorder.RegisterMethod(method);
                recorder.RecordMethodDuration(tree, 10);
                for (int j = 0; j < 1; j++)
                {
                    MethodDefinitionImpl subMethod = new MethodDefinitionImpl("nn" + j, "cc" + j, "mm" + j);
                    IRecordingTree subTree = recorder.RegisterMethod(subMethod, tree);
                    recorder.RecordMethodDuration(subTree, 10);
                }
            }

            return recorder.GetResults();
        }

        private IRecordingTree HelperMethodToBuildMockTreeWithDuplicates()
        {
            // MethodB is added twice, once under methodA and once under another method

            var methodA = HelperMethodToGetRecordingResult("n", "C", "A");
            var methodB = HelperMethodToGetRecordingResult("n", "C", "B");
            IRecordingTree tree = new RecordingTreeImpl();

            IRecordingTree subTree = tree.AddChild(HelperMethodToGetRecordingResult("n", "c", "01"));
            IRecordingTree subSubTree = subTree.AddChild(methodA);
            subSubTree.AddChild(methodB);
            subTree = tree.AddChild(HelperMethodToGetRecordingResult("n", "c", "02"));
            subTree.AddChild(methodB);

            return tree;
        }

        private void AssertNoDuplicatesInFlatList(IEnumerable<IRecordingResult> recordings)
        {
            var duplicates = HelperMethodToFindDuplicateIds(recordings);
            Assert.AreEqual(0, duplicates.Count,
                "Duplicates found in recordings: " + string.Join(", ", duplicates));
        }

        private void AssertDuplicatesInFlatList(IEnumerable<IRecordingResult> recordings)
        {
            var duplicates = HelperMethodToFindDuplicateIds(recordings);
            Assert.AreNotEqual(0, duplicates.Count,
                "Expected duplicates in recordings but none found");
        }

        private ISet<string> HelperMethodToFindDuplicateIds(IEnumerable<IRecordingResult> recordings)
        {
            ISet<string> foundMatches = new HashSet<string>();
            ISet<string> foundDuplicates = new HashSet<string>();
            foreach (IRecordingResult result in recordings)
            {
                string id = result.Id;
                if (foundMatches.Contains(id))
                {
                    foundDuplicates.Add(id);
                }
                else
                {
                    foundMatches.Add(id);
                }
            }
            return foundDuplicates;
        }

        private IRecordingResultWithDepth HelperMethodToGetStandardRecordingResult()
        {
            return new RecordingResultWithDepthImpl(new MethodDefinitionImpl("n", "c", "m"), 0);
        }

        private IRecordingResultWithDepth HelperMethodToGetRecordingResult(string namespaceName, string className, string method)
        {
            return new RecordingResultWithDepthImpl(new MethodDefinitionImpl(namespaceName, className, method), 0);
        }
    }
}