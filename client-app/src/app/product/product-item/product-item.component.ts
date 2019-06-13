import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/services/product.service';
import { NgForm } from '@angular/forms';


@Component({
    selector: 'product-item',
    templateUrl: './product-item.component.html'
})
export class ProductItemComponent implements OnInit {

    public product: any = {};
    operationMode = 'view';
    errMsg = '';
    constructor(private productService: ProductService) {
    }

    ngOnInit() {
        this.productService.selectedProduct.subscribe(selectedProduct => { this.product = selectedProduct; });
        this.productService.operationMode.subscribe(mode => { this.operationMode = mode; });
        this.productService.errMsg.subscribe(msg => this.errMsg = msg);
    }

    onSubmitAdd(form: NgForm) {
        this.productService.addProduct(form.value);
    }

    onSubmitEdit(form: NgForm) {
        this.productService.updateProduct(form.value);
        this.productService.setOperationMode('view');
    }

    setToEditMode() {
        this.productService.setOperationMode('edit');
    }
}
