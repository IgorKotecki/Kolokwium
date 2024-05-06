using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Controllers;

public class PrescriptionDto
{
    [Required]
    public int? IdMedication { get; set; }

}