using ShopBridge_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge_API.Data
{
    public class ShopBridgeDbContext:DbContext
    {
        public DbSet<ShopBridge> ShopBridge { get; set; }
        public DbSet<Error> Error { get; set; }
    }
}