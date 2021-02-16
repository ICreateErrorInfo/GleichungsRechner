using System.Collections.Generic;

using NUnit.Framework;

namespace GleichungsRechner.Tests {

    [TestFixture]
    class BerechnungsTests {

        public static IEnumerable<TestCaseData> TestCases() {
            yield return new TestCaseData {Input = "1+2*4", Expected   = 9};
            yield return new TestCaseData {Input = "1+2*4^2", Expected = 33};
            yield return new TestCaseData {Input = "12*2+34/2*3", Expected = 75};
            yield return new TestCaseData {Input = "30-12*10^2", Expected = -1170};
            yield return new TestCaseData {Input = "24^2+86^3", Expected = 636632};
            yield return new TestCaseData {Input = "10+5-5", Expected = 10};
            yield return new TestCaseData {Input = "10-5+5", Expected = 10};
            yield return new TestCaseData {Input = "10-42+8^3*98/7", Expected = 7136};
            yield return new TestCaseData {Input = "12*(2+4*2)+2", Expected = 122};
            yield return new TestCaseData {Input = "-12*(2-4*2)+2", Expected = 74};
        }

        [Test, TestCaseSource(nameof(TestCases))]
        public void Test(TestCaseData tcd) {
            var result = Berechne.Term(tcd.Input);
            Assert.That(result, Is.EqualTo(tcd.Expected).Within(0.00001));
        }

        public class TestCaseData {

            public string Input    { get; set; }
            public double Expected { get; set; }

            public override string ToString() => $"{Expected} = {Input}";

        }

    }

}