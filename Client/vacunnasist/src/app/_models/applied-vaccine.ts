import { DevelopedVaccine } from "./developed-vaccine";

export class AppliedVaccine {
    id!: number;
    userId!: number;
    vaccineId!: number;
    vaccine!:DevelopedVaccine;
    appliedDate!: Date;
    appliedBy?: String;
    comment?: string;
}