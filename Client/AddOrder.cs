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
            this.label8.Text += table;
            pricesKitchen = new double[] { 4.50, 4.00, 6.00, 7.25 };
            pricesBar = new double[] { 5.00, 1.50, 1.00, 3.50 };
            kitchen = new string[] { "Alheira de Mirandela", "Costeleta Grelhada", "Espetada de Porco", "Bife de Frango" };
            bar = new string[] { "Cocktail de frutas", "Fino traçado", "Água com groselha", "Sangria" };
                
        }

        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            double preco;
            if (cbTipo.SelectedItem.Equals("Bar"))
                    preco = Convert.ToDouble(udQuantidade.Value) * pricesBar[cbDescricao.SelectedIndex];
                else
                    preco = Convert.ToDouble(udQuantidade.Value) * pricesKitchen[cbDescricao.SelectedIndex];
            
            ListViewItem lvItem = new ListViewItem(new string[] { cbTipo.Text, cbDescricao.Text, udQuantidade.Value.ToString(), preco.ToString() + "€" });
            listView1.Items.Add(lvItem);
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDescricao.Items.Clear();
            cbDescricao.Text = "";
            if (cbTipo.SelectedItem.Equals("Bar"))
                foreach (string s in bar)
                    cbDescricao.Items.Add(s);
            else
                foreach (string s in kitchen)
                    cbDescricao.Items.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem lv in listView1.Items) {
                Order o;
                if (lv.SubItems[0].Equals("Bar"))
                    o = new Order(DateTime.Now, table, Convert.ToInt32(lv.SubItems[2]), Convert.ToDouble(lv.SubItems[3]), Convert.ToString(lv.SubItems[1]), OrderStatus.NotStarted, Locations.Bar);
                else
                    o = new Order(DateTime.Now, table, Convert.ToInt32(lv.SubItems[2]), Convert.ToDouble(lv.SubItems[3]), Convert.ToString(lv.SubItems[1]), OrderStatus.NotStarted, Locations.Kitchen);
                orders.Add(o);
            }
        }
    }
}
