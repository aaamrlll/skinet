import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/products';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  @ViewChild('search', { static: true }) searchTerm: ElementRef;

  totalCount: number;
  shopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getBrands();
    this.getProducts();
    this.getTypes();
  }

  getProducts(): void {
    this.shopService.getProducts(this.shopParams).subscribe(
      (res: IPagination) => {
        this.products = res.data;
        this.shopParams.pageIndex = res.pageIndex;
        this.shopParams.pageSize = res.pageSize;
        this.totalCount = res.count;
      },
      (err) => {
        console.log(err);
      },
      (): void => {
        console.log('completado');
      }
    );
  }
  getBrands(): void {
    this.shopService.getBrands().subscribe(
      (res: IBrand[]) => {
        res.unshift({ id: 0, name: 'All' });
        this.brands = res;
      },
      (err) => {
        console.log(err);
      },
      (): void => {
        console.log('completado');
      }
    );
  }
  getTypes(): void {
    this.shopService.getTypes().subscribe(
      (res: IType[]) => {
        res.unshift({ id: 0, name: 'All' });
        this.types = res;
      },
      (err) => {
        console.log(err);
      },
      (): void => {
        console.log('completado');
      }
    );
  }

  onBrandSelected(brandId: number): void {
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onSortSelected(sort: string): void {
    this.shopParams.sort = sort;
    this.getProducts();
  }
  onPageChanged(pageIndex: number): void {
    if (this.shopParams.pageIndex !== pageIndex) {
    this.shopParams.pageIndex = pageIndex;
    this.getProducts();
  }
  }
  onSearch(): void {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onReset(): void{
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
