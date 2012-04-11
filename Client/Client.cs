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

        }


        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            Order o;
            TreeNode tn = treeView1.SelectedNode;
            
            //assumindo que só temos 2 niveis
            if (tn.Parent != null)
                tn = tn.Parent;

            if (cbTipo.SelectedText.Equals("Bar"))
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
