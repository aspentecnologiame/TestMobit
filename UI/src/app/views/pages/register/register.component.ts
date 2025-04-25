import { Component } from '@angular/core';
import { IconDirective } from '@coreui/icons-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../../../core/services/api.service';
import { User } from './models/user';
import { of, map } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { AlertComponent, ButtonCloseDirective, TemplateIdDirective, ThemeDirective, ContainerComponent, RowComponent, ColComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective } from '@coreui/angular';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
    imports: [AlertComponent, ButtonCloseDirective, ContainerComponent, RowComponent, TemplateIdDirective, ColComponent, ThemeDirective, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, IconDirective, FormControlDirective, ButtonDirective, FormsModule, ReactiveFormsModule],
    providers: [ApiService]
  })
export class RegisterComponent {

  public username: string = '';
  public password: string = '';
  public repassword: string = '';

  public alertColor: string = 'danger';
  public alertMessage: string = 'A senha não confere!';
  public visible = false;

  constructor(private apiService: ApiService, private router: Router) { }

  public signUp(): void {

    if (this.password !== this.repassword) {
      this.visible = true;
      return;
    }

    const user: User = {
      login: this.username,
      password: this.password
    };

    this.apiService.executePost<User>('user/signup', user)
    .pipe(
      //map(response => { body: response.body; }), 
      catchError(err => of(console.log(err)))
    )
    .subscribe(response => {

      console.log(response);

      if (response.type !== 0) {
        let data = response.body.data;
        this.alertMessage = `Usuário ${data.login} cadastrado com sucesso!`
      this.alertColor = 'success';
      this.visible = true;
      setTimeout(() => this.router.navigate(['login']), 2000);
      }
      
    });
  
  }

  handle(error: any): void {
    console.error('An error occurred:', error);
  }

  public onVisibleChange(eventValue: boolean) {
    this.visible = eventValue;
  }

}
