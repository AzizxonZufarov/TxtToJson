using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TxtToJson.Data.Models;

namespace TxtToJson.Controllers
{
    // Здесь не требуется никакой логики, т.к. логика по чтению файла должна быть в репозитории
    
    public class FileController : Controller
    {
        private readonly ICarRepository _carRepository;
    
        public FileController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
    
        // лишнее
        public IActionResult FileUpload()
        {
            return View();
        }
        
        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetAll()
        {
            var cars = _carRepository.All();
            
            return Ok(cars);
            
            // Логику обработки файла в репозиторий
            // Можно прочесть все строки файла 1 методом
            // var lines = File.ReadAllLines(filePath);
            
            List<Car> cars = new List<Car>();
            using (var CurrentFile = new StreamReader(file.FileName))
            {
                string line = string.Empty;
                while ((line = CurrentFile.ReadLine()) != null)
                {
                    if (line.Trim() == "")
                        continue;
                    var parts = line.Split(';');
                    cars.Add(
                        new Car()
                        {
                            CarName = parts[0] + " " + parts[1],
                            Price = Convert.ToInt32(parts[2]),
                            Year = Convert.ToInt32(parts[3]),
                        });
                };
                TempData["msg"] = "Uploaded!";
                return Ok(JsonConvert.SerializeObject(cars, Formatting.Indented));
            }
        }
        
        // лишнее
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            List<Car> cars = new List<Car>();
            using (var CurrentFile = new StreamReader(file.FileName))
            {
                string line = string.Empty;
                while ((line = CurrentFile.ReadLine()) != null)
                {
                    if (line.Trim() == "")
                        continue;
                    var parts = line.Split(';');
                    cars.Add(
                        new Car()
                        {
                            CarName = parts[0] + " " + parts[1],
                            Price = Convert.ToInt32(parts[2]),
                            Year = Convert.ToInt32(parts[3]),
                        });
                };
                TempData["msg"] = "Uploaded!";
                return Ok(JsonConvert.SerializeObject(cars, Formatting.Indented));
            }
        }

        /*
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            //var isUploaded = await UploadFile(file);
            List<Car> cars = new List<Car>();
                using (var CurrentFile = new StreamReader(file.FileName))
                {
                    string line = string.Empty;
                    while ((line = CurrentFile.ReadLine()) != null)
                    {
                        if (line.Trim() == "")
                            continue;
                        var parts = line.Split(';');
                        List<Car> _cars = new List<Car>()
                        {
                            new Car 
                            {
                                CarName = parts[0] + " " + parts[1],
                                Price = Convert.ToInt32(parts[2]),
                                Year = Convert.ToInt32(parts[3]),
                            }
                        };
                    }
                }
            TempData["msg"] = "Uploaded!";
            //return View(cars);
            return Ok(new { cars });
        }
          public async Task<bool> UploadFile(IFormFile file)
        {
            string path="";
            bool iscopied = false;
                try
                { 
                    if (file.Length > 0)
                        {
                            string filename=Guid.NewGuid() + Path.GetExtension(file.FileName);
                            path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")); 
                                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                                { 
                                    await file.CopyToAsync(filestream);
                                }
                                iscopied = true;
                        }
                    else
                        {
                            iscopied = false;
                        }
                    }
                catch (Exception)
                {
                    throw;
                }

            return iscopied;

        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploadedfile = file;
            if (file != null)
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), uploadedfile.FileName);
                uploadedfile.CopyTo(new FileStream(fileName, FileMode.Create));
                int counter = 0;
                string line;
                StreamReader _file =
                    new StreamReader(fileName);
                while ((line = _file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    counter++;
                }
                _file.Close();
            }
            var lines = System.IO.File.ReadAllLines(file.FileName);
            var model = lines.Select(p => new
            {
                CarName = p.Split(";")[0] + " " + p.Split(";")[1],
                Year = p.Split(";")[2],
                Price = p.Split(";")[3],
            });
            var cars = new List<Car>()
                {
                    new Car { CarName = "Audi" }
                };

            return Ok(uploadedfile);
        }
        /*[HttpPost("Car")]
        public async Task<IActionResult> Index(IFormFile file)
        {
            var lines = System.IO.File.ReadAllLines(file.FileName);
            var model = lines.Select(p => new
            {
                CarName = p.Split(";")[0] + " " + p.Split(";")[1],
                Year = p.Split(";")[2],
                Price = p.Split(";")[3],
            });
            var json = JsonSerializer.Serialize(model);
            return Ok(new { json });
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var size = files.Sum(h => h.Length);
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if(formFile.Length >0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
                    filePaths.Add(filePath);
                    using (var stream=new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
                return Ok(new {files.Count,size,filePaths});
        }*/

    }
}
