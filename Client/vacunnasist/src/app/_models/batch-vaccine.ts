import { DevelopedVaccine } from './developed-vaccine';

export class BatchVaccine {
    public id!: number;
    public batchNumber!: string;
    public status!:number;
    public developedVaccine!: DevelopedVaccine;
    
    public dueDate!: Date;

    public quantity!: number;
    public remainingQuantity!: number;
    public overdueQuantity!: number;
    public distributedQuantity!: number;

    
}