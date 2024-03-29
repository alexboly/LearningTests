﻿using System;
using System.Collections;
using NUnit.Framework;
using System.Text;

namespace LearningTests
{
    [TestFixture]
    public class StringTests
    {
        private const sbyte asciiA = 65;
        private const sbyte asciiB = 66;
        private const sbyte asciiC = 67;
        private const sbyte asciiD = 68;
        private const sbyte asciiE = 69;
        private const sbyte endOfString = 0;
        private const sbyte zero = 0;
        private sbyte[] asciiBytes;

        [SetUp]
        public void Setup()
        {
            asciiBytes = new[] { asciiA, asciiB, asciiC, asciiD, asciiE, endOfString };
        }


        [Test]
        public void EmptyString()
        {
            const string s = "";

            Assert.AreEqual("", s);
        }

        [Test]
        public void ConstructFromRepeatedCharacter()
        {
            String s = new String('a', 10);

            Assert.AreEqual("aaaaaaaaaa", s);
        }

        [Test]
        public void ConstructFromCharArray()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e' };
            String s = new String(characters);

            Assert.AreEqual("abcde", s);
        }

        [Test]
        public void ConstructFromPartsOfCharArray()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e' };
            String s = new String(characters, 2, 2);

            Assert.AreEqual("cd", s);
        }

        [Test]
        public unsafe void ConstructFromCharPointer()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e', '\0' };

            fixed (char* pchar = &characters[0])
            {
                String s = new String(pchar);

                Assert.AreEqual("abcde", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfCharPointer()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e', '\0' };

            fixed (char* pchar = &characters[0])
            {
                String s = new String(pchar, 2, 2);

                Assert.AreEqual("cd", s);
            }
        }

        [Test]
        public unsafe void ConstructFromSbytePointer()
        {
            fixed(sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte);

                Assert.AreEqual("ABCDE", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfSbytePointer()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfSbytePointerWithASCIIEncoding()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2, Encoding.ASCII);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfSbytePointerWithUtf8Encoding()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2, Encoding.UTF8);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfSbytePointerWithUnicodeEncoding()
        {
            sbyte[] unicodeBytes = new[] { asciiA, zero, asciiB, zero, asciiC, zero, asciiD, zero, asciiE, zero, zero, zero };

            fixed (sbyte* pbyte = &unicodeBytes[0])
            {
                String s = new string(pbyte, 4, 4, Encoding.Unicode);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void ConstructFromPartOfSbytePointerWithBigEndianUnicodeEncoding()
        {
            sbyte[] unicodeBytes = new[] { zero, asciiA, zero, asciiB, zero, asciiC, zero, asciiD, zero, asciiE, zero, zero };

            fixed (sbyte* pbyte = &unicodeBytes[0])
            {
                String s = new string(pbyte, 4, 4, Encoding.BigEndianUnicode);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public void IndexerReturnsTheRightValue()
        {
            const string s = "goosfraba";

            Assert.AreEqual('a', s[6]);
        }

        [Test]
        [ExpectedException( ExpectedException = typeof(IndexOutOfRangeException), ExpectedMessage = "Index was outside the bounds of the array.")]
        public void IndexerThrowsExceptionWhenOutOfLowerBound()
        {
            const string s = "goosfraba";

            char shouldThrowException = s[-1];
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(IndexOutOfRangeException), ExpectedMessage = "Index was outside the bounds of the array.")]
        public void IndexerThrowsExceptionWhenOutOfUpperBound()
        {
            const string s = "goosfraba";

            char shouldThrowException = s[100];
        }

        [Test]
        public void ClonesTheString()
        {
            const string s = "goosfraba";

            string clone = s.Clone() as string;

            Assert.AreSame(s, clone);
        }

        [Test]
        public void CompareWithPrevious()
        {
            const string s = "goosfraba";

            int comparison = s.CompareTo("a");

            Assert.AreEqual(1, comparison);
        }

        [Test]
        public void CompareWithNext()
        {
            const string s = "goosfraba";

            int comparison = s.CompareTo("goosfrabb");

            Assert.AreEqual(-1, comparison);
        }
        
        [Test]
        public void CompareWithEqual()
        {
            const string s = "goosfraba";

            int comparison = s.CompareTo("goosfraba");

            Assert.AreEqual(0, comparison);
        }

        [Test]
        public void ContainsString()
        {
            const string s = "goosfraba";

            bool contains = s.Contains("oosfr");

            Assert.IsTrue(contains);
        }
        
        [Test]
        public void DoesntContainString()
        {
            const string s = "goosfraba";

            bool contains = s.Contains("oosfs");

            Assert.IsFalse(contains);
        }

        [Test]
        public void CopiesTheRightCharacters()
        {
            const string s = "goosfraba";
            char[] destination = new[]{'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a'};
            char[] expected = new[] { 'a', 'a', 'o', 's', 'a', 'a', 'a', 'a' };

            s.CopyTo(2, destination, 2, 2);

            Assert.AreEqual(expected, destination);
        }

        [Test]
        public void EndsWith()
        {
            const string s = "goosfraba";

            bool endsWithaba = s.EndsWith("aba");

            Assert.IsTrue(endsWithaba);
        }
        
        [Test]
        public void DoesntEndWith()
        {
            const string s = "goosfraba";

            bool doesntEndsWithfrabb = s.EndsWith("frabb");

            Assert.IsFalse(doesntEndsWithfrabb);
        }

        [Test]
        [TestCase(StringComparison.CurrentCulture)]
        [TestCase(StringComparison.CurrentCultureIgnoreCase)]
        [TestCase(StringComparison.InvariantCulture)]
        [TestCase(StringComparison.InvariantCultureIgnoreCase)]
        [TestCase(StringComparison.Ordinal)]
        [TestCase(StringComparison.OrdinalIgnoreCase)]
        public void EqualsWithItself(StringComparison comparisonType)
        {
            const string s = "goosfrab";

            bool equalsWithItself = s.Equals(s, comparisonType);

            Assert.IsTrue(equalsWithItself);
        }
    
        [Test]
        [TestCase(StringComparison.CurrentCulture)]
        [TestCase(StringComparison.CurrentCultureIgnoreCase)]
        [TestCase(StringComparison.InvariantCulture)]
        [TestCase(StringComparison.InvariantCultureIgnoreCase)]
        [TestCase(StringComparison.Ordinal)]
        [TestCase(StringComparison.OrdinalIgnoreCase)]
        public void EqualsWithSameValueDifferentInstance(StringComparison comparisonType)
        {
            const string s = "goosfraba";
            const string another = "goosfraba";

            bool equalsWithAnother = s.Equals(another, comparisonType);

            Assert.IsTrue(equalsWithAnother);
        }
        
        [Test]
        [TestCase(StringComparison.CurrentCulture)]
        [TestCase(StringComparison.CurrentCultureIgnoreCase)]
        [TestCase(StringComparison.InvariantCulture)]
        [TestCase(StringComparison.InvariantCultureIgnoreCase)]
        [TestCase(StringComparison.Ordinal)]
        [TestCase(StringComparison.OrdinalIgnoreCase)]
        public void DoesntEqualWithDifferent(StringComparison comparisonType)
        {
            const string s = "goosfraba";
            const string another = "goosfrabb";

            bool equalsWithDifferent = s.Equals(another, comparisonType);

            Assert.IsFalse(equalsWithDifferent);
        }

        [Test]
        [TestCase(StringComparison.CurrentCultureIgnoreCase)]
        [TestCase(StringComparison.InvariantCultureIgnoreCase)]
        [TestCase(StringComparison.OrdinalIgnoreCase)]
        public void EqualsWithSimilarIgnoreCase(StringComparison comparisonType)
        {
            const string s = "goosfraba";
            const string another = "gOOsfrABa";

            bool equalsWithSimilar = s.Equals(another, comparisonType);

            Assert.IsTrue(equalsWithSimilar);
        }

        [Test]
        [TestCase(StringComparison.CurrentCulture)]
        [TestCase(StringComparison.InvariantCulture)]
        [TestCase(StringComparison.Ordinal)]
        public void DifferentFromSimilarDontIgnoreCase(StringComparison comparisonType)
        {
            const string s = "goosfraba";
            const string another = "gOOsfrABa";

            bool equalsWithSimilar = s.Equals(another, comparisonType);

            Assert.IsFalse(equalsWithSimilar);
        }

        [Test]
        public void ReturnsEnumerator()
        {
            const string s = "goosfraba";

            IEnumerator enumerator = s.GetEnumerator();
            enumerator.MoveNext();

            Assert.AreEqual('g', enumerator.Current);
        }

        [Test]
        public void HashCode()
        {
            const string s = "abcde";
            const int valueReturnedByTheInitialRunOn32BitsNoIdeaHowToComputeIt = 398757997;

            int hash = s.GetHashCode();

            Assert.AreEqual(valueReturnedByTheInitialRunOn32BitsNoIdeaHowToComputeIt, hash);
        }

        [Test]
        public void TypeIsString()
        {
            const string s = "goosfraba";

            Type t = s.GetType();

            Assert.AreEqual(typeof(string), t);
        }

        [Test]
        public void TypeCodeIsString()
        {
            const string s = "goosfraba";

            TypeCode typeCode = s.GetTypeCode();

            Assert.AreEqual(TypeCode.String, typeCode);
        }

        [Test]
        public void IndexOf()
        {
            const string s = "goosfraba";

            int indexOf = s.IndexOf('o');

            Assert.AreEqual(1, indexOf);
        }
    }
}
