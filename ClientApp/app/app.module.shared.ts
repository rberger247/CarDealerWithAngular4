import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import * as Raven  from 'raven-js';
import{ ToastyModule} from 'ng2-toasty'
//import { UniversalModule } from 'angular2-universal';

import { VehicleService } from './services/vehicleService';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from "./components/vehicleform/vehicleform.component";
import { AppErrorHandler } from "./app.error-handler";

Raven.config('https://fa7751a1fbec47a695a2db3339a196c9@sentry.io/255301')
  .install();


@NgModule({
    declarations: [
        AppComponent,

        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent
    ],
    imports: [
        CommonModule,
      //  UniversalModule,
        ToastyModule.forRoot(),
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'vehicle-form', component: VehicleFormComponent},
            { path: '**', redirectTo: 'home' }
        ])

    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        VehicleService
    ]
})
export class AppModuleShared {
}
