import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'purchaseorderstatus' })
export class PurchaseOrderStatusPipe implements PipeTransform {
  transform(value: number): string {
    if (value == 0) {
      return 'Pendiente';
    }
    if (value == 1) {
      return 'Pagada';
    }
    return 'Entregada';
  }
}
