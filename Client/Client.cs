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
        Dictionary<Locations, Dictionary<int, List<Order>>> orders;
        ClientEventRepeater evRepeater;
       
        public Client()
        {
            RemotingConfiguration.Configure("Client.exe.config", false);
            InitializeComponent();
            ordersServer = (IOrderMap)RemoteNew.New(typeof(IOrderMap));
            orders = ordersServer.GetOrders();
            evRepeater = new ClientEventRepeater();
            evRepeater.clientEvent += new ClientDelegate(NewServerNotification);
            ordersServer.clientEvent += new ClientDelegate(evRepeater.Repeater);

            // dados de teste
            Order o = new Order(1, 1, 1, 1, "teste", OrderStatus.NotStarted, Locations.Bar);
            ordersServer.AddOrder(o);
            foreach (Order order in ordersServer.GetOrdersByLocation(Locations.Bar))
                textBox1.Text = textBox1.Text + order.Description;
            
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        public void NewServerNotification(ClientOperations op, Order order)
        {
            switch (op)
            {
                case ClientOperations.NewOrder:
                    NewServerNotification2();
                    break;
                case ClientOperations.Started:
                    NewServerNotification3();
                    break;
            } 
        }

        public void NewServerNotification2()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(NewServerNotification2));
            else
            {
                textBox1.Text = textBox1.Text + " Nova ordem ";
            } 
        }

        public void NewServerNotification3()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(NewServerNotification3));
            else
            {
                textBox1.Text = textBox1.Text + " Ordem Começada ";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
