import { VaccinesService } from 'src/app/_services/vaccines.service';
import { first } from 'rxjs/operators';
import { DevelopedVaccineService } from 'src/app/_services/developed-vaccine.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { Vaccine } from 'src/app/_models/vaccine';
import { VaccinesFilter } from 'src/app/_models/filters/vaccines-filter';

@Component({ templateUrl: 'new-vaccine.component.html' })
export class NewVaccineComponent implements OnInit {
    form!: UntypedFormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: UntypedFormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private accountService: AccountService,
        private developedVaccineService: DevelopedVaccineService,
        private vaccinesService: VaccinesService,
        private alertService: AlertService
    ) { 

    }

    public vaccines: Vaccine[] = [];

    ngOnInit() {
        let filter = new VaccinesFilter();
        this.vaccinesService.getAll(filter).subscribe((res: any) => {
            this.vaccines = res.vaccines;
        });
        
        this.form = this.formBuilder.group({
            name: ['', [Validators.required, Validators.maxLength(100)]],
            vaccineId: [null, Validators.required],
            daysToDelivery: [0, Validators.required]
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
        
        this.developedVaccineService.newVaccine(this.form.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success('Vacuna desarrollada creada correctamente', { keepAfterRouteChange: true });
                    this.router.navigate(['../../vaccines'],{
                     relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}