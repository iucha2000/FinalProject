 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogicLayer.Enums;
using BusinessLogicLayer.Utilities;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    internal class CustomerServices
    {
        //Implement CRUD operations

        //Read
        internal static String GetCustomer(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var customerQuery =
                from c in dbContext.Customers
                where c.CustomerId == n
                select new Customer
                {
                    CustomerId = c.CustomerId,
                    NameStyle = c.NameStyle,
                    Title = c.Title,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName,
                    Suffix = c.Suffix,
                    CompanyName = c.CompanyName,
                    SalesPerson = c.SalesPerson,
                    EmailAddress = c.EmailAddress,
                    Phone = c.Phone,
                    PasswordHash = c.PasswordHash,
                    PasswordSalt = c.PasswordSalt,
                    ModifiedDate = c.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = customerQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }

        internal static String GetAddress(int n)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var addressQuery =
                from a in dbContext.Addresses
                where a.AddressId == n
                select new Address
                {
                    AddressId = a.AddressId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateProvince = a.StateProvince,
                    CountryRegion = a.CountryRegion,
                    PostalCode = a.PostalCode,
                    ModifiedDate = a.ModifiedDate
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            var param = addressQuery.FirstOrDefault();
            var jsonData = JsonSerializer.Serialize(param, serializerOptions);
            return jsonData;
        }


        //Create
        internal static void CreateCustomer()
        {
            RetailDbContext dbContext = new RetailDbContext();

            Console.Write("Enter Customer Title: ");
            String Title = InputValidator.validateUpdatedText(null, 8);

            Console.Write("Enter Customer First Name: ");
            String FirstName = InputValidator.validateInputText(50);

            Console.Write("Enter Customer Middle Name: ");
            String MiddleName = InputValidator.validateUpdatedText(null, 50);

            Console.Write("Enter Customer Last Name: ");
            String LastName = InputValidator.validateInputText(50);

            Console.Write("Enter Customer Suffix: ");
            String Suffix = InputValidator.validateUpdatedText(null, 50);

            Console.Write("Enter Customer Company Name: ");
            String CompanyName = InputValidator.validateUpdatedText(null, 128);

            Console.Write("Enter Customer Sales Person: ");
            String SalesPerson = InputValidator.validateUpdatedText(null, 256);

            Console.Write("Enter Customer Email Address: ");
            String EmailAddress = InputValidator.validateUpdatedText(null, 50);

            Console.Write("Enter Customer Phone Number: ");
            String PhoneNumber = InputValidator.validateUpdatedText(null, 50);

            Console.Write("Enter Customer Password: ");
            String PasswordSalt = InputValidator.validateInputText(10);

            String hashedPassowrd = Convert.ToBase64String(HashPassword.encryptPassword(PasswordSalt));
            DateTime modifiedDate = DateTime.Now;


            Customer customer = new Customer()
            {
                NameStyle = false,
                Title = Title,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Suffix = Suffix,
                CompanyName = CompanyName,
                SalesPerson = SalesPerson,
                EmailAddress = EmailAddress,
                Phone = PhoneNumber,
                PasswordHash = hashedPassowrd,
                PasswordSalt = PasswordSalt,
                ModifiedDate = modifiedDate,
            };

            dbContext.Customers.Add(customer);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void CreateAddress()
        {
            RetailDbContext dbContext = new RetailDbContext();

            Console.Write("Enter Address Line 1: ");
            String AddressLine1 = InputValidator.validateInputText(60);

            Console.Write("Enter Address Line 2: ");
            String AddressLine2 = InputValidator.validateUpdatedText(null, 60);

            Console.Write("Enter City: ");
            String City = InputValidator.validateInputText(30);

            Console.Write("Enter State Province: ");
            String StateProvince = InputValidator.validateInputText(50);

            Console.Write("Enter Country Region: ");
            String CountryRegion = InputValidator.validateInputText(50);

            Console.Write("Enter Postal Code: ");
            String PostalCode = InputValidator.validateInputText(15);

            DateTime modifiedDate = DateTime.Now;


            Address address = new Address()
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                StateProvince = StateProvince,
                CountryRegion = CountryRegion,
                PostalCode = PostalCode,
                ModifiedDate = modifiedDate
            };

            dbContext.Addresses.Add(address);

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
        internal static void DeleteCustomer(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var customersQuery =
                from c in dbContext.Customers
                where c.CustomerId == entryId
                select c;

            var deletedCustomer = customersQuery.FirstOrDefault();

            dbContext.Customers.Remove(deletedCustomer);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void DeleteAddress(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var addressQuery =
                from a in dbContext.Addresses
                where a.AddressId == entryId
                select a;

            var deletedAddress = addressQuery.FirstOrDefault();

            dbContext.Addresses.Remove(deletedAddress);

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
        internal static void UpdateCustomer(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var customersQuery =
             from c in dbContext.Customers
             where c.CustomerId == entryId
             select c;

            var updatedCustomer = customersQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");

            Console.Write($"Enter Customer Title (current value for this property is '{updatedCustomer.Title}'): ");
            String Title = InputValidator.validateUpdatedText(updatedCustomer.Title, 8);

            Console.Write($"Enter Customer First Name (current value for this property is '{updatedCustomer.FirstName}'): ");
            String FirstName = InputValidator.validateUpdatedText(updatedCustomer.FirstName, 50);

            Console.Write($"Enter Customer Middle Name (current value for this property is '{updatedCustomer.MiddleName}'): ");
            String MiddleName = InputValidator.validateUpdatedText(updatedCustomer.MiddleName, 50);

            Console.Write($"Enter Customer Last Name (current value for this property is '{updatedCustomer.LastName}'): ");
            String LastName = InputValidator.validateUpdatedText(updatedCustomer.LastName, 50);

            Console.Write($"Enter Customer Suffix (current value for this property is '{updatedCustomer.Suffix}'): ");
            String Suffix = InputValidator.validateUpdatedText(updatedCustomer.Suffix, 10);

            Console.Write($"Enter Customer Company Name (current value for this property is '{updatedCustomer.CompanyName}'): ");
            String CompanyName = InputValidator.validateUpdatedText(updatedCustomer.CompanyName, 128);

            Console.Write($"Enter Customer Sales Person (current value for this property is '{updatedCustomer.SalesPerson}'): ");
            String SalesPerson = InputValidator.validateUpdatedText(updatedCustomer.SalesPerson, 256);

            Console.Write($"Enter Customer Email Address (current value for this property is '{updatedCustomer.EmailAddress}'): ");
            String EmailAddress = InputValidator.validateUpdatedText(updatedCustomer.EmailAddress, 50);

            Console.Write($"Enter Customer Phone Number (current value for this property is '{updatedCustomer.Phone}'): ");
            String PhoneNumber = InputValidator.validateUpdatedText(updatedCustomer.Phone, 25);

            Console.Write($"Enter Customer Password (current value for this property is hidden): ");
            String PasswordSalt = InputValidator.validateUpdatedText(updatedCustomer.PasswordSalt, 10);

            String hashedPassword = "";

            if (PasswordSalt.Equals(updatedCustomer.PasswordSalt))
            {
                hashedPassword = updatedCustomer.PasswordHash;
            }
            else
            {
                hashedPassword = Convert.ToBase64String(HashPassword.encryptPassword(PasswordSalt));
            }

            updatedCustomer.Title = Title;
            updatedCustomer.FirstName = FirstName;
            updatedCustomer.MiddleName = MiddleName;
            updatedCustomer.LastName = LastName;
            updatedCustomer.Suffix = Suffix;
            updatedCustomer.CompanyName = CompanyName;
            updatedCustomer.SalesPerson = SalesPerson;
            updatedCustomer.EmailAddress = EmailAddress;
            updatedCustomer.Phone = PhoneNumber;
            updatedCustomer.PasswordSalt = PasswordSalt;
            updatedCustomer.PasswordHash = hashedPassword;
            updatedCustomer.ModifiedDate = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        internal static void UpdateAddress(int entryId)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var addressQuery =
                from a in dbContext.Addresses
                where a.AddressId == entryId
                select a;

            var updatedAddress = addressQuery.FirstOrDefault();

            Console.WriteLine("Enter updated property or press 'Enter' to skip: ");

            Console.Write($"Enter Address Line 1 (current value for this property is '{updatedAddress.AddressLine1}'): ");
            String AddressLine1 = InputValidator.validateUpdatedText(updatedAddress.AddressLine1, 60);

            Console.Write($"Enter Address Line 2 (current value for this property is '{updatedAddress.AddressLine2}'): ");
            String AddressLine2 = InputValidator.validateUpdatedText(updatedAddress.AddressLine2, 60);

            Console.Write($"Enter City (current value for this property is '{updatedAddress.City}'): ");
            String City = InputValidator.validateUpdatedText(updatedAddress.City, 30);

            Console.Write($"Enter State Province (current value for this property is '{updatedAddress.StateProvince}'): ");
            String StateProvince = InputValidator.validateUpdatedText(updatedAddress.StateProvince, 50);

            Console.Write($"Enter Country Region (current value for this property is '{updatedAddress.CountryRegion}'): ");
            String CountryRegion = InputValidator.validateUpdatedText(updatedAddress.CountryRegion, 50);

            Console.Write($"Enter Postal Code (current value for this property is '{updatedAddress.PostalCode}'): ");
            String PostalCode = InputValidator.validateUpdatedText(updatedAddress.PostalCode, 15);

            updatedAddress.AddressLine1 = AddressLine1;
            updatedAddress.AddressLine2 = AddressLine2;
            updatedAddress.City = City;
            updatedAddress.StateProvince = StateProvince;
            updatedAddress.CountryRegion = CountryRegion;
            updatedAddress.PostalCode = PostalCode;
            updatedAddress.ModifiedDate = DateTime.Now;

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
        internal static void DisplayAllCustomers(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var customerQuery =
                from c in dbContext.Customers
                orderby c.CustomerId
                select new Customer
                {
                    CustomerId = c.CustomerId,
                    NameStyle = c.NameStyle,
                    Title = c.Title,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName,
                    Suffix = c.Suffix,
                    CompanyName = c.CompanyName,
                    SalesPerson = c.SalesPerson,
                    EmailAddress = c.EmailAddress,
                    Phone = c.Phone,
                    PasswordHash = c.PasswordHash,
                    PasswordSalt = c.PasswordSalt,
                    ModifiedDate = c.ModifiedDate,
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (Customer param in customerQuery.Take(customerQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {customerQuery.Count()} entries from table '{table}'");
        }

        internal static void DisplayAllAddresses(String table)
        {
            RetailDbContext dbContext = new RetailDbContext();

            var addressQuery =
                from a in dbContext.Addresses
                orderby a.AddressId
                select new Address
                {
                    AddressId = a.AddressId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateProvince = a.StateProvince,
                    CountryRegion = a.CountryRegion,
                    PostalCode = a.PostalCode,
                    ModifiedDate = a.ModifiedDate
                };

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true, AllowTrailingCommas = true };
            foreach (Address param in addressQuery.Take(addressQuery.Count()))
            {
                var jsonData = JsonSerializer.Serialize(param, serializerOptions);
                Console.WriteLine(jsonData);
            }
            Console.WriteLine($"\nDisplayed {addressQuery.Count()} entries from table '{table}'");
        }
    }
}
