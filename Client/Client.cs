using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        IOrderMap ordersServer;
        public double[] pricesBar;
        public double[] pricesKitchen;
        public string[] bar;
        public string[] kitchen;
        Dictionary<Locations, Dictionary<int, List<Order>>> orders;
        OperationEventRepeater evRepeater;
       
        public Client()
        {
            
            RemotingConfiguration.Configure("Client.exe.config", false);
            InitializeComponent();
            ordersServer = (IOrderMap)RemoteNew.New(typeof(IOrderMap));
            orders = ordersServer.GetOrders();
            evRepeater = new OperationEventRepeater();
            evRepeater.operationEvent += new OperationDelegate(NewServerNotification);
            ordersServer.clientEvent += new OperationDelegate(evRepeater.Repeater);

        }

        private void Client_Load(object sender, EventArgs e)
        {
            //foreach (Order order in ordersServer.GetOrdersByLocation(Locations.Bar))
              //  lbWaitingOrders.Items.Add(order.Id);

            pricesKitchen = new double[] { 4.50, 4.00, 6.00, 7.25 };
            pricesBar = new double[] { 5.00, 1.50, 1.00, 3.50 };
            kitchen = new string[] {"Alheira de Mirandela", "Costeleta Grelhada", "Espetada de Porco", "Bife de Frango"};
            bar = new string[] {"Cocktail de frutas", "Fino traçado", "Água com groselha", "Sangria"};
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            label1.Text = this.treeView1.Nodes[0].Text;
            label7.Text = "0 €";

            treeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
            treeView1.MouseDown += new MouseEventHandler(treeView1_mouseDown);
        }

        public void NewServerNotification(Operations op, Order order)
        {
            switch (op)
            {
                case Operations.NewOrder:
                    NewOrderNotification(order);
                    break;
                case Operations.Started:
                    OrderStartedNotification(order);
                    break;
                case Operations.Finished:
                    OrderFinishedNotification(order);
                    break;
                case Operations.Removed:
                    OrderRemovedNotification(order);
                    break;
              
            } 
        }

        public void NewOrderNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    NewOrderNotification(order);
                });
                return;
            }
            TreeNode tn = new TreeNode(order.Description);
            tn.BackColor = Color.IndianRed;
            tn.Name = order.Id;

            treeView1.Nodes[order.Table - 1].Nodes.Add(tn);

            treeView1.Nodes[order.Table - 1].Expand();
            label7.Text = ordersServer.GetTableCheck(order.Table).ToString() + " €";
        }

        public void OrderRemovedNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OrderRemovedNotification(order);
                });
                return;
            }
            
            foreach (TreeNode t in treeView1.Nodes[order.Table - 1].Nodes) 
            {
                if (t.Name == order.Id)
                {
                    treeView1.Nodes[order.Table - 1].Nodes.Remove(t);
                    break;
                }
            }
           
            label7.Text = ordersServer.GetTableCheck(order.Table).ToString() + " €";
        }

        public void OrderStartedNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OrderStartedNotification(order);
                });
                return;
            }

            treeView1.Nodes[order.Table - 1].Expand();
            foreach (TreeNode t in treeView1.Nodes[order.Table - 1].Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.Khaki;
                    break;
                }
            }

        }

        public void OrderFinishedNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OrderFinishedNotification(order);
                });
                return;
            }

            treeView1.Nodes[order.Table - 1].Expand();
            foreach (TreeNode t in treeView1.Nodes[order.Table - 1].Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.LightGreen;
                    break;
                }
            }

        }


        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            Order o;
            TreeNode tn = treeView1.SelectedNode;
            
            //assumindo que só temos 2 niveis
            if (tn.Parent != null)
                tn = tn.Parent;

            if (cbDescricao.SelectedItem == null || cbTipo.SelectedItem == null)
            {
                MessageBox.Show("Tem que preencher todos os campos para criar um pedido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                

            for (int i = 0; i < udQuantidade.Value; i++)
            {
                if (cbTipo.SelectedItem.Equals("Bar"))
                    o = new Order(treeView1.Nodes.IndexOf(tn) + 1, 1, pricesBar[cbDescricao.SelectedIndex], ((string)cbDescricao.SelectedItem), OrderStatus.NotStarted, Locations.Bar);
                else
                    o = new Order(treeView1.Nodes.IndexOf(tn) + 1, 1, pricesKitchen[cbDescricao.SelectedIndex], ((string)cbDescricao.SelectedItem), OrderStatus.NotStarted, Locations.Kitchen);
                ordersServer.AddOrder(o);
            }
        }

        private void TreeView1_AfterSelect(System.Object sender,
        System.Windows.Forms.TreeViewEventArgs e)
        {
            if(e.Node.Parent == null) {
                label1.Text = e.Node.Text;
                label7.Text = ordersServer.GetTableCheck(treeView1.Nodes.IndexOf(e.Node) + 1).ToString() +" €";
            }
        }

        private void btnCloseTable_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn.Parent != null)
                tn = tn.Parent;

            string value = ordersServer.GetTableCheck(treeView1.Nodes.IndexOf(tn) + 1).ToString() + " €";
            int ret = ordersServer.CloseTable(treeView1.Nodes.IndexOf(tn) + 1);
            if(ret == -1)
                MessageBox.Show("Impossível fechar mesa, pois há pedidos pendentes!", "Fechar Mesa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(ret == -2)
                MessageBox.Show("Ainda tem pedidos que não foram começados. Elimine-os primeiro e tente novamente!", "Fechar Mesa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Mesa fechada com sucesso! Total a pagar: " + value, "Fechar Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void treeView1_mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
                if (treeView1.GetNodeAt(e.X, e.Y).Parent != null)
                    contextMenuStrip1.Show(MousePosition);
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                switch (ordersServer.GetOrderById(treeView1.SelectedNode.Name).Status)
                {
                    case OrderStatus.Started:
                        MessageBox.Show("Não é possível eliminar o pedido seleccionado visto que já está a ser preparado.");
                        break;
                    case OrderStatus.Finished:
                        MessageBox.Show("Não é possível eliminar o pedido seleccionado visto que já está a ser preparado.");
                        break;
                    default:
                        if (MessageBox.Show("Tem a certeza que pretende eliminar o pedido?", "Eliminar pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            ordersServer.RemoveOrderById(treeView1.SelectedNode.Name);
                        }
                        break;
                }
            }
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

        private void tbQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

   

    }
}

/* Mechanism for instanciating a remote object through its interface, using the config file */

class RemoteNew
{
    private static Hashtable types = null;

    private static void InitTypeTable()
    {
        types = new Hashtable();
        foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            types.Add(entry.ObjectType, entry);
    }

    public static object New(Type type)
    {
        if (types == null)
            InitTypeTable();
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry)types[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}
