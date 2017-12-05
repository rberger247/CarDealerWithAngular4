﻿using AThirdCarDealership.Controllers.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
       // public KeyValuePairResource Model { get; set; }
       public ModelResource Model { get; set; }
        public MakeResource Make { get; set; }
      //  public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }

        public ICollection<FeatureResource> Features { get; set; }
      //  public ICollection<KeyValuePairResource> Features { get; set; }

        public VehicleResource()
        {
            //  Features = new Collection<KeyValuePairResource>();
            Features = new Collection<FeatureResource>();
        }
    }
}