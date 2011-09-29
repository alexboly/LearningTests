using System;
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
        public void StringConstructorWithRepeatedCharacter()
        {
            String s = new String('a', 10);

            Assert.AreEqual("aaaaaaaaaa", s);
        }

        [Test]
        public void StringConstructorWithCharArray()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e' };
            String s = new String(characters);

            Assert.AreEqual("abcde", s);
        }

        [Test]
        public void StringConstructorWithPartsOfCharArray()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e' };
            String s = new String(characters, 2, 2);

            Assert.AreEqual("cd", s);
        }

        [Test]
        public unsafe void StringConstructorFromCharPointer()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e', '\0' };

            fixed (char* pchar = &characters[0])
            {
                String s = new String(pchar);

                Assert.AreEqual("abcde", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfCharPointer()
        {
            char[] characters = new[] { 'a', 'b', 'c', 'd', 'e', '\0' };

            fixed (char* pchar = &characters[0])
            {
                String s = new String(pchar, 2, 2);

                Assert.AreEqual("cd", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromSbytePointer()
        {
            fixed(sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte);

                Assert.AreEqual("ABCDE", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfSbytePointer()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfSbytePointerWithASCIIEncoding()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2, Encoding.ASCII);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfSbytePointerWithUtf8Encoding()
        {
            fixed (sbyte* pbyte = &asciiBytes[0])
            {
                String s = new string(pbyte, 2, 2, Encoding.UTF8);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfSbytePointerWithUnicodeEncoding()
        {
            sbyte[] unicodeBytes = new[] { asciiA, zero, asciiB, zero, asciiC, zero, asciiD, zero, asciiE, zero, zero, zero };

            fixed (sbyte* pbyte = &unicodeBytes[0])
            {
                String s = new string(pbyte, 4, 4, Encoding.Unicode);

                Assert.AreEqual("CD", s);
            }
        }

        [Test]
        public unsafe void StringConstructorFromPartOfSbytePointerWithBigEndianUnicodeEncoding()
        {
            sbyte[] unicodeBytes = new[] { zero, asciiA, zero, asciiB, zero, asciiC, zero, asciiD, zero, asciiE, zero, zero };

            fixed (sbyte* pbyte = &unicodeBytes[0])
            {
                String s = new string(pbyte, 4, 4, Encoding.BigEndianUnicode);

                Assert.AreEqual("CD", s);
            }
        }
    }
}
