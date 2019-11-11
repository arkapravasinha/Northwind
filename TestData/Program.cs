using NothwindDAL;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerDAL customerDAL = new CustomerDAL();
            List<Customer> customers = customerDAL.GetCustomers();
            int pageCount;
            List<Customer> customers1 = customerDAL.GetCustomers(5,out pageCount,pageNo:2);
            Customer customer = customerDAL.GetCustomerDetails("ALFKI");
            

            if ((customers!=null)&&(customers.Count>0))
            {
                foreach (var item in customers)
                {
                    Console.WriteLine(item.CustomerID+"||"+item.ContactName);
                }
            }

            Console.WriteLine("----------------------------------------------------------");

            if ((customers1 != null) && (customers1.Count > 0))
            {
                foreach (var item in customers1)
                {
                    Console.WriteLine(item.CustomerID + "||" + item.ContactName);
                }
            }
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Page Count: "+pageCount);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(customer.CustomerID + "||" + customer.ContactName);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------");
            if (customerDAL.DeleteCustomer("ERNSH"))
            {
                Console.WriteLine("deleted ernsh");
            }
            Console.WriteLine("----------------------------------------------------------");
            Console.ReadLine();
        }
    }
}
