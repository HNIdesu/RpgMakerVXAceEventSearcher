﻿using RubyMarshal;
using RubyMarshal.Types;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RpgMakerVXAceEventSearcher
{
    internal class MainModelView
    {
        private readonly Dictionary<string,Item> _ItemList = [];
        public List<Item> ItemList { get; private set; } = [];
        public List<Map> MapList { get; private set; } = [];
        public List<CommonEvent> CommonEventList { get; private set; } = [];
        public List<SearchResult> SearchResultList { get; private set; } = [];
        private readonly Dictionary<int, TroopInfo> _TroopList = [];
        private readonly Dictionary<int, EnemyInfo> _EnemyList = [];
        public HashSet<EnumItemType> CheckedItemTypes { get; private set; } = [];
        public string SearchQuery { get; set; } = "";
        public void ApplyFilter()
        {
            ItemList.Clear();
            if (string.IsNullOrEmpty(SearchQuery) && CheckedItemTypes.Count == 0)
                ItemList.AddRange(_ItemList.Values);
            else
                ItemList.AddRange(_ItemList.Values.Where(item => 
                    item.Name.Contains(SearchQuery) && (CheckedItemTypes.Count <= 0 || CheckedItemTypes.Contains(item.ItemType))
                ));
        }
        private string GetItemId(int id,EnumItemType itemType)=> $"{id}{Enum.GetName(typeof(EnumItemType), itemType)}";
        public void SearchReferences(Item item)
        {
            SearchResultList.Clear();
            IEnumerable<SearchResult>? searchResult = null;
            switch (item.ItemType)
            {
                case EnumItemType.Item:
                    searchResult = SearchCommands(cmd => {
                        var isItemChange =//增减物品指令
                            cmd.Code == 126
                            && cmd.GetParameter(0)?.AsFixnum()?.ToInt32() == item.Id;
                        var isDropItem = false;//是否为掉落物
                        if(cmd.Code == 301
                            && cmd.GetParameter(0)?.AsFixnum()?.ToInt32() == 0//直接指定
                        )
                        {
                            var troopId = cmd.GetParameter(1)?.AsFixnum()?.ToInt32();
                            if (troopId != null)
                            {
                                isDropItem = _TroopList[troopId.Value].EnemyList.Any(enemy => enemy.DropItems.Any(dropItem => dropItem == item));
                            }
                        }
                        return isItemChange || isDropItem;
                    });
                    break;
                case EnumItemType.Weapon:
                    searchResult = SearchCommands(cmd => {
                        var isWeaponChange = cmd.Code == 127//队伍增减武器指令
                             && cmd.GetParameter(0)?.AsFixnum()?.ToInt32() == item.Id;
                        var isDropItem = false;//是否为掉落物
                        if (cmd.Code == 301
                            && cmd.GetParameter(0)?.AsFixnum()?.ToInt32() == 0//直接指定
                        )
                        {
                            var troopId = cmd.GetParameter(1)?.AsFixnum()?.ToInt32();
                            if (troopId != null)
                            {
                                isDropItem = _TroopList[troopId.Value].EnemyList.Any(enemy => enemy.DropItems.Any(dropItem => dropItem == item));
                            }
                        }
                        return isWeaponChange || isDropItem;
                    });
                    break;
                case EnumItemType.Armor:
                    searchResult = SearchCommands(cmd =>
                    {
                        try
                        {
                            var isArmorChange = cmd.Code == 128//队伍增减护甲指令
                            && cmd.GetParameter(0)?.AsFixnum()?.ToInt32() == item.Id;
                            bool isDropItem = false;//是否为掉落物
                            if (cmd.Code == 301 
                                && cmd.GetParameter(0)?.AsFixnum()?.ToInt32()== 0//直接指定
                            )
                            {
                                var troopId = cmd.GetParameter(1)?.AsFixnum()?.ToInt32();
                                if (troopId != null)
                                {
                                    isDropItem = _TroopList[troopId.Value].EnemyList.Any(enemy => enemy.DropItems.Any(dropItem => dropItem == item));
                                }
                            }
                            return isArmorChange || isDropItem;
                        }
                        catch(Exception)
                        {
                            return false;
                        }
                    });
                    break;
                case EnumItemType.Actor:
                    break;
                case EnumItemType.Event:
                    searchResult = SearchCommands(cmd =>
                            cmd.Code == 117
                            && cmd.Parameters[0] is Fixnum
                            && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                        );
                    break;
                case EnumItemType.Variable:
                    searchResult = SearchCommands(cmd =>
                            cmd.Code == 122
                            && cmd.Parameters[0] is Fixnum
                            && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                        );
                    break;
                case EnumItemType.Switch:
                    searchResult = SearchCommands(cmd =>
                            cmd.Code == 121
                            && cmd.Parameters[0] is Fixnum
                            && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                        );
                    break;
                case EnumItemType.Troop:
                    searchResult = SearchCommands( cmd =>
                            cmd.Code == 301//战斗处理
                            && cmd.Parameters.Count > 1
                            && cmd.Parameters[1] is Fixnum
                            && cmd.Parameters[1].AsFixnum()!.ToInt32() == item.Id
                        );
                    break;
                default:
                    break;
            }
            if (searchResult == null) return;
            SearchResultList.AddRange(searchResult);
        }

        private IEnumerable<SearchResult> SearchCommands(
            Func<Command, bool> func)
        {
            foreach (var map in MapList)
            {
                foreach (var ev in map.Data.AsObject()["@events"].AsHash())
                {
                    int pageIndex = 1;
                    foreach (var page in ev.Value.AsObject()["@pages"].AsArray())
                    {
                        foreach (var _cmd in page.AsObject()["@list"].AsArray())
                        {
                            var cmd = _cmd.AsObject();
                            var code = cmd["@code"].AsFixnum().ToInt32();
                            var parameters = cmd["@parameters"].AsArray();
                            var command = new Command(code, [.. parameters]);
                            if (func(command))
                            {
                                yield return new SearchResult(
                                    pageIndex: pageIndex,
                                    eventName: ev.Value.AsObject()["@name"].AsInstanceVariable().Base.AsString().Value,
                                    mapName: map.Name,
                                    mapId: map.ID,
                                    eventId: ev.Value.AsObject()["@id"].AsFixnum().ToInt32(),
                                    location: new Point(
                                        x: ev.Value.AsObject()["@x"].AsFixnum().ToInt32(),
                                        y: ev.Value.AsObject()["@y"].AsFixnum().ToInt32()
                                    )
                                );
                                break;
                            }
                        }
                        pageIndex++;
                    }
                }
            }
            foreach (var cv in CommonEventList)
            {
                foreach (var _cmd in cv.Data.AsObject()["@list"].AsArray())
                {
                    var cmd = _cmd.AsObject();
                    var code = cmd["@code"].AsFixnum().ToInt32();
                    var parameters = cmd["@parameters"].AsArray();
                    var command = new Command(code, [.. parameters]);
                    if (func(command))
                    {
                        yield return new SearchResult(
                            eventName: cv.Name,
                            eventId:cv.ID
                        );
                        break;
                    }
                }
            }
        }
        public void LoadItemList(string gameDirectory)
        {
            _ItemList.Clear();
            LoadActorData(Path.Combine(gameDirectory, "Actors.rvdata2"));
            LoadItemData(Path.Combine(gameDirectory, "Items.rvdata2"));
            LoadCommonEvent(Path.Combine(gameDirectory, "CommonEvents.rvdata2"));
            LoadWeaponData(Path.Combine(gameDirectory, "Weapons.rvdata2"));
            LoadArmorData(Path.Combine(gameDirectory, "Armors.rvdata2"));
            LoadSystemData(Path.Combine(gameDirectory, "System.rvdata2"));
            LoadEnemyData(Path.Combine(gameDirectory, "Enemies.rvdata2"));
            LoadTroopData(Path.Combine(gameDirectory, "Troops.rvdata2"));
            LoadMaps(gameDirectory);
            LoadMapInfo(Path.Combine(gameDirectory, "MapInfos.rvdata2"));
            ApplyFilter();
        }

        //地图数据
        private void LoadMaps(string path)
        {
            foreach (string filename in Directory.EnumerateFiles(path, "*.rvdata2"))
                if (Regex.IsMatch(filename, "Map\\d{3}.rvdata2"))//Map file
                    MapList.Add(new Map() { Data = new Decoder().Decode(File.OpenRead(filename)) });
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
                    var newId = id++;
                    _ItemList.Add(
                        GetItemId(newId, EnumItemType.Variable),
                        new Item(newId, variableInfo.Base.AsString().Value, EnumItemType.Variable));
                }
            }
            {
                int id = 1;
                foreach (var obj in root["@switches"].AsArray())
                {
                    if (obj is Nil) continue;
                    var switchInfo = obj.AsInstanceVariable();
                    var newId = id++;
                    _ItemList.Add(
                        GetItemId(newId, EnumItemType.Switch),
                        new Item(newId, switchInfo.Base.AsString().Value, EnumItemType.Switch));
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
                var id = weaponInfo["@id"].AsFixnum().ToInt32();
                _ItemList.Add(
                    GetItemId(id, EnumItemType.Armor),
                    new Item(
                        id,
                        weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                        EnumItemType.Armor
                    )
                );
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
                var id = weaponInfo["@id"].AsFixnum().ToInt32();
                _ItemList.Add(
                    GetItemId(id, EnumItemType.Weapon),
                    new Item(
                        id,
                        weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                        EnumItemType.Weapon
                    )
                );
            }
        }

        //公共事件
        private void LoadCommonEvent(string path)
        {
            var root = new Decoder().Decode(File.OpenRead(path)).AsArray();
            if (root == null)
            {
                MessageBox.Show("Failed to load common events.");
                return;
            }
            foreach (var obj in root)
            {
                if (obj is Nil) continue;
                var eventInfo = obj.AsObject();
                var name = eventInfo?["@name"].AsInstanceVariable()?.Base.AsString()?.Value;
                var id = eventInfo!["@id"]?.AsFixnum()?.ToInt32();
                _ItemList.Add(
                    GetItemId(id!.Value, EnumItemType.Event),
                    new Item(
                        id!.Value,
                        name!,
                        EnumItemType.Event
                    )
                );
                CommonEventList.Add(new CommonEvent(id.Value, name!) { Data = eventInfo });
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
                var id = itemInfo["@id"].AsFixnum().ToInt32();
                _ItemList.Add(
                    GetItemId(id, EnumItemType.Item),
                    new Item(
                        id,
                        itemInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                        EnumItemType.Item
                    )
                );
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
                var id = actorInfo["@id"].AsFixnum().ToInt32();
                _ItemList.Add(
                    GetItemId(id, EnumItemType.Actor),
                    new Item(
                        id,
                        actorInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                        EnumItemType.Actor
                    )
                );
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
                var id = enemyInfo["@id"].AsFixnum().ToInt32();
                var name = enemyInfo["@name"].AsInstanceVariable().Base.AsString().Value;
                var dropItems = enemyInfo["@drop_items"].AsArray()
                    .Where(it=> it.AsObject()["@kind"].AsFixnum().Value!=0)
                    .Select(it =>
                {
                    var id = it.AsObject()["@data_id"].AsFixnum().ToInt32();
                    EnumItemType itemType;
                    switch (it.AsObject()["@kind"].AsFixnum().Value)
                    {
                        case 1:
                            itemType = EnumItemType.Item;
                            break;
                        case 2:
                            itemType = EnumItemType.Weapon;
                            break;
                        default:
                            itemType = EnumItemType.Armor;
                            break;
                    }
                    return _ItemList[GetItemId(id, itemType)];
                }
                ).ToList();
                _EnemyList[id] = new EnemyInfo(id, name,dropItems);
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
                var id = troopInfo["@id"].AsFixnum().ToInt32();
                _ItemList.Add(
                    GetItemId(id, EnumItemType.Troop),
                    new Item(
                        id,
                        troopInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                        EnumItemType.Troop
                    )
                );
                _TroopList[id] = new TroopInfo(troopInfo["@members"].AsArray().Select(it => _EnemyList[it.AsObject()["@enemy_id"].AsFixnum().ToInt32()]).ToList());
            }
        }
    }
}
