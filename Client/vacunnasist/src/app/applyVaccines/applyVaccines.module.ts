import { VaccineTypePipe } from './../_helpers/vaccine-type.pipe';
import { ApplyVaccinesRoutingModule } from './applyVaccines-routing.module';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatExpansionModule } from '@angular/material/expansion';
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { DateAdapter, MatNativeDateModule, MatOptionModule, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { ApplyVaccinesComponent } from './applyVaccines.component';
import { MatSelectModule } from '@angular/material/select';


export const MY_DATE_FORMATS = {
    parse: {
      dateInput: 'DD/MM/YYYY',
    },
    display: {
      dateInput: 'DD/MM/YYYY',
      monthYearLabel: 'MMMM YYYY',
      dateA11yLabel: 'LL',
      monthYearA11yLabel: 'MMMM YYYY'
    },
  };

@NgModule({
  declarations: [
    ApplyVaccinesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ApplyVaccinesRoutingModule,
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
    MatCheckboxModule
  ],
  providers: [
    DatePipe,
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
  { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS},
  { provide: MAT_DATE_LOCALE, useValue: 'es-AR'},
  ]

})
export class ApplyVaccinesModule { }
