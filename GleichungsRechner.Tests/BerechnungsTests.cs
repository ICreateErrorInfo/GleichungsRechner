using System.Collections.Generic;

using NUnit.Framework;

namespace GleichungsRechner.Tests {

    [TestFixture]
    class BerechnungsTests {

        public static IEnumerable<TestCaseData> TestCases() {
            yield return new TestCaseData {Input = "1+2*4", Expected   = 9};
            yield return new TestCaseData {Input = "1+2*4^2", Expected = 33};
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void Test(TestCaseData tcd) {
            var result = Berechne.Term(tcd.Input);
            Assert.That(result, Is.EqualTo(tcd.Expected));
        }

        public class TestCaseData {

            public string Input    { get; set; }
            public double Expected { get; set; }

            public override string ToString() => $"{Expected} = {Input}";

        }

    }

}