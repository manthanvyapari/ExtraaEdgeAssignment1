using System.ComponentModel.DataAnnotations;

namespace ExtraaEdgeAssignment1.Model
{
    public class Costomer
    {

        [Key]
            public int CustomerId { get; set; }
            public string? Name { get; set; }
            public string? Address { get; set; }
            public int Phone { get; set; }


        
    }
}
