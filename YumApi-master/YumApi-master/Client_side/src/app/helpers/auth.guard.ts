import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { NavbarService } from "../providers/navbar.service";
import Swal from 'sweetalert2'

@Injectable()
export class AuthGuard implements CanActivate {
    
    constructor(private navbarService: NavbarService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.navbarService.isAuthenticated()) {
            return true;
        } else {
            this.router.navigate(['/home']);
            Swal.fire({
                title: "You Need To Login First!!!",
                confirmButtonColor: "#c24049"
              });
            return false;
        }

    }
}