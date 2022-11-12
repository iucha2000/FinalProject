using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Utilities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Services
    {
        static void Main(string[] args)
        {
            //Custom exception implementation left
        }
  
        public static int getTableInfo(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();
            int min = 0;
            int max = 0;

            switch (table)
            {
                case nameof(Tables.Products):
                    max = dbContext.Products.Max(x => x.ProductId);
                    min = dbContext.Products.Min(x => x.ProductId);
                    break;

                case nameof(Tables.Categories):
                    max = dbContext.Categories.Max(x => x.CategoryId);
                    min = dbContext.Categories.Min(x => x.CategoryId);
                    break;

                case nameof(Tables.ProductModels):
                    max = dbContext.ProductModels.Max(x => x.ProductModelId);
                    min = dbContext.ProductModels.Min(x => x.ProductModelId);
                    break;

                case nameof(Tables.Customers):
                    max = dbContext.Customers.Max(x => x.CustomerId);
                    min = dbContext.Customers.Min(x => x.CustomerId);
                    break;

                case nameof(Tables.Addresses):
                    max = dbContext.Addresses.Max(x => x.AddressId);
                    min = dbContext.Addresses.Min(x => x.AddressId);
                    break;

                case nameof(Tables.Orders):
                    max = dbContext.Orders.Max(x => x.OrderId);
                    min = dbContext.Orders.Min(x => x.OrderId);
                    break;

                case nameof(Tables.OrderDetails):
                    max = dbContext.OrderDetails.Max(x => x.OrderDetailId);
                    min = dbContext.OrderDetails.Min(x => x.OrderDetailId);
                    break;

                default:
                    return 0;                  
            }           
            Console.Write($"In table {table} select ID (from {min} to {max}): ");
            return max;
        }


        public static void executeOperation(String tableId, int operationId)
        {
            if (operationId == (int)Operations.Create)
            {
                CreateEntry(tableId);
                Console.WriteLine($"\nEntry added to '{tableId}'");
            }
            else if(operationId == (int)Operations.DisplayAll)
            {
                Console.WriteLine($"Displaying all entries in table '{tableId}': ");
                DisplayAll(tableId);
            }

            else
            {
                int maxId = getTableInfo(tableId);
                int entryId = InputValidator.validateInputNum(maxId);

                if (operationId == (int)Operations.Read)
                {
                    String data = ReadEntry(entryId, tableId);
                    InputValidator.validateReadEntry(data,entryId,tableId);                
                }

                else if (operationId == (int)Operations.Update)
                {
                    UpdateEntry(tableId, entryId);
                    Console.WriteLine($"\nEntry with Id '{entryId}' has been updated in table '{tableId}'");
                }

                else if (operationId == (int)Operations.Delete)
                {
                    DeleteEntry(tableId, entryId);
                    Console.WriteLine($"\nEntry with Id '{entryId}' has been deleted from table '{tableId}'");
                }
            }
        }


        internal static void CreateEntry(String table)
        {
            switch (table)
            {
                case nameof(Tables.Products):
                    ProductServices.CreateProduct();
                    break;
                case nameof(Tables.Categories):
                    ProductServices.CreateCategory();
                    break;
                case nameof(Tables.ProductModels):
                    ProductServices.CreateProductModel();   
                    break;
                case nameof(Tables.Customers):
                    CustomerServices.CreateCustomer();
                    break;
                case nameof(Tables.Addresses):
                    CustomerServices.CreateAddress();
                    break;
                case nameof(Tables.Orders):
                    OrderServices.CreateOrder();
                    break;
                case nameof(Tables.OrderDetails):
                    OrderServices.CreateOrderDetails();
                    break;
                default:
                    break;
            }
        }

        public static String ReadEntry(int entryId, String table)
        {
            switch (table)
            {
                case nameof(Tables.Products):
                    return ProductServices.GetProduct(entryId);

                case nameof(Tables.Categories):
                    return ProductServices.GetCategory(entryId);

                case nameof(Tables.ProductModels):
                    return ProductServices.GetProductModel(entryId);

                case nameof(Tables.Customers):
                    return CustomerServices.GetCustomer(entryId);

                case nameof(Tables.Addresses):
                    return CustomerServices.GetAddress(entryId);

                case nameof(Tables.Orders):
                    return OrderServices.GetOrder(entryId);

                case nameof(Tables.OrderDetails):
                    return OrderServices.GetOrderDetails(entryId);

                default:
                    return null;
            }
        }

        internal static void DeleteEntry(String table, int entryId)
        {
            switch (table)
            {
                case nameof(Tables.Products):
                    ProductServices.DeleteProduct(entryId);
                    break;
                case nameof(Tables.Categories):
                    ProductServices.DeleteCategory(entryId);
                    break;
                case nameof(Tables.ProductModels):
                    ProductServices.DeleteProductModel(entryId);
                    break;
                case nameof(Tables.Customers):
                    CustomerServices.DeleteCustomer(entryId);
                    break;
                case nameof(Tables.Addresses):
                    CustomerServices.DeleteAddress(entryId);
                    break;
                case nameof(Tables.Orders):
                    OrderServices.DeleteOrder(entryId);
                    break;
                case nameof(Tables.OrderDetails):
                    OrderServices.DeleteOrderDetail(entryId);
                    break;
                default:
                    break;
            }
        }

        internal static void UpdateEntry(String table, int entryId)
        {
            switch (table)
            {
                case nameof(Tables.Products):
                    ProductServices.UpdateProduct(entryId);
                    break;
                case nameof(Tables.Categories):
                    ProductServices.UpdateCategory(entryId);
                    break;
                case nameof(Tables.ProductModels):
                    ProductServices.UpdateProductModel(entryId);
                    break;
                case nameof(Tables.Customers):
                    CustomerServices.UpdateCustomer(entryId);
                    break;
                case nameof(Tables.Addresses):
                    CustomerServices.UpdateAddress(entryId);
                    break;
                case nameof(Tables.Orders):
                    OrderServices.UpdateOrder(entryId);
                    break;
                case nameof(Tables.OrderDetails):
                    OrderServices.UpdateOrderDetail(entryId);
                    break;
                default:
                    break;
            }
        }

        internal static void DisplayAll(String table)
        {
            switch (table)
            {
                case nameof(Tables.Products):
                    ProductServices.DisplayAllProducts(table);
                    break;
                case nameof(Tables.Categories):
                   ProductServices.DisplayAllCategories(table);
                    break;
                case nameof(Tables.ProductModels):
                   ProductServices.DisplayAllProductModels(table);
                    break;
                case nameof(Tables.Customers):
                   CustomerServices.DisplayAllCustomers(table);
                    break;
                case nameof(Tables.Addresses):
                   CustomerServices.DisplayAllAddresses(table);
                    break;
                case nameof(Tables.Orders):
                   OrderServices.DisplayAllOrders(table);
                    break;
                case nameof(Tables.OrderDetails):
                   OrderServices.DisplayAllOrderDetails(table);
                    break;
                default:
                    break;
            }
        }
    }
}
