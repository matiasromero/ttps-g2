import { AlertService } from 'src/app/_services/alert.service';
import { VaccinesService } from 'src/app/_services/vaccines.service';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { first } from 'rxjs/operators';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
  user?: User;

  constructor(
    private accountService: AccountService,
    private vaccinesService: VaccinesService,
    private alertService: AlertService,
    public dialog: MatDialog
  ) {
    this.accountService.user.subscribe((x) => (this.user = <User>x));
  }

  logout() {
    this.accountService.logout();
  }

  runCron() {
    this.vaccinesService
      .fireCron()
      .pipe(first())
      .subscribe({
        next: (obj) => {
          this.alertService.success('Tarea encolada correctamente', {
            keepAfterRouteChange: true,
          });
        },
        error: (error) => {
          this.alertService.error(error);
        },
      });
  }
}
