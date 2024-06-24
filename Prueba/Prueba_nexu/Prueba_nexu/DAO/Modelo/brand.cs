using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prueba_nexu.DAO.Modelo;

public partial class brand
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string nombre { get; set; } = null!;

    [InverseProperty("id_brandNavigation")]
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
