using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQL.Models
{
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int IdPersona { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Apellido { get; set; }
        [MaxLength(50)]
        public int Edad { get; set;}
        [MaxLength(100)]
        public string Correo { get; set;}
        
    }
}
