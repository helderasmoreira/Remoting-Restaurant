namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("Mesa 1");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Mesa 2");
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("Mesa 3");
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("Mesa 4");
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Mesa 5");
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("Mesa 6");
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("Mesa 7");
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Mesa 8");
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("Mesa 9");
            System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("Mesa 10");
            this.btnNovoPedido = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDescricao = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCloseTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.udQuantidade = new System.Windows.Forms.NumericUpDown();
            this.eliminarTolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQuantidade)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNovoPedido
            // 
            this.btnNovoPedido.Location = new System.Drawing.Point(199, 121);
            this.btnNovoPedido.Name = "btnNovoPedido";
            this.btnNovoPedido.Size = new System.Drawing.Size(123, 23);
            this.btnNovoPedido.TabIndex = 2;
            this.btnNovoPedido.Text = "Adicionar pedido";
            this.btnNovoPedido.UseVisualStyleBackColor = true;
            this.btnNovoPedido.Click += new System.EventHandler(this.btnNovoPedido_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(571, 30);
            this.panel2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Sala";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tipo:";
            // 
            // cbTipo
            // 
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Cozinha",
            "Bar"});
            this.cbTipo.Location = new System.Drawing.Point(9, 37);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(119, 21);
            this.cbTipo.TabIndex = 8;
            this.cbTipo.SelectedIndexChanged += new System.EventHandler(this.cbTipo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quantidade:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Descrição:";
            // 
            // cbDescricao
            // 
            this.cbDescricao.FormattingEnabled = true;
            this.cbDescricao.Location = new System.Drawing.Point(9, 80);
            this.cbDescricao.Name = "cbDescricao";
            this.cbDescricao.Size = new System.Drawing.Size(168, 21);
            this.cbDescricao.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.8349F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.1651F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.41975F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.58025F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 324);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 282);
            this.panel1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode51.Name = "Node0";
            treeNode51.Text = "Mesa 1";
            treeNode52.Name = "Node1";
            treeNode52.Text = "Mesa 2";
            treeNode53.Name = "Node2";
            treeNode53.Text = "Mesa 3";
            treeNode54.Name = "Node3";
            treeNode54.Text = "Mesa 4";
            treeNode55.Name = "Node4";
            treeNode55.Text = "Mesa 5";
            treeNode56.Name = "Node5";
            treeNode56.Text = "Mesa 6";
            treeNode57.Name = "Node6";
            treeNode57.Text = "Mesa 7";
            treeNode58.Name = "Node7";
            treeNode58.Text = "Mesa 8";
            treeNode59.Name = "Node8";
            treeNode59.Text = "Mesa 9";
            treeNode60.Name = "Node9";
            treeNode60.Text = "Mesa 10";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode51,
            treeNode52,
            treeNode53,
            treeNode54,
            treeNode55,
            treeNode56,
            treeNode57,
            treeNode58,
            treeNode59,
            treeNode60});
            this.treeView1.Size = new System.Drawing.Size(200, 282);
            this.treeView1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(209, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(365, 282);
            this.panel3.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.71631F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.28369F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(365, 282);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnCloseTable);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(359, 105);
            this.panel4.TabIndex = 0;
            // 
            // btnCloseTable
            // 
            this.btnCloseTable.Location = new System.Drawing.Point(201, 74);
            this.btnCloseTable.Name = "btnCloseTable";
            this.btnCloseTable.Size = new System.Drawing.Size(123, 23);
            this.btnCloseTable.TabIndex = 2;
            this.btnCloseTable.Text = "Fechar mesa";
            this.btnCloseTable.UseVisualStyleBackColor = true;
            this.btnCloseTable.Click += new System.EventHandler(this.btnCloseTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(273, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(177, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Moccasin;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.udQuantidade);
            this.panel5.Controls.Add(this.btnNovoPedido);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.cbTipo);
            this.panel5.Controls.Add(this.cbDescricao);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 114);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(359, 165);
            this.panel5.TabIndex = 1;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // udQuantidade
            // 
            this.udQuantidade.Location = new System.Drawing.Point(12, 120);
            this.udQuantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udQuantidade.Name = "udQuantidade";
            this.udQuantidade.Size = new System.Drawing.Size(62, 20);
            this.udQuantidade.TabIndex = 10;
            this.udQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // eliminarTolStripMenuItem
            // 
            this.eliminarTolStripMenuItem.Name = "eliminarTolStripMenuItem";
            this.eliminarTolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 324);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Client";
            this.Text = "Cliente Sala";
            this.Load += new System.EventHandler(this.Client_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQuantidade)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNovoPedido;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem eliminarTolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbDescricao;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnCloseTable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown udQuantidade;
      
    }
}

