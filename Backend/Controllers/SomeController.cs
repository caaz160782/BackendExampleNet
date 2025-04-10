using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            
            Thread.Sleep(1000);
            Console.WriteLine("conexion a bd terminada");

            Thread.Sleep(1000);
            Console.WriteLine("envio de email");

            Console.WriteLine("todo ha terminado");
            sw.Stop();

            return Ok(sw.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            var task1 = new Task<int>(() => {
                Thread.Sleep(1000);
                Console.WriteLine("conexion a bd terminada");
                return 007;
            });

            var task2 = new Task<int>(() => {
                Thread.Sleep(1000);
                Console.WriteLine("envio email");
                return 85;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("se ejecuta otra tarea");

            var res = await task1;
            var resEmail = await task2;
            Console.WriteLine("todo ha terminado");

            sw.Stop();


            return Ok(res +" "+ resEmail + " "+ sw.Elapsed);
        }
    }
}
