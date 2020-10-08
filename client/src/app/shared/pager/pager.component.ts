import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
  @Output() pageChanged = new EventEmitter<number>();
  @Input() pageSize: number;
  @Input() totalCount: number;

  constructor() {}

  ngOnInit(): void {}

  onPageChanged(event: any): void {
    this.pageChanged.emit(event.page);
  }
}
