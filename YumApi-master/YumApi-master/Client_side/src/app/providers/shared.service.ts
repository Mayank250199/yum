import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }

  private recipe = {};  
  
  setRecipe(Data) {      
     this.recipe = Data
   }  
   
   getRecipe() {  
     return this.recipe;  
   }   
}
