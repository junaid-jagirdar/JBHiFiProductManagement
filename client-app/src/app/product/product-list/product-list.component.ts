import { Component, OnInit, Inject } from '@angular/core';
import { ProductService } from 'src/services/product.service';

@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

    public products: any = [];
    selectedRowNumber: number = -1;

    constructor(private productService: ProductService) {

    }

    ngOnInit() {
        this.productService.getProducts().subscribe((updatedProducts: any) => { this.products = updatedProducts });
        this.productService.selectedProductIndex.subscribe(selectedIndex => { this.selectedRowNumber = selectedIndex });
    }

    setSelectedProduct(index: number) {
        this.productService.selectedProductIndex.next(index);
        this.productService.setSelectedProduct(index);
    }

    deleteSelectedProduct(index: number) {
        this.productService.deleteProduct(index);
        //console.log(index);
    }
}
