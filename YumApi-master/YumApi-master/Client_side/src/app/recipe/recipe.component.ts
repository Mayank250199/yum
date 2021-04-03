import { Component, OnInit } from '@angular/core';
import { SharedService } from '../providers/Shared.service';
import Swal from 'sweetalert2'
import {Router} from '@angular/router';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit {
  
  cartIngredient:any=[];
  Recipe:any
  CartCount:any;

  constructor(private sharedService:SharedService,private router:Router) { }
  ngOnInit() {
    this.getRecipe();
  if(localStorage.getItem("cartIngredient")){
   this.cartIngredient =  JSON.parse(localStorage.getItem("cartIngredient"));
   this.CartCount = this.cartIngredient.length;
   console.log(this.CartCount);
  }else{
    localStorage.setItem("cartIngredient", JSON.stringify(this.cartIngredient));
    this.CartCount=0;
  }
  }

  getRecipe(){
    this.Recipe = this.sharedService.getRecipe();
    console.log(this.Recipe);
  }

  addToCart(evt,Ing){
    if(localStorage.getItem("token")){
    if(evt.target.checked){
      this.cartIngredient.push(Ing);
    }
    if(!evt.target.checked){
      for (var i=0; i < this.cartIngredient.length; i++) {
        if (this.cartIngredient[i].id === Ing.id) {
          this.cartIngredient.splice(i,1);
        }
    }
  
    }
    localStorage.setItem("cartIngredient", JSON.stringify(this.cartIngredient));
    if(localStorage.getItem("cartIngredient")){
      this.cartIngredient =  JSON.parse(localStorage.getItem("cartIngredient"));
      this.CartCount = this.cartIngredient.length;
     }else{
       this.cartIngredient=[];
       this.CartCount=0;
     }
  }else{
    Swal.fire({
      title: "You Need To Login First!!!",
      confirmButtonColor: "#c24049"
    });
  this.router.navigate(['/']);
}
  }

}
