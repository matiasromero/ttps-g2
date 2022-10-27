import { Vaccine } from './../_models/vaccine';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { VaccinesFilter } from '../_models/filters/vaccines-filter';

@Injectable({ providedIn: 'root' })
export class VaccinesService {
    constructor(
        private http: HttpClient,
    ) {
      
    }

    getAll(filter: VaccinesFilter) {
        const headers = new HttpHeaders().set(
            'Content-Type',
            'application/json; charset=utf-8'
          );
          
        let params = new HttpParams();
        if (filter.type !== undefined)
            params = params.append('type', filter.type.toString());
          return this.http.get<Vaccine[]>(`${environment.apiUrl}/vaccines`, 
          {
             headers: headers,
          params: params
      });
    }
}