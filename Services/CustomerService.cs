using LiteDB;
using System.Threading.Tasks;
using TestAsync.Models;

namespace TestAsync.Services
{
    public class CustomerService
    {
        private readonly string dbName = "TestAsync.db";
        private readonly string tableName = "Customer";

        public async Task CreateCustomer(string name)
        {
            //LiteDB does not currently support async operations, so we must create a task to run this asynchronously.
            await Task.Run(() =>
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(dbName))
                {
                    // Get the customer collection (or create if not exists
                    var customers = db.GetCollection<Customer>(tableName);

                    // Generate the model we will insert
                    var customer = new Customer
                    {
                        Name = name,
                    };

                    // Insert new customer document (Id will be auto-incremented)
                    customers.Insert(customer);
                }
            });
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            //LiteDB does not currently support async operations, so we must create a task to run this asynchronously.
            return await Task.Run(() =>
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(dbName))
                {
                    // Get the customer collection (or create if not exists
                    var customers = db.GetCollection<Customer>(tableName);

                    // Search customer by id and return
                    return customers.FindOne(c => c.Name.Equals(name));
                }
            });
        }
    }
}
