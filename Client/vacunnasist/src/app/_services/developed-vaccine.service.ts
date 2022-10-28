import { UpdateVaccineRequest } from '../_models/update-vaccine';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DevelopedVaccine } from '../_models/developed-vaccine';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { DevelopedVaccinesFilter } from '../_models/filters/developed-vaccines-filter';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DevelopedVaccineService {
  constructor(private http: HttpClient) {}

  getAll(filter: DevelopedVaccinesFilter) {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    let params = new HttpParams();
    if (filter.isActive !== undefined)
      params = params.append('isActive', filter.isActive.toString());
    if (filter.name) params = params.append('name', filter.name.toString());
    if (filter.vaccineId)
      params = params.append('vaccineId', filter.vaccineId.toString());
    return this.http.get<DevelopedVaccine[]>(
      `${environment.apiUrl}/developedVaccines`,
      {
        headers: headers,
        params: params,
      }
    );
  }

  getById(id: number) {
    return this.http.get<DevelopedVaccine>(
      `${environment.apiUrl}/developedVaccines/${id}`
    );
  }

  canBeDeleted(id: number): Observable<boolean> {
    return this.http.get<boolean>(
      `${environment.apiUrl}/developedVaccines/${id}/can-delete`
    );
  }

  newVaccine(vaccine: DevelopedVaccine) {
    return this.http.post(`${environment.apiUrl}/developedVaccines`, vaccine);
  }

  update(id: number, model: DevelopedVaccine) {
    return this.http.put<UpdateVaccineRequest>(
      `${environment.apiUrl}/developedVaccines/${id}`,
      model
    );
  }

  cancel(id: number) {
    return this.http.delete(`${environment.apiUrl}/developedVaccines/${id}`);
  }
}
