using DEMO01_CRUD.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace DEMO01_CRUD.Models
{
    public class ClsUsuario
    {
        public int id { get; set; }   
        [Required]
        public string usuario { get; set; }
        [Required]
        [ContrasenaValidate(Allowed = new string[] { "hadson1", "erlita1", "cesar1" }, ErrorMessage = "Contraseña no valida")]
        public string contrasena { get; set; }
        [Range(3, 5)]
        public int intentos { get; set; }
        [Required]
        public decimal nivelSeg { get; set; }
        public DateTime fechaReg { get; set; }
    }
}