using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTHADotNetCore.PizzaAPI.Database
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<PizzaExtraModel> PizzaExtras { get; set; }
        public DbSet<PizzaOrderModel> PizzaOrders { get; set; }
        public DbSet<PizzaOrderDetailModel> PizzaOrderDetails { get; set; }
    }
}

[Table("Tbl_Pizza")]
public class PizzaModel
{
    [Key]
    [Column("PizzaId")]
    public int Id { get; set; }
    [Column("Pizza")]
    public string Name { get; set; }
    [Column("Price")]
    public decimal Price { get; set; }
}

[Table("Tbl_PizzaExtra")]
public class PizzaExtraModel
{
    [Key]
    [Column("PizzaExtraId")]
    public int Id { get; set; }
    [Column("PizzaExtraName")]
    public string Name { get; set; }
    [Column("Price")]
    public decimal Price { get; set; }
    public string PriceStr { get { return "$ " + Price; } }
}

[Table("Tbl_PizzaOrder")]
public class PizzaOrderModel
{
    [Key]
    [Column("PizzaOrderId")]
    public int Id { get; set; }
    [Column("PizzaOrderInvoiceNo")]
    public string InvoiceNo { get; set; }
    [Column("PizzaId")]
    public int PizzaId { get; set; }
    [Column("TotalPrice")]
    public decimal TotalPrice { get; set; }
}

[Table("Tbl_PizzaOrderDetail")]
public class PizzaOrderDetailModel
{
    [Key]
    [Column("PizzaOrderDetailId")]
    public int Id { get; set; }
    [Column("PizzaOrderInvoiceNo")]
    public string InvoiceNo { get; set; }
    [Column("PizzaExtraId")]
    public int PizzaExtraId { get; set; }
}

public class OrderRequest
{
    public int PizzaId { get; set; }
    public int[] Extra { get; set; }
}

public class MessageResponse
{
    public string InvoiceNo { get; set; }
    public string Message { get; set; }
    public decimal TotalAmount { get; set; }
}
