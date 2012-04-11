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
        WorkerEventRepeater evRepeater;
        Locations location;

        public Worker(string[] args)
        {
            InitializeComponent();
            RemotingConfiguration.Configure("Worker.exe.config", false);
            ordersServer = (IOrderMap)RemoteNew.New(typeof(IOrderMap));
            orders = ordersServer.GetOrders();

            evRepeater = new WorkerEventRepeater();
            evRepeater.workerEvent += new WorkerDelegate(NewServerNotification);

            //if(args[0].Equals("-b"))
                ordersServer.barEvent += new WorkerDelegate(evRepeater.Repeater);
               
                this.location = Locations.Bar;
            //else if(args[0].Equals("-k"))
             //   ordersServer.kitchenEvent += new WorkerDelegate(evRepeater.Repeater);
        }

        public void NewServerNotification(Operations op, Order order)
        {
            switch (op)
            {
                case Operations.NewOrder:
                    NewOrderNotification(order);
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
            lbInPreparation.Items.Add(order.Description);
            ordersServer.StartOrder(order);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            switch (location)
            {
                case Locations.Bar:
                    this.Text += "bar";
                    break;
                default:
                    break;
            }

            foreach (Order order in ordersServer.GetOrdersByLocation(location))
                lbWaitingOrders.Items.Add(order.Description);
        }

        private void btnStart_Click(object sender, EventArgs e)
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