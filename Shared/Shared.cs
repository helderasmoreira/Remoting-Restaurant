using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

[Serializable]
public class Order {

    string id;
    int table, quantity;
    double price;

  
    string description;
    OrderStatus status;
    Locations location;

    public Order(int table, int quantity, double price, string description, OrderStatus status, Locations location)
    {
        this.id = GenerateId();
        this.table = table;
        this.quantity = quantity;
        this.price = price;
        this.description = description;
        this.status = status;
        this.location = location;
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public int Table
    {
        get { return table; }
        set { table = value; }
    }

    public Locations Location
    {
        get { return location; }
        set { location = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public OrderStatus Status
    {
        get { return status; }
        set { status = value; }
    }

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public string GenerateId()
    {
     long i = 1;
     foreach (byte b in Guid.NewGuid().ToByteArray())
     {
      i *= ((int)b + 1);
     }
     return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }
}

public delegate void OperationDelegate(Operations op, Order order);

public interface IOrderMap {

    event OperationDelegate clientEvent;

    event OperationDelegate barEvent;
    event OperationDelegate kitchenEvent;
    

    Dictionary<Locations, Dictionary<int, List<Order>>> GetOrders();
    void AddOrder(Order order);
    void StartOrder(string orderId);
    void EndOrder(string orderId);
    List<Order> GetOrdersByLocation(Locations location);
    List<Order> GetOrdersByTable(int table);
    double GetTableCheck(int table);
    Order GetOrderById(string id);

}

public enum Operations { NewOrder, Checkout, Started, Finished };

public enum OrderStatus { NotStarted, Started, Finished };

public enum Locations { Kitchen, Bar };

public class OperationEventRepeater : MarshalByRefObject
{
    public event OperationDelegate operationEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(Operations op, Order order)
    {
        if (operationEvent != null)
            operationEvent(op, order);
    }
}

