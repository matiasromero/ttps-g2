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
import { PurchaseOrdersService } from 'src/app/_services/purchase-orders.service';
import { DevelopedVaccine } from 'src/app/_models/developed-vaccine';
import { DevelopedVaccinesFilter } from 'src/app/_models/filters/developed-vaccines-filter';

@Component({ templateUrl: 'new-purchase-order.component.html' })
export class NewPurchaseOrderComponent implements OnInit {
  form!: UntypedFormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private purchaseOrderService: PurchaseOrdersService,
    private developedVaccineService: DevelopedVaccineService,
    private vaccinesService: VaccinesService,
    private alertService: AlertService
  ) {}

  public vaccines: Vaccine[] = [];
  public developedVaccines: DevelopedVaccine[] = [];

  ngOnInit() {
    let vaccinesFilter = new VaccinesFilter();
    this.vaccinesService.getAll(vaccinesFilter).subscribe((res: any) => {
      this.vaccines = res.vaccines;
    });

    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    this.developedVaccineService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
      });

    this.form = this.formBuilder.group({
      vaccineId: [null],
      developedVaccineId: [null, Validators.required],
      quantity: [0, Validators.required],
    });
  }

  changeVaccine(vaccineId: number) {
    let developedVaccinesFilter = new DevelopedVaccinesFilter();
    developedVaccinesFilter.vaccineId = vaccineId;
    this.form.controls.developedVaccineId.setValue('', {
      onlySelf: true,
    });
    this.developedVaccineService
      .getAll(developedVaccinesFilter)
      .subscribe((res: any) => {
        this.developedVaccines = res.vaccines;
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

    this.purchaseOrderService
      .newPurchaseOrder(this.form.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Órden de compra creada correctamente', {
            keepAfterRouteChange: true,
          });
          this.router.navigate(['../../purchase-orders'], {
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
