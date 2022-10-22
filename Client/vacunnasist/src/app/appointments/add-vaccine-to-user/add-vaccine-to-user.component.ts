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


@Component({ templateUrl: 'add-vaccine-to-user.component.html' })
export class AddVaccineToUserComponent implements OnInit {
    form!: UntypedFormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: UntypedFormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private accountService: AccountService,
        private vaccinesServices: VaccineService,
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

    userId?: number;

    ngOnInit() {

        this.userId = parseInt(this.route.snapshot.paramMap.get('id')!);

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

    this.accountService.getById(this.userId).subscribe((res: User) => {
        this.changePatient(this.userId!);
        this.form.patchValue({
            id: res.id,
            patientId: res.id
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
        this.accountService.getById(this.form.get('patientId')?.value).subscribe((u: User) => {
                    let v = new Vaccine();
                    v.id = vaccineId.toString();
            });
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
        model.date = model.date +'T'+ this.form.get('time')?.value;
        this.appointmentsService.newVaccine(model)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success('Vacuna agregada correctamente', { keepAfterRouteChange: true });
                    this.loading = false;
                    this.router.navigate(['../../../users', 'edit', this.userId], { relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}