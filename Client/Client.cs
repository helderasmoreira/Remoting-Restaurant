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
        public double[] prices;
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

            prices = new double[] { 10.0, 20.0, 30.0 };
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            label1.Text = this.treeView1.Nodes[0].Text;
            label7.Text = "0 €";

            treeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
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
            TreeNode tn = new TreeNode(order.Quantity.ToString() + " - " + order.Description);
            tn.BackColor = Color.Red;
            tn.Name = order.Id;

            treeView1.Nodes[order.Table - 1].Nodes.Add(tn);
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

            foreach (TreeNode t in treeView1.Nodes[order.Table - 1].Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.Yellow;
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

            foreach (TreeNode t in treeView1.Nodes[order.Table - 1].Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.Green;
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

            if (cbTipo.SelectedItem.Equals("Bar"))
                o = new Order(treeView1.Nodes.IndexOf(tn) + 1, Convert.ToInt32(tbQuantidade.Text), prices[cbDescricao.SelectedIndex] * Convert.ToDouble(tbQuantidade.Text), ((string)cbDescricao.SelectedItem), OrderStatus.NotStarted, Locations.Bar);
            else
                o = new Order(treeView1.Nodes.IndexOf(tn) + 1, Convert.ToInt32(tbQuantidade.Text), prices[cbDescricao.SelectedIndex] * Convert.ToDouble(tbQuantidade.Text), ((string)cbDescricao.SelectedItem), OrderStatus.NotStarted, Locations.Kitchen);
            ordersServer.AddOrder(o);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
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
