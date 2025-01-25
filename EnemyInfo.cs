namespace RpgMakerVXAceEventSearcher
{
    internal struct EnemyInfo(int id,string name, List<Item> dropItems)
    {
        public int Id = id;
        public string Name = name;
        public List<Item> DropItems = dropItems;
    }
}
