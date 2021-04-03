import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  path = this.config.path;

  constructor(private http: HttpClient, private config: Config) { }

  postOrder(data) {
    return this.http.post(this.path + '/api/Orders',data);
 }

 userProfile(id) {
  return this.http.get(this.path + '/api/UserProfiles/'+id)
}

}
