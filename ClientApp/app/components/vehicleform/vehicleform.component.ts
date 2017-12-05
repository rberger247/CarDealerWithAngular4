import { Component, OnInit, ViewChild, TemplateRef, ViewContainerRef } from '@angular/core';

import { SaveVehicle, Vehicle } from './../../models/vehicle';

import { VehicleService } from "../../Services/VehicleService";




@Component({
    selector: 'vehicleform',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css'],
    providers: [VehicleService]
  
}

)
export class VehicleFormComponent implements OnInit {
   
    makes: any[];
    models: any[];
    features: any[];
    vehicle: any[];
    //vehicle: SaveVehicle = {
    //    id: 0,
    //    makeId: 0,
    //    modelId: 0,
    //    isRegistered: false,
    //    features: [],
    //    contact: {
    //        name: '',
    //        email: '',
    //        phone: '',
    //    }

    //};

   
    @ViewChild("vc", { read: ViewContainerRef }) vc: ViewContainerRef;
   
    constructor(private vehicleService: VehicleService) {}

    ngOnInit() {
        this.vehicleService.getMakes().subscribe(makes =>
            this.makes = makes);
            console.log( this.makes);

        
        this.vehicleService.getModels().subscribe(models => 
           this.models = models)

        this.vehicleService.getFeatures().subscribe(features =>
            this.features = features);
    }
    submit() {



    }

    //onMakeChange() {
    //    var selectedMake = this.makes.find(m => m.makeid == this.vehicle.Name);
    //    console.log(this.makes)
    //    var opt = "<option value='8'>Beer</option>";

    //   //this.models = selectedMake ? selectedMake.models: [];
    //  //  console.log(selectedMake.name);
       
     
    //}
}