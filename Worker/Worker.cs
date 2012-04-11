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
        Dictionary<Locations, Dictionary<int, List<Order>>> orders;
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

                //TODO
                case Operations.Started:
                    break;
                case Operations.Finished:
                    break;
                default:
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
            

            lbWaitingOrders.Items.Add(order.Id);
            ordersServer.StartOrder(order.Id);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            switch (location)
            {
                case Locations.Bar:
                    this.Text += "bar";
                    break;
                case Locations.Kitchen:
                    this.Text += "kitchen";
                    break;
                default:
                    break;
            }

            foreach (Order order in ordersServer.GetOrdersByLocation(location))
                lbWaitingOrders.Items.Add(order.Id);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ordersServer.StartOrder(((string)lbWaitingOrders.SelectedItem));
   
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            ordersServer.EndOrder(((string)lbWaitingOrders.SelectedItem));
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