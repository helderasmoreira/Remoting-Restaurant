﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

[Serializable]
public class Order {

    int id, table, quantity, price;
    string description;
    OrderStatus status;
    Locations location;

    public Order(int id, int table, int quantity, int price, string description, OrderStatus status, Locations location)
    {
        this.id = id;
        this.table = table;
        this.quantity = quantity;
        this.price = price;
        this.description = description;
        this.status = status;
        this.location = location;
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
}

public delegate void ClientDelegate(ClientOperations op, Order order);

public delegate void WorkerDelegate(WorkerOperations op, Order order);

public interface IOrderMap {

    Dictionary<Locations, Dictionary<int, List<Order>>> GetOrders();
    void AddOrder(Order order);
    List<Order> GetOrdersByLocation(Locations location);
    List<Order> GetOrdersByTable(int table);

}

public enum ClientOperations { Order, Checkout };

public enum WorkerOperations { Started, Finished };

public enum OrderStatus { NotStarted, Started, Finished };

public enum Locations { Kitchen, Bar };

