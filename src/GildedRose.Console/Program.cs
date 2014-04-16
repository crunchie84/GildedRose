using System.Collections.Generic;

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
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }
                    };

      app.UpdateQuality();

      System.Console.ReadKey();

    }

    public void UpdateQuality()
    {
      for (var i = 0; i < Items.Count; i++)
      {
        var item = Items[i];

        UpdateQualityForItem(item);
      }
    }

    private void UpdateQualityForItem(Item item)
    {
      if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
        UpdateReqularItemQuality(item);
      else
        UpdateIrregularItemQuality(item);


      DecreaseSellIn(item);

      AdjustQuality_SellIn_ZeroOrLess(item);

    }

    private static void AdjustQuality_SellIn_ZeroOrLess(Item item)
    {
      if (item.SellIn >= 0) return;

      if (item.Name == "Aged Brie")
      {
        if (item.Quality < 50)
        {
          item.Quality = item.Quality + 1;
        }
      }
      else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
      {
        item.Quality = item.Quality - item.Quality;
      }
      else if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
      {
        item.Quality = item.Quality - 1;
      }
    }

    private static void DecreaseSellIn(Item item)
    {
      if (item.Name != "Sulfuras, Hand of Ragnaros")
      {
        item.SellIn = item.SellIn - 1;
      }
    }

    private static void UpdateIrregularItemQuality(Item item)
    {
      if (item.Quality < 50)
      {
        item.Quality = item.Quality + 1;

        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
          if (item.SellIn < 11)
          {
            if (item.Quality < 50)
            {
              item.Quality = item.Quality + 1;
            }
          }

          if (item.SellIn < 6)
          {
            if (item.Quality < 50)
            {
              item.Quality = item.Quality + 1;
            }
          }
        }
      }
    }

    private static void UpdateReqularItemQuality(Item item)
    {
      if (item.Quality > 0)
      {
        if (item.Name != "Sulfuras, Hand of Ragnaros")
        {
          item.Quality = item.Quality - 1;
        }
      }
    }
  }

  public class Item
  {
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
  }

}
