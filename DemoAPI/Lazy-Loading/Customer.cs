using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI
{
    public class Customer
    {
        private int _noofOrders;
        public String CustomerID { get; set; }
        public string CustomerName { get; set; }
        private Lazy<List<Order>> _orders;

        public Customer(int noofOrders,string _CustomerName)
        {
            _noofOrders = noofOrders;
            CustomerID = Guid.NewGuid().ToString();
            CustomerName = _CustomerName;                     
        }

        public List<Order> orders
        {
            get
            { 
                _orders = new Lazy<List<Order>>(() => CreateOrders(_noofOrders));
                return _orders.Value;
            }                 
        }

        private List<Order> CreateOrders(int noofOrders)
        {
            List<Order> orders = new List<Order>();
            for (int i =0; i <= noofOrders; i++)
            {
                orders.Add(new Order(Guid.NewGuid().ToString()));
            }

            return orders;
        }
    }

    public class Order
    {
        public Order(String _OrderID)
        {
            OrderID = _OrderID;
        }
        public String OrderID { get; set; }
    }
}
