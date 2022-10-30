import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AppliedVaccinesService } from 'src/app/_services/applied-vaccine.service';
import { AppliedVaccine } from 'src/app/_models/applied-vaccine';

@Component({ templateUrl: 'view-apply-vaccine.component.html' })
export class ViewApplyVaccineComponent implements OnInit {
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private applyVaccineService: AppliedVaccinesService,
    private _location: Location
  ) {}

  public applyVaccineId?: number;
  public applyVaccine?: AppliedVaccine;

  ngOnInit() {
    this.applyVaccineId = parseInt(this.route.snapshot.paramMap.get('id')!);
    this.applyVaccineService
      .getById(this.applyVaccineId)
      .subscribe((res: AppliedVaccine) => {
        console.log(res);
        this.applyVaccine = res;
      });
  }

  backClicked() {
    this._location.back();
  }
}
