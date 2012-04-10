using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class OrderMap : MarshalByRefObject, IOrderMap {

    Dictionary<Locations, Dictionary<int, List<Order>>> orders;

    public OrderMap()
    {
        Console.WriteLine("Constructor called.");
        this.orders = new Dictionary<Locations, Dictionary<int, List<Order>>>();

        //dados de teste
        Dictionary<int, List<Order>> d = new Dictionary<int, List<Order>>();
        List<Order> l = new List<Order>();
        d.Add(1,l);
        this.orders.Add(Locations.Bar, d);
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
        //TODO Notify order.location that an order was added
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

}
