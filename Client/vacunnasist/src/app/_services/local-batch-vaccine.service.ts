import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LocalBatchVaccinesFilter } from '../_models/filters/local-batch-vaccines-filter';
import { LocalBatchVaccine } from '../_models/local-batch-vaccine';

@Injectable({ providedIn: 'root' })
export class LocalBatchVaccineService {
  constructor(private http: HttpClient) {}

  getAll(filter: LocalBatchVaccinesFilter) {
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
    if (filter.province)
      params = params.append('province', filter.province.toString());
    return this.http.get<LocalBatchVaccine[]>(
      `${environment.apiUrl}/localBatchVaccines`,
      {
        headers: headers,
        params: params,
      }
    );
  }

  getById(id: number) {
    return this.http.get<LocalBatchVaccine>(
      `${environment.apiUrl}/localBatchVaccines/${id}`
    );
  }
}
