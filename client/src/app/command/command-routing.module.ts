import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { ListCommandComponent } from './list-command/list-command.component';
import { CreateCommandComponent } from './create-command/create-command.component';

const routes: Routes = [
  { path: '', component: ListCommandComponent },
  { path: 'create', component: CreateCommandComponent },
  { path: 'edit/:commandId', component: CreateCommandComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CommandRoutingModule { }
