using System;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Medicine
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public required int Stock { get; set; }
    public required string Description { get; set; }
    public required DateTime CreateAt { get; set; }
    public required DateTime UpdateAt { get; set; }

    #region Relationships 
    public ICollection<Treatment> Treatments { get; set; } = [];

    #endregion
}
