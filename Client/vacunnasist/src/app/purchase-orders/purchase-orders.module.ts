import { NewPurchaseOrderComponent } from './new/new-purchase-order.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatExpansionModule } from '@angular/material/expansion';
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import {
  DateAdapter,
  MatNativeDateModule,
  MatOptionModule,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
} from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { MY_DATE_FORMATS } from '../account/account.module';
import { MatSelectModule } from '@angular/material/select';
import { PurchaseOrdersRoutingModule } from './purchase-orders-routing.module';
import { PurchaseOrdersComponent } from './purchase-orders.component';
import { PurchaseOrderStatusPipe } from '../_helpers/purchase-order-status.pipe';

@NgModule({
  declarations: [
    PurchaseOrdersComponent,
    NewPurchaseOrderComponent,
    PurchaseOrderStatusPipe,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PurchaseOrdersRoutingModule,
    MatIconModule,
    MatRadioModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatTooltipModule,
    MatNativeDateModule,
    MatInputModule,
    MatTabsModule,
    MatOptionModule,
    MatSelectModule,
  ],
  providers: [
    DatePipe,
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE],
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS },
    { provide: MAT_DATE_LOCALE, useValue: 'es-AR' },
  ],
})
export class PurchaseOrdersModule {}
