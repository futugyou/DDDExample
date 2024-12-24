namespace Example.Application;

/// <summary>
/// 子领域Customer的视图模型
/// </summary>
public class CustomerViewModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(5)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The E-mail is Required")]
    [EmailAddress]
    [DisplayName("E-mail")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The BirthDate is Required")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
    [DisplayName("Birth Date")]
    public DateTime BirthDate { get; set; }
    /// <summary>
    /// 省份
    /// </summary>
    [Required(ErrorMessage = "The Province is Required")]
    [DisplayName("Province")]
    public required string Province { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// 区县
    /// </summary>
    public required string County { get; set; }

    /// <summary>
    /// 街道
    /// </summary>
    public required string Street { get; set; }
}
