
namespace RpgMakerVXAceEventSearcher
{
    internal class TreeView1:TreeView
    {
        protected List<Item> itemList;
        internal void SetDataSource(List<Item> itemList)
        {
            this.itemList = itemList;
        }
        internal void NotifyDatasetChanged()
        {
            if (itemList != null)
            {
                foreach(Item item in itemList)
                {
                    TreeNode parent = Nodes[((int)item.ItemType)];
                    TreeNode node = new TreeNode();
                    node.Tag = item;
                    node.Text = item.ToString();
                    parent.Nodes.Add(node);
                }
            }
        }

        

    }

    
}
