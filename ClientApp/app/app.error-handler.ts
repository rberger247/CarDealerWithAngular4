import * as Raven from 'raven-js';
import { ErrorHandler, Inject, NgZone } from '@angular/core';
import { ToastyService } from "ng2-toasty";

export class AppErrorHandler implements ErrorHandler {

    constructor(
        @Inject(NgZone) private ngZone : NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {

    }
    
    handleError(error: any): void {
        Raven.captureException(error.originalError || error)
        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'error',
                msg: 'something bad',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000

            });
          
            
        });
    }
}