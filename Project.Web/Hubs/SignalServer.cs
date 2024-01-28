using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Data.Repositories;
using Project.Core.DTOs;

namespace Project.Web.Hubs
{
    public class SignalServer : Hub
    {

        public async Task SendProductsWithCategory(List<ProductWithCategoryDto> products)
    {
        await Clients.All.SendAsync("ReceiveProductsWithCategory", products);
    }
        public async Task Save(Product product)
    {
        await Clients.All.SendAsync("ItemAdded", product);
    }
        

    }
}