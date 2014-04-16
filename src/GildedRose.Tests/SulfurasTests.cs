using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
  [TestFixture]
  public class SulfurasTests : MyBaseTests
  {
    [SetUp]
    public void SetUp()
    {
      program = new Program();
      program.Items = new List<Item>();
    }

    //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality

    [Test]
    public void UpdateQuality_ShouldNever_Decrease_SellIn()
    {
      program.Items.Add(new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80});

      program.UpdateQuality();

      Assert.AreEqual(MIN_SELLIN_VALUE, program.Items.First().SellIn);
    }

    [Test]
    public void UpdateQuality_ShouldNever_Decrease_Quality()
    {
      program.Items.Add(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 });

      program.UpdateQuality();

      Assert.AreEqual(80, program.Items.First().Quality);
    }
  }
}
