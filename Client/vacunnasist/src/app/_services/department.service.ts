import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AppliedVaccinesFilter } from '../_models/filters/applied-vaccines-filter';
import { AppliedVaccine } from '../_models/applied-vaccine';
import { NewApplicationVaccineRequest } from '../_models/new-application-vaccine';
import { Department } from '../_models/department';

@Injectable({ providedIn: 'root' })
export class DepartmentService {
  constructor(private http: HttpClient) {}

  getAll() {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    return this.http.get<Department[]>(`${environment.apiUrl}/departments`, {
      headers: headers,
    });
  }

  getById(id: number) {
    return this.http.get<Department>(`${environment.apiUrl}/departments/${id}`);
  }

  getRandomByProvince(province: string) {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    let params = new HttpParams();
    params = params.append('provinceName', province);

    return this.http.get<Department>(
      `${environment.apiUrl}/departments/random-by-province`,
      {
        headers: headers,
        params: params,
      }
    );
  }
}
