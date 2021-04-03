import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RecipeComponent } from './recipe/recipe.component';
import { CartComponent } from './cart/cart.component';
import { AuthGuard } from './helpers/auth.guard';

const routes: Routes = [
  {path: 'home' , component: HomeComponent},
  {path: 'recipe' , component: RecipeComponent},
  {path: 'cart' , component: CartComponent, canActivate: [AuthGuard]},
  {path: '', pathMatch: 'full', redirectTo: 'home'} 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
