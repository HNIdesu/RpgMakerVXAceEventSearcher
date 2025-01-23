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
            TreeNode treeNode1 = new TreeNode("道具");
            TreeNode treeNode2 = new TreeNode("武器");
            TreeNode treeNode3 = new TreeNode("防具");
            TreeNode treeNode4 = new TreeNode("变量");
            TreeNode treeNode5 = new TreeNode("公共事件");
            TreeNode treeNode6 = new TreeNode("开关");
            TreeNode treeNode7 = new TreeNode("人物");
            TreeNode treeNode8 = new TreeNode("敌人");
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            打开项目ToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            button1 = new Button();
            textBox1 = new TextBox();
            treeView1 = new TreeView1();
            listView_SearchResult = new ListView();
            folderBrowserDialog1 = new FolderBrowserDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            SearchReferenceToolStripMenuItem = new ToolStripMenuItem();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
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
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem });
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
            splitContainer1.SplitterDistance = 338;
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
            splitContainer2.Panel1.Controls.Add(button1);
            splitContainer2.Panel1.Controls.Add(textBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(treeView1);
            splitContainer2.Size = new Size(338, 646);
            splitContainer2.SplitterDistance = 66;
            splitContainer2.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(278, 21);
            button1.Name = "button1";
            button1.Size = new Size(47, 23);
            button1.TabIndex = 1;
            button1.Text = "搜索";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 23);
            textBox1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "道具";
            treeNode2.Name = "节点1";
            treeNode2.Text = "武器";
            treeNode3.Name = "节点2";
            treeNode3.Text = "防具";
            treeNode4.Name = "节点3";
            treeNode4.Text = "变量";
            treeNode5.Name = "节点4";
            treeNode5.Text = "公共事件";
            treeNode6.Name = "节点5";
            treeNode6.Text = "开关";
            treeNode7.Name = "节点6";
            treeNode7.Text = "人物";
            treeNode8.Name = "节点7";
            treeNode8.Text = "敌人";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8 });
            treeView1.Size = new Size(338, 576);
            treeView1.TabIndex = 0;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // listView_SearchResult
            // 
            listView_SearchResult.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader4, columnHeader2, columnHeader3, columnHeader5 });
            listView_SearchResult.Dock = DockStyle.Fill;
            listView_SearchResult.Location = new Point(0, 0);
            listView_SearchResult.Name = "listView_SearchResult";
            listView_SearchResult.Size = new Size(829, 646);
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
        private Button button1;
        private TextBox textBox1;
        private TreeView1 treeView1;
        private FolderBrowserDialog folderBrowserDialog1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem SearchReferenceToolStripMenuItem;
        private ListView listView_SearchResult;
    }
}