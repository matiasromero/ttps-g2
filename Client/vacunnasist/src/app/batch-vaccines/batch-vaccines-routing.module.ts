import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../account/layout/layout.component';
import { BatchVaccinesComponent } from './batch-vaccines.component';
import { NewDistributionOrderComponent } from './new-distribution/new-distribution.component';
import { ViewBatchVaccineComponent } from './view/view-batch-vaccine.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: BatchVaccinesComponent },
      { path: 'new-distribution', component: NewDistributionOrderComponent },
      { path: ':id', component: ViewBatchVaccineComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BatchVaccinesRoutingModule {}
