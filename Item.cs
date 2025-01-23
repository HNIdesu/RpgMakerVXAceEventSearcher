
namespace RpgMakerVXAceEventSearcher
{
    internal enum EnumItemType { Item = 0, Weapon=1, Armor = 2 , Variable=3, Event = 4, Switch=5, Actor = 6 ,Enemy=7 }
    internal class Item(int id,string name,EnumItemType enumItemType)
    {
        public int Id { get;private set; } = id;
        public string Name { get;private set; } = name;
        public EnumItemType ItemType { get;private set; } = enumItemType;
        public override string ToString()=> $"id:{Id} Name:{Name}";
    }
}
