import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseRequest } from '../models/base-request';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly URL_API: string = 'https://localhost:7010/api/v1/';

  constructor(protected httpClient: HttpClient) { }

  public executePost<T>(endpoint: string, parameters?: T): Observable<any> {
    const body: BaseRequest = { data: parameters };
    const req = new HttpRequest('POST', `${this.URL_API}${endpoint}`, body);
    return this.callApi(req);
  }

  public executePut<T>(endpoint: string, parameters?: T): Observable<any> {
    const body: BaseRequest = { data: parameters };
    const req = new HttpRequest('PUT', `${this.URL_API}${endpoint}`, body);
    return this.callApi(req);
  }

  public executeGet<T>(endpoint: string, parameters?: T): Observable<any> {
    const req = new HttpRequest('GET', `${this.URL_API}${endpoint}`, parameters);
    return this.callApi(req);
  }

  public executePatch<T>(endpoint: string, parameters?: T): Observable<any> {
    const body: BaseRequest = { data: parameters };
    const req = new HttpRequest('PATCH', `${this.URL_API}${endpoint}`, body);
    return this.callApi(req);
  }

  private callApi<T>(req: HttpRequest<T>): Observable<any> {
    return this.httpClient.request(req);
  }
}
