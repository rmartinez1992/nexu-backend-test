using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prueba_nexu.DAO.Modelo;

public partial class Model
{
    [Key]
    public int id { get; set; }

    public int id_brand { get; set; }

    [StringLength(100)]
    public string? nombre { get; set; }

    [Column(TypeName = "money")]
    public decimal average_price { get; set; }

    [ForeignKey("id_brand")]
    [InverseProperty("Models")]
    public virtual brand id_brandNavigation { get; set; } = null!;
}
