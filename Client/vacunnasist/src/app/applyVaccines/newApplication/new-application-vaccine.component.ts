import { AccountService } from 'src/app/_services/account.service';
import { DevelopedVaccinesFilter } from '../../_models/filters/developed-vaccines-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/_services/alert.service';
import Swal from 'sweetalert2';
import { DevelopedVaccine } from '../../_models/developed-vaccine';
import { AppliedVaccinesFilter } from '../../_models/filters/applied-vaccines-filter';
import { HttpClient } from '@angular/common/http';
import { AppliedVaccine } from 'src/app/_models/applied-vaccine';
import { AppliedVaccinesService } from 'src/app/_services/applied-vaccine.service';
import { LocalBatchVaccine } from 'src/app/_models/local-batch-vaccine';
import { LocalBatchVaccinesFilter } from 'src/app/_models/filters/local-batch-vaccines-filter';
import { LocalBatchVaccineService } from 'src/app/_services/local-batch-vaccine.service';
import { User } from 'src/app/_models/user';
import { DepartmentService } from 'src/app/_services/department.service';
import { Department } from 'src/app/_models/department';

@Component({ templateUrl: 'new-application-vaccine.component.html' })
export class NewApplyVaccineComponent implements OnInit {
  form!: UntypedFormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private appliedVaccineService: AppliedVaccinesService,
    private localBatchVaccineService: LocalBatchVaccineService,
    private departmentService: DepartmentService,
    private alertService: AlertService,
    private accountService: AccountService,
    private http: HttpClient
  ) {}

  public previousAppliance: AppliedVaccine[] = [];
  public filterApplied = new AppliedVaccinesFilter();
  public localBatchVaccines: LocalBatchVaccine[] = [];
  public filterBatch = new LocalBatchVaccinesFilter();
  public developedVaccines: DevelopedVaccine[] = [];

  ngOnInit() {
    this.form = this.formBuilder.group({
      dni: [null, [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      surname: ['', [Validators.required, Validators.maxLength(100)]],
      gender: ['', Validators.required],
      birthDate: ['', Validators.required],
      department: ['', Validators.required],
      province: ['', Validators.required],
      pregnant: [false],
      healthWorker: [false],
      developedVaccineId: [null, Validators.required],
    });

    this.accountService.user.subscribe((x) => (this.user = <User>x));

    this.localBatchVaccineService
      .getAll(this.filterBatch)
      .subscribe((res: any) => {
        this.localBatchVaccines = res.vaccines;
        this.localBatchVaccines.forEach((batch) =>
          this.developedVaccines.push(batch.batchVaccine.developedVaccine)
        );
      });
  }

  user?: User;

  // convenience getter for easy access to form fields
  get f() {
    return this.form.controls;
  }

  onKeyUp(event: any) {
    if (event.target.value.length === 8) this.onFetchData(event.target.value);
  }

  onFetchData(dni: number) {
    this.filterApplied.dni = dni;
    this.loading = true;
    this.http
      .get(`https://api.claudioraverta.com/personas/${dni}`)
      .pipe(first())
      .subscribe({
        next: (resp: any) => {
          const date_birth = resp.fecha_hora_nacimiento.split(' ');
          this.form.patchValue({
            dni: resp.DNI,
            name: resp.nombre,
            surname: resp.apellido,
            gender: resp.genero,
            birthDate: date_birth[0],
            province: resp.jurisdiccion,
            pregnant: resp.embarazada,
            healthWorker: resp.personal_salud,
          });

          this.departmentService
            .getRandomByProvince(resp.jurisdiccion)
            .pipe(first())
            .subscribe((res: Department) => {
              this.form.patchValue({
                department: res.name,
              });
              this.loading = false;
            });
        },
        error: (error) => {
          this.alertService.error(
            'La persona con DNI ' + dni + ' no fue encontrada.'
          );
          this.loading = false;
        },
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

    this.appliedVaccineService
      .newApplication(this.form.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Persona vacunada correctamente', {
            keepAfterRouteChange: true,
          });
          this.router.navigate(['../../apply-vaccines'], {
            relativeTo: this.route,
          });
        },
        error: (error) => {
          this.alertService.error(error);
          this.loading = false;
        },
      });
  }
}
