using RubyMarshal;
using RubyMarshal.Types;

namespace RpgMakerVXAceEventSearcher
{
    internal sealed partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        List<MapInfo> MapList { get; set; } = new List<MapInfo>();

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var itemList = new List<Item>();
                treeView1.SetDataSource(itemList);
                foreach (string filename in Directory.EnumerateFiles(Path.Combine(folderBrowserDialog1.SelectedPath,"Data"), "*.rvdata2"))
                {
                    if (filename.EndsWith("Actors.rvdata2"))
                    {
                        var root = new Decoder().Decode(File.OpenRead(filename)).AsArray();
                        foreach (var obj in root)
                        {
                            if (obj is Nil)
                                continue;
                            var actorInfo = obj.AsObject();
                            itemList.Add(new Item(
                                actorInfo["@id"].AsFixnum().ToInt32(),
                                actorInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                                EnumItemType.Arctor
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
                            itemList.Add(new Item(
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
                            itemList.Add(new Item(
                                eventInfo["@id"].AsFixnum().ToInt32(),
                                eventInfo["@name"].AsInstanceVariable().Base.AsString().Value,
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
                            itemList.Add(new Item(
                                weaponInfo["@id"].AsFixnum().ToInt32(),
                                weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                                EnumItemType.Weapon
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
                                if (obj is Nil)continue;
                                var variableInfo = obj.AsInstanceVariable();
                                itemList.Add(new Item(id++, variableInfo.Base.AsString().Value,EnumItemType.Variable));
                            }
                        }
                        {
                            int id = 1;
                            foreach (var obj in root["@switches"].AsArray())
                            {
                                if (obj is Nil)continue;
                                var switchInfo = obj.AsInstanceVariable();
                                itemList.Add(new Item(id++, switchInfo.Base.AsString().Value, EnumItemType.Switch));
                            }
                        }
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(filename, "Map\\d{3}.rvdata2"))//Map file
                    {
                        MapList.Add(new MapInfo() { Map = new Decoder().Decode(File.OpenRead(filename)) });
                    }
                    else if (filename.EndsWith("MapInfos.rvdata2"))
                    {
                        var root =new Decoder().Decode(File.OpenRead(filename)).AsHash();
                        foreach (var pair in root)
                        {
                            var index = pair.Key.AsFixnum().ToInt32() - 1;
                            MapList[index].Name = pair.Value.AsObject()["@name"].AsInstanceVariable().Base.AsString().Value;
                            MapList[index].ID = pair.Value.AsObject()["@order"].AsFixnum()!.ToInt32();
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
                            itemList.Add(new Item(
                                weaponInfo["@id"].AsFixnum().ToInt32(),
                                weaponInfo["@name"].AsInstanceVariable().Base.AsString().Value,
                                EnumItemType.Armor
                            ));
                        }
                    }
                }
                treeView1.NotifyDatasetChanged();

            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Tag is Item)
                {
                    contextMenuStrip1.Show(treeView1, e.Location.X, e.Location.Y);
                    contextMenuStrip1.Tag = e.Node.Tag;
                }

            }
        }


        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            if (e.ClickedItem == SearchReferenceToolStripMenuItem)
            {
                Item item = (Item)contextMenuStrip1.Tag!;
                IEnumerable<Utility.SearchCommandResult>? searchResult=null;
                switch (item.ItemType)
                {
                    case EnumItemType.Item:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                cmd.Code == 126
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id);
                            break;
                        }
                        
                    case EnumItemType.Weapon:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 127
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                ); 
                            break;
                        }
                        
                    case EnumItemType.Arctor:
                        break;
                    case EnumItemType.Event:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 117
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                );
                            break;
                        }
                    case EnumItemType.Variable:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 122
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                );
                            break; 
                        }
                    case EnumItemType.Switch:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 121
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                );
                            break;
                        }
                    case EnumItemType.Armor:
                        {
                            {
                                listView_SearchResult.Items.Clear();
                                searchResult = Utility.SearchCommands(MapList, cmd =>
                                        cmd.Code == 128
                                        && cmd.Parameters.Count > 0
                                        && cmd.Parameters[0] is Fixnum
                                        && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                    );
                                break;
                            }
                        }
                    default:
                        break;
                }
                if (searchResult == null) return;

                foreach (var result in searchResult)
                {
                    listView_SearchResult.Items.Add(new ListViewItem(new string[5] {
                        result.MapID.ToString(),
                        result.EventID.ToString(),
                        result.MapName,
                        result.EventName,
                        (1+result.PageIndex).ToString()
                    }));
                }
            }
            
        }
    }
}