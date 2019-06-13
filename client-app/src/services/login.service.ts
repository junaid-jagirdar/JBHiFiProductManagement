import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Http, RequestOptions, Request, RequestMethod, Headers, Response } from '@angular/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable()
export class LoginService {

    authToken = new BehaviorSubject<string>('');

    constructor(private http: Http, private router: Router) { }

    getAuthToken(loginModel: any) {
        this.http.post( environment.apiURL + '/api/token', loginModel).subscribe((result: Response) => {
            this.authToken.next(result.json().token);

            this.router.navigate(['product'])
        }, error => console.error(error));


        return this.authToken;
    }

}
