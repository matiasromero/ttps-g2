import { UpdatePurchaseOrderRequest } from './../_models/update-purchase-order';
import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { PurchaseOrdersService } from '../_services/purchase-orders.service';
import { PurchaseOrder } from '../_models/purchase-order';
import { PurchaseOrdersFilter } from '../_models/filters/purchase-orders-filter';
import Swal from 'sweetalert2';
import { first } from 'rxjs/operators';

@Component({ templateUrl: 'purchase-orders.component.html' })
export class PurchaseOrdersComponent {
  formFilter!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private purchaseOrdersService: PurchaseOrdersService,
    private alertService: AlertService,
    private dp: DatePipe
  ) {
    this.formFilter = this.formBuilder.group({
      batchNumber: ['', [Validators.maxLength(20)]],
      status: [''],
      eta: [null],
      purchaseDate: [null],
    });

    this.route.queryParams.subscribe((params) => {
      if (params.batchNumber) {
        this.formFilter.controls.batchNumber.setValue(params.batchNumber, {
          onlySelf: true,
        });
      }
      if (params.status) {
        this.formFilter.controls.status.setValue(params.status);
      }
      if (params.eta) {
        this.formFilter.controls.eta.setValue(params.eta, {
          onlySelf: true,
        });
      }
      if (params.purchaseDate) {
        this.formFilter.controls.purchaseDate.setValue(params.purchaseDate, {
          onlySelf: true,
        });
      }

      this.loadData();
    });
  }

  public purchaseOrders: PurchaseOrder[] = [];
  public filter = new PurchaseOrdersFilter();
  maxDate: Date = new Date();

  loadData() {
    const batchNumber = this.formFilter.get('batchNumber')?.value;
    const purchaseDate = this.formFilter.get('purchaseDate')?.value;
    const eta = this.formFilter.get('eta')?.value;
    const status = this.formFilter.get('status')?.value;
    this.filter.batchNumber = batchNumber;
    this.filter.eta = eta;
    this.filter.purchaseDate = purchaseDate;
    this.filter.status = status;
    this.purchaseOrdersService.getAll(this.filter).subscribe((res: any) => {
      this.purchaseOrders = res.purchaseOrders;
    });
  }

  panelOpenState = false;

  // convenience getter for easy access to form fields
  get f() {
    return this.formFilter.controls;
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

    this.loading = false;
  }

  clear() {
    // reset alerts on submit
    this.alertService.clear();

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

    this.applyFilter();
  }

  addPurchaseOrder() {
    this.router.navigate(['purchase-orders', 'new']);
  }

  confirmPurchaseOrderQuestion(po: PurchaseOrder) {
    Swal.fire({
      title: 'Confirmar órden de compra',
      text:
        'Confirma el pago de la órden de compra # ' +
        po.id +
        ' (Lote # ' +
        po.batchNumber +
        ')',
      icon: 'question',
      showCancelButton: true,
      cancelButtonText: 'No',
      confirmButtonText: 'Si, confirmar!',
    }).then((result) => {
      if (result.value) {
        this.doConfirmPurchaseOrder(po);
      }
    });
  }

  doConfirmPurchaseOrder(u: PurchaseOrder) {
    this.loading = true;
    let po = new UpdatePurchaseOrderRequest();
    po.status = 1;
    this.purchaseOrdersService
      .update(+u.id, po)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success(
            'Órden de compra confirmada correctamente',
            { keepAfterRouteChange: true }
          );
          this.loadData();
        },
        error: (error) => {
          this.alertService.error(error);
          this.loading = false;
        },
      });
  }

  deliverPurchaseOrderQuestion(po: PurchaseOrder) {
    Swal.fire({
      title: 'Confirmar arrivo de órden de compra',
      text:
        'Confirma el arrivo de la órden de compra # ' +
        po.id +
        ' (Lote # ' +
        po.batchNumber +
        ')',
      icon: 'question',
      showCancelButton: true,
      cancelButtonText: 'No',
      confirmButtonText: 'Si, confirmar!',
    }).then((result) => {
      if (result.value) {
        this.doDeliverPurchaseOrder(po);
      }
    });
  }

  doDeliverPurchaseOrder(u: PurchaseOrder) {
    this.loading = true;
    let po = new UpdatePurchaseOrderRequest();
    po.status = 2;
    this.purchaseOrdersService
      .update(+u.id, po)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Órden de compra arrivada correctamente', {
            keepAfterRouteChange: true,
          });
          this.loadData();
        },
        error: (error) => {
          this.alertService.error(error);
          this.loading = false;
        },
      });
  }
}
