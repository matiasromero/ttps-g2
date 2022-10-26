import { UpdateVaccineRequest } from './../_models/update-vaccine';
import { AppliedVaccine } from './../_models/applied-vaccine';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Vaccine } from '../_models/vaccine';
import { DownloadCertificateModel } from '../_models/download-certificate';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { VaccinesFilter } from '../_models/filters/vaccines-filter';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class VaccineService {
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
        if (filter.isActive !== undefined)
            params = params.append('isActive', filter.isActive.toString());
          if (filter.name)
            params = params.append('name', filter.name.toString());
          return this.http.get<Vaccine[]>(`${environment.apiUrl}/vaccines`, 
          {
             headers: headers,
          params: params
      });
    }

    getById(id: number) {
        return this.http.get<Vaccine>(`${environment.apiUrl}/vaccines/${id}`);
    }

    canBeDeleted(id: number): Observable<boolean> {
        return this.http.get<boolean>(`${environment.apiUrl}/vaccines/${id}/can-delete`);
    }

    newVaccine(vaccine: Vaccine) {
        return this.http.post(`${environment.apiUrl}/vaccines`, vaccine);
    }

    update(id: number, model: Vaccine) {
        return this.http.put<UpdateVaccineRequest>(`${environment.apiUrl}/vaccines/${id}`, model);
    }

    cancel(id: number) {
        return this.http.delete(`${environment.apiUrl}/vaccines/${id}`);
    }

    
}