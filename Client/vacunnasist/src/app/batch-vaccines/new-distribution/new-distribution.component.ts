import { BatchVaccineService } from 'src/app/_services/batch-vaccine.service';
import { VaccinesService } from 'src/app/_services/vaccines.service';
import { first } from 'rxjs/operators';
import { DevelopedVaccineService } from 'src/app/_services/developed-vaccine.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { Vaccine } from 'src/app/_models/vaccine';
import { VaccinesFilter } from 'src/app/_models/filters/vaccines-filter';
import { DevelopedVaccine } from 'src/app/_models/developed-vaccine';

@Component({ templateUrl: 'new-distribution.component.html' })
export class NewDistributionOrderComponent implements OnInit {
  form!: UntypedFormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private batchVaccineService: BatchVaccineService,
    private vaccinesService: VaccinesService,
    private alertService: AlertService
  ) {}

  public vaccines: Vaccine[] = [];
  public developedVaccines: DevelopedVaccine[] = [];

  ngOnInit() {
    let vaccinesFilter = new VaccinesFilter();
    vaccinesFilter.withStock = true;
    this.vaccinesService.getAll(vaccinesFilter).subscribe((res: any) => {
      this.vaccines = res.vaccines;
    });

    this.form = this.formBuilder.group({
      vaccineId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      province: ['Buenos Aires', Validators.required],
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
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

    this.batchVaccineService
      .newDistribution(this.form.value)
      .pipe(first())
      .subscribe({
        next: (r: any) => {
          this.alertService.success(r.message, {
            keepAfterRouteChange: true,
          });
          this.router.navigate(['../../batch-vaccines'], {
            relativeTo: this.route,
          });
        },
        error: (error) => {
          this.alertService.error(error);
          this.loading = false;
        },
      });
  }
}
