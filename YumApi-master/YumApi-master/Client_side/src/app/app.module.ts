import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

//providers
import { NavbarService } from './providers/navbar.service';
import { HomeService } from './providers/home.service';
import { Config } from './providers/config.service';
import { SharedService } from './providers/Shared.service';
import { CartService } from './providers/cart.service';
import { AuthGuard } from './helpers/auth.guard';
import { AuthInterceptor } from './helpers/jwt.interceptor';

//Component
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { RecipeComponent } from './recipe/recipe.component';
import { CartComponent } from './cart/cart.component';

//pipe
import { SearchPipe } from './pipes/search.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    RecipeComponent,
    CartComponent,
    SearchPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    Config,
    HomeService,
    NavbarService,
    SharedService,
    CartService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    } 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
