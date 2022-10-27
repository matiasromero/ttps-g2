import { Pipe, PipeTransform } from '@angular/core';
/*
 * Transforms user role to spanish text
 * Usage:
 *   user.role | userrole
 */
@Pipe({ name: 'vaccinetype' })
export class VaccineTypePipe implements PipeTransform {
  transform(value: string): string {
    if (value == '0') {
      return 'Calendario';
    }
    if (value == '1') {
      return 'Pandemia';
    }
    return 'Estacional';
  }
}
