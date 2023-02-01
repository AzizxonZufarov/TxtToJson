using System.Collections.Generic;
using TxtToJson.Data.Interfaces;
using TxtToJson.Data.Models;

namespace TxtToJson.Data.Repositories
{
    public interface ICarRepository
    {
        Car[] All();        
    }

    public class ReadonlyCarFileRepository : ICarRepository
    {
    
    }
    
    public static class CarHelper
    {
        public Car FromString(string value)
        {
            return new Car();
        }
    }
}
