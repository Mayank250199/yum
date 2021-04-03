import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  path = this.config.path;
  constructor(private http: HttpClient, private config: Config) { }

  registerUser(data) {
   return this.http.post(this.path + '/api/users/register', data)
  }

  loginUser(data) {
    return this.http.post(this.path + '/api/users/login', data)
  }

  userProfile(id) {
    return this.http.get(this.path + '/api/UserProfiles/'+id)
  }

  isAuthenticated() {
    return localStorage.getItem("token") != null;
}
}
