import { BatchVaccine } from './batch-vaccine';

export class LocalBatchVaccine {
  public id!: number;
  public batchVaccine!: BatchVaccine;

  public distributionDate!: Date;
  public province!: string;

  public quantity!: number;
  public remainingQuantity!: number;
  public overdueQuantity!: number;
}
