namespace GildedRose.Console
{
  public class Item
  {
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }
  }

  public abstract class CalculateAbleItemWrapper : Item
  {
    public abstract void CalculateNextDayValues();
  }
}