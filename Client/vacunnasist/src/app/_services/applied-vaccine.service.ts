import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AppliedVaccinesFilter } from '../_models/filters/applied-vaccines-filter';
import { AppliedVaccine } from '../_models/applied-vaccine';
import { NewApplicationVaccineRequest } from '../_models/new-application-vaccine';

@Injectable({ providedIn: 'root' })
export class AppliedVaccinesService {
  constructor(private http: HttpClient) {}

  getAll(filter: AppliedVaccinesFilter) {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    let params = new HttpParams();
    if (filter.appliedById) params = params.append('appliedById', filter.appliedById.toString());
    if (filter.province)
      params = params.append('province', filter.province.toString());
    if (filter.vaccineId)
     params = params.append('vaccineId', filter.vaccineId.toString());
    if (filter.developedVaccineId)
     params = params.append('developedVaccineId', filter.developedVaccineId.toString());
    if(filter.dni)
      params = params.append('dni', filter.dni.toString())

    return this.http.get<AppliedVaccine[]>(
      `${environment.apiUrl}/appliedVaccine`,
      {
        headers: headers,
        params: params,
      }
    );
  }

  getById(id: number) {
    return this.http.get<AppliedVaccine>(
      `${environment.apiUrl}/appliedVaccine/${id}`
    );
  }

  newApplication(newApplication: NewApplicationVaccineRequest) {
    return this.http.post(`${environment.apiUrl}/appliedVaccine`, newApplication);
  }
}
