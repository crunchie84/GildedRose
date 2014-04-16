using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
  [TestFixture]
  public class AgedBrieTests : MyBaseTests
  {
    [SetUp]
    public void SetUp()
    {
      program = new Program();
      program.Items = new List<Item>();
    }

    /**
     *   - "Aged Brie" actually increases in Quality the older it gets
     *   - The Quality of an item is never more than 50
     */
    
    [Test(Description="'Aged Brie' actually increases in Quality the older it gets")]
    public void UpdateQuality_ShouldIncrease_Quality_AgedBrieItem()
    {
      program.Items.Add(new Item { Name = "Aged Brie", SellIn = 2, Quality = MIN_QUALITY_VALUE});

      program.UpdateQuality();

      Assert.AreEqual(1, program.Items.First().Quality);
    }

    [Test(Description = "ASSERT - 'Aged Brie' also decreases sellIn")]
    public void UpdateQuality_ShouldDecrease_SellIn_AgedBrieItem()
    {
      program.Items.Add(new Item { Name = "Aged Brie", SellIn = 2, Quality = MIN_QUALITY_VALUE });

      program.UpdateQuality();

      Assert.AreEqual(1, program.Items.First().Quality);
    }

    [Test(Description="ASSERT - after sellin has passed quality keeps increasing")]
    public void UpdateQuality_SellIn_Passed_ShouldStill_increaseQuality_TwiceAsFast()
    {
      program.Items.Add(new Item { Name = "Aged Brie", SellIn = 1, Quality = MIN_QUALITY_VALUE });

      program.UpdateQuality(); // sellin 0, quality -> 1 
      program.UpdateQuality(); // sellin stays 0, quality -> 2?

      Assert.AreEqual(3, program.Items.First().Quality);
    }

    [Test(Description = "The Quality of an item is never more than 50")]
    public void UpdateQuality_ShouldNot_Exceed_50()
    {
      program.Items.Add(new Item { Name = "Aged Brie", SellIn = 2, Quality = MAX_QUALITY_VALUE });

      program.UpdateQuality();

      Assert.AreEqual(MAX_QUALITY_VALUE, program.Items.First().Quality);
      Assert.AreEqual(1, program.Items.First().SellIn);
    }
  }
}
