using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen_AppPets.Models
{
    public class PetModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Weight { get; set; }
        public string Comments { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
