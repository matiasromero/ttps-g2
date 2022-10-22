import { Appointment } from './../../_models/appointment';
import { first } from 'rxjs/operators';
import { Vaccine } from './../../_models/vaccine';
import { VaccineService } from 'src/app/_services/vaccine.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { AppointmentService } from 'src/app/_services/appointment.service';
import { User } from 'src/app/_models/user';
import { UsersFilter } from 'src/app/_models/filters/users-filter';
import { NewConfirmedAppointmentRequest } from 'src/app/_models/new-confirmed-appointment';
import { DatePipe } from '@angular/common';
import { VaccinesFilter } from 'src/app/_models/filters/vaccines-filter';


@Component({ templateUrl: 'confirm-appointment.component.html' })
export class ConfirmAppointmentComponent implements OnInit {
    form!: UntypedFormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: UntypedFormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private accountService: AccountService,
        private appointmentsService: AppointmentService,
        private alertService: AlertService,
        private dp: DatePipe
    ) { 
        if (this.accountService.userValue.role !== 'administrator') {
            this.router.navigate(['/']);
        }
    }

    public vaccines: Vaccine[] = [];
    public patients: User[] = [];
    public vaccinators: User[] = [];
    public minDate: Date = new Date();
    public maxDate: Date = new Date();
    public patientVaccine!: Vaccine;

    appointmentId?: number;

    ngOnInit() {

        this.appointmentId = parseInt(this.route.snapshot.paramMap.get('id')!);

        let filter1 = new UsersFilter();
      filter1.role = 'vacunator';
      this.accountService.getAll(filter1).subscribe((res: any) => {
        this.vaccinators = res.users;
    });

    let filter2 = new UsersFilter();
      filter2.role = 'patient';
      this.accountService.getAll(filter2).subscribe((res: any) => {
        this.patients = res.users;
    });

    this.appointmentsService.getById(this.appointmentId).subscribe((res: Appointment) => {
        this.patientVaccine = new Vaccine();
        this.patientVaccine.id = res.vaccineId.toString();
        this.patientVaccine.name = res.vaccineName!;
        this.changePatient(res.patientId);
        
        this.form.patchValue({
            id: res.id,
            patientId: res.patientId,
            vaccineId: res.vaccineId,
        });
    });

        this.form = this.formBuilder.group({
            vaccineId: [null, Validators.required],
            patientId: [null, Validators.required], 
            vaccinatorId: [null, Validators.required],
            date: [new Date(), Validators.required],
            time: [null, Validators.required]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    changePatient(patientId: number) {
        this.accountService.getById(patientId).subscribe((u: User) => {
            let filter = new VaccinesFilter();
        filter.isActive = true;
        filter.canBeRequested = true;
        });

        
      }

      changeVaccine(vaccineId: number) {
       
      }

    onSubmit() {
        this.submitted = true;
        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        var model = new NewConfirmedAppointmentRequest();
        model.patientId = this.form.get('patientId')?.value;
        model.vaccinatorId = this.form.get('vaccinatorId')?.value;
        model.vaccineId = this.form.get('vaccineId')?.value;
        model.date = this.dp.transform(this.form.value.date, 'yyyy-MM-dd')!;
        model.date = model.date +'T'+ this.form.get('time')?.value + ':00.000';
        model.currentId = this.appointmentId!;
        this.appointmentsService.newConfirmedAppointment(model)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success('Turno cargado correctamente', { keepAfterRouteChange: true });
                    this.loading = false;
                    this.router.navigate(['../'], { relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}