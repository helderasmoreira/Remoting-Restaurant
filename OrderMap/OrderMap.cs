using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Text;

public class OrderMap : MarshalByRefObject, IOrderMap {

    Dictionary<Locations, Dictionary<int, List<Order>>> orders;
    public event OperationDelegate clientEvent;
    public event OperationDelegate barEvent;
    public event OperationDelegate kitchenEvent;
 

    public OrderMap()
    {
        Console.WriteLine("Constructor called.");
 

        this.orders = new Dictionary<Locations, Dictionary<int, List<Order>>>();

        //dados de teste
        Dictionary<int, List<Order>> bar = new Dictionary<int, List<Order>>();
        Dictionary<int, List<Order>> kitchen = new Dictionary<int, List<Order>>();
        for (int i = 0; i < 10; i++)
        {
            List<Order> l = new List<Order>();
            bar.Add(i+1, l);
            List<Order> l2 = new List<Order>();
            kitchen.Add(i + 1, l2);
        }

        this.orders.Add(Locations.Bar, bar);
        this.orders.Add(Locations.Kitchen, kitchen);
    }

    public Dictionary<Locations, Dictionary<int, List<Order>>> GetOrders()
    {
        Console.WriteLine("GetOrders called!");
        return orders;
    }

    public void AddOrder(Order order)
    {

        Console.WriteLine("AddOrder called!");
        orders[order.Location][order.Table].Add(order);
        NotifyClients(Operations.NewOrder, order);

        switch (order.Location)
        {
            case Locations.Bar:
                NotifyWorkers(Operations.NewOrder, order, barEvent);
                break;
            case Locations.Kitchen:
                NotifyWorkers(Operations.NewOrder, order, kitchenEvent);
                break;
        }
 
    }

    public void StartOrder(string orderId)
    {
        Order order = GetOrderById(orderId);
        order.Status = OrderStatus.Started;
        Console.WriteLine("StartOrder called!");        
        NotifyClients(Operations.Started, order);
    }

    public void EndOrder(string orderId)
    {
        Order order = GetOrderById(orderId);
        order.Status = OrderStatus.Finished;
        Console.WriteLine("EndOrder called!");
        NotifyClients(Operations.Finished, order);
    }

    public Order GetOrderById(string id)
    {
        //TODO return order with given id (à não trolha)
        foreach (Dictionary<int, List<Order>> location in orders.Values)
            foreach (List<Order> table in location.Values)
                foreach(Order o in table)
                    if (o.Id.Equals(id))
                        return o;
        
        return null;
    }

    public List<Order> GetOrdersByLocation(Locations location) 
    {   
        return orders[location].Values.SelectMany(x => x).ToList();
    }

    public List<Order> GetOrdersByTable(int table)
    {
        List<List<Order>> l = new List<List<Order>>();
        foreach (Locations loc in orders.Keys) 
            l.Add(orders[loc][table]);
        return l.SelectMany(x => x).ToList();    
    }

    public double GetTableCheck(int table)
    {
        double total = 0.0;
        foreach (Order o in GetOrdersByTable(table))
            total += o.Price;
        return total;
    }

    void NotifyClients(Operations op, Order order)
    {
        if (clientEvent != null)
        {
            Delegate[] invkList = clientEvent.GetInvocationList();

            foreach (OperationDelegate handler in invkList)
            {
                try
                {
                    IAsyncResult ar = handler.BeginInvoke(op, order, null, null);
                    Console.WriteLine("Invoking event handler");
                }
                catch (Exception)
                {
                    clientEvent -= handler;
                }
            }
        }
    }

    void NotifyWorkers(Operations op, Order order, OperationDelegate wd)
    {
        if (wd != null)
        {
            Delegate[] invkList = wd.GetInvocationList();

            foreach (OperationDelegate handler in invkList)
            {
                try
                {
                    IAsyncResult ar = handler.BeginInvoke(op, order, null, null);
                    Console.WriteLine("Invoking event handler");
                }
                catch (Exception)
                {
                    wd -= handler;
                }
            }
        }
    }

}
