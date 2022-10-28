import { DevelopedVaccine } from './developed-vaccine';

export class PurchaseOrder {
  public id!: number;
  public batchNumber!: string;
  public status!: number;
  public developedVaccine!: DevelopedVaccine;

  public purchaseDate!: Date;
  public eta?: Date;
  public deliveredTime?: Date;

  public quantity!: number;
}
