namespace RpgMakerVXAceEventSearcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ColumnHeader columnHeader1;
            ColumnHeader columnHeader2;
            ColumnHeader columnHeader3;
            ColumnHeader columnHeader4;
            ColumnHeader columnHeader5;
            ColumnHeader columnHeader_Id;
            ColumnHeader columnHeader_Name;
            ColumnHeader columnHeader_Type;
            ColumnHeader columnHeader6;
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            打开项目ToolStripMenuItem = new ToolStripMenuItem();
            filterToolStripMenuItem = new ToolStripMenuItem();
            itemToolStripMenuItem = new ToolStripMenuItem();
            weaponToolStripMenuItem = new ToolStripMenuItem();
            armorToolStripMenuItem = new ToolStripMenuItem();
            variableToolStripMenuItem = new ToolStripMenuItem();
            eventToolStripMenuItem = new ToolStripMenuItem();
            switchToolStripMenuItem = new ToolStripMenuItem();
            actorToolStripMenuItem1 = new ToolStripMenuItem();
            enemyToolStripMenuItem = new ToolStripMenuItem();
            troopToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            textBox_FilterItem = new TextBox();
            listView1 = new ListView();
            listView_SearchResult = new ListView();
            folderBrowserDialog1 = new FolderBrowserDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            SearchReferenceToolStripMenuItem = new ToolStripMenuItem();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader_Id = new ColumnHeader();
            columnHeader_Name = new ColumnHeader();
            columnHeader_Type = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "MapID";
            columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "EventID";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "EventName";
            columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "MapName";
            columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Page";
            // 
            // columnHeader_Id
            // 
            columnHeader_Id.Text = "Id";
            // 
            // columnHeader_Name
            // 
            columnHeader_Name.Text = "Name";
            columnHeader_Name.Width = 200;
            // 
            // columnHeader_Type
            // 
            columnHeader_Type.Text = "Type";
            columnHeader_Type.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Location";
            columnHeader6.Width = 100;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem, filterToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1171, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 打开项目ToolStripMenuItem });
            文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            文件ToolStripMenuItem.Size = new Size(44, 21);
            文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开项目ToolStripMenuItem
            // 
            打开项目ToolStripMenuItem.Name = "打开项目ToolStripMenuItem";
            打开项目ToolStripMenuItem.Size = new Size(124, 22);
            打开项目ToolStripMenuItem.Text = "打开项目";
            打开项目ToolStripMenuItem.Click += OpenProjectToolStripMenuItem_Click;
            // 
            // filterToolStripMenuItem
            // 
            filterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemToolStripMenuItem, weaponToolStripMenuItem, armorToolStripMenuItem, variableToolStripMenuItem, eventToolStripMenuItem, switchToolStripMenuItem, actorToolStripMenuItem1, enemyToolStripMenuItem, troopToolStripMenuItem });
            filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            filterToolStripMenuItem.Size = new Size(44, 21);
            filterToolStripMenuItem.Text = "筛选";
            filterToolStripMenuItem.DropDownItemClicked += filterToolStripMenuItem_DropDownItemClicked;
            // 
            // itemToolStripMenuItem
            // 
            itemToolStripMenuItem.CheckOnClick = true;
            itemToolStripMenuItem.Name = "itemToolStripMenuItem";
            itemToolStripMenuItem.Size = new Size(125, 22);
            itemToolStripMenuItem.Text = "Item";
            // 
            // weaponToolStripMenuItem
            // 
            weaponToolStripMenuItem.CheckOnClick = true;
            weaponToolStripMenuItem.Name = "weaponToolStripMenuItem";
            weaponToolStripMenuItem.Size = new Size(125, 22);
            weaponToolStripMenuItem.Text = "Weapon";
            // 
            // armorToolStripMenuItem
            // 
            armorToolStripMenuItem.CheckOnClick = true;
            armorToolStripMenuItem.Name = "armorToolStripMenuItem";
            armorToolStripMenuItem.Size = new Size(125, 22);
            armorToolStripMenuItem.Text = "Armor";
            // 
            // variableToolStripMenuItem
            // 
            variableToolStripMenuItem.CheckOnClick = true;
            variableToolStripMenuItem.Name = "variableToolStripMenuItem";
            variableToolStripMenuItem.Size = new Size(125, 22);
            variableToolStripMenuItem.Text = "Variable";
            // 
            // eventToolStripMenuItem
            // 
            eventToolStripMenuItem.CheckOnClick = true;
            eventToolStripMenuItem.Name = "eventToolStripMenuItem";
            eventToolStripMenuItem.Size = new Size(125, 22);
            eventToolStripMenuItem.Text = "Event";
            // 
            // switchToolStripMenuItem
            // 
            switchToolStripMenuItem.CheckOnClick = true;
            switchToolStripMenuItem.Name = "switchToolStripMenuItem";
            switchToolStripMenuItem.Size = new Size(125, 22);
            switchToolStripMenuItem.Text = "Switch";
            // 
            // actorToolStripMenuItem1
            // 
            actorToolStripMenuItem1.CheckOnClick = true;
            actorToolStripMenuItem1.Name = "actorToolStripMenuItem1";
            actorToolStripMenuItem1.Size = new Size(125, 22);
            actorToolStripMenuItem1.Text = "Actor";
            // 
            // enemyToolStripMenuItem
            // 
            enemyToolStripMenuItem.CheckOnClick = true;
            enemyToolStripMenuItem.Name = "enemyToolStripMenuItem";
            enemyToolStripMenuItem.Size = new Size(125, 22);
            enemyToolStripMenuItem.Text = "Enemy";
            enemyToolStripMenuItem.Visible = false;
            // 
            // troopToolStripMenuItem
            // 
            troopToolStripMenuItem.Name = "troopToolStripMenuItem";
            troopToolStripMenuItem.Size = new Size(125, 22);
            troopToolStripMenuItem.Text = "Troop";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 671);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1171, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listView_SearchResult);
            splitContainer1.Size = new Size(1171, 646);
            splitContainer1.SplitterDistance = 426;
            splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(textBox_FilterItem);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(listView1);
            splitContainer2.Size = new Size(426, 646);
            splitContainer2.SplitterDistance = 25;
            splitContainer2.TabIndex = 0;
            // 
            // textBox_FilterItem
            // 
            textBox_FilterItem.Dock = DockStyle.Fill;
            textBox_FilterItem.Location = new Point(0, 0);
            textBox_FilterItem.Name = "textBox_FilterItem";
            textBox_FilterItem.Size = new Size(426, 23);
            textBox_FilterItem.TabIndex = 0;
            textBox_FilterItem.TextChanged += textBox_FilterItem_TextChanged;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader_Id, columnHeader_Name, columnHeader_Type });
            listView1.Dock = DockStyle.Fill;
            listView1.FullRowSelect = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(0, 0);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(426, 617);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.MouseUp += listView1_MouseUp;
            // 
            // listView_SearchResult
            // 
            listView_SearchResult.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader4, columnHeader2, columnHeader3, columnHeader5, columnHeader6 });
            listView_SearchResult.Dock = DockStyle.Fill;
            listView_SearchResult.FullRowSelect = true;
            listView_SearchResult.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView_SearchResult.Location = new Point(0, 0);
            listView_SearchResult.MultiSelect = false;
            listView_SearchResult.Name = "listView_SearchResult";
            listView_SearchResult.Size = new Size(741, 646);
            listView_SearchResult.TabIndex = 1;
            listView_SearchResult.UseCompatibleStateImageBehavior = false;
            listView_SearchResult.View = View.Details;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { SearchReferenceToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(125, 26);
            contextMenuStrip1.ItemClicked += contextMenuStrip1_ItemClicked;
            // 
            // SearchReferenceToolStripMenuItem
            // 
            SearchReferenceToolStripMenuItem.Name = "SearchReferenceToolStripMenuItem";
            SearchReferenceToolStripMenuItem.Size = new Size(124, 22);
            SearchReferenceToolStripMenuItem.Text = "查找调用";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1171, 693);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "RMEventSearcher";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private ToolStripMenuItem 打开项目ToolStripMenuItem;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TextBox textBox_FilterItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem SearchReferenceToolStripMenuItem;
        private ListView listView_SearchResult;
        private ListView listView1;
        private ToolStripMenuItem filterToolStripMenuItem;
        private ToolStripMenuItem itemToolStripMenuItem;
        private ToolStripMenuItem weaponToolStripMenuItem;
        private ToolStripMenuItem armorToolStripMenuItem;
        private ToolStripMenuItem variableToolStripMenuItem;
        private ToolStripMenuItem eventToolStripMenuItem;
        private ToolStripMenuItem switchToolStripMenuItem;
        private ToolStripMenuItem actorToolStripMenuItem1;
        private ToolStripMenuItem enemyToolStripMenuItem;
        private ToolStripMenuItem troopToolStripMenuItem;
    }
}