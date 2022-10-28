import { DevelopedVaccine } from "./developed-vaccine";

export class AppliedVaccine {
    id!: number;
    userId!: number;
    vaccineId!: number;
    batchNumber!: string;
    vaccine!: DevelopedVaccine;
    appliedDate!: Date;
    appliedBy?: string;
    comment?: string;
}