import { ProductService } from 'src/services/product.service';
import { Component, OnInit } from '@angular/core';

@Component
({
    selector: 'product',
    templateUrl: './product.component.html'
})
export class ProductComponent {
    filter: any = {brand:'', model:'', description: '' };

    constructor(private productService: ProductService) {

    }

    addProduct() {
        this.productService.setOperationMode('add');
    }

    onFilterChanged() {
        console.log(this.filter);
        this.productService.getProductsWithFilter(this.filter);
    }
}
