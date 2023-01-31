using System.Collections.Generic;
using TxtToJson.Data.Models;

namespace TxtToJson.Data.Interfaces
{
    public interface ICar
    {
        public IEnumerable<Car> AllCars();

        Car GetCarsInRange();
    }
}
