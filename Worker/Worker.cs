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
            //else if(args[0].Equals("-k"))
             //   ordersServer.kitchenEvent += new WorkerDelegate(evRepeater.Repeater);
        }

        public void NewServerNotification(Operations op, Order order)
        {
            NewServerNotification2();
            textBox1.Text = textBox1.Text + " vou começar a order ";
            ordersServer.StartOrder(order);
        }

        public void NewServerNotification2()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(NewServerNotification2));
            else
            {
                textBox1.Text = textBox1.Text + " Nova ordem de back ";
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
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