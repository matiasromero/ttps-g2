import { DevelopedVaccinesFilter } from '../../_models/filters/developed-vaccines-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import Swal from 'sweetalert2';
import { DevelopedVaccineService } from '../../_services/developed-vaccine.service';
import { DevelopedVaccine } from '../../_models/developed-vaccine';
import { VaccinesService } from '../../_services/vaccines.service';
import { Vaccine } from '../../_models/vaccine';
import { VaccinesFilter } from '../../_models/filters/vaccines-filter';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({ templateUrl: 'new-application-vaccine.component.html' })
export class NewApplyVaccineComponent implements OnInit {
  form!: UntypedFormGroup;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: UntypedFormBuilder,
      private router: Router,
      private route: ActivatedRoute,
      private accountService: AccountService,
      private developedVaccineService: DevelopedVaccineService,
      private vaccinesService: VaccinesService,
      private alertService: AlertService,
      private http: HttpClient
  ) {}

  public vaccines: Vaccine[] = [];

  ngOnInit() {
      let filter = new VaccinesFilter();
      this.vaccinesService.getAll(filter).subscribe((res: any) => {
          this.vaccines = res.vaccines;
      });
      
      this.form = this.formBuilder.group({
          dni: ['',  [Validators.required, Validators.maxLength(20)]],
          name: ['', [Validators.required, Validators.maxLength(100)]],
          surname: ['', [Validators.required, Validators.maxLength(100)]],
          gender: ['', Validators.required],
          birthDate: ['', Validators.required],
          province: ['', Validators.required],
          pregnant: [false],
          healthWorker: [false],
      });
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onKeyUp(event: any) {
    if(event.target.value.length === 8)
      this.onFetchData(event.target.value);
  }

  onFetchData(dni: number) {
    this.http.get(`https://api.claudioraverta.com/personas/${dni}`)
      .pipe(first()).subscribe({
          next: (resp: any) => {
            const date_birth = resp.fecha_hora_nacimiento.split(" ");
            this.form.patchValue({
              dni: resp.DNI,
              name: resp.nombre,
              surname: resp.apellido,
              gender: resp.genero,
              birthDate: date_birth[0],
              province: resp.jurisdiccion,
              pregnant: resp.embarazada,
              healthWorker: resp.personal_salud
            });
            //Obtener las dosis previas de esta perosna si hay
            //Obtener los lotes disponibles para la provincia de la persona
          },
          error: error => {
            this.alertService.error('La persona con DNI '+dni+' no fue encontrada.');
            this.loading = false;
          }
        });
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
      
      /*this.developedVaccineService.newVaccine(this.form.value)
          .pipe(first())
          .subscribe({
              next: () => {
                  this.alertService.success('Vacuna desarrollada creada correctamente', { keepAfterRouteChange: true });
                  this.router.navigate(['../../vaccines'],{
                   relativeTo: this.route });
              },
              error: error => {
                  this.alertService.error(error);
                  this.loading = false;
              }
          });*/
  }
}