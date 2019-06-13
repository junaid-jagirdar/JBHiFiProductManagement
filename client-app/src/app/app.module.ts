import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { LoginService } from 'src/services/login.service';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductItemComponent } from './product/product-item/product-item.component';
import { ProductService } from 'src/services/product.service';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    LoginComponent,
    ProductListComponent,
    ProductItemComponent
],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpModule,
    CommonModule
  ],
  providers: [LoginService , ProductService],
  bootstrap: [AppComponent],
  exports: [ProductListComponent,ProductItemComponent,ProductComponent]
})
export class AppModule { }


