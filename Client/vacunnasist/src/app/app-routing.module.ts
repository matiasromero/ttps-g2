import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';

const accountModule = () =>
  import('./account/account.module').then((x) => x.AccountModule);
const usersModule = () =>
  import('./users/users.module').then((x) => x.UsersModule);
const vaccinesModule = () =>
  import('./vaccines/vaccines.module').then((x) => x.VaccinesModule);
const batchVaccinesModule = () =>
  import('./batch-vaccines/batch-vaccines.module').then(
    (x) => x.BatchVaccinesModule
  );
const purchaseOrdersModule = () =>
  import('./purchase-orders/purchase-orders.module').then(
    (x) => x.PurchaseOrdersModule
  );

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'account', loadChildren: accountModule },
  {
    path: 'users',
    loadChildren: usersModule,
    canActivate: [AuthGuard],
    data: {
      roles: ['administrator'],
    },
  },
  {
    path: 'vaccines',
    loadChildren: vaccinesModule,
    canActivate: [AuthGuard],
    data: {
      roles: ['operator', 'analyst'],
    },
  },
  {
    path: 'batch-vaccines',
    loadChildren: batchVaccinesModule,
    canActivate: [AuthGuard],
    data: {
      roles: ['operator', 'analyst'],
    },
  },
  {
    path: 'purchase-orders',
    loadChildren: purchaseOrdersModule,
    canActivate: [AuthGuard],
    data: {
      roles: ['operator'],
    },
  },
  // otherwise redirect to home
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
