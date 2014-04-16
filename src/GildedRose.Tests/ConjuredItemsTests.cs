using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
  [TestFixture]
  public class ConjuredItemsTests : MyBaseTests
  {
    [SetUp]
    public void SetUp()
    {
      program = new Program();
      program.Items = new List<Item>();
    }

    //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality

    [Test]
    [TestCase(6, 4, "6-2 = 4")]
    [TestCase(2, 0, "2-2 = 0")]
    [TestCase(1, 0, "1-2 = 0 (min value)")]
    public void UpdateQuality_ShouldDecrease_Quality_TwiceAsFast_As_RegularItems(int initialQuality, int expectedQuality, string message)
    {
      program.Items.Add(new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = initialQuality });

      program.UpdateQuality();

      Assert.AreEqual(expectedQuality, program.Items.First().Quality, message);
    }

    [Test]
    [TestCase(2, 1, "2-1 = 1")]
    [TestCase(1, 0, "1-1 = 0")]
    [TestCase(0, -1, "0-1 = -1")]
    public void UpdateQuality_ShouldDecrease_SellIn_RegularPace(int initialSellin, int expectedResultSellIn, string message)
    {
      program.Items.Add(new Item { Name = "Conjured Mana Cake", SellIn = initialSellin, Quality = 20 });

      program.UpdateQuality();

      Assert.AreEqual(expectedResultSellIn, program.Items.First().SellIn, message);
    }


  }
}
