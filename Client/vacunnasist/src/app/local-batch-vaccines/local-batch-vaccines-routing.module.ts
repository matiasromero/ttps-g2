import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../account/layout/layout.component';
import { LocalBatchVaccinesComponent } from './local-batch-vaccines.component';
import { ViewLocalBatchVaccineComponent } from './view/view-local-batch-vaccine.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: LocalBatchVaccinesComponent },
      { path: ':id', component: ViewLocalBatchVaccineComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LocalBatchVaccinesRoutingModule {}
