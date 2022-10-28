import { BatchVaccine } from './../_models/batch-vaccine';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BatchVaccinesFilter } from '../_models/filters/batch-vaccines-filter';

@Injectable({ providedIn: 'root' })
export class BatchVaccineService {
  constructor(private http: HttpClient) {}

  getAll(filter: BatchVaccinesFilter) {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    let params = new HttpParams();
    if (filter.batchNumber)
      params = params.append('batchNumber', filter.batchNumber.toString());
    if (filter.vaccineId)
      params = params.append('vaccineId', filter.vaccineId.toString());
    if (filter.developedVaccineId)
      params = params.append(
        'developedVaccineId',
        filter.developedVaccineId.toString()
      );
    return this.http.get<BatchVaccine[]>(
      `${environment.apiUrl}/batchVaccines`,
      {
        headers: headers,
        params: params,
      }
    );
  }

  getById(id: number) {
    return this.http.get<BatchVaccine>(
      `${environment.apiUrl}/batchVaccines/${id}`
    );
  }
}
