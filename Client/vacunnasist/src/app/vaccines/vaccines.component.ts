import { VaccinesFilter } from './../_models/filters/vaccines-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import Swal from 'sweetalert2';
import { VaccineService } from '../_services/vaccine.service';
import { Vaccine } from '../_models/vaccine';

@Component({ templateUrl: 'vaccines.component.html' })
export class VaccinesComponent implements OnInit {
    formFilter!: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private vaccineService: VaccineService,
        private alertService: AlertService,
    ) { 
        // redirect to home if already logged in
        if (this.accountService.userValue.role !== 'administrator') {
            this.router.navigate(['/']);
        }

        this.formFilter = this.formBuilder.group({
            name: ['', [Validators.maxLength(100)]],
            isActive: [true],
        });
        

            this.route.queryParams.subscribe((params) => {
             
                if (params.name) {
                    this.formFilter.controls.name.setValue(params.name, {
                        onlySelf: true,
                      });
                }
                if (params.isActive) {
                  this.formFilter.controls.isActive.setValue(params.isActive === "true", {
                    onlySelf: true,
                  });
                }
          
                this.loadData();
              });
    }

    public vaccines: Vaccine[] = [];
    public filter = new VaccinesFilter();

    ngOnInit() {
       
    }

    loadData() {
        const name = this.formFilter.get('name')?.value;
        const isActive = this.formFilter.get('isActive')?.value;
        this.filter.name =name;
        this.filter.isActive =isActive;
        this.vaccineService.getAll(this.filter).subscribe((res: any) => {
            this.vaccines = res.vaccines;
        });

    }

    panelOpenState = false;

    // convenience getter for easy access to form fields
    get f() { return this.formFilter.controls; }

    deleteVaccineQuestion(v: Vaccine) {
      Swal
      .fire({
        title: '¿Está seguro?',
        text: 'Dar de baja a ' + v.name,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonText: 'Si, dar de baja!'
      })
      .then(result => {
        if (result.value) {
          this.deleteVaccine(v);
          
        }
      });
    }

    deleteVaccine(v: Vaccine) {
      this.vaccineService.canBeDeleted(+v.id).subscribe((res: boolean) => {
        if (!res) {
          Swal
      .fire({
        title: 'Oops...',
        text: 'No se puede dar de baja ya que posee turnos pendientes y/o confirmados. Por favor, cancélelos primero.',
        icon: 'error',
      })
        } else {
          this.doDelete(v);
        }
    });
  }

  doDelete(v: Vaccine) {
    this.loading = true;
    this.vaccineService.cancel(+v.id).pipe(first())
      .subscribe({
          next: () => {
            Swal
            .fire({
              title: 'Hecho',
              text: 'Vacuna desactivada correctamente.',
              icon: 'success',
            });
                this.loadData();
                this.loading = false;
          },
          error: (error: string) => {
              this.alertService.error(error);
              this.loading = false;
          }
      });
  }


    editVaccine(v: Vaccine) {
        this.router.navigate(['vaccines/edit/', v.id]);
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

        const name = this.formFilter.get('name')?.value;
        const isActive = this.formFilter.get('isActive')?.value;
        const queryParams: any = {};

        queryParams.type = this.route.snapshot.queryParamMap.get('type');
        //const filter = this.route.snapshot.queryParamMap.get('filter');

          if (name) {
            queryParams.name = name;
          }
          if (isActive !== undefined) {
            queryParams.isActive = isActive;
          }

          this.router.navigate(['/vaccines'], {
            queryParams,
          });

          this.loading = false;
    }

    clear() {
      // reset alerts on submit
      this.alertService.clear();

      this.formFilter.controls.name.setValue('', {
        onlySelf: true,
      });
      this.formFilter.controls.isActive.setValue(true);

      this.applyFilter();
  }

  addVaccine() {
    this.router.navigate(['vaccines','new']);
  }
}