import { DevelopedVaccinesFilter } from '../_models/filters/developed-vaccines-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
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
export class ApplyVaccinesComponent implements OnInit {
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
    private developedVaccinesService: DevelopedVaccineService,
    private alertService: AlertService,
    private http: HttpClient,
    private dp: DatePipe
  ) {
    this.accountService.user.subscribe((x) => (this.user = <User>x));

    this.formFilter = this.formBuilder.group({
      province: [''],
      vaccineId: [''],
      developedVaccineId: [''],
      dni: ['', Validators.maxLength(20)],
    });

    this.route.queryParams.subscribe((params) => {
      if (params.dni) {
        this.formFilter.controls.dni.setValue(params.dni, {
          onlySelf: true,
        });
      }
      if (params.province) {
        this.formFilter.controls.province.setValue(params.province, {
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
      this.loadData();
    });
    this.isHiddenButton = this.user?.role == 'vacunator';
  }

  public filter = new AppliedVaccinesFilter();
  public vaccines: Vaccine[] = [];
  public developedVaccines: DevelopedVaccine[] = [];
  public appliedVaccines: AppliedVaccine[] = [];
  user?: User;
  isHiddenButton: boolean;

  ngOnInit() {
    let vaccinesFilter = new VaccinesFilter();
    this.vaccinesService.getAll(vaccinesFilter).subscribe((res: any) => {
      this.vaccines = res.vaccines;
    });

    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    this.developedVaccinesService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
      });
  }

  loadData() {
    const dni = this.formFilter.get('dni')?.value;
    const province = this.formFilter.get('province')?.value;
    const vaccineId = this.formFilter.get('vaccineId')?.value;
    const developedVaccineId = this.formFilter.get('developedVaccineId')?.value;

    this.filter.dni = dni;
    this.filter.province = province;
    this.filter.vaccineId = vaccineId;
    this.filter.developedVaccineId = developedVaccineId;

    this.appliedVaccinesService.getAll(this.filter).subscribe((res: any) => {
      this.appliedVaccines = res.vaccines;
    });
  }

  panelOpenState = false;

  // convenience getter for easy access to form fields
  get f() {
    return this.formFilter.controls;
  }

  addApplication() {
    this.router.navigate(['apply-vaccines', 'new']);
  }

  changeVaccine(vaccineId: number) {
    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    developedVaccinesFilter.vaccineId = vaccineId;
    this.formFilter.controls.developedVaccineId.setValue('', {
      onlySelf: true,
    });
    this.developedVaccinesService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
      });
  }

  applyFilter() {
    this.submitted = true;

    this.alertService.clear();

    if (this.formFilter.invalid) {
      return;
    }

    this.loading = true;

    const dni = this.formFilter.get('dni')?.value;
    const vaccineId = this.formFilter.get('vaccineId')?.value;
    const developedVaccineId = this.formFilter.get('developedVaccineId')?.value;
    const province = this.formFilter.get('province')?.value;
    const queryParams: any = {};

    if (dni) {
      queryParams.batchNumber = dni;
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

    this.router.navigate(['/apply-vaccines'], {
      queryParams,
    });

    this.loading = false;
  }

  clear() {
    // reset alerts on submit
    this.alertService.clear();

    this.formFilter.controls.dni.setValue('', {
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

  select(v: AppliedVaccine) {
    this.router.navigate(['apply-vaccines', v.id]);
  }
}
