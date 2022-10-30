import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'patientgender' })
export class PatientGenderPipe implements PipeTransform {
  transform(value: string): string {
    if (value == 'male') {
      return 'Hombre';
    }
    if (value == 'female') {
      return 'Mujer';
    }
    return 'Otro';
  }
}
