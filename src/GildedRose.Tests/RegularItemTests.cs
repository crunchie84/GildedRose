using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
  /*
  - 
  - All items have a Quality value which denotes how valuable the item is
  - At the end of each day our system lowers both values for every item

  Pretty simple, right? Well this is where it gets interesting:

  - Once the sell by date has passed, Quality degrades twice as fast
  - The Quality of an item is never negative
  - "Aged Brie" actually increases in Quality the older it gets
  - The Quality of an item is never more than 50
  - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
  - "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
   * 
   */
  [TestFixture]
  public class RegularItemTests : MyBaseTests
  {
    [SetUp]
    public void SetUp()
    {
      program = new Program();
      program.Items = new List<Item>();
    }

    [Test(Description = "At the end of each day our system lowers both values for every item")]
    public void UpdateQuality_ShouldDecrease_Quality()
    {
        program.Items.Add(new Item { Name = "+5 Dexterity Vest 1", SellIn = 10, Quality = 20 });

        program.UpdateQuality();

        Assert.AreEqual(19, program.Items.First().Quality);
    }

    [Test(Description = "At the end of each day our system lowers both values for every item")]
    public void UpdateQuality_ShouldDecrease_SellIn()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest 1", SellIn = 10, Quality = 20 });

      program.UpdateQuality();

      Assert.AreEqual(9, program.Items.First().SellIn);
    }

    [Test]
    public void UpdateQuality_MultipleItems_ShouldAll_DecreaseQuality()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest 1", SellIn = 10, Quality = 20 });
      program.Items.Add(new Item { Name = "+5 Dexterity Vest 2", SellIn = 10, Quality = 20 });

      program.UpdateQuality();
      Assert.AreEqual(19, program.Items.First().Quality);
      Assert.AreEqual(19, program.Items.Last().Quality);
    }

    [Test]
    public void UpdateQuality_MultipleItems_ShouldAll_DecreseSellIn()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest 1", SellIn = 10, Quality = 20 });
      program.Items.Add(new Item { Name = "+5 Dexterity Vest 2", SellIn = 10, Quality = 20 });

      program.UpdateQuality();

      Assert.AreEqual(9, program.Items.First().SellIn);
      Assert.AreEqual(9, program.Items.Last().SellIn);
    }

    [Test(Description = "The Quality of an item is never negative")]
    public void Quality_Becomes_NeverNegative()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 1 });

      program.UpdateQuality();
      program.UpdateQuality();

      var item = program.Items.First();
      Assert.GreaterOrEqual(MIN_QUALITY_VALUE, item.Quality);
    }

    [Test(Description = "ASSERTION - The Sellin of an item is never negative")]
    public void SellIn_Becomes_NeverNegative()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest", SellIn = 1, Quality = 10 });
      program.UpdateQuality();//sellin = 0 
      program.UpdateQuality();// sellin y u no -1?

      var item = program.Items.First();
      Assert.GreaterOrEqual(MIN_SELLIN_VALUE, item.SellIn);
    }

    [Test(Description = "Once the sell by date has passed, Quality degrades twice as fast")]
    public void ExpirationPassed_QualityDegradesTwiceAsFast()
    {
      program.Items.Add(new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 });
      program.UpdateQuality();
      Assert.AreEqual(18, program.Items.Last().Quality);
    }
  }
}