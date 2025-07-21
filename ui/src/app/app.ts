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
  newitem="";

  APIURL="http://localhost:8000/";

  constructor(private http:HttpClient){}

  
}
