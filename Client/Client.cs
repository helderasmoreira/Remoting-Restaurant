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

        }

        private void Client_Load(object sender, EventArgs e)
        {
            foreach (Order order in ordersServer.GetOrdersByLocation(Locations.Bar))
                lbEspera.Items.Add(order.Description);
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
            lbEspera.Items.Add(order.Description);
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
            lbEmPreparacao.Items.Add(order.Description);
            lbEspera.Items.Remove(order.Description);
        }


        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            Order o = new Order(1, 1, 1, 1, ((string)cbDescricao.SelectedItem), OrderStatus.NotStarted, Locations.Bar);
            
            ordersServer.AddOrder(o);
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
