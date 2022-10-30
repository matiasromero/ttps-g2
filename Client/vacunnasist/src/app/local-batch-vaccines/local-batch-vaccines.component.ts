import { AccountService } from 'src/app/_services/account.service';
import { Vaccine } from './../_models/vaccine';
import { DevelopedVaccine } from './../_models/developed-vaccine';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { VaccinesFilter } from '../_models/filters/vaccines-filter';
import { VaccinesService } from '../_services/vaccines.service';
import { DevelopedVaccineService } from '../_services/developed-vaccine.service';
import { DevelopedVaccinesFilter } from '../_models/filters/developed-vaccines-filter';
import { LocalBatchVaccineService } from '../_services/local-batch-vaccine.service';
import { LocalBatchVaccine } from '../_models/local-batch-vaccine';
import { LocalBatchVaccinesFilter } from '../_models/filters/local-batch-vaccines-filter';
import { User } from '../_models/user';

@Component({ templateUrl: 'local-batch-vaccines.component.html' })
export class LocalBatchVaccinesComponent implements OnInit {
  formFilter!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private localBatchVaccineService: LocalBatchVaccineService,
    private vaccineService: VaccinesService,
    private developedVaccineService: DevelopedVaccineService,
    private alertService: AlertService
  ) {
    this.accountService.user.subscribe((x) => (this.user = <User>x));

    this.formFilter = this.formBuilder.group({
      batchNumber: ['', [Validators.maxLength(20)]],
      vaccineId: [''],
      developedVaccineId: [''],
      province: [''],
    });

    this.route.queryParams.subscribe((params) => {
      if (params.batchNumber) {
        this.formFilter.controls.batchNumber.setValue(params.batchNumber, {
          onlySelf: true,
        });
      }
      if (params.vaccineId) {
        this.formFilter.controls.vaccineId.setValue(+params.vaccineId);
      }
      if (params.developedVaccineId) {
        this.formFilter.controls.developedVaccineId.setValue(
          +params.developedVaccineId
        );
      }
      if (params.province) {
        this.formFilter.controls.province.setValue(params.province, {
          onlySelf: true,
        });
      }

      this.loadData();
    });
  }

  public localBatchVaccines: LocalBatchVaccine[] = [];
  public filter = new LocalBatchVaccinesFilter();
  public vaccines: Vaccine[] = [];
  public developedVaccines: DevelopedVaccine[] = [];
  user?: User;

  ngOnInit() {
    let vaccinesFilter = new VaccinesFilter();
    this.vaccineService.getAll(vaccinesFilter).subscribe((res: any) => {
      this.vaccines = res.vaccines;
    });

    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    this.developedVaccineService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
      });
  }

  loadData() {
    const batchNumber = this.formFilter.get('batchNumber')?.value;
    const vaccineId = this.formFilter.get('vaccineId')?.value;
    const developedVaccineId = this.formFilter.get('developedVaccineId')?.value;
    const province = this.formFilter.get('province')?.value;
    this.filter.batchNumber = batchNumber;
    this.filter.vaccineId = vaccineId;
    this.filter.developedVaccineId = developedVaccineId;
    this.filter.province = province;
    this.localBatchVaccineService.getAll(this.filter).subscribe((res: any) => {
      this.localBatchVaccines = res.vaccines;
    });
  }

  panelOpenState = false;

  // convenience getter for easy access to form fields
  get f() {
    return this.formFilter.controls;
  }

  changeVaccine(vaccineId: number) {
    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    developedVaccinesFilter.vaccineId = vaccineId;
    this.formFilter.controls.developedVaccineId.setValue('', {
      onlySelf: true,
    });
    this.developedVaccineService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
      });
  }

  applyFilter() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.formFilter.invalid) {
      return;
    }

    this.loading = true;

    const batchNumber = this.formFilter.get('batchNumber')?.value;
    const vaccineId = this.formFilter.get('vaccineId')?.value;
    const developedVaccineId = this.formFilter.get('developedVaccineId')?.value;
    const province = this.formFilter.get('province')?.value;
    const queryParams: any = {};

    if (batchNumber) {
      queryParams.batchNumber = batchNumber;
    }
    if (vaccineId) {
      queryParams.vaccineId = vaccineId;
    }
    if (developedVaccineId) {
      queryParams.developedVaccineId = developedVaccineId;
    }
    if (province) {
      queryParams.province = province;
    }

    this.router.navigate(['/local-batch-vaccines'], {
      queryParams,
    });

    this.loading = false;
  }

  clear() {
    // reset alerts on submit
    this.alertService.clear();

    this.formFilter.controls.batchNumber.setValue('', {
      onlySelf: true,
    });
    this.formFilter.controls.vaccineId.setValue('', {
      onlySelf: true,
    });
    this.formFilter.controls.developedVaccineId.setValue('', {
      onlySelf: true,
    });
    this.formFilter.controls.province.setValue('', {
      onlySelf: true,
    });

    this.applyFilter();
  }

  select(batch: LocalBatchVaccine) {
    this.router.navigate(['local-batch-vaccines', batch.id]);
  }
}
