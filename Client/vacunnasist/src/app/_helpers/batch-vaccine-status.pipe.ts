import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'batchvaccinestatus' })
export class BatchVaccineStatusPipe implements PipeTransform {
  transform(value: number): string {
    if (value == 0) {
      return 'Activa';
    }
    return 'Vencida';
  }
}
