using System;
using System.Collections.Generic;
using NUnit.Framework;
using PerformanceRecorder.Formatter.Helper;

namespace PerformanceRecorderTest.Formatter.Helper
{
    public class StringExtensionTest
    {
        private static IEnumerable<TestCaseData> StringAlignmentTestProvider()
        {
            yield return new TestCaseData("a | b\r\naa | b", "|", "a   b\r\naa  b");
            yield return new TestCaseData("a | b\r\naa | b\r\naaa | b", "|", "a    b\r\naa   b\r\naaa  b");
        }
        
        [Test]
        [TestCaseSource("StringAlignmentTestProvider")]
        public void GIVEN_InputStringWithMarkers_WHEN_AlignedToMarkers_THEN_OutputStringMatchesExpected(
            string input, string marker, string expected)
        {
            // Act
            string alignedString = input.AlignStringsToMarker(marker);
            Console.Write(alignedString);

            // Assert
            Assert.AreEqual(expected, alignedString, "Aligned string should match expected output");
        }
    }
}