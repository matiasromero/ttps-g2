import { Department } from './department';

export class Patient {
  id!: number;
  name!: string;
  surname!: string;
  dni!: string;
  gender!: string;
  birthDate!: string;
  department!: Department;
  pregnant!: boolean;
  healthWorker!: boolean;
}
