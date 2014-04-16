using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
  public class Program
  {
    public IList<Item> Items;
    static void Main(string[] args)
    {
      System.Console.WriteLine("OMGHAI!");

      var app = new Program()
                    {
                      Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item{Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }
                    };

      app.UpdateQuality();

      System.Console.ReadKey();

    }

    public void UpdateQuality()
    {
      //fixup borked items by making them regular items
      Items = Items
        .Where(item => item.GetType() == typeof (Item))
        .Select(item =>
        {
          switch (item.Name)
          {
            case "Aged Brie":
              return new AgedItem {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn} as Item;
            case "Sulfuras, Hand of Ragnaros":
              return new LegendaryItem {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn} as Item;
            case "Backstage passes to a TAFKAL80ETC concert":
              return new FixedDateItem { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn } as Item;
            case "Conjured Mana Cake":
              return new ConjuredItem { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn } as Item;
            default:
              return new RegularItem { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn } as Item;
          }
        }).ToList();

      //now let all items calculate their value
      Items = Items.OfType<CalculateAbleItemWrapper>()
        .Select(item =>
      {
        item.CalculateNextDayValues();
        return item;
      }).Cast<Item>().ToList();
    }

    private void UpdateQualityForItem(RegularItem regularItem)
    {
      UpdateItemQuality(regularItem);

      DecreaseSellIn(regularItem);

      AdjustQuality_SellIn_ZeroOrLess(regularItem);

    }

    private static void UpdateItemQuality(RegularItem regularItem)
    {
      if (regularItem.Name != "Aged Brie" && regularItem.Name != "Backstage passes to a TAFKAL80ETC concert")
        UpdateReqularItemQuality(regularItem);
      else
        UpdateIrregularItemQuality(regularItem);
    }

    private static void AdjustQuality_SellIn_ZeroOrLess(RegularItem regularItem)
    {
      if (regularItem.SellIn >= 0) return;

      if (regularItem.Name == "Aged Brie")
      {
        if (regularItem.Quality < 50)
        {
          regularItem.Quality = regularItem.Quality + 1;
        }
      }
      else if (regularItem.Name == "Backstage passes to a TAFKAL80ETC concert")
      {
        regularItem.Quality = regularItem.Quality - regularItem.Quality;
      }
      else if (regularItem.Quality > 0 && regularItem.Name != "Sulfuras, Hand of Ragnaros")
      {
        regularItem.Quality = regularItem.Quality - 1;
      }
    }

    private static void DecreaseSellIn(RegularItem regularItem)
    {
      if (regularItem.Name != "Sulfuras, Hand of Ragnaros")
      {
        regularItem.SellIn = regularItem.SellIn - 1;
      }
    }

    private static void UpdateIrregularItemQuality(RegularItem regularItem)
    {
      if (regularItem.Quality < 50)
      {
        regularItem.Quality = regularItem.Quality + 1;

        if (regularItem.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
          if (regularItem.SellIn < 11)
          {
            if (regularItem.Quality < 50)
            {
              regularItem.Quality = regularItem.Quality + 1;
            }
          }

          if (regularItem.SellIn < 6)
          {
            if (regularItem.Quality < 50)
            {
              regularItem.Quality = regularItem.Quality + 1;
            }
          }
        }
      }
    }

    private static void UpdateReqularItemQuality(RegularItem regularItem)
    {
      if (regularItem.Quality > 0)
      {
        if (regularItem.Name == "Sulfuras, Hand of Ragnaros")
          return;

        if (regularItem.Name == "Conjured Mana Cake")
          regularItem.Quality = Math.Max(0, regularItem.Quality - 2);
        else
        {
          regularItem.Quality = regularItem.Quality - 1;
        }
      }
    }
  }
}
