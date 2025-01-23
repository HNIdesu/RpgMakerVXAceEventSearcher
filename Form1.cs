namespace RpgMakerVXAceEventSearcher
{
    internal sealed partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        private MainModelView _MainModelView = new MainModelView();

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _MainModelView.LoadItemList(Path.Combine(folderBrowserDialog1.SelectedPath, "Data"));
                listView1.Items.Clear();
                listView1.Items.AddRange(_MainModelView.ItemList.Select(item => new ListViewItem([
                    item.Id.ToString(),
                    item.Name,
                    Enum.GetName(typeof(EnumItemType),item.ItemType)??""
                ])
                {
                    Tag = item
                }).ToArray());
            }
                
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem == SearchReferenceToolStripMenuItem)
            {
                _MainModelView.SearchReferences((Item)contextMenuStrip1.Tag!);
                listView_SearchResult.Items.Clear();
                foreach (var result in _MainModelView.SearchResultList)
                {
                    listView_SearchResult.Items.Add(new ListViewItem([
                        result.MapID.ToString(),
                        result.MapName,
                        result.EventID.ToString(),
                        result.EventName,
                        (1+result.PageIndex).ToString()
                    ]));
                }
            }

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    contextMenuStrip1.Tag = item.Tag;
                    contextMenuStrip1.Show(Control.MousePosition);
                }
            }
        }

    }
}