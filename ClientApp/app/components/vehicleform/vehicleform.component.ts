//import { Component, OnInit } from '@angular/core';

//import * as _ from 'underscore';
//import { ToastyService } from "ng2-toasty";
//import { SaveVehicle, Vehicle } from './../../models/vehicle';
//import { VehicleService } from "../../Services/VehicleService";
//import { ActivatedRoute, Router } from "@angular/router";
//import { Observable } from "rxjs/Observable";
//import 'rxjs/add/Observable/forkJoin';




//@Component({
//    selector: 'vehicleform',
//    templateUrl: './vehicle-form.component.html',
//    styleUrls: ['./vehicle-form.component.css'],
//    providers: [VehicleService]

//}

//)
//export class VehicleFormComponent implements OnInit {

//    makes: any[];
//    models: any[];
//    features: any[];
//    vehicle: SaveVehicle = {
//        id: 0,
//        makeId: 0,
//        modelId: 0,
//        isRegistered: false,
//        features: [],
//        contact: {
//            name: '',
//            email: '',
//            phone: '',
//        }

//    };




//    constructor(
//        private route: ActivatedRoute,
//        private router: Router,
//        private vehicleService: VehicleService,
//        private toastyService: ToastyService
//    ) {
//        route.params.subscribe(p => {
           
//            this.vehicle.id = +p['id'];
//        });

//        }
    
    


//    ngOnInit() {

//        this.vehicleService.getVehicle(this.vehicle.id).subscribe(v => {
//            this.vehicle = v;
//        })
//        var sources = [
//            this.vehicleService.getMakes(),
//            this.vehicleService.getFeatures(),
//        ];

//        if (this.vehicle.id)
//            sources.push(this.vehicleService.getVehicle(this.vehicle.id));

//        Observable.forkJoin(sources).subscribe(data => {
//            this.makes = data[0];
//            this.features = data[1];

//            if (this.vehicle.id) {
//                this.setVehicle(data[2]);
//                this.populateModels();
//            }
//        }, err => {
//            if (err.status == 404)
//                this.router.navigate(['/home']);
//        });
//    }

//    private setVehicle(v: Vehicle) {
//        this.vehicle.id = v.id;
//        this.vehicle.makeId = v.make.id;
//        this.vehicle.modelId = v.model.id;
//        this.vehicle.isRegistered = v.isRegistered;
//        this.vehicle.contact = v.contact;
//        this.vehicle.features = _.pluck(v.features, 'id');
//    }

//    onMakeChange() {
//        this.populateModels();

//        delete this.vehicle.modelId;
//    }

//    private populateModels() {

//        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
//        this.models = selectedMake ? selectedMake.models : [];
//    }

//    onFeatureToggle(featureId: any, $event: any) {
//        if ($event.target.checked)
//            this.vehicle.features.push(featureId);
//        else {
//            var index = this.vehicle.features.indexOf(featureId);
//            this.vehicle.features.splice(index, 1);
//        }
//    }

//    submit() {
//        if (this.vehicle.id) {
//            this.vehicleService.update(this.vehicle)
//                .subscribe(x => {
//                    this.toastyService.success({
//                        title: 'Success',
//                        msg: 'The vehicle was sucessfully updated.',
//                        theme: 'bootstrap',
//                        showClose: true,
//                        timeout: 5000
//                    });
//                });
//        }
//        else {
//            this.vehicleService.create(this.vehicle)
//                .subscribe(x => console.log(x));

//        }
//    }

//}
  

           

   

// section 6

import { VehicleService } from '../../services/vehicleService';

import * as _ from 'underscore';
import { Component, OnInit } from '@angular/core';
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
import { Vehicle, SaveVehicle } from "../../models/vehicle";
@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html'

})
export class VehicleFormComponent implements OnInit {
    makes: any[];
    models: any[];
    features: any[];
    contact: any = {};

    vehicle: any = {
        features: [],
        contact: {}
    };
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

    constructor(
        private vehicleService: VehicleService,
        private toastyService: ToastyService,
        private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            this.vehicle.id = +p['id'] || 0 ;

        }

        )
    }


    ngOnInit() {
        //--------------------------this works but does not populate boxes
        //var sources = [
        //    this.vehicleService.getMakes(),
        //    this.vehicleService.getFeatures(),


        //];
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures(),
        ];

        if (this.vehicle.id)
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[0];
            this.features = data[1];

            if (this.vehicle.id) {
                this.setVehicle(data[2]);
                this.populateModels();
            }
        }, err => {
            if (err.status == 404)
                this.router.navigate(['/home']);
        });

        //if(this.vehicle.id)

        //sources.push( this.vehicleService.getVehicle(this.vehicle.id))
        //Observable.forkJoin(sources
        //   ).

        //    });
        //-------------------------------------------------------------------------------------------
        //    subscribe(data => {
        //        this.makes = data[0];
        //        this.features = data[1];
        //        if(this.vehicle.id)
        //        this.vehicle = data[2];

        //    }
        //    ,
        //    err => {
        //        if (err.status == 400)
        //            this.router.navigate(['/vehicles']);

    }

    private setVehicle(v: Vehicle) {
        this.vehicle.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.contact = v.contact;
        this.vehicle.features = _.pluck(v.features, 'id');
    }
    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }


    onMakeChange() {
        //var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        //this.models = selectedMake ? selectedMake.models : [];
        this.populateModels();
        delete this.vehicle.modelId;
    }
    onFeatureToggle(featureId, $event) {
        if ($event.target.checked)
            this.vehicle.features.push(featureId);
        else {

            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }





    }
    delete() {

        if (confirm('are you sure ?')) {

            this.vehicleService.delete(this.vehicle.id).subscribe(x => {

                this.toastyService.success({
                    title: 'Success',
                    msg: ' record was deleted.',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                });

            });
            ;
        }
    }
    submit() {
        var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
        result$.subscribe(vehicle => {
            this.toastyService.success({
                title: 'Success',
                msg: 'Data was sucessfully saved.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
            this.router.navigate(['/vehicles/', vehicle.id])
        });
    }
}

