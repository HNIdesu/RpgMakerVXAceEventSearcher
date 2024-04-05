
namespace RpgMakerVXAceEventSearcher
{
    internal enum EnumItemType { Item = 0, Weapon=1, Arctor=6 , Event=4, Variable=3, Switch=5 }
    internal struct Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumItemType ItemType { get; set; }
        public override string ToString()
        {
            return $"id:{Id} Name:{Name}";
        }
    }
}
