import { AppliedVaccine } from './../../_models/applied-vaccine';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { DatePipe } from '@angular/common';
import { User } from 'src/app/_models/user';
import Swal from 'sweetalert2';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';


@Component({ templateUrl: 'profile.component.html' })
export class ProfileComponent implements OnInit {
    form!: UntypedFormGroup;
    loading = false;
    submitted = false;
    minDate: Date = new Date();
    maxDate: Date = new Date();

    constructor(
        private formBuilder: UntypedFormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService,
        private dp: DatePipe
    ) { 
    }

    public user: User = new User;
    ngOnInit() {
        this.minDate = new Date(1900, 0, 1);

        this.loadData();
        
        this.form = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(20)]],
            fullName: ['', [Validators.required, Validators.maxLength(100)]],
            dni: ['', [Validators.required, Validators.maxLength(20)]],
            address: ['', [Validators.required, Validators.maxLength(200)]],
            birthDate: [new Date(), Validators.required],
            email:['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            gender: ['male', Validators.required],
        });
    }

    loadData() {
        this.accountService.myProfile().subscribe((res: any) => {
            this.user = res;
            this.form.patchValue({
                username: res.userName,
                fullName: res.fullName,
                dni: res.dni,
                address: res.address,
                birthDate: res.birthDate,
                email: res.email,
                gender: res.gender,
            });
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
        this.accountService.update(+this.accountService.userValue.id, this.form.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success('Perfil modificado correctamente', { keepAfterRouteChange: true });
                    this.router.navigate(['/'], { relativeTo: this.route });
                },
                error: (error: string) => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }

    deleteVaccineQuestion(v: AppliedVaccine) {
        Swal
      .fire({
        title: '¿Está seguro?',
        text: 'Va a eliminar la vacuna: ' + v.vaccine.name,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonText: 'Si, eliminar!'
      })
      .then(result => {
        if (result.value) {
          this.deleteVaccine(v);
          
        }
      });
    }

    deleteVaccine(v: AppliedVaccine) {
        this.accountService.deleteVaccine(+this.accountService.userValue.id, v.id)
        .pipe(first())
        .subscribe({
            next: () => {
                Swal.fire('Eliminada!', 'Vacuna eliminada', 'success');
                this.loadData();
            },
            error: (error: string) => {
                this.alertService.error(error);
                this.loading = false;
            }
        });
    }

    addVaccine() {
        this.router.navigate(['account/profile/add-vaccine']);
    }
}