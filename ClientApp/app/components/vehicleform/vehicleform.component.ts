import { Component, OnInit } from '@angular/core';

import { ToastyService } from "ng2-toasty";

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
    vehicle: any = {
        features: [],
        contact: {

        }

    };

   


    constructor(private vehicleService: VehicleService,
        private toastyService: ToastyService
  ) { }

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
        this.vehicleService.create(this.vehicle).subscribe(
            x => console.log(x),
            err => {
                this.toastyService.error({
                    title: 'error',
                    msg: 'an unexpected error',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000

                });


            });

}
  

           
            



      
    onMakeChange() {
        this.populateModels();

        delete this.vehicle.modelId;
    }
    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models =
            //selectedMake
           // ?
            selectedMake.models
        //: [];
    }

    onFeatureToggle(featureId: any, $event: any) {
        if ($event.target.checked)
            this.vehicle.features.push(featureId);
        else {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }

   

}