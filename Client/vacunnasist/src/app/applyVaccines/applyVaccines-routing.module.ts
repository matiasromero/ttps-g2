import { ViewApplyVaccineComponent } from './view/view-apply-vaccine.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../account/layout/layout.component';
import { ApplyVaccinesComponent } from './applyVaccines.component';
import { NewApplyVaccineComponent } from './newApplication/new-application-vaccine.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ApplyVaccinesComponent },
      { path: 'new', component: NewApplyVaccineComponent },
      { path: ':id', component: ViewApplyVaccineComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ApplyVaccinesRoutingModule {}
