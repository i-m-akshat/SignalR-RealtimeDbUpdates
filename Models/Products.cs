using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }  
        public string ProductName { get; set; } 
        public string Category { get; set; }    
        public string UnitPrice { get; set; }   
        public string StockQuantity { get; set; }
    }
}
