import { DevelopedVaccinesFilter } from '../_models/filters/developed-vaccines-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import Swal from 'sweetalert2';
import { DevelopedVaccineService } from '../_services/developed-vaccine.service';
import { DevelopedVaccine } from '../_models/developed-vaccine';
import { VaccinesService } from '../_services/vaccines.service';
import { Vaccine } from '../_models/vaccine';
import { VaccinesFilter } from '../_models/filters/vaccines-filter';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { User } from '../_models/user';
import { AppliedVaccinesFilter } from '../_models/filters/applied-vaccines-filter';
import { AppliedVaccine } from '../_models/applied-vaccine';
import { AppliedVaccinesService } from '../_services/applied-vaccine.service';

@Component({ templateUrl: 'applyVaccines.component.html' })
export class ApplyVaccinesComponent {
  formFilter!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: UntypedFormBuilder,
      private router: Router,
      private route: ActivatedRoute,
      private accountService: AccountService,
      private appliedVaccinesService: AppliedVaccinesService,
      private vaccinesService: VaccinesService,
      private alertService: AlertService,
      private http: HttpClient,
      private dp: DatePipe
  ) { 
    this.formFilter = this.formBuilder.group({
      batchNumber: ['', [Validators.maxLength(20)]],
      //fullName: ['', [Validators.maxLength(100)]],
      province: [''],
      vaccineId: [''],
      developedVaccineId: [''],
      appliedDate: [null],
      dni: ['']
    });

    this.route.queryParams.subscribe((params) => {
      if (params.batchNumber) {
        this.formFilter.controls.batchNumber.setValue(params.batchNumber, {
          onlySelf: true,
        });
      }
      /*if (params.fullName) {
        this.formFilter.controls.fullName.setValue(params.fullName, { onlySelf: true, });
      }*/
      if (params.province) {
        this.formFilter.controls.province.setValue(params.province, {
          onlySelf: true,
        });
      }
      if (params.appliedDate) {
        this.formFilter.controls.appliedDate.setValue(params.appliedDate, {
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
      if (params.dni) {
        this.formFilter.controls.dni.setValue(params.dni, {
          onlySelf: true,
        });
      }

      this.loadData();
    });
  }

  public filter = new AppliedVaccinesFilter();
  public appliedVaccines: AppliedVaccine[] = [];
  public user: User = new User;

loadData(){
  this.accountService.myProfile().subscribe((res: any) => {
    this.user = res;      
  });

  const dni = this.formFilter.get('dni')?.value;
  const batchNumber = this.formFilter.get('batchNumber')?.value;
  //const fullName = this.formFilter.get('fullName')?.value;
  const province = this.formFilter.get('province')?.value;
  const appliedDate = this.formFilter.get('appliedDate')?.value;
  const vaccineId = this.formFilter.get('vaccineId')?.value;
  const developedVaccineId = this.formFilter.get('developedVaccineId')?.value;

  this.filter.appliedBy = this.user.id;
  this.filter.dni = dni;
  this.filter.appliedDate = appliedDate;
  this.filter.batchNumber = batchNumber;
  this.filter.province = province;
  this.filter.vaccineId = vaccineId;
  this.filter.developedVaccineId = developedVaccineId;

  //Obtener las vacunas aplicadas por el user.id

  this.appliedVaccinesService.getAll(this.filter).subscribe((res: any) => {
    this.appliedVaccines = res.vaccines;
  });
}

  // convenience getter for easy access to form fields
  get f() { return this.formFilter.controls; }

  addApplication(){
    this.router.navigate(['apply-vaccines', 'new'])
  }

  applyFilter() {
    /*this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.formFilter.invalid) {
      return;
    }

    this.loading = true;

    const batchNumber = this.formFilter.get('batchNumber')?.value;
    const status = this.formFilter.get('status')?.value;
    const purchaseDate = this.dp.transform(
      this.formFilter.get('purchaseDate')?.value,
      'yyyy-MM-dd'
    );
    const eta = this.dp.transform(
      this.formFilter.get('eta')?.value,
      'yyyy-MM-dd'
    );
    const queryParams: any = {};

    if (batchNumber) {
      queryParams.batchNumber = batchNumber;
    }
    if (status) {
      queryParams.status = status;
    }
    if (purchaseDate) {
      queryParams.purchaseDate = purchaseDate;
    }
    if (eta) {
      queryParams.eta = eta;
    }

    this.router.navigate(['/purchase-orders'], {
      queryParams,
    });

    this.loading = false;*/
  }

  clear() {
    // reset alerts on submit
    /*this.alertService.clear();

    this.formFilter.controls.batchNumber.setValue('', {
      onlySelf: true,
    });
    this.formFilter.controls.status.setValue('', {
      onlySelf: true,
    });
    this.formFilter.controls.purchaseDate.setValue(null, {
      onlySelf: true,
    });
    this.formFilter.controls.eta.setValue(null, {
      onlySelf: true,
    });

    this.applyFilter();*/
  }


}