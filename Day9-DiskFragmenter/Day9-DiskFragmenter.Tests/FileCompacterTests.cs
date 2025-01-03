﻿using System;
using NUnit.Framework;

namespace Day9_DiskFragmenter.Tests;

using MemorySpan = (int fileId, int length);

[TestFixture]
[TestOf(typeof(FileCompacter))]
public class FileCompacterTests
{
    private static object[] _compactingTestCases =
    [
        new object[] { "", Array.Empty<(int, int)>() },
        new object[] { "1", new MemorySpan[] { (0, 1) } },
        new object[] { "11", new MemorySpan[] { (0, 1) } },
        new object[] { "111", new MemorySpan[] { (0, 1), (1, 1) } },
        new object[] { "1111", new MemorySpan[] { (0, 1), (1, 1) } },
        new object[] { "11111", new MemorySpan[] { (0, 1), (2, 1), (1, 1) } },
        new object[] { "111111", new MemorySpan[] { (0, 1), (2, 1), (1, 1) } },
        new object[] { "1111111", new MemorySpan[] { (0, 1), (3, 1), (1, 1), (2, 1) } },

        new object[] { "22101", new MemorySpan[] { (0, 2), (2, 1), (1, 1) } },

        new object[]
        {
            "2333133121414131402", new MemorySpan[]
            {
                (0, 2), (9, 2), (8, 1), (1, 3), (8, 3), (2, 1), (7, 3), (3, 3), (6, 1), (4, 2), (6, 1), (5, 4), (6, 2)
            }
        }
    ];

    [Test]
    [TestCaseSource(nameof(_compactingTestCases))]
    public void CompactingTests(string input, MemorySpan[] expectedMemorySpans)
    {
        Assert.That(FileCompacter.CompactFiles(input), Is.EqualTo(expectedMemorySpans));
    }

    private static object[] _compactingTestCases2 =
    [
        new object[] { "", Array.Empty<(int, int)>() },
        new object[] { "1", new MemorySpan[] { (0, 1) } },
        new object[] { "11", new MemorySpan[] { (0, 1), (0, 1) } },
        new object[] { "111", new MemorySpan[] { (0, 1), (1, 1), (0, 1) } },
        new object[] { "1111", new MemorySpan[] { (0, 1), (1, 1), (0, 2) } },
        new object[] { "11111", new MemorySpan[] { (0, 1), (2, 1), (1, 1), (0, 2) } },
        new object[] { "111111", new MemorySpan[] { (0, 1), (2, 1), (1, 1), (0, 3) } },
        new object[] { "1111111", new MemorySpan[] { (0, 1), (3, 1), (1, 1), (2, 1), (0, 3) } },

        new object[] { "22101", new MemorySpan[] { (0, 2), (2, 1), (1, 1), (0, 2) } },

        new object[]
        {
            "2333133121414131402", new MemorySpan[]
            {
                (0, 2), (9, 2), (2, 1), (1, 3), (7, 3), (0, 1), (4, 2), (0, 1), (3, 3), (0, 4), (5, 4), (0, 1), (6, 4),
                (0, 5), (8, 4), (0, 2)
            }
        }
    ];

    [Test]
    [TestCaseSource(nameof(_compactingTestCases2))]
    public void CompactingTestsPart2(string input, MemorySpan[] expectedMemorySpans)
    {
        Assert.That(FileCompacter.CompactFiles2(input), Is.EqualTo(expectedMemorySpans));
    }
}