import { Pipe, PipeTransform } from '@angular/core';
/*
 * Transforms user role to spanish text
 * Usage:
 *   user.role | userrole
 */
@Pipe({ name: 'userrole' })
export class UserRolePipe implements PipeTransform {
  transform(value: string): string {
    if (value == 'operator') {
      return 'Operador nacional';
    }
    if (value == 'administrator') {
      return 'Administrador';
    }
    if (value == 'vacunator') {
      return 'Vacunador';
    }
    if (value == 'analyst') {
      return 'Analista provincial';
    }
    return 'Paciente';
  }
}
