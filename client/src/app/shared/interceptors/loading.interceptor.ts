import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { LoadingService } from '../services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private loadingService: LoadingService) { }

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (req.method === 'POST' && req.url.includes('orders')) {
      return next.handle(req);
    }

    if (req.method === 'DELETE') {
        return next.handle(req);
    }

    if (req.url.includes('emailverify')) {
        return next.handle(req);
    }

    this.loadingService.loading();

    return next.handle(req)
        .pipe(
            finalize(() => {
                this.loadingService.idle();
            })
        );
  }
}
