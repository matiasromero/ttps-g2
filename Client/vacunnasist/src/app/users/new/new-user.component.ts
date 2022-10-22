import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { AppointmentService } from 'src/app/_services/appointment.service';
import { DatePipe } from '@angular/common';
import { trigger } from '@angular/animations';


@Component({ templateUrl: 'new-user.component.html' })
export class NewUserComponent implements OnInit {
    form!: UntypedFormGroup;
    loading = false;
    submitted = false;
    minDate: Date = new Date();
    maxDate: Date = new Date();

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

    public type: String = "patient";

    ngOnInit() {
        this.minDate = new Date(1900, 0, 1);

        let typeParam = this.route.snapshot.queryParams['type'];
        if (typeParam){
            this.type = typeParam;
        }

        this.form = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(20)]],
            password: ['', Validators.required],
            fullName: ['', [Validators.required, Validators.maxLength(100)]],
            dni: ['', [Validators.required, Validators.maxLength(20)]],
            address: ['', [Validators.required, Validators.maxLength(200)]],
            birthDate: [new Date(), Validators.required],
            email:['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            gender: ['male', Validators.required],
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        
        this.form.value.birthDate = this.dp.transform(this.form.value.birthDate, 'yyyy-MM-dd');
        this.form.value.dni = String(this.form.value.dni);
        this.form.value.role = this.type;
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success((this.type == 'patient' ? 'Paciente ' : (this.type == 'vacunator' ? 'Vacunador' : 'Usuario')) + ' creado correctamente', { keepAfterRouteChange: true });
                    this.router.navigate(['../../users'], { 
                        queryParams: {type: this.type, isActive: true},
                     relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}