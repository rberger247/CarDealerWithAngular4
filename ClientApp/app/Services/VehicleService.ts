import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {
    private readonly vehiclesEndpoint = '/api/vehicles';

    constructor(private http: Http) { }

    getFeatures() {
        return this.http.get('/api/features')
            .map(res => res.json());
    }

    getMakes() {
        return this.http.get('/api/makes')
            .map(res => res.json());
    }
    create(vehicle : any) {
        return this.http.post('/api/vehicles', vehicle)
            .map(res => res.json());
    }
    //create(vehicle) {
    //    return this.http.post(this.vehiclesEndpoint, vehicle)
    //        .map(res => res.json());
    //}


    //update(vehicle: SaveVehicle) {
    //    return this.http.put('/api/vehicles/' + vehicle.id, vehicle)
    //        .map(res => res.json());
    //}
    getModels() {
        return this.http.get('/api/models').map(res => res.json());



    }

}
