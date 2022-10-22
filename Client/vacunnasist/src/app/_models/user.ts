import { AppliedVaccine } from "./applied-vaccine";

export class User {
    id!: string;
    userName!: string;
    address!: string;
    password!: string;
    fullName!: string;
    gender!: string;
    email!: string;
    birthDate!: Date;
    age!:number;
    dni!: string;
    role!: string;
    token!: string;
    isActive!: boolean;
    vaccines: AppliedVaccine[] = [];
    
    public isPatient(): boolean {
         return this.role === 'patient';
    }
}
