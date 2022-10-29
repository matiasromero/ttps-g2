import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { BatchVaccineService } from 'src/app/_services/batch-vaccine.service';
import { BatchVaccine } from 'src/app/_models/batch-vaccine';

@Component({ templateUrl: 'view-batch-vaccine.component.html' })
export class ViewBatchVaccineComponent implements OnInit {
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private batchVaccineService: BatchVaccineService,
    private _location: Location
  ) {}

  public batchVaccineId?: number;
  public batchVaccine?: BatchVaccine;

  ngOnInit() {
    this.batchVaccineId = parseInt(this.route.snapshot.paramMap.get('id')!);
    this.batchVaccineService
      .getById(this.batchVaccineId)
      .subscribe((res: BatchVaccine) => {
        this.batchVaccine = res;
      });
  }

  backClicked() {
    this._location.back();
  }
}
