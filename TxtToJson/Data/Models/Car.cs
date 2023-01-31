using System;
using System.Collections.Generic;

namespace TxtToJson.Data.Models
{
    public class Car
    {
        public int CarId { get; set; }

        public string CarName { get; set; }

        public string CarPosition { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }
    }
    
    //public class Car
    //{
    //    public int CarId { get; set; }            // ИД в файле нет, поэтому пока лучше убрать бы
    //    public string CarName { get; set; }       // Это марка Brand 
    //    public string CarPosition { get; set; }   // Это Model, + это и так понятно, что относится к Car
    //    public int Year { get; set; }             
    //    public int Price { get; set; }
    //}
}
