import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule,HttpClientModule,FormsModule,ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('ui');

  items:any=[];
  
  checked_total=0;
  checkedIds = new Set<number>();

  newitem="";
  newprice="";

  APIURL="http://localhost:8000/";

  constructor(private http:HttpClient){}
  ngOnInit(){
    this.get_all();
  }

  get_all(){
    this.http.get(this.APIURL+"get_all").subscribe((res)=>{
      this.items=res;
    })
  }

  add_item(){
    let body=new FormData();
    body.append('item',this.newitem);
    body.append('price', Number(this.newprice).toString());
    this.http.post(this.APIURL+"add_item",body).subscribe((res)=>{
      alert(res)
      this.newitem="";
      this.newprice="";
      this.get_all();
    })
  }

  delete_item(id:any){
    let body=new FormData();
    body.append('id',id);
    this.http.post(this.APIURL+"delete_item",body).subscribe((res)=>{
      alert(res)
      this.get_all();
    }
    )
  }

  update_total() {
  this.checked_total = this.items
    .filter((item: any) => this.checkedIds.has(item.id))
    .reduce((sum: number, item: any) => sum + Number(item.price), 0);
}


  toggleCheckbox(item: any) {
    if (this.checkedIds.has(item.id)) {
      this.checkedIds.delete(item.id);
    } else {
      this.checkedIds.add(item.id);
    }

    this.update_total();
  }

}
