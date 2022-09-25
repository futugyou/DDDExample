namespace Example.Application;

/// <summary>
/// 子领域Customer的视图模型
/// </summary>
public class CustomerViewModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(2)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The E-mail is Required")]
    [EmailAddress]
    [DisplayName("E-mail")]
    public string Email { get; set; }

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
    public string Province { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// 区县
    /// </summary>
    public string County { get; set; }

    /// <summary>
    /// 街道
    /// </summary>
    public string Street { get; set; }
}
