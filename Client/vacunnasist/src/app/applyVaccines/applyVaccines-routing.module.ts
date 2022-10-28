import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../account/layout/layout.component';
import { ApplyVaccinesComponent } from './applyVaccines.component';
import { NewApplyVaccineComponent } from './newApplication/new-application-vaccine.component';
//import { EditVaccineComponent } from './edit/edit-vaccine.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: ApplyVaccinesComponent },
            { path: 'new', component: NewApplyVaccineComponent },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ApplyVaccinesRoutingModule { }