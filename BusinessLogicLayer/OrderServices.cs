using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Utilities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BusinessLogicLayer
{
    internal class OrderServices
    {
        //Implement CRUD operations   

        //Read
        internal static String GetOrder(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderQuery =
                from o in dbContext.Orders
                where o.OrderId == n
                select new Order
                {
                    OrderId = o.OrderId,
                    RevisionNumber = o.RevisionNumber,
                    OrderDate = o.OrderDate,
                    ShipDate = o.ShipDate,
                    Status = o.Status,
                    OrderNumber = o.OrderNumber,
                    OnlineOrderFlag = o.OnlineOrderFlag,
                    PurchaseOrderNumber = o.PurchaseOrderNumber,
                    AccountNumber = o.AccountNumber,
                    CustomerId = o.CustomerId,
                    ShipToAddress = o.ShipToAddress,
                    BillToAddress = o.BillToAddress,
                    ShipMethod = o.ShipMethod,
                    CreditCardApprovalCode = o.CreditCardApprovalCode,
                    SubTotal = o.SubTotal,
                    TaxAmt = o.TaxAmt,
                    Freight = o.Freight,
                    TotalDue = o.TotalDue,
                    Comment = o.Comment,
                    ModifiedDate = o.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = orderQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }

        internal static String GetOrderDetails(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderDetailsQuery =
                from od in dbContext.OrderDetails
                where od.OrderDetailId == n
                select new OrderDetail
                {
                    OrderId = od.OrderId,
                    OrderDetailId = od.OrderDetailId,
                    OrderQty = od.OrderQty,
                    ProductId = od.ProductId,
                    UnitPrice = od.UnitPrice,
                    UnitPriceDiscount = od.UnitPriceDiscount,
                    LineTotal = od.LineTotal,
                    ModifiedDate = od.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = orderDetailsQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }


        //Create
        internal static void CreateOrder()
        {
            Random random = new Random();
            RetailDbContext dbContext = new RetailDbContext();

            Console.Write("Enter Order Revision Number: ");
            int RevisionNumber = InputValidator.validateInputNum(50);

            Console.Write("Enter Order Status: ");
            int OrderStatus = InputValidator.validateInputNum(6);

            int maxCustomerId = Services.getTableInfo(nameof(Tables.Customers));
            Console.Write("Enter Order Customer Id: ");
            int customerId = InputValidator.validateInputNum(maxCustomerId);

            int maxShipAddressId = Services.getTableInfo(nameof(Tables.Addresses));
            Console.Write("Enter Order Ship Address Id: ");
            int shipAddressId = InputValidator.validateInputNum(maxShipAddressId);

            int maxBillAddressId = Services.getTableInfo(nameof(Tables.Addresses));
            Console.Write("Enter Order Bill Address Id: ");
            int billAddressId = InputValidator.validateInputNum(maxBillAddressId);

            Console.Write("Enter Order Ship Method: ");
            String ShipMethod = InputValidator.validateInputText(50);

            Console.Write("Enter Order SubTotal: ");
            double Subtotal = InputValidator.validateInputNum(1000000);

            double percentage = ((double)random.NextInt64(5, 25)) / 100;
            double TaxAmount = Subtotal * percentage;

            Console.Write("Enter Order Freight: ");
            double Freight = InputValidator.validateInputNum(100000);

            double TotalDue = Subtotal + TaxAmount + Freight;

            Console.Write("Enter Order Comment: ");
            String Comment = InputValidator.validateInputText(10000);

            Order order = new Order()
            {
                RevisionNumber = (byte)RevisionNumber,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddHours(random.NextInt64(1460)),
                Status = (byte)OrderStatus,
                OnlineOrderFlag = false,
                PurchaseOrderNumber = "PO" + RandomStringGenerator.RandomString(10),
                AccountNumber = "10-4020-" + RandomStringGenerator.RandomString(6),
                CustomerId = customerId,
                ShipToAddressId = shipAddressId,
                BillToAddressId = billAddressId,
                ShipMethod = ShipMethod,
                CreditCardApprovalCode = null,
                SubTotal = (decimal)Subtotal,
                TaxAmt = (decimal)TaxAmount,
                Freight = (decimal)Freight,
                TotalDue = (decimal)TotalDue,
                Comment = Comment,
                ModifiedDate = DateTime.Now
            };

            dbContext.Orders.Add(order);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void CreateOrderDetails()
        {
            Random random = new Random();
            RetailDbContext dbContext = new RetailDbContext();

            int maxOrderId = Services.getTableInfo(nameof(Tables.Orders));
            Console.Write("Enter OrderDetails order Id: ");
            int orderId = InputValidator.validateInputNum(maxOrderId);

            int maxProductId = Services.getTableInfo(nameof(Tables.Products));
            Console.Write("Enter OrderDetails product Id: ");
            int productId = InputValidator.validateInputNum(maxProductId);

            Console.Write("Enter OrderDetails product quantity: ");
            int quantity = InputValidator.validateInputNum(100);

            var productListPrice =
                from p in dbContext.Products
                where p.ProductId == productId
                select p.ListPrice;

            double UnitPrice = Convert.ToDouble(productListPrice.FirstOrDefault());
            double UnitPriceDiscount = ((double)random.NextInt64(50)) / 100;

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = orderId,
                OrderQty = (short)quantity,
                ProductId = productId,
                UnitPrice = (decimal)UnitPrice,
                UnitPriceDiscount = (decimal)UnitPriceDiscount,
                ModifiedDate = DateTime.Now,
            };

            dbContext.OrderDetails.Add(orderDetail);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }


        //Delete
        internal static void DeleteOrder(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderQuery =
                from o in dbContext.Orders
                where o.OrderId == entryId
                select o;

            var deletedOrder = orderQuery.FirstOrDefault();

            dbContext.Orders.Remove(deletedOrder);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void DeleteOrderDetail(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderDetailQuery =
                from o in dbContext.OrderDetails
                where o.OrderDetailId == entryId
                select o;

            var deletedOrderDetail = orderDetailQuery.FirstOrDefault();

            dbContext.OrderDetails.Remove(deletedOrderDetail);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }


        //Update
        internal static void UpdateOrder(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderQuery =
                from o in dbContext.Orders
                where o.OrderId == entryId
                select o;

            var updatedOrder = orderQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");

            Console.Write($"Enter Order Revision Number (current value for this property is '{updatedOrder.RevisionNumber}'): ");
            int RevisionNumber = InputValidator.validateUpdatedNum(updatedOrder.RevisionNumber, 50);

            Console.Write($"Enter Order Status (current value for this property is '{updatedOrder.Status}'): ");
            int OrderStatus = InputValidator.validateUpdatedNum(updatedOrder.Status, 6);

            int maxCustomerId = Services.getTableInfo(nameof(Tables.Customers));
            Console.Write($"Enter Order Customer Id (current value for this property is '{updatedOrder.CustomerId}'): ");
            int customerId = InputValidator.validateUpdatedNum(updatedOrder.CustomerId, maxCustomerId);

            int maxShipAddressId = Services.getTableInfo(nameof(Tables.Addresses));
            Console.Write($"Enter Order Ship Address Id (current value for this property is '{updatedOrder.ShipToAddressId}'): ");
            int shipAddressId = InputValidator.validateUpdatedNum((double)updatedOrder.ShipToAddressId, maxShipAddressId);

            int maxBillAddressId = Services.getTableInfo(nameof(Tables.Addresses));
            Console.Write($"Enter Order Bill Address Id (current value for this property is '{updatedOrder.BillToAddressId}'): ");
            int billAddressId = InputValidator.validateUpdatedNum((double)updatedOrder.BillToAddressId, maxBillAddressId);

            Console.Write($"Enter Order Ship Method (current value for this property is '{updatedOrder.ShipMethod}'): ");
            String ShipMethod = InputValidator.validateUpdatedText(updatedOrder.ShipMethod, 50);

            Console.Write($"Enter Order SubTotal (current value for this property is '{updatedOrder.SubTotal}'): ");
            double Subtotal = InputValidator.validateUpdatedNum((double)updatedOrder.SubTotal, 1000000);

            double percentage = (double)updatedOrder.TaxAmt / (double)updatedOrder.SubTotal;
            double TaxAmount = Subtotal * percentage;

            Console.Write($"Enter Order Freight (current value for this property is '{updatedOrder.Freight}'): ");
            double Freight = InputValidator.validateUpdatedNum((double)updatedOrder.Freight, 100000);

            double TotalDue = Subtotal + TaxAmount + Freight;

            Console.Write($"Enter Order Comment (current value for this property is '{updatedOrder.Comment}'): ");
            String Comment = InputValidator.validateUpdatedText(updatedOrder.Comment, 10000);

            updatedOrder.RevisionNumber = (byte)RevisionNumber;
            updatedOrder.Status = (byte)OrderStatus;
            updatedOrder.CustomerId = customerId;
            updatedOrder.ShipToAddressId = shipAddressId;
            updatedOrder.BillToAddressId = billAddressId;
            updatedOrder.ShipMethod = ShipMethod;
            updatedOrder.SubTotal = (decimal)Subtotal;
            updatedOrder.TaxAmt = (decimal)TaxAmount;
            updatedOrder.Freight = (decimal)Freight;
            updatedOrder.TotalDue = (decimal)TotalDue;
            updatedOrder.Comment = Comment;
            updatedOrder.ModifiedDate = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void UpdateOrderDetail(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderDetailQuery =
                from o in dbContext.OrderDetails
                where o.OrderDetailId == entryId
                select o;

            var updatedOrderDetail = orderDetailQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");

            int maxProductId = Services.getTableInfo(nameof(Tables.Products));
            Console.Write($"Enter OrderDetails product Id (current value for this property is '{updatedOrderDetail.ProductId}'): ");
            int productId = InputValidator.validateUpdatedNum(updatedOrderDetail.ProductId, maxProductId);

            Console.Write($"Enter OrderDetails product quantity (current value for this property is '{updatedOrderDetail.OrderQty}'): ");
            int quantity = InputValidator.validateUpdatedNum(updatedOrderDetail.OrderQty, 1000);

            var productListPrice =
                from p in dbContext.Products
                where p.ProductId == productId
                select p.ListPrice;

            double UnitPrice = Convert.ToDouble(productListPrice.FirstOrDefault());
            double UnitPriceDiscount = (double)updatedOrderDetail.UnitPriceDiscount;

            updatedOrderDetail.OrderQty = (short)quantity;
            updatedOrderDetail.ProductId = productId;
            updatedOrderDetail.UnitPrice = (decimal)UnitPrice;
            updatedOrderDetail.UnitPriceDiscount = (decimal)UnitPriceDiscount;
            updatedOrderDetail.ModifiedDate = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }


        //Display All Entries
        internal static void DisplayAllOrders(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderQuery =
                from o in dbContext.Orders
                orderby o.OrderId
                select new Order
                {
                    OrderId = o.OrderId,
                    RevisionNumber = o.RevisionNumber,
                    OrderDate = o.OrderDate,
                    ShipDate = o.ShipDate,
                    Status = o.Status,
                    OrderNumber = o.OrderNumber,
                    OnlineOrderFlag = o.OnlineOrderFlag,
                    PurchaseOrderNumber = o.PurchaseOrderNumber,
                    AccountNumber = o.AccountNumber,
                    CustomerId = o.CustomerId,
                    ShipToAddress = o.ShipToAddress,
                    BillToAddress = o.BillToAddress,
                    ShipMethod = o.ShipMethod,
                    CreditCardApprovalCode = o.CreditCardApprovalCode,
                    SubTotal = o.SubTotal,
                    TaxAmt = o.TaxAmt,
                    Freight = o.Freight,
                    TotalDue = o.TotalDue,
                    Comment = o.Comment,
                    ModifiedDate = o.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (Order param in orderQuery.Take(orderQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {orderQuery.Count()} entries from table '{table}'");
        }

        internal static void DisplayAllOrderDetails(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var orderDetailsQuery =
                from od in dbContext.OrderDetails
                orderby od.OrderId
                select new OrderDetail
                {
                    OrderId = od.OrderId,
                    OrderDetailId = od.OrderDetailId,
                    OrderQty = od.OrderQty,
                    ProductId = od.ProductId,
                    UnitPrice = od.UnitPrice,
                    UnitPriceDiscount = od.UnitPriceDiscount,
                    LineTotal = od.LineTotal,
                    ModifiedDate = od.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (OrderDetail param in orderDetailsQuery.Take(orderDetailsQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {orderDetailsQuery.Count()} entries from table '{table}'");
        }
    }
}
