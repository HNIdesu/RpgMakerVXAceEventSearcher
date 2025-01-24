using RubyMarshal;
using RubyMarshal.Types;
using System.Text.RegularExpressions;

namespace RpgMakerVXAceEventSearcher
{
    internal class MainModelView
    {
        private readonly List<Item> _ItemList = [];
        public List<Item> ItemList { get; private set; } = [];
        public List<MapInfo> MapList { get; private set; } = [];
        public List<SearchCommandResult> SearchResultList { get; private set; } = [];

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
            IEnumerable<SearchCommandResult>? searchResult = null;
            switch (item.ItemType)
            {
                case EnumItemType.Item:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
                            cmd.Code == 126
                            && cmd.Parameters[0] is Fixnum
                            && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id);
                        break;
                    }

                case EnumItemType.Weapon:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
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
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
                                cmd.Code == 117
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Variable:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
                                cmd.Code == 122
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Switch:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
                                cmd.Code == 121
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                    }
                case EnumItemType.Armor:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
                                cmd.Code == 128
                                && cmd.Parameters.Count > 0
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                            );
                        break;
                        
                    }
                case EnumItemType.Troop:
                    {
                        searchResult = Utility.SearchFromCommands(MapList, cmd =>
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
            //LoadEnemyData(Path.Combine(gameDirectory, "Enemies.rvdata2"));
            LoadTroopData(Path.Combine(gameDirectory, "Troops.rvdata2"));
            LoadActorData(Path.Combine(gameDirectory, "Actors.rvdata2"));
            LoadItemData(Path.Combine(gameDirectory, "Items.rvdata2"));
            LoadCommonEvent(Path.Combine(gameDirectory, "CommonEvents.rvdata2"));
            LoadWeaponData(Path.Combine(gameDirectory, "Weapons.rvdata2"));
            LoadArmorData(Path.Combine(gameDirectory, "Armors.rvdata2"));
            LoadSystemData(Path.Combine(gameDirectory, "System.rvdata2"));
            LoadMaps(gameDirectory);
            LoadMapInfo(Path.Combine(gameDirectory, "MapInfos.rvdata2"));
            ApplyFilter();
        }

        //地图数据
        private void LoadMaps(string path)
        {
            foreach (string filename in Directory.EnumerateFiles(path, "*.rvdata2"))
                if (Regex.IsMatch(filename, "Map\\d{3}.rvdata2"))//Map file
                    MapList.Add(new MapInfo() { Map = new Decoder().Decode(File.OpenRead(filename)) });
        }

        //系统数据，包括全局开关和全局变量
        private void LoadSystemData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsObject();
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

        //地图信息
        private void LoadMapInfo(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsHash();
            var json = root.ToJson();
            foreach (var pair in root)
            {
                var index = pair.Key.AsFixnum().ToInt32() - 1;
                MapList[index].Name = pair.Value.AsObject()["@name"].AsInstanceVariable().Base.AsString().Value;
                MapList[index].ID = pair.Key.AsFixnum()!.ToInt32();
            }
        }

        //防具信息
        private void LoadArmorData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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

        //武器信息
        private void LoadWeaponData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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
        }

        //公共事件
        private void LoadCommonEvent(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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

        //物品信息
        private void LoadItemData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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

        //角色信息
        private void LoadActorData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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

        //敌人信息
        private void LoadEnemyData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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
        }

        //敌群信息
        private void LoadTroopData(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
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
    }
}
