import { LocalBatchVaccineService } from './../../_services/local-batch-vaccine.service';
import { LocalBatchVaccine } from './../../_models/local-batch-vaccine';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({ templateUrl: 'view-local-batch-vaccine.component.html' })
export class ViewLocalBatchVaccineComponent implements OnInit {
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private localBatchVaccineService: LocalBatchVaccineService,
    private _location: Location
  ) {}

  public localBatchVaccineId?: number;
  public localBatchVaccine?: LocalBatchVaccine;

  ngOnInit() {
    this.localBatchVaccineId = parseInt(
      this.route.snapshot.paramMap.get('id')!
    );
    this.localBatchVaccineService
      .getById(this.localBatchVaccineId)
      .subscribe((res: LocalBatchVaccine) => {
        this.localBatchVaccine = res;
      });
  }

  backClicked() {
    this._location.back();
  }
}
