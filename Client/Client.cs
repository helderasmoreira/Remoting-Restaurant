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
       
        Dictionary<Locations, Dictionary<String, List<Order>>> orders;
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
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            label1.Text = this.treeView1.Nodes[0].Text;
            label7.Text = "0 €";

           
            foreach (TreeNode tn2 in treeView1.Nodes)
            {
                foreach (Order order in ordersServer.GetOrdersByTable(tn2.Text))
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

                    tn2.Nodes.Add(tn);
                    tn2.Expand();
                }
            }
        
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
            if (order.Price >= 0)
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
                label7.Text = ordersServer.GetTableCheck(order.Table).ToString() + " €";
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


        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            ArrayList newOrder = new ArrayList();
            AddOrder ao = new AddOrder(ref newOrder, treeView1.SelectedNode.Name);
            ao.ShowDialog();
        }

        private void TreeView1_AfterSelect(System.Object sender,
        System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                label1.Text = e.Node.Text;
                label7.Text = ordersServer.GetTableCheck(e.Node.Text) + " €";
                label3.Text = ordersServer.GetTableTime(e.Node.Text);
            }
            else
            {
                label1.Text = e.Node.Parent.Text;
                label7.Text = ordersServer.GetTableCheck(e.Node.Parent.Text) + " €";
                label3.Text = ordersServer.GetTableTime(e.Node.Parent.Text);
            }


        }

        private void btnCloseTable_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn.Parent != null)
                tn = tn.Parent;

            string value = ordersServer.GetTableCheck(tn.Name).ToString() + " €";
            int ret = ordersServer.CloseTable(tn.Text);
            if (ret == -1)
                MessageBox.Show("Impossível fechar mesa, pois há pedidos pendentes!", "Fechar Mesa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (ret == -2)
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
                else if (treeView1.GetNodeAt(e.X, e.Y).Parent == null)
                    contextMenuStrip2.Show(MousePosition);
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

        private void addTable_Click(object sender, EventArgs e)
        {
            if (!tableID.Text.Equals(""))
            {
                String mesa = "Mesa " + tableID.Text;
                foreach(TreeNode t in treeView1.Nodes)
                {
                    if (t.Text.Equals(mesa))
                        return; 
                }

                Order o = new Order(DateTime.Now, mesa, 1, -1.0, mesa, OrderStatus.NotStarted, Locations.Bar);

               ordersServer.AddOrder(o);
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("Não é possível eliminar a mesa pois contém pedidos. ");
            }
            else
            {
                Order o = new Order(DateTime.Now, treeView1.SelectedNode.Text, 1, -2.0, treeView1.SelectedNode.Text, OrderStatus.NotStarted, Locations.Bar);

                ordersServer.AddOrder(o);

                //treeView1.Nodes.Remove(treeView1.SelectedNode);
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
