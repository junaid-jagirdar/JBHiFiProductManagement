import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { LoginService } from './login.service';
import { Http, RequestOptions, Request, RequestMethod, Headers, Response } from '@angular/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class ProductService {

    products = new BehaviorSubject<any>([]);

    selectedProduct = new BehaviorSubject<any>([]);
    selectedProductIndex = new BehaviorSubject<number>(-1);

    operationMode = new BehaviorSubject<string>('view');

    errMsg = new BehaviorSubject<string>('');

    constructor(private http: Http,private loginService: LoginService
    ) { }

    getAuthOptions(): any {
        let headers = new Headers();
        headers.append('Authorization', `Bearer ${this.loginService.authToken.value}`);
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    //getting product  filter
    getProducts(): any {
        this.errMsg.next('');
        let options = this.getAuthOptions();
        this.http.get(environment.apiURL + '/api/products', options).subscribe((result: Response) => {
            this.products.next(result.json());
        }, error => console.error(error));

        return this.products;
    }

    //get a product by ID
    getOriginProduct(product: any): any {
        let options = this.getAuthOptions();
        this.http.get(environment.apiURL + 'api/products/' + product.id, options).subscribe(result => {
            let index = this.selectedProductIndex.value;
            let prods = this.products.value;

            let originProduct = result.json();
            prods.splice(index, 1, originProduct);

            console.log('origin', originProduct);
            console.log('prods', prods);
            this.products.next(prods);

        }, error => {
            console.error(error);
            this.errMsg.next(error.statusText);
        });
    }

    //get products with filter
    getProductsWithFilter(filter: any): any {
        this.errMsg.next('');
        let options = this.getAuthOptions();
        this.http.get(environment.apiURL + '/api/products' + "?brand=" + filter.brand
            + "&model=" + filter.model
            + "&description=" + filter.description, options).subscribe((result: Response) => {
            this.products.next(result.json());
        }, error => console.error(error));

        return this.products;
    }

    //add a new product
    addProduct(product: any) {
        this.errMsg.next('');
        let options = this.getAuthOptions();
        this.http.post(environment.apiURL + '/api/products', product, options).subscribe(result => {

            let addedProduct = result.json();
            let newLength = this.products.value.push(addedProduct);
            this.selectedProduct.next(this.products.value[newLength - 1]);
            this.selectedProductIndex.next(newLength - 1);
            this.operationMode.next('view');
        }, error => {
            console.error(error);
            this.errMsg.next(error.statusText);
        });
    }

    //update existing product
    updateProduct(product: any) {
        this.errMsg.next('');
        let options = this.getAuthOptions();
        this.http.put(environment.apiURL+ '/api/products/' + product.id, product, options).subscribe(result => {
            this.getProducts();
        }, error => {
            console.error(error);
            this.errMsg.next(error.statusText);
            this.getOriginProduct(product);
            this.operationMode.next('edit');
        });
    }

    //delete a product
    deleteProduct(index: number) {
        this.errMsg.next('');
        let options = this.getAuthOptions();
        let prods = this.products.value;

        let prodToDelete = prods.splice(index, 1)[0];
        this.products.next(prods);

        //console.log(prodToDelete);
        this.http.delete(environment.apiURL+ 'api/products/' + prodToDelete.id, options).subscribe(result => {

            if (result.status == 200) {
                this.selectedProduct.next({});
                this.selectedProductIndex.next(-1);
            }

        }, error => {
            console.error(error);
            this.errMsg.next(error.statusText);
        });
    }

    setOperationMode(mode: string) {
        this.operationMode.next(mode);
    }

    setSelectedProduct(index: number) {
        if (this.products.value.length > 0) {
            this.selectedProduct.next(this.products.value[index]);

        } else {
            this.selectedProduct.next({});
        }

        this.operationMode.next('view');

    }
}
