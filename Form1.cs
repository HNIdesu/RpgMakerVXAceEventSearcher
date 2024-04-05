using Arche.RpgMaker2MV.VXACE;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace RpgMakerVXAceEventSearcher
{
    internal partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        List<MapInfo> MapList { get; set; } = new List<MapInfo>();

        private void 打开项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                List<Item> itemList = new List<Item>();
                treeView1.SetDataSource(itemList);
                foreach (string filename in Directory.EnumerateFiles(Path.Combine(folderBrowserDialog1.SelectedPath,"Data"), "*.json"))
                {
                    if (filename.EndsWith("Actors.json"))
                    {
                        JsonArray arr = (JsonArray)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        foreach (JsonObject obj in arr)
                        {
                            if (obj == null)
                                continue;
                            Item item = new Item();
                            item.Id = obj["@id"].GetValue<int>();
                            item.ItemType = EnumItemType.Arctor;
                            item.Name = obj["@name"].GetValue<string>();
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("Items.json"))
                    {
                        JsonArray arr = (JsonArray)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        foreach (JsonObject obj in arr)
                        {
                            if (obj == null)
                                continue;
                            Item item = new Item();
                            item.Id = obj["@id"].GetValue<int>();
                            item.Name = obj["@name"].GetValue<string>();
                            item.ItemType = EnumItemType.Item;
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("CommonEvents.json"))
                    {
                        JsonArray arr = (JsonArray)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        foreach (JsonObject obj in arr)
                        {
                            if (obj == null)
                                continue;
                            Item item = new Item();
                            item.Id = obj["@id"].GetValue<int>();
                            item.Name = obj["@name"].GetValue<string>();
                            item.ItemType = EnumItemType.Event;
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("Weapons.json"))
                    {
                        JsonArray arr = (JsonArray)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        foreach (JsonObject obj in arr)
                        {
                            if (obj == null)
                                continue;
                            Item item = new Item();
                            item.Id = obj["@id"].GetValue<int>();
                            item.Name = obj["@name"].GetValue<string>();
                            item.ItemType = EnumItemType.Weapon;
                            itemList.Add(item);
                        }
                    }
                    else if (filename.EndsWith("System.json"))
                    {
                        //变量查询
                        JsonObject obj = (JsonObject)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        {
                            JsonArray varr = (JsonArray)obj["@variables"];
                            int id = 1;
                            foreach (JsonValue val in varr)
                            {
                                if (val == null)
                                    continue;
                                Item item = new Item();
                                item.Id = id++;
                                item.Name = val.GetValue<string>();
                                item.ItemType = EnumItemType.Variable;
                                itemList.Add(item);
                            }
                        }

                        {
                            JsonArray varr = (JsonArray)obj["@switches"];
                            int id = 1;
                            foreach (JsonValue val in varr)
                            {
                                if (val == null)
                                    continue;
                                Item item = new Item();
                                item.Id = id++;
                                item.Name = val.GetValue<string>();
                                item.ItemType = EnumItemType.Switch;
                                itemList.Add(item);
                            }
                        }
                    }
                    else if (Regex.IsMatch(filename, "Map\\d{3}.json$"))//地图文件
                    {
                        MapList.Add(new MapInfo() { Map = Helper.Parse<Map>(File.ReadAllText(filename, Encoding.UTF8)) });
                    }
                    else if (filename.EndsWith("MapInfos.json"))
                    {
                        JsonObject obj = (JsonObject)JsonNode.Parse(File.ReadAllText(filename, Encoding.UTF8));
                        foreach (var pair in obj.GetEnumerator().ToEnumerable())
                        {
                            JsonObject mapObj = (JsonObject)pair.Value;
                            MapList[int.Parse(pair.Key) - 1].Name = mapObj["@name"].GetValue<string>();
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
            if (e.ClickedItem == 查找调用ToolStripMenuItem)
            {
                Item item = (Item)contextMenuStrip1.Tag;
                switch (item.ItemType)
                {
                    case EnumItemType.Item:
                        {
                            listView_SearchResult.Items.Clear();
                            foreach (var map in MapList)
                            {
                                foreach (var ev in map.Map.Events)
                                {
                                    foreach (var page in ev.Pages)
                                    {
                                        bool evFlag = false;
                                        foreach (var cmd in page.List)
                                        {
                                            if (cmd.Code == 126 && (int.Parse(cmd.Parameters[0].ToString()) == item.Id))
                                            {
                                                listView_SearchResult.Items.Add(new ListViewItem(new string[3] { map.Name, ev.Name, Array.IndexOf(ev.Pages, page).ToString() }));
                                                evFlag = true;
                                                break;
                                            }

                                        }
                                        if (evFlag) break;
                                    }
                                }
                            }
                            break;
                        }
                        
                    case EnumItemType.Weapon:
                        {
                            listView_SearchResult.Items.Clear();
                            foreach (var map in MapList)
                            {
                                foreach (var ev in map.Map.Events)
                                {
                                    foreach (var page in ev.Pages)
                                    {
                                        bool evFlag = false;
                                        foreach (var cmd in page.List)
                                        {
                                            if (cmd.Code == 127 && (int.Parse(cmd.Parameters[0].ToString()) == item.Id))
                                            {
                                                listView_SearchResult.Items.Add(new ListViewItem(new string[3] { map.Name, ev.Name, Array.IndexOf(ev.Pages, page).ToString() }));
                                                evFlag = true;
                                                break;
                                            }

                                        }
                                        if (evFlag) break;
                                    }
                                }
                            }
                            break;
                        }
                    case EnumItemType.Arctor:
                        break;
                    case EnumItemType.Event:
                        break;
                    case EnumItemType.Variable:
                        {
                            listView_SearchResult.Items.Clear();
                            foreach (var map in MapList)
                            {
                                foreach (var ev in map.Map.Events)
                                {
                                    foreach (var page in ev.Pages)
                                    {
                                        bool evFlag = false;
                                        foreach (var cmd in page.List)
                                        {
                                            if (cmd.Code == 122 && (int.Parse(cmd.Parameters[0].ToString()) == item.Id))
                                            {
                                                listView_SearchResult.Items.Add(new ListViewItem(new string[3] { map.Name, ev.Name,Array.IndexOf(ev.Pages,page).ToString() }));
                                                evFlag = true;
                                                break;
                                            }

                                        }
                                        if (evFlag) break;
                                    }
                                }
                            }
                        }
                        break;
                    case EnumItemType.Switch:
                        {
                            listView_SearchResult.Items.Clear();
                            foreach (var map in MapList)
                            {
                                foreach (var ev in map.Map.Events)
                                {
                                    foreach (var page in ev.Pages)
                                    {
                                        bool evFlag = false;
                                        foreach (var cmd in page.List)
                                        {
                                            if (cmd.Code == 121 && (int.Parse(cmd.Parameters[0].ToString()) == item.Id))
                                            {
                                                listView_SearchResult.Items.Add(new ListViewItem(new string[3] { map.Name, ev.Name, Array.IndexOf(ev.Pages, page).ToString() }));
                                                evFlag = true;
                                                break;
                                            }

                                        }
                                        if (evFlag) break;
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}