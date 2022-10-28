import { DevelopedVaccine } from "./developed-vaccine";

export class AppliedVaccine {
    id!: number;
    userId!: number;
    vaccineId!: number;
    developedVaccineId!: number;
    batchNumber!: string;
    vaccine!: DevelopedVaccine;
    appliedDate!: Date;
    appliedBy?: string;
    province?: string;
    fullName?: string;
    dni?: string;
}