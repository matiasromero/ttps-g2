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
    province!:string;
    token!: string;
    isActive!: boolean;
    vaccines: AppliedVaccine[] = [];
}
