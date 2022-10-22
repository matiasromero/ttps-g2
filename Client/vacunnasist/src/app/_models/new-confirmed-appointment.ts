export class NewConfirmedAppointmentRequest {
    vaccineId!: number;
    vaccinatorId!: number;
    patientId!: number;
    date!: string;
    currentId?:number;
    status?:number;
    comment?:string;
}