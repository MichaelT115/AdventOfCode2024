using System.Collections.Generic;
using NUnit.Framework;

namespace Day5_PrintQueue.Tests;

[TestFixture]
[TestOf(typeof(PrintQueuer))]
public class PrintQueuerTest
{
    private static readonly (int comesBefore, int comesAfter)[] ExampleRules =
    [
        (47, 53),
        (97, 13),
        (97, 61),
        (97, 47),
        (75, 29),
        (61, 13),
        (75, 53),
        (29, 13),
        (97, 29),
        (53, 29),
        (61, 53),
        (97, 53),
        (61, 29),
        (47, 13),
        (75, 47),
        (97, 75),
        (47, 61),
        (75, 61),
        (47, 29),
        (75, 13),
        (53, 13)
    ];

    public static IEnumerable<((int comesBefore, int comesAfter)[], int[])> InvalidTestCases()
    {
        yield return ([(1, 2)], [2, 1]);
        yield return (ExampleRules, [75, 97, 47, 61, 53]);
        yield return (ExampleRules, [61, 13, 29]);
        yield return (ExampleRules, [97, 13, 75, 29, 47]);
    }

    public static IEnumerable<((int comesBefore, int comesAfter)[], int[])> ValidTestCases()
    {
        yield return ([(1, 2)], [1, 2]);
        yield return (ExampleRules, [75, 47, 61, 53, 29]);
        yield return (ExampleRules, [97, 61, 53, 29, 13]);
        yield return (ExampleRules, [75, 29, 13]);
    }

    [TestCaseSource(nameof(InvalidTestCases))]
    public void
        Print_Queue_Is_Invalid_If_The_Second_Number_Of_A_Page_Number_Appears_Before_The_First_Number_Of_The_Page_Number(
            ((int comesBefore, int comesAfter)[] pageOrderingRules, int[] queue) testCase) =>
        Assert.That(new PrintQueuer(testCase.pageOrderingRules).IsValidQueue(testCase.queue), Is.False);

    [TestCaseSource(nameof(ValidTestCases))]
    public void
        Print_Queue_Is_Valid_If_Ordering_Is_Followed(
            ((int comesBefore, int comesAfter)[] pageOrderingRules, int[] queue) testCase) =>
        Assert.That(new PrintQueuer(testCase.pageOrderingRules).IsValidQueue(testCase.queue), Is.True);


    public static IEnumerable<((int comesBefore, int comesAfter)[], int[][], int expected)> ExampleTestCase()
    {
        yield return (ExampleRules, [
            [75, 47, 61, 53, 29],
            [97, 61, 53, 29, 13],
            [75, 29, 13],
            [75, 97, 47, 61, 53],
            [61, 13, 29],
            [97, 13, 75, 29, 47]
        ], 143);
    }

    [TestCaseSource(nameof(ExampleTestCase))]
    public void
        Print_Queue_Returns_Sum_Of_The_Pages_Numbers_Of_The_Page_In_The_Middle_Of_A_Queue(
            ((int comesBefore, int comesAfter)[] pageOrderingRules, int[][] queues, int expected) testCase) =>
        Assert.That(new PrintQueuer(testCase.pageOrderingRules).ProcessQueues(testCase.queues),
            Is.EqualTo(testCase.expected));

    public static IEnumerable<((int comesBefore, int comesAfter)[], int[] startQueue, int[] correctedQueue)>
        FixingTestCase()
    {
        yield return ([(1, 2)], [2, 1], [1, 2]);
        yield return ([(1, 2)], [2, 1, 3], [1, 2, 3]);
        yield return ([(1, 2)], [0, 2, 1, 3], [0, 1, 2, 3]);

        yield return ([(1, 2), (2, 3)], [3, 2, 1], [1, 2, 3]);
        yield return ([(1, 2), (1, 3)], [3, 2, 1], [1, 2, 3]);

        yield return (ExampleRules, [75, 97, 47, 61, 53], [97, 75, 47, 61, 53]);
        yield return (ExampleRules, [61, 13, 29], [61, 29, 13]);
        yield return (ExampleRules, [97, 13, 75, 29, 47], [97, 75, 47, 29, 13]);
    }

    [TestCaseSource(nameof(FixingTestCase))]
    public void Invalid_Queues_Are_Corrected(
        ((int comesBefore, int comesAfter)[] pageOrderingRules, int[] startingQueue, int[] correctedQueue) testCase)
    {
        new PrintQueuer(testCase.pageOrderingRules).TryFixQueue(testCase.startingQueue);
        Assert.That(testCase.startingQueue, Is.EqualTo(testCase.correctedQueue));
    }
}