using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRCRUDPractice.Controllers;
using SignalRCRUDPractice.Models;


namespace SignalRCRUDPractice
{
    public class SignalRServer:Hub
    {
        //ProductsController _controller = null;
        private readonly IServiceProvider _serviceProvider;
        public SignalRServer( IServiceProvider serviceProvider)
        {
            
            _serviceProvider = serviceProvider;
        }
        //public async Task SendProducts()
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var context=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //        var products=context.Products;
        //        await Clients.All.SendAsync("RecievedProducts", products);
        //    }

        //}
        public async Task SendProducts()
        {
            try
            {
                // Ensure that the hub is not a singleton, or otherwise handle the service scope correctly
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var products = await context.Products.ToListAsync(); // Ensure you fetch the products list

                    // Check if products is not null or empty
                    if (products != null && products.Any())
                    {
                        await Clients.All.SendAsync("ReceivedProducts", products);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework like Serilog or NLog)
                Console.WriteLine($"Error in SendProducts: {ex.Message}");
                // Optionally, you could notify clients of the error if appropriate
            }
        }

    }
}
