import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent],
  imports: [CommonModule, SharedModule],
  exports: [ShopComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ShopModule {}
