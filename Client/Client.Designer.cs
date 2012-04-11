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
            this.cbDescricao = new System.Windows.Forms.ComboBox();
            this.btnNovoPedido = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbInPreparation = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbWaitingOrders = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbOrdersReady = new System.Windows.Forms.ListBox();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDescricao
            // 
            this.cbDescricao.FormattingEnabled = true;
            this.cbDescricao.Items.AddRange(new object[] {
            "Alheira ",
            "Prego",
            "Bacalhau"});
            this.cbDescricao.Location = new System.Drawing.Point(12, 12);
            this.cbDescricao.Name = "cbDescricao";
            this.cbDescricao.Size = new System.Drawing.Size(121, 21);
            this.cbDescricao.TabIndex = 1;
            // 
            // btnNovoPedido
            // 
            this.btnNovoPedido.Location = new System.Drawing.Point(139, 10);
            this.btnNovoPedido.Name = "btnNovoPedido";
            this.btnNovoPedido.Size = new System.Drawing.Size(123, 23);
            this.btnNovoPedido.TabIndex = 2;
            this.btnNovoPedido.Text = "Criar novo pedido";
            this.btnNovoPedido.UseVisualStyleBackColor = true;
            this.btnNovoPedido.Click += new System.EventHandler(this.btnNovoPedido_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNovoPedido);
            this.panel2.Controls.Add(this.cbDescricao);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(522, 60);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(528, 396);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbInPreparation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbWaitingOrders);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbOrdersReady);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 324);
            this.panel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "pedidos a serem feitos";
            // 
            // lbInPreparation
            // 
            this.lbInPreparation.FormattingEnabled = true;
            this.lbInPreparation.Location = new System.Drawing.Point(268, 43);
            this.lbInPreparation.Name = "lbInPreparation";
            this.lbInPreparation.Size = new System.Drawing.Size(120, 95);
            this.lbInPreparation.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "pedidos em espera";
            // 
            // lbWaitingOrders
            // 
            this.lbWaitingOrders.FormattingEnabled = true;
            this.lbWaitingOrders.Location = new System.Drawing.Point(142, 43);
            this.lbWaitingOrders.Name = "lbWaitingOrders";
            this.lbWaitingOrders.Size = new System.Drawing.Size(120, 95);
            this.lbWaitingOrders.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "pedidos prontos";
            // 
            // lbOrdersReady
            // 
            this.lbOrdersReady.FormattingEnabled = true;
            this.lbOrdersReady.Location = new System.Drawing.Point(9, 43);
            this.lbOrdersReady.Name = "lbOrdersReady";
            this.lbOrdersReady.Size = new System.Drawing.Size(120, 95);
            this.lbOrdersReady.TabIndex = 1;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 396);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Client";
            this.Text = "Cliente Sala";
            this.Load += new System.EventHandler(this.Client_Load);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDescricao;
        private System.Windows.Forms.Button btnNovoPedido;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbOrdersReady;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbWaitingOrders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbInPreparation;
    }
}

