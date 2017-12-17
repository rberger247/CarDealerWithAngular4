using AThirdCarDealership.Controllers.Resources;
using AThirdCarDealership.Core;
using AThirdCarDealership.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vega.Core;
using vega.Models;

namespace AThirdCarDealership.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly PhotoRepository photoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly string[] Accepted_File_Types = new[] {".jpg", ".png", ".jpeg"};
        public PhotosController(IHostingEnvironment host, IVehicleRepository repository, IUnitOfWork unitOfWork, PhotoRepository photoRepository, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.mapper = mapper;
         this.repository = repository;
         this.host  = host;
            this.unitOfWork = unitOfWork;
        }

       
        [Route("/api/vehicles/{vehicleId}/photos")]
        [HttpPost]

        public async Task <IActionResult> Upload(int vehicleId, IFormFile  file)
        {

            // returns path of folder
         var uploadsFolderPath =    Path.Combine(host.WebRootPath, "uploads");
            var vehicle =  await repository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
                return BadRequest("no vehicle");
            if (file == null)
                return BadRequest("Null File");
         //   if(file.Length > photoSettings.MaxBytes)
            //    return BadRequest("empty File");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);
  // if(!photoSettings.isAcceptedFile(file.FileName))
      //        return BadRequest("invalid path");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
         var filePath =    Path.Combine(uploadsFolderPath, fileName);


            using (var stream = new FileStream(filePath, FileMode.Create) )
            {
                await  file.CopyToAsync(stream);

            }
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return Ok( mapper.Map<Photo, PhotoResource>(photo));
        }
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {

            var photos = await photoRepository.GetPhotos(vehicleId);
           return  mapper.Map<IEnumerable<Photo>,IEnumerable<PhotoResource>>(photos);

            
        }
    }
}
