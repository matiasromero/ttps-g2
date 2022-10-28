import { PurchaseOrder } from './../_models/purchase-order';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PurchaseOrdersFilter } from '../_models/filters/purchase-orders-filter';
import { UpdatePurchaseOrderRequest } from '../_models/update-purchase-order';
import { NewPurchaseOrderRequest } from '../_models/new-purchase-order';

@Injectable({ providedIn: 'root' })
export class PurchaseOrdersService {
  constructor(private http: HttpClient) {}

  getAll(filter: PurchaseOrdersFilter) {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    let params = new HttpParams();
    if (filter.batchNumber)
      params = params.append('batchNumber', filter.batchNumber.toString());
    if (filter.eta) params = params.append('eta', filter.eta.toString());
    if (filter.purchaseDate)
      params = params.append('purchaseDate', filter.purchaseDate.toString());
    if (filter.status)
      params = params.append('status', filter.status.toString());
    return this.http.get<PurchaseOrder[]>(
      `${environment.apiUrl}/purchaseOrders`,
      {
        headers: headers,
        params: params,
      }
    );
  }

  getById(id: number) {
    return this.http.get<PurchaseOrder>(
      `${environment.apiUrl}/purchaseOrders/${id}`
    );
  }

  newPurchaseOrder(po: NewPurchaseOrderRequest) {
    return this.http.post(`${environment.apiUrl}/purchaseOrders`, po);
  }

  update(id: number, model: UpdatePurchaseOrderRequest) {
    return this.http.put<UpdatePurchaseOrderRequest>(
      `${environment.apiUrl}/purchaseOrders/${id}`,
      model
    );
  }
}
