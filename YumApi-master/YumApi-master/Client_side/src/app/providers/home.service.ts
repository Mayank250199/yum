import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  path = this.config.path;

  constructor(private http: HttpClient, private config: Config) { }

  getallRecipe() {
    return this.http.get(this.path + '/api/recipes');
}
  


}
