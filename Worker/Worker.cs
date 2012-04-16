using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Threading;

namespace Worker
{
    public partial class Worker : Form
    {
        IOrderMap ordersServer;
        Dictionary<Locations, Dictionary<String, List<Order>>> orders;
        OperationEventRepeater evRepeater;
        Locations location;

        public Worker(string[] args)
        {
            InitializeComponent();
            RemotingConfiguration.Configure("Worker.exe.config", false);
            ordersServer = (IOrderMap)RemoteNew.New(typeof(IOrderMap));
            orders = ordersServer.GetOrders();

            evRepeater = new OperationEventRepeater();
            evRepeater.operationEvent += new OperationDelegate(NewServerNotification);

            if (args[0].Equals("-b"))
            {
                ordersServer.barEvent += new OperationDelegate(evRepeater.Repeater);
                this.location = Locations.Bar;
            }
            else if (args[0].Equals("-k"))
            {
                ordersServer.kitchenEvent += new OperationDelegate(evRepeater.Repeater);
                this.location = Locations.Kitchen;
            }
        }

        public void NewServerNotification(Operations op, Order order)
        {
            switch (op)
            {
                case Operations.NewOrder:
                    NewOrderNotification(order);
                    break;
                case Operations.Started:
                    StartedOrderNotification(order);
                    break;
                case Operations.Finished:
                    FinishedNotification(order);
                    break;
                case Operations.Removed:
                    RemovedNotification(order);
                    break;
                default:
                    break;
            } 
 
        }

        public void StartedOrderNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartedOrderNotification(order);
                });
                return;
            }

            TreeNode temp = null;

            foreach (TreeNode tn2 in treeView1.Nodes)
                if (tn2.Text.Equals(order.Table))
                    temp = tn2;

            temp.Expand();
            foreach (TreeNode t in temp.Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.Khaki;
                    break;
                }
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

            if (order.Price >= 0.0)
            {
                TreeNode tn = new TreeNode(order.Description);
                tn.BackColor = Color.IndianRed;
                tn.Name = order.Id;
                TreeNode temp = null;

                foreach (TreeNode tn2 in treeView1.Nodes)
                    if (tn2.Text.Equals(order.Table))
                        temp = tn2;

                temp.Nodes.Add(tn);

                temp.Expand();
                label3.Text = ordersServer.GetTableTime(order.Table);
            }
            else if (order.Price == -1.0)
            {
                treeView1.Nodes.Add(order.Description);
            }
            else if (order.Price == -2.0)
            {
                foreach (TreeNode tn in treeView1.Nodes)
                {
                    if (tn.Text.Equals(order.Description))
                    {
                        treeView1.Nodes.Remove(tn);
                        return;
                    }
                }
            }
        }

        public void RemovedNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    RemovedNotification(order);
                });
                return;
            }

            TreeNode temp = null;

            foreach (TreeNode tn2 in treeView1.Nodes)
                if (tn2.Text.Equals(order.Table))
                    temp = tn2;

            foreach (TreeNode t in temp.Nodes)
            {
                if (t.Name == order.Id)
                {
                    temp.Nodes.Remove(t);
                    break;
                }
            }
        }

        public void FinishedNotification(Order order)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    FinishedNotification(order);
                });
                return;
            }

            TreeNode temp = null;

            foreach (TreeNode tn2 in treeView1.Nodes)
                if (tn2.Text.Equals(order.Table))
                    temp = tn2;

            temp.Expand();
            foreach (TreeNode t in temp.Nodes)
            {
                if (t.Name == order.Id)
                {
                    t.BackColor = Color.LightGreen;
                    break;
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            switch (location)
            {
                case Locations.Bar:
                    label8.Text = "Bar";
                    break;
                case Locations.Kitchen:
                    label8.Text = "Kitchen";
                    break;
                default:
                    break;
            }

            foreach (Order order in ordersServer.GetOrdersByLocation(location))
            {
                TreeNode tn = new TreeNode(order.Description);
                switch (order.Status)
                {
                    case OrderStatus.Finished:
                        tn.BackColor = Color.LightGreen;
                        break;
                    case OrderStatus.Started:
                        tn.BackColor = Color.Khaki;
                        break;
                    case OrderStatus.NotStarted:
                        tn.BackColor = Color.IndianRed;
                        break;
                }
                tn.Name = order.Id;
                TreeNode temp = null;

                foreach (TreeNode tn2 in treeView1.Nodes)
                    if (tn2.Text.Equals(order.Table))
                        temp = tn2;
                temp.Nodes.Add(tn);
                
            }
            treeView1.ExpandAll();
            label1.Text = "Mesa 1";
            label3.Text = ordersServer.GetTableTime("Mesa 1");
            treeView1.SelectedNode = treeView1.Nodes[0];
            button1.Text = "Começar pedido";
            button1.Enabled = false;


            treeView1.AfterSelect += new TreeViewEventHandler(TreeView1_AfterSelect);
        }

        private void TreeView1_AfterSelect(System.Object sender,
        System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                label1.Text = e.Node.Text;
                label3.Text = ordersServer.GetTableTime(e.Node.Text);
                button1.Text = "Começar pedido";
                button1.Enabled = false;
            }
            else {
                switch (ordersServer.GetOrderById(treeView1.SelectedNode.Name).Status)
                {
                case OrderStatus.NotStarted:
                    button1.Text = "Começar pedido";
                    button1.Enabled = true;
                    break;
                case OrderStatus.Started:
                    button1.Text = "Terminar pedido";
                    button1.Enabled = true;
                    break;
                case OrderStatus.Finished:
                    button1.Enabled = false;
                    break;
                default:
                    break;
                }

                label1.Text = e.Node.Parent.Text;
                label3.Text = ordersServer.GetTableTime(e.Node.Parent.Name);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Parent == null)
                return;

            switch (ordersServer.GetOrderById(treeView1.SelectedNode.Name).Status)
            {
                case OrderStatus.NotStarted:
                    ordersServer.StartOrder(treeView1.SelectedNode.Name);
                    button1.Text = "Terminar pedido";
                    button1.Enabled = true;
                    break;
                case OrderStatus.Started:
                    ordersServer.EndOrder(treeView1.SelectedNode.Name);
                    button1.Enabled = false;
                    break;
                default:
                    break;

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