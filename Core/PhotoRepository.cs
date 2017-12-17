using AThirdCarDealership.Core.Models;
using AThirdCarDealership.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AThirdCarDealership.Core
{
    public class PhotoRepository
    {

        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {



            return await context.Photos.Where(v => v.VehicleId == vehicleId).ToListAsync();

        }
            
        }

    }

