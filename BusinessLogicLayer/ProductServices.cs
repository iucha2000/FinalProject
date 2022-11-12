using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Utilities;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    internal class ProductServices
    {
        //Implement CRUD operations

        //Read
        internal static String GetProduct(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productQuery =
                from p in dbContext.Products
                where p.ProductId == n
                select new Product
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    ProductNumber = p.ProductNumber,
                    Color = p.Color,
                    StandardCost = p.StandardCost,
                    ListPrice = p.ListPrice,
                    Size = p.Size,
                    Weight = p.Weight,
                    ProductCategoryId = p.ProductCategoryId,
                    ProductModelId = p.ProductModelId,
                    SellStartDate = p.SellStartDate,
                    SellEndDate = p.SellEndDate,
                    DiscontinuedDate = p.DiscontinuedDate,
                    PhotoFileName = p.PhotoFileName,
                    ModifiedDate = p.ModifiedDate
                };
            
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = productQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);

            if (jsonData.Equals("null"))
            {
                Console.WriteLine("its null");
            }
            return jsonData;
        }

        internal static String GetCategory(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();
            var categoryQuery =
                from c in dbContext.Categories
                where c.CategoryId == n
                select new Category
                {
                    CategoryId = c.CategoryId,
                    ParentCategoryId = c.ParentCategoryId,
                    Name = c.Name,
                    ModifiedDate = c.ModifiedDate
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };         
            var param = categoryQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }

        internal static String GetProductModel(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();
            var productModelQuery =
                from pm in dbContext.ProductModels
                where pm.ProductModelId == n
                select new ProductModel
                {
                    ProductModelId = pm.ProductModelId,
                    Name = pm.Name,
                    CatalogDescription = pm.CatalogDescription,
                    ModifiedDate = pm.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = productModelQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }
     

        //Create
        internal static void CreateProduct()
        {
            RetailDbContext dbContext = new RetailDbContext();
          
            Console.Write("Enter Product Name: ");
            String Name = InputValidator.validateInputText(50);

            Console.Write("Enter Product Number: ");
            String productNumber = InputValidator.validateInputText(25);

            Console.Write("Enter Product Color: ");
            String color = InputValidator.validateUpdatedText(null,15);
        
            Console.Write("Enter Product List Price: ");
            double listPrice = InputValidator.validateInputNum(100000);

            Console.Write("Enter Product Standard Cost: ");
            double standartCost = InputValidator.validateInputNum((int)listPrice);

            Console.Write("Enter Product Size: ");
            String Size = InputValidator.validateUpdatedText(null,100);

            Console.Write("Enter Product Weight: ");
            double Weight = InputValidator.validateInputNum(10000);

            Console.Write("Enter Product Category Id: ");
            int categoryId = InputValidator.validateInputNum(41);

            Console.Write("Enter Product Model Id: ");
            int modelId = InputValidator.validateInputNum(128);

            Console.Write("Enter Product Photo File Name: ");
            String photoFileName = InputValidator.validateUpdatedText(null,50);

            DateTime sellStartDate = DateTime.Now;
            DateTime modifiedDate = sellStartDate;

            Product product = new Product()
            {
                Name = Name,
                ProductNumber = productNumber,
                Color = color,
                StandardCost = (decimal)standartCost,
                ListPrice = (decimal)listPrice,
                Size = Size,
                Weight = (decimal)Weight,
                ProductCategoryId = categoryId,
                ProductModelId = modelId,
                SellStartDate = sellStartDate,
                SellEndDate = null,
                DiscontinuedDate = null,
                PhotoFileName = photoFileName,
                ModifiedDate = modifiedDate
            };
          
            dbContext.Products.Add(product);

            try
            {
                dbContext.SaveChanges();
            }          
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

        }

        internal static void CreateCategory()
        {
            RetailDbContext dbContext = new RetailDbContext();

            Console.Write("Enter Category Name: ");
            String Name = InputValidator.validateInputText(50);

            Console.Write("Enter Parent Category Id: ");
            int ParentCategoryId = InputValidator.validateInputNum(41);

            DateTime modifiedDate = DateTime.Now;

            Category category = new Category
            {
                Name = Name,
                ParentCategoryId = ParentCategoryId,
                ModifiedDate = modifiedDate
            };

            dbContext.Categories.Add(category);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void CreateProductModel()
        {
            RetailDbContext dbContext = new RetailDbContext();

            Console.Write("Enter Product Model Name: ");
            String Name = InputValidator.validateInputText(50);

            DateTime modifiedDate = DateTime.Now;

            ProductModel productModel = new ProductModel
            {
                Name = Name,
                CatalogDescription = null,
                ModifiedDate = modifiedDate
            };

            dbContext.ProductModels.Add(productModel);

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
        internal static void DeleteProduct(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productQuery =
                from p in dbContext.Products
                where p.ProductId == entryId
                select p;

            var deletedProduct = productQuery.FirstOrDefault();

            dbContext.Products.Remove(deletedProduct);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void DeleteCategory(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var categoryQuery =
                from c in dbContext.Categories
                where c.CategoryId == entryId
                select c;

            var deletedCategory = categoryQuery.FirstOrDefault();

            dbContext.Categories.Remove(deletedCategory);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void DeleteProductModel(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productModelQuery =
                from pm in dbContext.ProductModels
                where pm.ProductModelId == entryId
                select pm;

            var deletedProductModel = productModelQuery.FirstOrDefault();

            dbContext.ProductModels.Remove(deletedProductModel);

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
        internal static void UpdateProduct(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productQuery =
                from p in dbContext.Products
                where p.ProductId == entryId
                select p;

            var updatedProduct = productQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");

            Console.Write($"Enter Product Name (current value for this property is '{updatedProduct.Name}'): ");
            String Name = InputValidator.validateUpdatedText(updatedProduct.Name ,50);

            Console.Write($"Enter Product Number (current value for this property is '{updatedProduct.ProductNumber}'): ");
            String productNumber = InputValidator.validateUpdatedText(updatedProduct.ProductNumber,25);

            Console.Write($"Enter Product Color (current value for this property is '{updatedProduct.Color}'): ");
            String color = InputValidator.validateUpdatedText(updatedProduct.Color,15);

            Console.Write($"Enter Product List Price (current value for this property is '{updatedProduct.ListPrice}'): ");
            double listPrice = InputValidator.validateUpdatedNum((double)updatedProduct.ListPrice,100000);

            Console.Write($"Enter Product Standard Cost (current value for this property is '{updatedProduct.StandardCost}'): ");
            double standartCost = InputValidator.validateUpdatedNum((double)updatedProduct.StandardCost,(int)listPrice);

            Console.Write($"Enter Product Size (current value for this property is '{updatedProduct.Size}'): ");
            String Size = InputValidator.validateUpdatedNum(Convert.ToDouble(updatedProduct.Size),100).ToString();

            Console.Write($"Enter Product Weight (current value for this property is '{updatedProduct.Weight}'): ");
            double Weight = InputValidator.validateUpdatedNum((double)updatedProduct.Weight,10000);

            Console.Write($"Enter Product Category Id (current value for this property is '{updatedProduct.ProductCategoryId}'): ");
            int categoryId = InputValidator.validateUpdatedNum((double)updatedProduct.ProductCategoryId,41);

            Console.Write($"Enter Product Model Id (current value for this property is '{updatedProduct.ProductModelId}'): ");
            int modelId = InputValidator.validateUpdatedNum((double)updatedProduct.ProductModelId,128);

            updatedProduct.Name = Name;
            updatedProduct.ProductNumber = productNumber;
            updatedProduct.Color = color;
            updatedProduct.ListPrice = (decimal)listPrice;
            updatedProduct.StandardCost = (decimal)standartCost;
            updatedProduct.Size = Size;
            updatedProduct.Weight = (decimal)Weight;
            updatedProduct.ProductCategoryId = categoryId;
            updatedProduct.ProductModelId = modelId;
            updatedProduct.ModifiedDate = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void UpdateCategory(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var categoryQuery =
              from c in dbContext.Categories
              where c.CategoryId == entryId
              select c;

            var updateCategory = categoryQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");
            Console.WriteLine();

            Console.Write($"Enter Category Name (current value for this property is '{updateCategory.Name}'): ");
            String Name = InputValidator.validateUpdatedText(updateCategory.Name, 50);

            Console.Write($"Enter Parent Category Id (current value for this property is '{updateCategory.ParentCategoryId}'): ");
            int parentCategoryId = InputValidator.validateUpdatedNum((double)updateCategory.ParentCategoryId,50);

            updateCategory.Name = Name;
            updateCategory.ParentCategoryId = parentCategoryId;
            updateCategory.ModifiedDate = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void UpdateProductModel(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productModelQuery =
               from pm in dbContext.ProductModels
               where pm.ProductModelId == entryId
               select pm;

            var updateProductModel = productModelQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");
            Console.WriteLine();

            Console.Write($"Enter Product Model Name (current value for this property is '{updateProductModel.Name}'): ");
            String Name = InputValidator.validateUpdatedText(updateProductModel.Name, 50);

            updateProductModel.Name = Name;
            updateProductModel.ModifiedDate = DateTime.Now;

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
        internal static void DisplayAllProducts(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productQuery =
               from p in dbContext.Products
               orderby p.ProductId
               select new Product
               {
                   ProductId = p.ProductId,
                   Name = p.Name,
                   ProductNumber = p.ProductNumber,
                   Color = p.Color,
                   StandardCost = p.StandardCost,
                   ListPrice = p.ListPrice,
                   Size = p.Size,
                   Weight = p.Weight,
                   ProductCategoryId = p.ProductCategoryId,
                   ProductModelId = p.ProductModelId,
                   SellStartDate = p.SellStartDate,
                   SellEndDate = p.SellEndDate,
                   DiscontinuedDate = p.DiscontinuedDate,
                   PhotoFileName = p.PhotoFileName,
                   ModifiedDate = p.ModifiedDate
               };           

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (Product p in productQuery.Take(productQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(p, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {productQuery.Count()} entries from table '{table}'");
        }

        internal static void DisplayAllCategories(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var categoryQuery =
                from c in dbContext.Categories
                orderby c.CategoryId
                select new Category
                {
                    CategoryId = c.CategoryId,
                    ParentCategoryId = c.ParentCategoryId,
                    Name = c.Name,
                    ModifiedDate = c.ModifiedDate
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (Category param in categoryQuery.Take(categoryQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {categoryQuery.Count()} entries from table '{table}'");
        }

        internal static void DisplayAllProductModels(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var productModelQuery =
                from pm in dbContext.ProductModels
                orderby pm.ProductModelId
                select new ProductModel
                {
                    ProductModelId = pm.ProductModelId,
                    Name = pm.Name,
                    CatalogDescription = pm.CatalogDescription,
                    ModifiedDate = pm.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (ProductModel param in productModelQuery.Take(productModelQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {productModelQuery.Count()} entries from table '{table}'");
        }
    }
}
