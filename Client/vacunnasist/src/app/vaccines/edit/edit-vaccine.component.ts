import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import { DatePipe, Location } from '@angular/common';
import { DevelopedVaccineService } from 'src/app/_services/developed-vaccine.service';
import { VaccinesFilter } from 'src/app/_models/filters/vaccines-filter';
import { Vaccine } from 'src/app/_models/vaccine';
import { VaccinesService } from 'src/app/_services/vaccines.service';

@Component({ templateUrl: 'edit-vaccine.component.html' })
export class EditVaccineComponent implements OnInit {
  form!: UntypedFormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private developedVaccineService: DevelopedVaccineService,
    private alertService: AlertService,
    private vaccinesService: VaccinesService,
    private _location: Location
  ) {}

  public vaccineId?: number;
  public vaccines: Vaccine[] = [];

  ngOnInit() {
    let filter = new VaccinesFilter();
    this.vaccinesService.getAll(filter).subscribe((res: any) => {
      this.vaccines = res.vaccines;
    });

    this.vaccineId = parseInt(this.route.snapshot.paramMap.get('id')!);
    this.developedVaccineService.getById(this.vaccineId).subscribe((res) => {
      this.form.patchValue({
        id: res.id,
        name: res.name,
        vaccineId: res.vaccine.id,
        daysToDelivery: res.daysToDelivery,
        isActive: res.isActive,
      });
    });

    this.form = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      vaccineId: [null, Validators.required],
      daysToDelivery: [0, Validators.required],
      isActive: [true, Validators.required],
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  backClicked() {
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

    this.developedVaccineService
      .update(this.vaccineId!, this.form.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Vacuna modificada correctamente', {
            keepAfterRouteChange: true,
          });
          this._location.back();
        },
        error: (error) => {
          this.alertService.error(error);
          this.loading = false;
        },
      });
  }
}
