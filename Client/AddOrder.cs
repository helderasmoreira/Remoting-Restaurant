using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Client
{
    public partial class AddOrder : Form
    {

        public double[] pricesBar;
        public double[] pricesKitchen;
        public string[] bar;
        public string[] kitchen;
        public ArrayList orders;
        public string table;

        public AddOrder(ref ArrayList orders, string table)
        {
            
            this.table = table;
           
            this.orders = orders;
            InitializeComponent();
        }

      

        private void AddOrder_Load(object sender, EventArgs e)
        {
            this.Text = table;
            this.label8.Text += " - " + table;
            pricesKitchen = new double[] { 4.50, 4.00, 6.00, 7.25 };
            pricesBar = new double[] { 5.00, 1.50, 1.00, 3.50 };
            kitchen = new string[] { "Alheira de Mirandela", "Costeleta Grelhada", "Espetada de Porco", "Bife de Frango" };
            bar = new string[] { "Cocktail de frutas", "Fino traçado", "Água com groselha", "Sangria" };
                
        }

        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            double preco;
            ListViewItem lvItem;

            if ((!radioButton1.Checked && !radioButton2.Checked) || cbDescricao.SelectedItem == null)
                return;

            if (radioButton2.Checked)
            {
                preco = Convert.ToDouble(udQuantidade.Value) * pricesBar[cbDescricao.SelectedIndex];
                lvItem = new ListViewItem(new string[] { radioButton2.Text, cbDescricao.Text, udQuantidade.Value.ToString(), preco.ToString() });
            }
            else
            {
                preco = Convert.ToDouble(udQuantidade.Value) * pricesKitchen[cbDescricao.SelectedIndex];
                lvItem = new ListViewItem(new string[] { radioButton1.Text, cbDescricao.Text, udQuantidade.Value.ToString(), preco.ToString() });
            }
            
            
            listView1.Items.Add(lvItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem lv in listView1.Items) {
                Order o;
                for (int i = 0; i < Convert.ToInt32(lv.SubItems[2].Text); i++ )
                {
                    if (lv.SubItems[0].Text.Equals("Bar"))
                        o = new Order(DateTime.Now, table, 1, Convert.ToDouble(lv.SubItems[3].Text) / Convert.ToDouble(lv.SubItems[2].Text), lv.SubItems[1].Text, OrderStatus.NotStarted, Locations.Bar);
                    else
                        o = new Order(DateTime.Now, table, 1, Convert.ToDouble(lv.SubItems[3].Text) / Convert.ToDouble(lv.SubItems[2].Text), lv.SubItems[1].Text, OrderStatus.NotStarted, Locations.Kitchen);
                    orders.Add(o);
                }
            }
            this.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0] != null)
                listView1.Items.Remove(listView1.SelectedItems[0]);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cbDescricao.Items.Clear();
            cbDescricao.Text = "";
            label3.Text = "- €";
            if (radioButton2.Checked)
                foreach (string s in bar)
                    cbDescricao.Items.Add(s);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cbDescricao.Items.Clear();
            cbDescricao.Text = "";
            label3.Text = "- €";
            if (radioButton1.Checked)
                foreach (string s in kitchen)
                    cbDescricao.Items.Add(s);
        }

        private void cbDescricao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDescricao.SelectedItem == null)
            {
                label3.Text = "- €";
                return;
            }

            if (radioButton2.Checked)
                label3.Text = pricesBar[cbDescricao.SelectedIndex].ToString() + " €";
            else
                label3.Text = pricesKitchen[cbDescricao.SelectedIndex].ToString() + " €";
        }
    }
}
