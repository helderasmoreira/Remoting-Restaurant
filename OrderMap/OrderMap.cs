using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Text;

public class OrderMap : MarshalByRefObject, IOrderMap {

    Dictionary<Locations, Dictionary<String, List<Order>>> orders;
    public event OperationDelegate clientEvent;
    public event OperationDelegate barEvent;
    public event OperationDelegate kitchenEvent;
 

    public OrderMap()
    {
        Console.WriteLine("Constructor called.");
 

        this.orders = new Dictionary<Locations, Dictionary<String, List<Order>>>();

        //dados de teste
        Dictionary<String, List<Order>> bar = new Dictionary<String, List<Order>>();
        Dictionary<String, List<Order>> kitchen = new Dictionary<String, List<Order>>();
        for (int i = 0; i < 10; i++)
        {
            List<Order> l = new List<Order>();
            bar.Add("Mesa " + (i+1), l);
            List<Order> l2 = new List<Order>();
            kitchen.Add("Mesa " + (i + 1), l2);
        }

        this.orders.Add(Locations.Bar, bar);
        this.orders.Add(Locations.Kitchen, kitchen);
    }

    public Dictionary<Locations, Dictionary<String, List<Order>>> GetOrders()
    {
        Console.WriteLine("GetOrders called!");
        return orders;
    }

    public void AddOrder(Order order)
    {

        Console.WriteLine("AddOrder called!");
        if(order.Price >= 0.0)
            orders[order.Location][order.Table].Add(order);
        else if (order.Price == -1.0)
        {
            List<Order> l = new List<Order>();
            List<Order> l2 = new List<Order>();
            orders[Locations.Bar].Add(order.Table, l);
            orders[Locations.Kitchen].Add(order.Table, l2);
            NotifyClients(Operations.NewOrder, order);
            NotifyWorkers(Operations.NewOrder, order, barEvent);
            NotifyWorkers(Operations.NewOrder, order, kitchenEvent);
            return;
        }
        else if(order.Price == -2.0)
        {
            orders[Locations.Bar].Remove(order.Table);
            orders[Locations.Kitchen].Remove(order.Table);
            NotifyClients(Operations.NewOrder, order);
            NotifyWorkers(Operations.NewOrder, order, barEvent);
            NotifyWorkers(Operations.NewOrder, order, kitchenEvent);
            return;
        }

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

        if (order.Location == Locations.Bar)
        {
            NotifyWorkers(Operations.Started, order, barEvent);
        }
        else
            NotifyWorkers(Operations.Started, order, kitchenEvent);
    }

    public string GetTableTime(string id)
    {
        if (orders[Locations.Bar].ContainsKey(id) && orders[Locations.Kitchen].ContainsKey(id))
        {
            if (orders[Locations.Bar][id].Count() == 0 && orders[Locations.Kitchen][id].Count() == 0)
                return " - ";
            DateTime dt = DateTime.Now;
            foreach (Order o in GetOrdersByTable(id))
                if (o.Time.CompareTo(dt) == -1)
                    dt = o.Time;

            return dt.ToShortTimeString();
        }
        return "";
    }

    public void EndOrder(string orderId)
    {
        Order order = GetOrderById(orderId);
        order.Status = OrderStatus.Finished;
        Console.WriteLine("EndOrder called!");
        NotifyClients(Operations.Finished, order);

        if (order.Location == Locations.Bar)
            NotifyWorkers(Operations.Finished, order, barEvent);
        else
            NotifyWorkers(Operations.Finished, order, kitchenEvent);
    }

    public Order GetOrderById(string id)
    {
        //TODO return order with given id (à não trolha)
        foreach (Dictionary<string, List<Order>> location in orders.Values)
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

    public List<Order> GetOrdersByTable(string table)
    {
        List<List<Order>> l = new List<List<Order>>();
        foreach (Locations loc in orders.Keys)
        {
            if(orders[loc].ContainsKey(table))
                l.Add(orders[loc][table]);
        }
        return l.SelectMany(x => x).ToList();    
    }

    public double GetTableCheck(string table)
    {
        double total = 0.0;
        foreach (Order o in GetOrdersByTable(table))
            total += o.Price;
        return total;
    }

    public int CloseTable(string id)
    {
        List<Order> orders = GetOrdersByTable(id);
        int ret = 0;
        foreach (Order o in orders)
        {
            if (o.Status == OrderStatus.Started)
                return -1;
            else if (o.Status == OrderStatus.NotStarted)
                ret = -2;
        }

        if (ret == 0)
        {
            foreach (Order o in orders)
            {
                NotifyClients(Operations.Removed, o);
                if(o.Location == Locations.Bar)
                    NotifyWorkers(Operations.Removed, o, barEvent);
                else
                    NotifyWorkers(Operations.Removed, o, kitchenEvent);
            }
            RemoveAllOrdersTable(id);
        }
            
        return ret;
    }

    public void RemoveOrderById(string id)
    {
        foreach (Dictionary<string, List<Order>> location in orders.Values)
            foreach (List<Order> table in location.Values)
                foreach (Order o in table)
                    if (o.Id.Equals(id))
                    {
                        table.Remove(o);
                        NotifyClients(Operations.Removed, o);
                        if (o.Location == Locations.Bar)
                            NotifyWorkers(Operations.Removed, o, barEvent);
                        else
                            NotifyWorkers(Operations.Removed, o, kitchenEvent);
                        return;
                    }
    }

    void RemoveAllOrdersTable(string id)
    {
         orders[Locations.Bar][id].Clear();
         orders[Locations.Kitchen][id].Clear();
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
