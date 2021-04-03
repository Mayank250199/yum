import { Component, OnInit } from '@angular/core';
import { NavbarService } from '../providers/navbar.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import Swal from 'sweetalert2'
import { Router } from '@angular/router';

declare var $:any;
declare var Modernizr: any;
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
login:any;
signup:any;
loggedIn:any =false;
loader = false;
signUpForm: FormGroup;
loginForm: FormGroup;

  constructor(private navbarService:NavbarService,private router:Router) { }

  ngOnInit() {
    this.checklogin();

    this.signUpForm = new FormGroup({
      'username': new FormControl(null, Validators.required),
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, Validators.required)
    });

    this.loginForm = new FormGroup({
      'Username': new FormControl(null, Validators.required),
      'Password': new FormControl(null, Validators.required)
    });

    this.modal();

  }

  registerUser(){
    this.loader = true;
    this.navbarService.registerUser(this.signUpForm.value).subscribe((res) => {
      console.log(res); 
      Swal.fire({
        title: "User Succeesfully Register!! you can now Login!!",
        confirmButtonColor: "#c24049"
      })
      this.signUpForm.reset();
      this.loader = false;
      this.router.navigate(['/home']);
  }, (err) => {
    Swal.fire({
      title: "User Already Exit!!",
      confirmButtonColor: "#c24049"
    })
    this.loader = false;
  });
  }
 
  loginUser(){
    this.loader = true;
    this.navbarService.loginUser(this.loginForm.value).subscribe((res) => {
      console.log(res);  
      Swal.fire({
      title: "Login Successful",
      confirmButtonColor: "#c24049"
    });
      window.location.reload();
      this.router.navigate(['/home']);
      localStorage.setItem("token", res['user_Token']);
      localStorage.setItem("user_Id", res['user']['id']);
      localStorage.setItem("userProfile_Id", res['user']['userProfileId']);
      localStorage.setItem("username", res['user']['username']);
      this.navbarService.userProfile(res['user']['userProfileId']).subscribe((res)=>{
        localStorage.setItem("cartIngredient", JSON.stringify(res['cartIngredient']));
      },(err)=>{
        Swal.fire({
          title: "User Profile Error",
          confirmButtonColor: "#c24049"
        })
      });
      this.loginForm.reset();
      this.router.navigate(['/home']);
      this.loader = false;
  }, (err) => {
    Swal.fire({
      title: "User Not Exit!!",
      confirmButtonColor: "#c24049"
    })
    this.loader = false;
  });
  }
 
  checklogin(){
    if(localStorage.getItem('token')){
      this.loggedIn=true;
      this.router.navigate(['/']);
    }
  }
  modal(){
    $(document).ready(function($){
      var $form_modal = $('.cd-user-modal'),
        $form_login = $form_modal.find('#cd-login'),
        $form_signup = $form_modal.find('#cd-signup'),
        $form_forgot_password = $form_modal.find('#cd-reset-password'),
        $form_modal_tab = $('.cd-switcher'),
        $tab_login = $form_modal_tab.children('li').eq(0).children('a'),
        $tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $forgot_password_link = $form_login.find('.cd-form-bottom-message a'),
        $back_to_login_link = $form_forgot_password.find('.cd-form-bottom-message a'),
        $s_sidebar__nav = $('.toggle');
    
      //open modal
      $s_sidebar__nav .on('click', function(event){
    
        if( $(event.target).is($s_sidebar__nav ) ) {
          // on mobile open the submenu
          $(this).children('ul').toggleClass('is-visible');
        } else {
          // on mobile close submenu
          $s_sidebar__nav .children('ul').removeClass('is-visible');
          //show modal layer
          $form_modal.addClass('is-visible');	
          //show the selected form
          ( $(event.target).is('.cd-signup') ) ? signup_selected() : login_selected();
        }
    
      });
    
      //close modal
      $('.cd-user-modal').on('click', function(event){
        if( $(event.target).is($form_modal) || $(event.target).is('.cd-close-form') ) {
          $form_modal.removeClass('is-visible');
        }	
      });
      //close modal when clicking the esc keyboard button
      $(document).keyup(function(event){
          if(event.which=='27'){
            $form_modal.removeClass('is-visible');
          }
        });

      //hide or show password
      $('.hide-password').on('click', function(){
        var $this= $(this),
          $password_field = $this.prev('input');
        
        ( 'password' == $password_field.attr('type') ) ? $password_field.attr('type', 'text') : $password_field.attr('type', 'password');
        ( 'Hide' == $this.text() ) ? $this.text('Show') : $this.text('Hide');
        //focus and move cursor to the end of input field
        $password_field.putCursorAtEnd();
      });
    
      //show forgot-password form 
      $forgot_password_link.on('click', function(event){
        event.preventDefault();
        forgot_password_selected();
      });
    
      //back to login from the forgot-password form
      $back_to_login_link.on('click', function(event){
        event.preventDefault();
        login_selected();
      });
    
      function login_selected(){
        $form_login.addClass('is-selected');
        $form_signup.removeClass('is-selected');
        $form_forgot_password.removeClass('is-selected');
        $tab_login.addClass('selected');
        $tab_signup.removeClass('selected');
      }
    
      function signup_selected(){
        $form_login.removeClass('is-selected');
        $form_signup.addClass('is-selected');
        $form_forgot_password.removeClass('is-selected');
        $tab_login.removeClass('selected');
        $tab_signup.addClass('selected');
      }
    
      function forgot_password_selected(){
        $form_login.removeClass('is-selected');
        $form_signup.removeClass('is-selected');
        $form_forgot_password.addClass('is-selected');
      }
    
      //REMOVE THIS - it's just to show error messages 
      $form_login.find('input[type="submit"]').on('click', function(event){
        event.preventDefault();
        $form_login.find('input[type="email"]').toggleClass('has-error').next('span').toggleClass('is-visible');
      });
      $form_signup.find('input[type="submit"]').on('click', function(event){
        event.preventDefault();
        $form_signup.find('input[type="email"]').toggleClass('has-error').next('span').toggleClass('is-visible');
      });
    
    
      //IE9 placeholder fallback
      //credits http://www.hagenburger.net/BLOG/HTML5-Input-Placeholder-Fix-With-$.html
      if(!Modernizr.input.placeholder){
        $('[placeholder]').focus(function() {
          var input = $(this);
          if (input.val() == input.attr('placeholder')) {
            input.val('');
            }
        }).blur(function() {
           var input = $(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
            input.val(input.attr('placeholder'));
            }
        }).blur();
        $('[placeholder]').parents('form').submit(function() {
            $(this).find('[placeholder]').each(function() {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
               input.val('');
            }
            })
        });
      }
    
    });
    
    
    //credits https://css-tricks.com/snippets/$/move-cursor-to-end-of-textarea-or-input/
    $.fn.putCursorAtEnd = function() {
      return this.each(function() {
          // If this function exists...
          if (this.setSelectionRange) {
              // ... then use it (Doesn't work in IE)
              // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
              var len = $(this).val().length * 2;
              this.setSelectionRange(len, len);
          } else {
            // ... otherwise replace the contents with itself
            // (Doesn't work in Google Chrome)
              $(this).val($(this).val());
          }
      });
    };
  }



    flogin(){
        //switch from a tab to another
        this.login=true;
        this.signup=false;
  }

   fsignup(){
    this.login=false;
    this.signup=true;
}

logout() {
  localStorage.clear();
  window.location.reload();
  this.router.navigate(['/home']);
}

  }
 