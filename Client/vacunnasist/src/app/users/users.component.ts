import { UsersFilter } from 'src/app/_models/filters/users-filter';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { AlertService } from 'src/app/_services/alert.service';
import Swal from 'sweetalert2';
import { User } from '../_models/user';

@Component({ templateUrl: 'users.component.html' })
export class UsersComponent implements OnInit {
    formFilter!: FormGroup;
    loading = false;
    submitted = false;
    public type: String = "patient";
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService,
    ) { 


        this.formFilter = this.formBuilder.group({
            fullName: ['', [Validators.maxLength(100)]],
            isActive: [true],
        });
        

            this.route.queryParams.subscribe((params) => {
                if (params.fullName) {
                    this.formFilter.controls.fullName.setValue(params.fullName, {
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

    public users: User[] = [];
    public filter = new UsersFilter();

    ngOnInit() {
       
    }

    loadData() {
        const fullName = this.formFilter.get('fullName')?.value;
        const isActive = this.formFilter.get('isActive')?.value;
        this.filter.fullName =fullName;
        this.filter.isActive =isActive;
        this.accountService.getAll(this.filter).subscribe((res: any) => {
            this.users = res.users;
        });

    }

    panelOpenState = false;

    // convenience getter for easy access to form fields
    get f() { return this.formFilter.controls; }

    deleteUserQuestion(u: User) {
      Swal
      .fire({
        title: '??Est?? seguro?',
        text: 'Dar de baja a ' + u.fullName + ' con DNI ' + u.dni,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonText: 'Si, dar de baja!'
      })
      .then(result => {
        if (result.value) {
          this.deleteUser(u);
          
        }
      });
    }

    deleteUser(u: User) {
      this.accountService.canBeDeleted(+u.id).subscribe((res: boolean) => {
        if (!res) {
          Swal
      .fire({
        title: 'Oops...',
        text: 'No se puede dar de baja ya que posee turnos pendientes y/o confirmados. Por favor, cancelelos primero.',
        icon: 'error',
      })
        } else {
          this.doDelete(u);
        }
    });
  }

  doDelete(u: User) {
    this.loading = true;
    this.accountService.update(+u.id, {isActive: false}).pipe(first())
      .subscribe({
          next: () => {
            Swal
            .fire({
              title: 'Hecho',
              text: 'Usuario desactivado correctamente.',
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

    editUser(u: User) {
        this.router.navigate(['users/edit/', u.id]);
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

        const fullName = this.formFilter.get('fullName')?.value;
        const isActive = this.formFilter.get('isActive')?.value;
        const queryParams: any = {};

        queryParams.type = this.route.snapshot.queryParamMap.get('type');
        //const filter = this.route.snapshot.queryParamMap.get('filter');

          if (fullName) {
            queryParams.fullName = fullName;
          }
          if (isActive !== undefined) {
            queryParams.isActive = isActive;
          }

          this.router.navigate(['/users'], {
            queryParams,
          });

          this.loading = false;
    }

    clear() {
      // reset alerts on submit
      this.alertService.clear();

      this.formFilter.controls.fullName.setValue('', {
        onlySelf: true,
      });
      this.formFilter.controls.isActive.setValue(true);

      this.applyFilter();
  }

  addUser() {
    this.router.navigate(['users','new'], { queryParams: { type: this.type }});
  }
}