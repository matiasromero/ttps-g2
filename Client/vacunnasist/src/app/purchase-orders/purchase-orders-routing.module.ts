import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../account/layout/layout.component';
import { NewPurchaseOrderComponent } from './new/new-purchase-order.component';
import { PurchaseOrdersComponent } from './purchase-orders.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: PurchaseOrdersComponent },
      { path: 'new', component: NewPurchaseOrderComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PurchaseOrdersRoutingModule {}
