import { Component, OnInit } from '@angular/core';
import { CartService } from '../providers/cart.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartIngredient:any;
  CartCount:any;
  Totalprice:any=0;
  Order:any;
  userProfileId:any;
  constructor(
    private cartService:CartService,
    private router:Router
  ) { }

  ngOnInit() {
    this.checkUser();
    if(localStorage.getItem("cartIngredient")){
    this.cartIngredient =  JSON.parse(localStorage.getItem("cartIngredient"));
    }
    this.userProfileId=localStorage.getItem("userProfile_Id");
    this.CartInfo();

  }

  checkUser(){
    if(localStorage.getItem("token")){
      console.log("cart");
    }else{
      Swal.fire({
        title: "Login First!!",
        confirmButtonColor: "#c24049"
      })
      this.router.navigate(['/']);
    }
  }

  CartInfo(){
    this.Totalprice = 0;
    if(this.cartIngredient){
    this.CartCount= this.cartIngredient.length;
    }
    if(this.CartCount>0){
    for (var i=0; i < this.cartIngredient.length; i++) {
         this.Totalprice += this.cartIngredient[i].price;
      }
    }else{
      this.Totalprice = 0;
    }
  }

  PostOrders(){
    if(this.cartIngredient){
    for (var i=0; i < this.cartIngredient.length; i++) {
     delete this.cartIngredient[i].id;
  }

    this.Order= { 
      "cartItem":this.cartIngredient,
      "totalPrice": this.Totalprice,
      "userProfileId":parseInt(this.userProfileId) 
    }

    
    this.cartService.postOrder(this.Order).subscribe((res) => {
     Swal.fire({
      title: "Your Order is Placed Successfully!!",
      confirmButtonColor: "#c24049"
    });
    this.cartIngredient=[];
    this.CartInfo();
    localStorage.removeItem("cartIngredient");
    delete this.cartIngredient;
  }, (err) => {
   Swal.fire({
    title: "Something Went Wrong!!",
    confirmButtonColor: "#c24049"
  })
  });
}else{
  Swal.fire({
    title: "Your Cart is Empty",
    confirmButtonColor: "#c24049"
  })
}
  }

  delItem(cart){
    for (var i=0; i < this.cartIngredient.length; i++) {
      if (this.cartIngredient[i].id === cart.id) {
        this.cartIngredient.splice(i,1);
      }
  }
 this.CartInfo();
 localStorage.setItem("cartIngredient", this.cartIngredient);
 this.cartIngredient =  JSON.parse(localStorage.getItem("cartIngredient"));

}
  
}
