using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using WebApi.DataAccess;
using WebApi.DataContainer;
using WebApi.VolumeCalculator;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/calculate")]
    public class VolumeController : ControllerBase
    {
        private Calculator calculator;
        private DataContext context;
        
        public VolumeController(Calculator calculator, DataContext context)
        {
            this.calculator = calculator;
            this.context = context;
        }

        [HttpGet]
        [Route("{type}")]
        public async Task<ActionResult<VolumeResult>> CalculateVolume([FromRoute] string type, [FromQuery] double height, [FromQuery] double radius)
        {
            double volume = 0.00;
            if (string.Equals("Cylinder", type, StringComparison.OrdinalIgnoreCase))
            {
                volume = calculator.CalculateVolumeCylinder(height, radius);
            }
            else if (string.Equals("Cone", type, StringComparison.OrdinalIgnoreCase))
            {
                volume = calculator.CalculateVolumeCone(radius, height);
            }

            Console.WriteLine(volume);
            VolumeResult result = new VolumeResult();
            result.Height = height;
            result.Radius = radius;
            result.Type = type;
            result.Volume = volume;
            
            try
                {
                    EntityEntry<VolumeResult> vr = await context.Volume.AddAsync(result);
                    await context.SaveChangesAsync();
                    return Ok(vr.Entity);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw new Exception("Internal Server Error. Try to reload the app!");
                }
            

        }
        
        [HttpGet]
        public async Task<ActionResult<IList<VolumeResult>>> GetHistory([FromQuery] double height, [FromQuery] double radius)
        {
            try
                {
                    IList<VolumeResult> results = await context.Volume.ToListAsync();
                    if (results != null)
                    {
                        return Ok(results);
                    }

                    throw new Exception("No calculations are made!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw new Exception("Internal Server Error. Try to reload the app!");
                }
        }
    }
}