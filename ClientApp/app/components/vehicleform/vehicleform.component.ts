import { Component, OnInit } from '@angular/core';



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
    onMakeChange() {
        this.populateModels();

        delete this.vehicle.modelId;
    }
    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
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