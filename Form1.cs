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
                        var root = Decoder.Decode(File.OpenRead(filename)).AsArray();
                        foreach (var obj in root)
                        {
                            if (obj is Nil)
                                continue;
                            var actorInfo = obj.AsObject();
                            var item = new Item();
                            item.Id = actorInfo["@id"].AsFixnum().ToInt32();
                            item.ItemType = EnumItemType.Arctor;
                            var name = actorInfo["@name"];
                            item.Name = name.AsInstanceVariable().Name.ToString();
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("Items.rvdata2"))
                    {
                        var root = Decoder.Decode(File.OpenRead(filename)).AsArray();
                        foreach (var obj in root)
                        {
                            if (obj is Nil)
                                continue;
                            var item = new Item();
                            var itemInfo = obj.AsObject();
                            item.Id = itemInfo["@id"].AsFixnum().ToInt32();
                            var name = itemInfo["@name"];
                            item.Name = name.AsInstanceVariable().Name.ToString();
                            item.ItemType = EnumItemType.Item;
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("CommonEvents.rvdata2"))
                    {
                        var root = Decoder.Decode(File.OpenRead(filename)).AsArray();
                        foreach (var obj in root)
                        {
                            if (obj is Nil)
                                continue;
                            var item = new Item();
                            var eventInfo = obj.AsObject();
                            item.Id = eventInfo["@id"].AsFixnum().ToInt32();
                            var name = eventInfo["@name"];
                            item.Name = name.AsInstanceVariable().Name.ToString();
                            item.ItemType = EnumItemType.Event;
                            itemList.Add(item);
                        }

                    }
                    else if (filename.EndsWith("Weapons.rvdata2"))
                    {
                        var root = Decoder.Decode(File.OpenRead(filename)).AsArray();
                        foreach (var obj in root)
                        {
                            if (obj is Nil)
                                continue;
                            var item = new Item();
                            var weaponInfo = obj.AsObject();
                            item.Id = weaponInfo["@id"].AsFixnum().ToInt32();
                            var name = weaponInfo["@name"];
                            item.Name = name.AsInstanceVariable().Name.ToString();
                            item.ItemType = EnumItemType.Weapon;
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("System.rvdata2"))
                    {
                        var root = Decoder.Decode(File.OpenRead(filename)).AsObject();
                        {
                            int id = 1;
                            foreach (var obj in root["@variables"].AsArray())
                            {
                                if (obj is Nil)
                                    continue;
                                var variableInfo = obj.AsInstanceVariable();
                                var item = new Item();
                                item.Id = id++;
                                item.Name = variableInfo.Name.ToString();
                                item.ItemType = EnumItemType.Variable;
                                itemList.Add(item);
                            }
                        }
                        {
                            int id = 1;
                            foreach (var obj in root["@switches"].AsArray())
                            {
                                if (obj is Nil)
                                    continue;
                                var switchInfo = obj.AsInstanceVariable();
                                Item item = new ();
                                item.Id = id++;
                                item.Name = switchInfo.Name.ToString();
                                item.ItemType = EnumItemType.Switch;
                                itemList.Add(item);
                            }
                        }
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(filename, "Map\\d{3}.rvdata2"))//Map file
                    {
                        MapList.Add(new MapInfo() { Map = Decoder.Decode(File.OpenRead(filename)) });
                    }
                    else if (filename.EndsWith("MapInfos.rvdata2"))
                    {
                        var root = Decoder.Decode(File.OpenRead(filename)).AsHash();
                        foreach (var pair in root)
                        {
                            var index = pair.Key.AsFixnum().ToInt32() - 1;
                            MapList[index].Name = pair.Value.AsObject()["@name"].AsInstanceVariable().Name.ToString();
                            MapList[index].ID = pair.Value.AsObject()["@order"].AsFixnum()!.ToInt32();
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
                                && cmd.Parameters.Count > 0
                                && cmd.Parameters[0] is Fixnum
                                && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id);
                            break;
                        }
                        
                    case EnumItemType.Weapon:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 127
                                    && cmd.Parameters.Count > 0
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                ); 
                            break;
                        }
                        
                    case EnumItemType.Arctor:
                        break;
                    case EnumItemType.Event:
                        break;
                    case EnumItemType.Variable:
                        {
                            listView_SearchResult.Items.Clear();
                            searchResult = Utility.SearchCommands(MapList, cmd =>
                                    cmd.Code == 122
                                    && cmd.Parameters.Count > 0
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
                                    && cmd.Parameters.Count > 0
                                    && cmd.Parameters[0] is Fixnum
                                    && cmd.Parameters[0].AsFixnum()!.ToInt32() == item.Id
                                );
                            break;
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