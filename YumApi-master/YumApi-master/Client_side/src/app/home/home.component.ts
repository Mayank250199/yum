import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HomeService } from '../providers/home.service';
import { SharedService } from '../providers/Shared.service';
import { Router } from '@angular/router';
declare var $:any;
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchVal = "";
  signUpForm: FormGroup;
  allRecipe:any
  constructor(
    private homeService:HomeService, 
    private sharedService:SharedService,
    private router:Router
  ) { }

  ngOnInit() {
    // this.owl();
    this.getallRecipe();
  }

  getallRecipe(){
    this.homeService.getallRecipe().subscribe(data => {
        this.allRecipe = data;
        console.log(this.allRecipe);
      },err=>{
        // this.Loader=false;
      });
  }

  getRecipeInfo(Data){
    console.log("click");
    this.sharedService.setRecipe(Data);
    this.router.navigate(['/recipe']);
  }

  

}
