import { Injectable } from '@angular/core';
import { Http } from "@angular/http";

@Injectable()
export class PhotoService {

    constructor(private http: Http) {


    }
    Upload(vehicleId, photo) {
        var formData = new FormData();

        //this file is the key used in postman and references the parameter in the controller
        formData.append('file', photo);
     return   this.http.post(`/api/vehicles/${vehicleId}/photos`, formData).map(res => res.json())
    }

    GetPhotos(vehicleId) {

        return this.http.get(`/api/vehicles/${vehicleId}/photos`).map(res => res.json());



    }
}
   