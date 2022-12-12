import { Department } from './department';
import { LocalBatchVaccine } from './local-batch-vaccine';
import { Patient } from './patient';
import { User } from './user';

export class AppliedVaccine {
  id!: number;
  user!: User;
  patient!: Patient;
  appliedDate!: Date;
  localBatchVaccine!: LocalBatchVaccine;
}
