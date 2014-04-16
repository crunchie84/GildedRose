using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
  [TestFixture]
  public class BackStagePassTests : MyBaseTests
  {
    [SetUp]
    public void SetUp()
    {
      program = new Program();
      program.Items = new List<Item>();
    }

     /**
      * "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; 
      * Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or 
      * less but Quality drops to 0 after the concert.
      */

    [Test]
    [TestCase(40, 20, 21, "> 10 days increases quality by 1")]
    [TestCase(10, 20, 22, "10 days or less increases quality by 2")]
    [TestCase(9, 20, 22, "10 days or less increases quality by 2")]
    [TestCase(5, 20, 23, "5 days or less increases quality by 3")]
    [TestCase(4, 20, 23, "5 days or less increases quality by 3")]
    [TestCase(4, 48, 50, "5 days or less increases quality by 3 but max out at 50")]
    [TestCase(0, 48, 0, "sellIn passed thus quality goes to 0")]
    public void BackStagePass_SellInApproaches_ShouldIncrease_Quality(int sellIn, int initialQuality, int expectedResultQuality, string message)
    {
      program.Items.Add(new RegularItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = initialQuality });

      program.UpdateQuality();

      Assert.AreEqual(expectedResultQuality, program.Items.First().Quality, message);
    }

  }
}
