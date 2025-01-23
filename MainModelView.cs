using RubyMarshal;
using RubyMarshal.Types;

namespace RpgMakerVXAceEventSearcher
{
    internal class MainModelView
    {
        private readonly List<Item> _ItemList = [];
        public List<Item> ItemList { get; private set; } = [];
        public List<MapInfo> MapList { get; private set; } = [];
        public List<Utility.SearchCommandResult> SearchResultList { get; private set; } = [];

        public HashSet<EnumItemType> CheckedItemTypes { get; private set; } = [];
        public string SearchQuery { get; set; } = "";
        public void ApplyFilter()
        {
            ItemList.Clear();
            if (string.IsNullOrEmpty(SearchQuery) && CheckedItemTypes.Count == 0)
                ItemList.AddRange(_ItemList);
            else
                ItemList.AddRange(_ItemList.Where(item => 
                    CheckedItemTypes.Contains(item.ItemType) &&
                    item.Name.Contains(SearchQuery)
                ));
            
        }
        public void SearchReferences(Item item)
        {
            SearchResultList.Clear();
            IEnumerable<Utility.SearchCommandResult>? searchResult = null;
            switch (item.ItemType)
            {
                case EnumItemType.Item:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                            cmd.Code == 126
                            && cmd.Parameters[0] is Fixnum
                            && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id);
                        break;
                    }

                case EnumItemType.Weapon:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 127
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }

                case EnumItemType.Actor:
                    break;
                case EnumItemType.Event:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 117
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Variable:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 122
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Switch:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 121
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Armor:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 128
                                && cmd.Parameters.Count > 0
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                        
                    }
                case EnumItemType.Troop:
                    {
                        searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 301//战斗处理
                                && cmd.Parameters.Count > 1
                                && cmd.Parameters[1] is Fixnum
                                && cmd.Parameters[1].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                default:
                    break;
            }
            if (searchResult == null) return;
            SearchResultList.AddRange(searchResult);
        }
        public void LoadItemList(string gameDirectory)
        {
            _ItemList.Clear();
            foreach (string filename in Directory.EnumerateFiles(gameDirectory, "*.rvdata2"))
            {
                if (filename.EndsWith("Actors.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var actorInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            actorInfo["@id"].AsFixnum().ToInt32(),
                            actorInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Actor
                        ));
                    }
                }
                else if (filename.EndsWith("Items.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var itemInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            itemInfo["@id"].AsFixnum().ToInt32(),
                            itemInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Item
                        ));
                    }
                }
                else if (filename.EndsWith("CommonEvents.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var eventInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            eventInfo["@id"].AsFixnum().ToInt32(),
                            eventInfo["@name"].AsInstanceVariable()?.Base.AsString()?.Value,
                            EnumItemType.Event
                        ));
                    }

                }
                else if (filename.EndsWith("Weapons.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var weaponInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            weaponInfo["@id"].AsFixnum().ToInt32(),
                            weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Weapon
                        ));
                    }
                }/*敌人
                else if (filename.EndsWith("Enemies.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var enemyInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            enemyInfo["@id"].AsFixnum().ToInt32(),
                            enemyInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Enemy
                        ));
                    }
                }*/
                //敌群
                else  if (filename.EndsWith("Troops.rvdata2")) 
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var troopInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            troopInfo["@id"].AsFixnum().ToInt32(),
                            troopInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Troop
                        ));
                    }
                }
                else if (filename.EndsWith("System.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsObject();
                    {
                        int id = 1;
                        foreach (var obj in root["@variables"].AsArray())
                        {
                            if (obj is Nil) continue;
                            var variableInfo = obj.AsInstanceVariable();
                            _ItemList.Add(new Item(id++, variableInfo.Base.AsString().Value, EnumItemType.Variable));
                        }
                    }
                    {
                        int id = 1;
                        foreach (var obj in root["@switches"].AsArray())
                        {
                            if (obj is Nil) continue;
                            var switchInfo = obj.AsInstanceVariable();
                            _ItemList.Add(new Item(id++, switchInfo.Base.AsString().Value, EnumItemType.Switch));
                        }
                    }
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(filename, "Map\\d{3}.rvdata2"))//Map file
                {
                    MapList.Add(new MapInfo() { Map = new Decoder().Decode(File.OpenRead(filename)) });
                }
                else if (filename.EndsWith("MapInfos.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsHash();
                    var json = root.ToJson();
                    foreach (var pair in root)
                    {
                        var index = pair.Key.AsFixnum().ToInt32() - 1;
                        MapList[index].Name = pair.Value.AsObject()["@name"].AsInstanceVariable().Base.AsString().Value;
                        MapList[index].ID = pair.Key.AsFixnum()!.ToInt32();
                    }

                }
                else if (filename.EndsWith("Armors.rvdata2"))
                {
                    var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                    foreach (var obj in root)
                    {
                        if (obj is Nil)
                            continue;
                        var weaponInfo = obj.AsObject();
                        _ItemList.Add(new Item(
                            weaponInfo["@id"].AsFixnum().ToInt32(),
                            weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                            EnumItemType.Armor
                        ));
                    }
                }
            }
            ApplyFilter();
        }

    }
}
