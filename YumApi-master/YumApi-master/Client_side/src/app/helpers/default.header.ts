// import { Injectable } from '@angular/core';
// import { Http, XHRBackend, RequestOptions, Request, RequestOptionsArgs, Response, Headers } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/catch';

// function getToken(): any {
//   let token = localStorage.getItem('access_token');
//   let header:any;
//   header = 'Token '+ localStorage.getItem('access_token') || '';
//   return header;
// }

// @Injectable()
// export class CustomHttpService extends Http {
//   constructor (backend: XHRBackend, options: RequestOptions) {
//     options.headers = new Headers({
//       'Authorization': `${getToken()}`,
//     });
//     super(backend, options);
//   }

//   // its like interceptor, calls by each methods internally like get, post, put, delete etc
//   request(url: string|Request, options?: RequestOptionsArgs): Observable<Response> {
//     if (typeof url === 'string') {
//       if (!options) {
//         options = { headers: new Headers() };
//       }
//       options.headers.set('Authorization', `${getToken()}`);
//     } else {
//       url.headers.set('Authorization', `${getToken()}`);
//     }
//     return super.request(url, options);
//   }
// }
