import { Vehicle, KeyValuePair } from './../../models/vehicle';

import { VehicleService } from "../../Services/VehicleService";
import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";

@Component({
    templateUrl: './vehicle-list.component.html',
     selector: 'vehiclelist',

    providers: [VehicleService]
})
export class VehicleListComponent implements OnInit {
    vehicle: any;
    features: any[];
    //filter: {
    //};
    filter: any = {};
  private readonly PAGE_SIZE = 3;
  vehicles: any = {
      features: [],
      contact: {}

  };
//  allVehicles: Vehicle [];
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { }
  ];

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {

    //  this.vehicleService.GetVehicles(this.filter).subscribe(vehicles => this.vehicles  = vehicles);

      //var sources = [
      //    this.vehicleService.getMakes(),
      //    this.vehicleService.getFeatures(),
      //];

      //if (this.vehicle.id)
      //    sources.push(this.vehicleService.getVehicle(this.vehicle.id));

      //Observable.forkJoin(sources).subscribe(data => {
      //    this.makes = data[0];
      //    this.features = data[1];

      //    if (this.vehicle.id) {
      //        this.setVehicle(data[2]);
      //        this.populateModels();
      //    }
      //}, err => {
      //    if (err.status == 404)
      //        this.router.navigate(['/home']);
      //});
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateVehicles();
  }

  private populateVehicles() {

      this.vehicleService.GetVehicles(this.filter).subscribe(vehicles  => this.vehicles  = vehicles);

    //this.vehicleService.GetVehicles(this.query)
    //  .subscribe(result => this.queryResult = result);
  }
  onFilterChange() {
      this.populateVehicles();



  }
  //onFilterChange() {

  //  // this.filter.makeId;
  //    var vehicles = this.allVehicles;
  //    if (this.filter.makeId) {

  //       vehicles = vehicles.filter(v => v.make.id == this.filter.makeId)
  //    };
  //    if (this.filter.modelId) {
  //        v => v.model.id == this.filter.modelId;
  //    }
  //    this.vehicles = vehicles;
  //  //this.query.page = 1;
  //  //this.populateVehicles();
  //}

  resetFilter() {
      this.filter = {};
      this.onFilterChange();

    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
 //   this.populateVehicles();
  }

  sortBy(columnName: any) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
  //  this.populateVehicles();
  }

  onPageChange(page: any) {
    this.query.page = page;
 //   this.populateVehicles();
  }
}