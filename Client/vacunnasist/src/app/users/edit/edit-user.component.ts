import { User } from 'src/app/_models/user';
import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { DatePipe, Location } from '@angular/common';


@Component({ templateUrl: 'edit-user.component.html' })
export class EditUserComponent implements OnInit {
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
        private alertService: AlertService,
        private dp: DatePipe,
        private _location: Location
    ) { 

    }

    public type: String = "patient";
    userId?: number;
    userRole?:string;
    user: User = new User();

    ngOnInit() {
        this.minDate = new Date(1900, 0, 1);

        this.userId = parseInt(this.route.snapshot.paramMap.get('id')!);
        this.accountService.getById(this.userId).subscribe(res => {
            this.type = res.role;
            this.userRole = res.role;
            this.user = res;
        this.form.patchValue({
            id: res.id,
            username: res.userName,
                fullName: res.fullName,
                dni: res.dni,
                address: res.address,
                birthDate: res.birthDate,
                email: res.email,
                gender: res.gender,
            role: res.role,
            isActive: res.isActive,
            province: res.province
        });

     
    });

        this.form = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(20)]],
            fullName: ['', [Validators.required, Validators.maxLength(100)]],
            dni: ['', [Validators.required, Validators.maxLength(20)]],
            address: ['', [Validators.required, Validators.maxLength(200)]],
            birthDate: [new Date(), Validators.required],
            email:['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            gender: ['male', Validators.required],
            province: ['', Validators.required],
            role: ['', Validators.required],
        });
    }

    addVaccine() {
        this.router.navigate(['appointments/add-vaccine-to-user', this.userId]);
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }
    
    backClicked()
    {
        this._location.back();
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
        
        this.form.value.birthDate = this.dp.transform(this.form.value.birthDate, 'yyyy-MM-dd');
        this.form.value.dni = String(this.form.value.dni);
        this.accountService.update(this.userId!, this.form.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success((this.type == 'patient' ? 'Paciente ' : (this.type == 'vacunator' ? 'Vacunador ' : 'Usuario')) + ' modificado correctamente', { keepAfterRouteChange: true });
                     this._location.back();
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}