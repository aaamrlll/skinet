import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { ApibaseUrl } from '../shared/shared.module';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = ApibaseUrl;

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams): Observable<IPagination> {
    let params = new HttpParams();
    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageIndex.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    return this.http
      .get<IPagination>(`${this.baseUrl}products`, {
        observe: 'response',
        params,
      })
      .pipe<IPagination>(
        map((res: HttpResponse<IPagination>) => {
          return res.body;
        })
      );
  }
  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(`${this.baseUrl}products/brands`);
  }
  getTypes(): Observable<IType[]> {
    return this.http.get<IType[]>(`${this.baseUrl}products/types`);
  }
}
