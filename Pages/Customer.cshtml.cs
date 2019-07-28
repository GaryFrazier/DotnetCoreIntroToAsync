using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestAsync.Models;
using TestAsync.Services;

namespace TestAsync.Pages
{
    public class CustomerModel : PageModel
    {
        private CustomerService customerService = new CustomerService();

        public Customer currentCustomer;

        public void OnGet()
        {

        }

        public async Task OnPostAsync()
        {
            var name = Request.Form["name"];

            // Note this is just an example, you should not save to the database and immediately get the info back if it is not needed
            if (!string.IsNullOrEmpty(name))
            {
                // Start the operation, and wait for it to finish before moving on
                await customerService.CreateCustomer(name);

                // Start the operation, but do not wait for the operation to complete, we will wait for the value when we need it to prevent blocking.
                var newCustomer = customerService.GetCustomerByName(name);

                // Do other stuff here while customer is being fetched from the database


                // Await the result, we cannot set the customer to a task of the customer so we must wait for the value.
                currentCustomer = await newCustomer;
            }
        }
    }
}